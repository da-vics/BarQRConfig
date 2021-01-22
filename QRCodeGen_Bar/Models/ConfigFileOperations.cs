using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace QRCodeGen_Bar.Models
{
    public class ConfigFileOperations
    {
        private string _configFileName = "configSettings.json";
        private ScannerDetails _scannerdetails { get; set; }

        public async Task<ScannerDetails> Cofigure()
        {
            var dir = string.Empty;

            try
            {
                dir = Directory.GetCurrentDirectory();
                Debug.WriteLine(dir);
            }

            catch (Exception args)
            {
                MessageBox.Show(args.Message, "Error");
            }

            if (!File.Exists(@$"{dir}/{_configFileName}"))
            {
                ///Default PlaceHolder
                _scannerdetails = new ScannerDetails
                {
                    Url_Upload = "http://192.168.43.179:80/fieldadmin/api/dataupload",
                    Url_Key = "http://192.168.43.179:80/fieldadmin/api/RegisterFieldDevice",
                    WifiSSID = "project",
                    WifiPassword = "projectdev",
                    AccessKey = "MasterKey:SMARTERDATA"
                };

                var _serialiseobject = JsonConvert.SerializeObject(_scannerdetails, Formatting.Indented);

                using (StreamWriter file = File.CreateText(@$"{dir}/{_configFileName}"))
                {
                    await file.WriteAsync(_serialiseobject);
                }
            }

            else
            {
                var jsonStr = await File.ReadAllTextAsync(@$"{dir}/{_configFileName}");
                _scannerdetails = JsonConvert.DeserializeObject<ScannerDetails>(jsonStr);
            }

            return _scannerdetails;
        }

        public async Task ModifySettings(ScannerDetails scannerDetails)
        {
            var dir = string.Empty;

            try
            {
                dir = Directory.GetCurrentDirectory();
                Debug.WriteLine(dir);
            }

            catch (Exception args)
            {
                Debug.WriteLine(args.Message);
            }
            var _serialiseobject = JsonConvert.SerializeObject(scannerDetails, Formatting.Indented);

            using (StreamWriter file = File.CreateText(@$"{dir}/{_configFileName}"))
            {
                await file.WriteAsync(_serialiseobject);
            }
        }

    }
}
