using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace com.tigerword.twsplash
{
    public class Splash
    {
        Type typeOfSplashWindow;
        Type tyoeOfMainWindow;
        int sMiliSecond = 5000;
        public static Splash instance = new Splash();
        public static Splash Create<wintype>()
        {
            if (instance == null)
                instance = new Splash();
            instance.typeOfSplashWindow = typeof(wintype);
            return instance;
        }

        public static Splash Create()
        {
            if (instance == null)
                instance = new Splash();
            return instance;
        }

        public Splash UseSplash<wintype>()
        {
            typeOfSplashWindow = typeof(wintype);
            return instance;
        }

        public Splash UseDefaultSplash()
        {
            //typeOfSplashWindow = typeof(wintype);
            return instance;
        }
        public Splash UseWindow<wintype>()
        {
            tyoeOfMainWindow = typeof(wintype);
            return this;
        }

        public Splash Wait(int mili_second)
        {
            sMiliSecond = mili_second;
            return this;
        }
        #region Variables

        private static Thread uiThread = null;

        //private static WpfSplashScreen applicationSplashScreen = null;

        #endregion
        public void Run(List<string> args = null)
        {

            uiThread = new Thread(SplashFunction);

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Name = "CoreComponents.WPF Demo Thread";

            uiThread.Start();

            // You can put your init logique here :             
            //lock (lock_string)
            //{
            //    Thread.Sleep(sMiliSecond);
            //}
            
                while (true)
                {
                lock (lock_string)
                {
                    if (splash_done)
                        break;
                }
                Thread.Sleep(100);
                }
            
            Thread.Sleep(sMiliSecond);
            System.Windows.Window mainWpfWindow = (System.Windows.Window)Activator.CreateInstance(tyoeOfMainWindow);
                //app.InitializeComponent();
            
            splashWpfWindow.Dispatcher.InvokeShutdown();
            splashWpfWindow = null;


            mainWpfWindow.Show();
            Dispatcher.Run();
        }
        static string lock_string = "app";
        static bool splash_done = false;
        System.Windows.Window splashWpfWindow = null;
        private static void SplashFunction()
        {

                instance.splashWpfWindow = (System.Windows.Window)Activator.CreateInstance(instance.typeOfSplashWindow);
                instance.splashWpfWindow.Show();
            lock (lock_string)
            {
                splash_done = true;
            }
            Dispatcher.Run();

        }
    }
}
