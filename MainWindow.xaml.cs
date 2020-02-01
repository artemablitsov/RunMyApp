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
