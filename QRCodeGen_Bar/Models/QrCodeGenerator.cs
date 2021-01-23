using QRCoder;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QRCodeGen_Bar.Models
{
    public class QrCodeGenerator
    {
        private QRCodeGenerator qrGenerator = new QRCodeGenerator();
        private string _qrFloderName = "QRCodeImages";
        private string QrName = "QrConfig";
        private System.Windows.Controls.Image _displayQrImage { get; set; }

        public QrCodeGenerator(System.Windows.Controls.Image displayQrImage)
        {
            _displayQrImage = displayQrImage;
        }

        public void GenerateQRCode(string configdata)
        {

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(configdata, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            var datetemp = DateTime.Now;
            QrName = $"{QrName}_{datetemp.Date.ToShortDateString()}_{ datetemp.Hour}_{datetemp.Minute}_{datetemp.Second}.png";
            QrName = QrName.Replace('/', '_');
            MessageBox.Show(QrName);


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

            if (Directory.Exists(@$"{dir}\{_qrFloderName}"))
            {

                try
                {
                    qrCodeImage.Save(@$"{dir}\{_qrFloderName}\{QrName}");
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(@$"{dir}\{_qrFloderName}\{QrName}");
                    img.EndInit();
                    _displayQrImage.Source = img;

                    Debug.WriteLine("Image Generated!", "Successful");

                }

                catch (InvalidOperationException args)
                {
                    MessageBox.Show(args.Message);
                }

                catch (System.Runtime.InteropServices.ExternalException args)
                {
                    MessageBox.Show("error saving QrCode");
                    Debug.WriteLine(args.Message);
                }

            }///

            else
            {
                try
                {
                    Directory.CreateDirectory(@$"{dir}\{_qrFloderName}");
                    qrCodeImage.Save(@$"{dir}\{_qrFloderName}\{QrName}");

                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(@$"{dir}\{_qrFloderName}\{QrName}");
                    img.EndInit();
                    _displayQrImage.Source = img;
                }

                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Permissions required to access directory");
                }

                catch (ArgumentNullException args)
                {
                    MessageBox.Show(args.Message);
                }

                catch (UriFormatException args)
                {
                    MessageBox.Show(args.Message);
                }

                catch (InvalidOperationException args)
                {
                    MessageBox.Show(args.Message);
                }

                MessageBox.Show("Image Generated!", "Successful");

            }
        }
    }
}
