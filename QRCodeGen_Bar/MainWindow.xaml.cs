using QRCodeGen_Bar.ViewModels;
using System;
using System.Collections.Generic;
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

namespace QRCodeGen_Bar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(QRGeneratedImage);
        }

        #region CloseBtnEvent
        private void CloseWindowButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region MinimiseWindowBtnEvent
        private void MinimiseWindowButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region MaximiseWindowBtnEvent
        private void MaximiseWindowButton(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                MaximiseIcon.Icon = FontAwesome.Sharp.IconChar.WindowMaximize;
            }

            else
            {
                this.WindowState = WindowState.Maximized;
                MaximiseIcon.Icon = FontAwesome.Sharp.IconChar.WindowRestore;
            }

        }
        #endregion
    }
}
