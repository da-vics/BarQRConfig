using System.Windows;
using System.Windows.Input;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QRCodeGen_Bar.ViewModels.Commands;
using QRCodeGen_Bar.Models;

namespace QRCodeGen_Bar.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private Window _mainWindow { get; set; }

        private ScannerDetails _scannerdetails { get; set; } = new ScannerDetails();
        private ConfigFileOperations _configOperation { get; set; } = new ConfigFileOperations();

#nullable enable

        public string? Url_Upload { get => _scannerdetails.Url_Upload; set { _scannerdetails.Url_Upload = value; } }
        public string? Url_Key { get => _scannerdetails.Url_Key; set { _scannerdetails.Url_Key = value; } }
        public string? WifiSSID { get => _scannerdetails.WifiSSID; set { _scannerdetails.WifiSSID = value; } }
        public string? WifiPassword { get => _scannerdetails.WifiPassword; set { _scannerdetails.WifiPassword = value; } }
        public string? AccessKey { get => _scannerdetails.AccessKey; set { _scannerdetails.AccessKey = value; } }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveSettingsCommand { get; set; }

        public SettingsPageViewModel(Window window)
        {
            _mainWindow = window;
            CloseWindowCommand = new CommandProp(() => _mainWindow.Close());
            SaveSettingsCommand = new CommandProp(async () => await SaveSettings());

            Task.Run(async () =>
            {
                _scannerdetails = await _configOperation.Cofigure();

            });
        }

        private async Task SaveSettings()
        {

            Url_Upload = Url_Upload?.Trim();
            Url_Key = Url_Key?.Trim();
            WifiSSID = WifiSSID?.Trim();
            WifiPassword = WifiPassword?.Trim();
            AccessKey = AccessKey?.Trim();

            await _configOperation.ModifySettings(_scannerdetails);
            _mainWindow.Close();
        }
    }
}
