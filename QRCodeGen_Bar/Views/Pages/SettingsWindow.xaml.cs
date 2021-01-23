using QRCodeGen_Bar.ViewModels;
using System;
using System.Windows;

namespace QRCodeGen_Bar.Views.Pages
{

    public partial class SettingsWindow : Window
    {
        public SettingsWindow(Action action)
        {
            InitializeComponent();
            DataContext = new SettingsPageViewModel(this, action);
        }
    }
}
