<h2 align="center"><bold>v0.001</bold></h2>
<h1 align="center">TwSplash (Tiger-Word-Splash)</h1>

TwSplash, an WPF splash window class, is an .NET Wpf splash windows class. It is designed for my WPF desktop publish app, but can be used for other application. 

It's very early stage, so it's not helpful yet. Only for someone to think about splash design pattern, can get some idea (and only idea).

A basic usage is following:

(한국어)
TwSplash는 개인적으로 만들고 있는 WPF DTP 프로그램에서 사용할 Splash 클래스입니다.
아직 디자인 중이라 별 도움이 안되겠지만, 디자인 패턴을 찾고 있는 분이라면 아이디어 정도는 도움이 될 것입니다. 
사용법은 아래와 같습니다. 

````csharp
        public App()
        {
            Startup += (sender, args) =>
            {
                // Type1 : Use WpfApp1.Splash As SplashWindow
                com.tigerword.twsplash.Splash
                .Create()
                .UseSplash<WpfApp1.Splash>()
                .UseWindow<WpfApp1.MainWindow>()
                .Wait(4000)
                .Run();
                // @TODO Type2 : Use Spash - Default Splash (with default splash image)  As SplashWindow
//                com.tigerword.twsplash.Splash
//.UseDefaultSplash()
//.UseWindow<WpfApp1.MainWindow>()
//.Wait(4000)
//.Run();
                // @TODO Type3 : Use Image (with default splash window) As SplashWindow
//                com.tigerword.twsplash.Splash
//.UseImage()
//.UseWindow<WpfApp1.MainWindow>()
//.Wait(4000)
//.Run();
            };
        }
````

### References
* WPFFluent (Good instance management for splash) - https://github.com/ComputingScienceCuriosity/WPFluent
