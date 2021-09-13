using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Toolkit.Widget.Extensions
{
    public static class SingleInstanceApp
    {
        private static SingleInstance singleInstance;

        static SingleInstanceApp()
        {
            Configure();
        }

        private static void Configure()
        {
            string appTypeName = Application.Current.GetType().FullName;
            string identifier = @$"SingleInstance_{appTypeName}_PassArgumentsAsync";
            singleInstance = new SingleInstance(identifier);

            singleInstance.ArgumentsReceived.Subscribe(args =>
            {
                SingleInstance.ActivateWindow(Process.GetCurrentProcess());
            });
        }

        /// <summary>
        ///  Checks if the instance of the application attempting to start is the first instance.
        ///  If not, activates the first instance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>True if this is the first instance of the application.</returns>
        public static bool InitializeAsFirstInstance(object sender, StartupEventArgs e)
        {
            bool isFirstInstance = singleInstance.IsFirstInstance;
            if (!isFirstInstance)
            {
                singleInstance.PassArgumentsToFirstInstance(e.Args);
                if (sender is Application app) app.Shutdown();
            }
            else
            {
                singleInstance.ListenForArgumentsFromSuccessiveInstances();
            }
            return isFirstInstance;
        }
    }
}