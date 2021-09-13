using Autofac.Builder;
using Autofac.Features.Scanning;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Toolkit.Library.Extensions;

namespace Autofac
{
    /// <summary>
    /// Autofac扩展
    /// </summary>
    public static class AutofacExtensions
    {
        /// <summary>
        /// 用于辅助批量注册，筛选继承于指定类型的子类。
        /// </summary>
        /// <typeparam name="T">指定类型的基类，所有继承与它的子类都会被包含</typeparam>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAssignableAssemblyTypes<T>(this ContainerBuilder builder
            , IEnumerable<Assembly> assemblies)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            return builder.RegisterAssemblyTypes(assemblies.ToArray())
                .AssignableTo<T>();
        }

        /// <summary>
        /// 注册Serilog
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle> RegisterSerilogFromConfiguration(this ContainerBuilder builder)
        {
            return builder.Register<ILogger>((c, p) =>
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();

                return logger;
            });
        }

        /// <summary>
        /// 注册MediatR
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="notifications"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle> RegisterMediatR(this ContainerBuilder builder,
            params Type[] notifications)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            var mm = from mediatrOpenType in mediatrOpenTypes
                     from notification in notifications
                     select (mediatrOpenType, notification);
            foreach (var (mediatrOpenType, notification) in mm)
            {
                builder.RegisterAssemblyTypes(notification.GetTypeInfo().Assembly)
                           .AsClosedTypesOf(mediatrOpenType)
                           .AsImplementedInterfaces();
            }

            return builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}