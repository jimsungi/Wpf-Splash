using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += (sender, args) =>
            {
                // Type1 : Use WpfApp1.Splash As SplashWindow
                //com.tigerword.twsplash.Splash
                //.Create()
                //.UseSplash<WpfApp1.Splash>()
                //.UseWindow<WpfApp1.MainWindow>()
                //.Wait(4000)
                //.Run();
                // @TODO Type2 : Use Spash - Default Splash (with default splash image)  As SplashWindow
                com.tigerword.twsplash.Splash
                .Create()
                .UseDefaultSplash()
                .UseWindow<WpfApp1.MainWindow>()
                .Wait(4000)
                .Run();
                // @TODO Type3 : Use Image (with default splash window) As SplashWindow
                //com.tigerword.twsplash.Splash
                //.Create()
                //.UseImage(new System.Windows.Media.Imaging.BitmapImage())
                //.UseWindow<WpfApp1.MainWindow>()
                //.Wait(4000)
                //.Run();
            };
        }
    }
}
