using Akavache;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Toolkit.Client.Models;
using Toolkit.Client.Views;
using Toolkit.Library.Extensions;
using Toolkit.Widget;
using Toolkit.Widget.Extensions;

namespace Toolkit.Client
{
    public class Bootstrapper : BootstrapperBase
    {
        private IContainer container;

        public Bootstrapper()
        {
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            Initialize();

            Akavache.Registrations.Start("AkavacheExperiment");
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<ShellViewModel>();

            builder.RegisterType<TransactionInterceptor>();

            builder.RegisterAssignableAssemblyTypes<IDisplayModule>(SelectAssemblies())
                .As<IDisplayModule>()
                .InterceptedBy(typeof(TransactionInterceptor))//注册拦截器
                .EnableClassInterceptors();

            //注册MediatR
            builder.RegisterMediatR(typeof(ClipboardNotification)/*, typeof(Test1Notification)*/);

            //从appsettings.json读取serilog配置
            builder.RegisterSerilogFromConfiguration().SingleInstance();

            var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.secret.json")
                   .Build();

            //注入自定义配置
            builder.Register((c, p) => GetToolkitConfig()).SingleInstance();

            container = builder.Build();
        }

        protected override void PrepareApplication()
        {
            base.PrepareApplication();
            AddViewLocatorRuleForProxiedViewModels();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            if (SingleInstanceApp.InitializeAsFirstInstance(sender, e))
            {
                DisplayRootViewFor<ShellViewModel>();
            }
        }

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <param name="service"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Ignore.</exception>
        protected override object GetInstance(Type service, string key)
        {
            object instance = default;
            if (string.IsNullOrWhiteSpace(key))
            {
                if (container?.TryResolve(service, out instance) ?? false)
                    return instance;
            }
            else
            {
                if (container?.TryResolveKeyed(key, service, out instance) ?? false)
                    return instance;
            }
            throw new NullReferenceException($"Could not locate any instances of contract {key ?? service.Name}.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container?.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            container?.InjectProperties(instance);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            Log.CloseAndFlush();
            BlobCache.Shutdown().Wait();
            base.OnExit(sender, e);
        }

        private void AddViewLocatorRuleForProxiedViewModels()
        {
            var originalViewTypeLocator = ViewLocator.LocateTypeForModelType;

            ViewLocator.LocateTypeForModelType = (modelType, displayLocation, context) =>
            {
                var viewModelType = modelType;

                var viewModelTypeName = viewModelType.FullName;
                if (viewModelTypeName.StartsWith("Castle.Proxies") && viewModelTypeName.EndsWith("Proxy"))
                    viewModelType = viewModelType.BaseType;

                return originalViewTypeLocator(viewModelType, displayLocation, context);
            };
        }

        private ToolkitConfig GetToolkitConfig()
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.secret.json", optional: false, reloadOnChange: true)
                    .Build();
            var config = new ToolkitConfig();
            configuration.GetSection("toolkitConfig").Bind(config);
            return config;
        }

        #region 异常处理部分

        /// <summary>
        /// 非UI线程异常全局捕获
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Log.Fatal(ex, "全局捕获");
            }
            else
            {
                Log.Fatal(e.ExceptionObject.ToString());
            }
        }

        /// <summary>
        /// UI线程异常全局捕获
        /// </summary>
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Fatal(e.Exception, "UI线程异常全局捕获");
            base.OnUnhandledException(sender, e);
        }

        /// <summary>
        /// Task线程内未捕获异常处理事件
        /// </summary>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            //task线程内未处理捕获
            Log.Fatal(args?.Exception?.Message, "线程内未处理异常");
            //MessageBox.Show("捕获线程内未处理异常：" + args.Exception?.Message);
            args?.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
        }

        #endregion 异常处理部分
    }
}