using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using WeAreDevs_API;
namespace Open_source_WPF
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        WeAreDevs_API.ExploitAPI api = new WeAreDevs_API.ExploitAPI();
        private TimeSpan duration { get; set; } = TimeSpan.FromSeconds(1);
        private QuarticEase ease { get; set; } = new QuarticEase { EasingMode = EasingMode.EaseInOut };

        public MainWindow()
        {
            InitializeComponent();
            topborder.Opacity = 0;
            AvalonEdit.Opacity = 0;
            scriptborder.Opacity = 0;
            Executebutton.Opacity = 0;
            clearbutton.Opacity = 0;
            attachbutton.Opacity = 0;

            WebClient client = new WebClient();
            //   XmlTextReader xshd_reader = new XmlTextReader(client.DownloadString("")); //your lua.xshd in web(Uploud pastebin)
            // XmlTextReader xshd_reader = new XmlTextReader(Environment.CurrentDirectory + "\\bin\\lua.xshd"); //lua.xshd in file
            // AvalonEdit.SyntaxHighlighting = HighlightingLoader.Load(xshd_reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance); //lua.xshd loader
        }


        public void FadeIn(DependencyObject element)
        {
            DoubleAnimation fadeAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = duration,
                EasingFunction = ease
            };

            Storyboard.SetTarget(fadeAnimation, element);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(OpacityProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeAnimation);
            storyboard.Begin();
        }

        private async void ifwindowsloaded(object sender, EventArgs e)
        {
            FadeIn(MainUI);
            await Task.Delay(1200);
            FadeIn(topborder);
            await Task.Delay(500);
            FadeIn(AvalonEdit);
            FadeIn(scriptborder);
            await Task.Delay(500);
            FadeIn(Executebutton);
            FadeIn(clearbutton);
            FadeIn(attachbutton);
        }

        //Scriptlist make you

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0); //close all
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            AvalonEdit.Text = ""; //Clear
        }

        private void execute(object sender, RoutedEventArgs e)
        {
            api.SendLuaScript(AvalonEdit.Text); //Execute
        }

        private async void attach(object sender, RoutedEventArgs e)
        {
           // api.LaunchExploit();//Attach
            topborderlabel.Content = "Your Executor Name (" + "Injecting...)";
            await Task.Delay(3000); //Timer
            if (api.isAPIAttached())
            {
                MessageBox.Show("Executor Injected!", "Your Executor Name");
                topborderlabel.Content = "Your Executor Name (" + "Injected!)";
                await Task.Delay(3000);
                topborderlabel.Content = "Your Executor Name " + "";
            }
            else
            {
                topborderlabel.Content = "Your Executor Name (" + "Failed!)";
                await Task.Delay(3000);
                topborderlabel.Content = "Your Executor Name " + "";
            }
        }

        private void topborder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove(); //drag
        }

        private void ix_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0); //bug fixed lol
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Minimized; //minimize
        }

        private void ix1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.WindowState = WindowState.Minimized; //bug fixed :/
        }
    }
}
