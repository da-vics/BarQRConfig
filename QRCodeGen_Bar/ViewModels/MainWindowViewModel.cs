﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using QRCodeGen_Bar.Models;
using QRCodeGen_Bar.ViewModels.Commands;
using QRCodeGen_Bar.Views.Pages;

namespace QRCodeGen_Bar.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Brush _settingsButtonColor { get; set; } = Brushes.SkyBlue;
        private Image _qrCodeGeneratedImage { get; set; }
        private ScannerDetails _scannerdetails { get; set; }

        public Brush SettingsButtonColor { get => _settingsButtonColor; set { _settingsButtonColor = value; } }
        public ICommand SettingCommand { get; set; }

        public MainWindowViewModel(Image displayQRImage)
        {
            _scannerdetails = new ScannerDetails();
            _qrCodeGeneratedImage = displayQRImage;
            var _serialiseobject = string.Empty;
            Task.Run(async () =>
            {
                var configops = new ConfigFileOperations();
                _scannerdetails = await configops.Cofigure();
                _serialiseobject = JsonConvert.SerializeObject(_scannerdetails, Formatting.Indented);

            });

            while (string.IsNullOrEmpty(_serialiseobject)) ;


            Debug.WriteLine(_serialiseobject);
            _serialiseobject = "config:" + _serialiseobject;
            QrCodeGenerator qrCodeGenerator = new QrCodeGenerator(_qrCodeGeneratedImage);
            qrCodeGenerator.GenerateQRCode(_serialiseobject);


            SettingCommand = new CommandProp(SettingsBtnCLick);

        }


        private void SettingsBtnCLick()
        {
            SettingsButtonColor = Brushes.DarkGray;

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();

            SettingsButtonColor = Brushes.SkyBlue;
        }

    }
}
