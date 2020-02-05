using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RunMyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowModel(true);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1 && e.ChangedButton == MouseButton.Left && Keyboard.Modifiers != ModifierKeys.Control)
            {
                var path = ((MainWindowModel)this.DataContext).AppToRun;
                if (!string.IsNullOrEmpty(path))
                {
                    Process.Start(path);
                }
                path = ((MainWindowModel)this.DataContext).VbsToRun;
                if (!string.IsNullOrEmpty(path))
                {
                    Process scriptProc = new Process();
                    scriptProc.StartInfo.FileName = string.IsNullOrEmpty(((MainWindowModel)this.DataContext).VbsInterpreter)?@"cscript": ((MainWindowModel)this.DataContext).VbsInterpreter;
                    scriptProc.StartInfo.Arguments = "/Nologo " + path;
                    scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    scriptProc.StartInfo.UseShellExecute = false;
                    scriptProc.StartInfo.CreateNoWindow = true;
                    scriptProc.Start();
                    scriptProc.WaitForExit();
                    scriptProc.Close();
                }
            } else if (e.ClickCount == 1 && e.ChangedButton == MouseButton.Left && Keyboard.Modifiers == ModifierKeys.Control)
            {
                DragMove();
            } else if (e.ClickCount == 2 && e.ChangedButton == MouseButton.Right)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
