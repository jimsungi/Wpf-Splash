using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using Prism.Unity;
using Prism.Modularity;

namespace com.tigerword.splash
{
    public class Splash
    {

        #region Static Variables
        private static Thread? uiThread = null;
        public static Splash instance = new Splash();
        static string splash_run_locker = "splash_lock";
        static bool splash_done = false;
        static SplashOption splash_option = SplashOption.USE_DEFAULT_SPLASH;
        enum SplashOption
        {
            USE_DEFAULT_SPLASH,
            USE_SPLASH_IMAGE,
            USE_USER_SPLASH
        }
        #endregion
        #region Variables
        Type typeOfSplashWindow=typeof(com.tigerword.splash.SplashWindow);
        Type? tyoeOfMainWindow =null;
        Type? typeOfBootstrapper = null;
        BitmapImage? splashImage = null;
        int sMiliSecond = 5000;
        System.Windows.Window? splashWpfWindow = null;
        #endregion
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
            splash_option = SplashOption.USE_USER_SPLASH;
            typeOfSplashWindow = typeof(wintype);
            return instance;
        }


        public Splash UseImage(BitmapImage image)
        {
            splash_option = SplashOption.USE_SPLASH_IMAGE;
            splashImage = image;
            return instance;
        }

        public Splash UseWindow<wintype>()
        {
            tyoeOfMainWindow = typeof(wintype);
            return this;
        }

        public Splash UseBootStraper<bootstraptype>()
        {
            typeOfBootstrapper = typeof(bootstraptype);
            return this;
        }
        public Splash UseDefaultSplash()
        {
            splash_option = SplashOption.USE_DEFAULT_SPLASH;
            typeOfSplashWindow = typeof(com.tigerword.splash.SplashWindow);
            return instance;
        }

        public Splash Wait(int mili_second)
        {
            sMiliSecond = mili_second;
            return this;
        }

        public void Run(List<string>? args = null)
        {
            uiThread = new Thread(SplashFunction);

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Name = "Splash Runner";

            uiThread.Start();

           
            while (true)
            {
                lock (splash_run_locker)
                {
                    if (splash_done)
                        break;
                }
                Thread.Sleep(100);
            }
            
            Thread.Sleep(sMiliSecond);
            if (tyoeOfMainWindow == null)
                return;
            if (splashWpfWindow == null)
                return;
            System.Windows.Window? mainWpfWindow = Activator.CreateInstance(tyoeOfMainWindow) as System.Windows.Window;
            //Bootstrapper? unityBootstrapper = Activator.CreateInstance(typeOfBootstrapper) as UnityBootstrapper;
            if (mainWpfWindow == null)
                return;
            splashWpfWindow.Dispatcher.InvokeShutdown();
            splashWpfWindow = null;
            if (mainWpfWindow != null)
            {
                mainWpfWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                mainWpfWindow.Topmost = true;
                mainWpfWindow.Show();
            }
            //else if ()

                Dispatcher.Run();
        }

        public void RunSplashOnly(List<string>? args = null)
        {
            uiThread = new Thread(SplashFunction);

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Name = "Splash Runner";

            uiThread.Start();


            while (true)
            {
                lock (splash_run_locker)
                {
                    if (splash_done)
                        break;
                }
                Thread.Sleep(100);
            }

            Thread.Sleep(sMiliSecond);
            //if (tyoeOfMainWindow == null)
            //    return;
            if (splashWpfWindow == null)
                return;
            //System.Windows.Window? mainWpfWindow = Activator.CreateInstance(tyoeOfMainWindow) as System.Windows.Window;
            //Bootstrapper? unityBootstrapper = Activator.CreateInstance(typeOfBootstrapper) as UnityBootstrapper;
            //if (mainWpfWindow == null)
            //    return;
            splashWpfWindow.Dispatcher.InvokeShutdown();
            splashWpfWindow = null;
            //if (mainWpfWindow != null)
            //{
            //    mainWpfWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //    mainWpfWindow.Topmost = true;
            //    mainWpfWindow.Show();
            //}
            //else if ()

            //Dispatcher.Run();
        }
        private static void SplashFunction()
        {
            switch(splash_option)
            {
 
                case SplashOption.USE_USER_SPLASH:
                    {
                        if (instance.typeOfSplashWindow == null)
                            return;
                        instance.splashWpfWindow = Activator.CreateInstance(instance.typeOfSplashWindow) as System.Windows.Window;
                        if (instance.splashWpfWindow == null)
                            return;
                        instance.splashWpfWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        instance.splashWpfWindow.Topmost = true;
                        instance.splashWpfWindow.Show();                        
                    }
                    break;
                case SplashOption.USE_SPLASH_IMAGE:
                    {
                        if (instance.splashImage == null)
                            throw new Exception("Splash Image not found");
                        instance.splashWpfWindow = Activator.CreateInstance(typeof(com.tigerword.splash.SplashWindow)) as com.tigerword.splash.SplashWindow;
                        if (instance.splashWpfWindow == null)
                            return;
                        com.tigerword.splash.SplashWindow? sWindow = instance.splashWpfWindow as com.tigerword.splash.SplashWindow;
                        if(sWindow !=null)
                        {
                            sWindow.SetImage(instance.splashImage);
                            sWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            sWindow.Topmost = true;
                            sWindow.Show();                            
                        }
                        else
                        {
                            throw new SplashException(SplashOption.USE_SPLASH_IMAGE.ToString() + ": Cannot Create");
                        }
                    }
                    break;
                case SplashOption.USE_DEFAULT_SPLASH:
                default:
                    {
                        instance.splashWpfWindow = Activator.CreateInstance(typeof(SplashWindow)) as SplashWindow;
                        if (instance.splashWpfWindow == null)
                            return;
                        com.tigerword.splash.SplashWindow? sWindow = instance.splashWpfWindow as com.tigerword.splash.SplashWindow;
                        if (sWindow != null)
                        {
                            sWindow.InitializeComponent();
                            sWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            sWindow.Topmost = true;
                            sWindow.Show();                            
                        }
                        else
                        {
                            throw new SplashException(SplashOption.USE_DEFAULT_SPLASH.ToString() + ": Cannot Create");
                        }
                    }
                    break;                    
            }
            

            lock (splash_run_locker)
            {
                splash_done = true;
            }
            Dispatcher.Run();
        }
    }
}
