using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using PiHatWPF.ViewModel;
using PiHatWPF.Model;
using System.Windows.Input;
using PiHatWPF.Commands;
using PiHatWPF.Enums;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PiHatWPF.ViewModel
{
    class JoystickViewModel : BaseViewModel
    {
        private bool _isListening;
        private Brush _upColor;
        private Brush _leftColor;
        private Brush _middleColor;
        private Brush _rightColor;
        private Brush _downColor;
        private CancellationTokenSource _cts;
        private CancellationToken _ct;
        private IoTServer _server;
        private ConfigParams _config;
        #region Properties
        public bool IsListening
        {
            get
            {
                return _isListening;
            }
            set
            {
                _isListening = value;
                OnPropertyChanged("IsListening");
            }
        }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public Brush UpColor
        {
            get
            {
                return _upColor;
            }
            set
            {
                _upColor = value;
                OnPropertyChanged("UpColor");
            }
        }
        public Brush LeftColor
        {
            get
            {
                return _leftColor;
            }
            set
            {
                _leftColor = value;
                OnPropertyChanged("LeftColor");
            }
        }
        public Brush MiddleColor
        {
            get
            {
                return _middleColor;
            }
            set
            {
                _middleColor = value;
                OnPropertyChanged("MiddleColor");
            }
        }
        public Brush RightColor
        {
            get
            {
                return _rightColor;
            }
            set
            {
                _rightColor = value;
                OnPropertyChanged("RightColor");
            }
        }

        public Brush DownColor
        {
            get
            {
                return _downColor;
            }
            set
            {
                _downColor = value;
                OnPropertyChanged("DownColor");
            }
        }
        #endregion

        public JoystickViewModel()
        {
            _isListening = false;

            _config = new ConfigParams();
            _server = new IoTServer(_config.IpAddress, _config.IpPort);
            StartCommand = new ConfigButtonCommand(StartListening);
            StopCommand = new ConfigButtonCommand(StopListening);

            UpColor = Brushes.LightGray;
            LeftColor = Brushes.LightGray;
            MiddleColor = Brushes.LightGray;
            RightColor = Brushes.LightGray;
            DownColor = Brushes.LightGray;

        }

        public async void StartListening()
        {

            if (!IsListening)
            {
                _cts = new CancellationTokenSource();
                _ct = _cts.Token;
                IsListening = true;


                await JoystickLoop();
            }


        }

        private void StopListening()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                IsListening = false;
            }
            

        }

        private async Task JoystickLoop()
        {
            while (!_ct.IsCancellationRequested)
            {

                var response = await _server.GETJoystick();
                JoystickModel responseJson = null;
                try
                {
                    responseJson = await Task.Run(() => JsonConvert.DeserializeObject<JoystickModel>(response));
                }
                catch (JsonSerializationException e)
                {
                    Debug.WriteLine(e);
                }


                if (responseJson == null)
                    continue;

                IndicateChange(responseJson);
                await Task.Delay(40);

            }
        }


        //Method updating BoxView colors based on joystick model
        private void IndicateChange(JoystickModel joystick)
        {
            switch (joystick.Direction)
            {
                case SenseTickDirections.Up:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            UpColor = Brushes.Gray;
                            break;
                        case SenseTickActions.Held:
                            UpColor = Brushes.DimGray;
                            break;
                        case SenseTickActions.Released:
                            UpColor = Brushes.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Left:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            LeftColor = Brushes.Gray;
                            break;
                        case SenseTickActions.Held:
                            LeftColor = Brushes.DimGray;
                            break;
                        case SenseTickActions.Released:
                            LeftColor = Brushes.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Middle:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            MiddleColor = Brushes.Gray;
                            break;
                        case SenseTickActions.Held:
                            MiddleColor = Brushes.DimGray;
                            break;
                        case SenseTickActions.Released:
                            MiddleColor = Brushes.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Right:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            RightColor = Brushes.Gray;
                            break;
                        case SenseTickActions.Held:
                            RightColor = Brushes.DimGray;
                            break;
                        case SenseTickActions.Released:
                            RightColor = Brushes.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Down:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            DownColor = Brushes.Gray;
                            break;
                        case SenseTickActions.Held:
                            DownColor = Brushes.DimGray;
                            break;
                        case SenseTickActions.Released:
                            DownColor = Brushes.LightGray;
                            break;
                    }
                    break;
            }
        }
    }
}
