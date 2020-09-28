using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
using PiHatWPF.Commands;
using PiHatWPF.Model;
using System.Diagnostics;

namespace PiHatWPF.ViewModel
{
    class LedViewModel:BaseViewModel
    {
        #region Properties

        private byte rBrush;
        private byte gBrush;
        private byte bBrush;
        private SolidColorBrush currentColor; 
        public SolidColorBrush CurrentColor
        {
            get
            {
                return currentColor;
            }
            set
            {
                currentColor = value;
                OnPropertyChanged("CurrentColor");
            }
        }
        public string RBrush 
        {
            get
            {
                return rBrush.ToString();
            }

            set
            {
                if (Byte.TryParse(value,out byte bytevalue))
                {
                    if (rBrush != bytevalue)
                    {
                        if (bytevalue > 255)
                            rBrush = 255;
                        else if (bytevalue < 0)
                            rBrush = 0;
                        else
                        rBrush = bytevalue;

                        CurrentColor = new SolidColorBrush(Color.FromArgb(153, rBrush, gBrush, bBrush));
                        OnPropertyChanged("RBrush");
                    }
                }
            }
        }

        public string GBrush
        {
            get
            {
                return gBrush.ToString();
            }

            set
            {
                if (Byte.TryParse(value, out byte bytevalue))
                {
                    if (gBrush != bytevalue)
                    {
                        if (bytevalue > 255)
                            gBrush = 255;
                        else if (bytevalue < 0)
                            gBrush = 0;
                        else
                        gBrush = bytevalue;

                        CurrentColor = new SolidColorBrush(Color.FromArgb(153,rBrush, gBrush, bBrush));
                        OnPropertyChanged("GBrush");
                    }
                }
            }
        }

        public string BBrush
        {
            get
            {
                return bBrush.ToString();
            }

            set
            {
                if (Byte.TryParse(value, out byte bytevalue))
                {
                    if (bBrush != bytevalue)
                    {
                        if (bytevalue > 255)
                            bBrush = 255;
                        else if (bytevalue < 0)
                            bBrush = 0;
                        else
                        bBrush = bytevalue;

                        CurrentColor = new SolidColorBrush(Color.FromArgb(153,rBrush, gBrush, bBrush));
                        OnPropertyChanged("BBrush");
                    }
                }
            }
        }

        public LedButtonCommand ClearButton { get; set; }
        public LedButtonCommand SendButton { get; set; }
        #endregion
        #region Fields
        private Dictionary<Tuple<int, int>, Rectangle> ledMatrix = new Dictionary<Tuple<int, int>, Rectangle>();
        private int[] ledMatrixData = new int[64];
        private List<Rectangle> selectedLeds = new List<Rectangle>();
        private IoTServer Server;
        private ConfigParams Config;
        private readonly string ip = "192.168.0.20";
        private readonly int initialColor = 0;
        private readonly int[] InitialColors = { 0, 0, 0, 0, 0, 0, 0, 0,
                                                 0, 0, 0, 0, 0, 0, 0, 0,
                                                 0, 0, 0, 0, 0, 0, 0, 0,
                                                 0, 0, 0, 0, 0, 0, 0, 0,
                                                 0, 0, 0, 0, 0, 0, 0, 0,
                                                 0, 0, 0, 0, 0, 0, 0, 0,
                                                 0, 0, 0, 0, 0, 0, 0, 0,
                                                 0, 0, 0, 0, 0, 0, 0, 0};

        public Grid ViewLedMatrix { get; set; }

       
        

        private LedCommand LedClicked;
        #endregion
        public LedViewModel()
        {
            this.LedMatrixInit();
            rBrush = this.IntToRgb(initialColor)[0];
            gBrush = this.IntToRgb(initialColor)[1];
            bBrush = this.IntToRgb(initialColor)[2];
            ClearButton = new LedButtonCommand(ClearLed);
            SendButton = new LedButtonCommand(SendLed);
            currentColor = new SolidColorBrush(Color.FromArgb(153, rBrush, gBrush, bBrush));
            Config = new ConfigParams();
            Server = new IoTServer(Config.IpAddress, Config.IpPort);
            

            
        }

        private void LedMatrixInit()
        {
            BrushConverter bc = new BrushConverter();
            int[] ledColors = InitialColors; 
            Grid grid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            for (int i = 0; i < 8; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int j = 0; j < 8; j++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    Rectangle rect = new Rectangle()
                    {
                        Fill = new SolidColorBrush(Color.FromArgb(153,(byte)((ledColors[i * 8 + j] >> 16) & 0xFF), (byte)((ledColors[i * 8 + j] >> 8) & 0xFF), (byte)((ledColors[i * 8 + j] >> 0) & 0xFF))),
                        Width = 30,
                        Height = 30,
                        Margin = new Thickness(5, 5, 5, 5),
                      
                    };

                    LedClicked = new LedCommand(this, i, j);
                    MouseAction action = MouseAction.LeftClick;
                    MouseGesture gesture = new MouseGesture(action);
                    MouseBinding binding = new MouseBinding(LedClicked, gesture);
                    rect.InputBindings.Add(binding);

                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    grid.Children.Add(rect);
                    ledMatrix.Add(Tuple.Create(i, j), rect);
                    ledMatrixData[i*8 + j] = initialColor;
                }
            }

            ViewLedMatrix = grid;
        }

        private byte[] IntToRgb(int color)
        {
            byte r, g, b;
            byte[] result = new byte[3];
            r = (byte)((color >> 16) & 0xFF);
            g = (byte)((color >> 8) & 0xFF);
            b = (byte)((color >> 0) & 0xFF);

            result[0] = r;
            result[1] = g;
            result[2] = b;
            return result;
            
        }

        private int RgbToInt(byte r, byte g, byte b)
        {
            int result;
            result = (((r & 0x00ff) << 16) ) | (((g & 0x00ff) << 8)) | (((b & 0x00ff)));
            return result;
        }

        public void LedClickedFunction(int x, int y)
        {
            Tuple<int, int> pos = new Tuple<int, int>(x, y);
            Console.WriteLine("LED" + x.ToString() + y.ToString() + "Clicked." + pos.ToString());
            ledMatrix[pos].Fill= currentColor;
            ledMatrixData[x * 8 + y] = RgbToInt(currentColor.Color.R, currentColor.Color.G, currentColor.Color.B);
            
            
        }

        public async void ClearLed()
        {
            string serverResponse = null;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Tuple<int, int> pos = new Tuple<int, int>(i, j);
                    byte[] defaultColor = IntToRgb(initialColor);
                    ledMatrix[pos].Fill = new SolidColorBrush(Color.FromArgb(153,defaultColor[0],defaultColor[1],defaultColor[2]));
                    ledMatrixData[i*8 + j] = RgbToInt(defaultColor[0], defaultColor[1], defaultColor[2]);

                     
                }
            }
            serverResponse = await Server.POSTData(ledMatrixData);
        }

        public async void SendLed()
        {
            string serverResponse = null;
            serverResponse = await Server.POSTData(ledMatrixData);
            Debug.WriteLine(serverResponse);
            
            
           
        }
    }
}
