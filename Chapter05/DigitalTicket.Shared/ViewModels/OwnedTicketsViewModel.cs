using DigitalTicket.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using ZXing.Mobile;
using ZXing.QrCode;

namespace DigitalTicket.ViewModels
{
    public class OwnedTicketsViewModel : ObservableObject
    {
        public List<OwnedTicket> Tickets = new List<OwnedTicket>();

        private OwnedTicket currentTicket;
        public OwnedTicket CurrentTicket
        {
            get
            {
                return currentTicket;
            }
            set
            {
                SetProperty(ref currentTicket, value);
            }
        }

        private ImageSource currentQRCode;
        public ImageSource CurrentQRCode
        {
            get
            {
                return currentQRCode;
            }
            set
            {
                SetProperty(ref currentQRCode, value);
            }
        }

        private Visibility qrCodeVisibility = Visibility.Collapsed;
        public Visibility QRCodeVisibility
        {
            get
            {
                return qrCodeVisibility;
            }
            set
            {
                SetProperty(ref qrCodeVisibility, value);
            }
        }

        public ICommand HideQRCodeCommand;

        public OwnedTicketsViewModel()
        {
            Tickets = OwnedTicketsRepository.LoadTicketsAsync().Result;
            Tickets.ForEach(ticket =>
            {
                ticket.ShowQRCodeCommand = new RelayCommand(() =>
                {
                    var options = new QrCodeEncodingOptions
                    {
                        DisableECI = true,
                        CharacterSet = "UTF-8",
                        Width = 250,
                        Height = 250,
                    };
                    var write = new BarcodeWriter
                    {
                        Format = ZXing.BarcodeFormat.QR_CODE
                    };
                    CurrentQRCode = write.Write(ticket.TicketID);
                    QRCodeVisibility = Visibility.Visible;
                    CurrentTicket = ticket;
                });

            });

            HideQRCodeCommand = new RelayCommand(() =>
            {
                QRCodeVisibility = Visibility.Collapsed;
            });
        }
    }
}
