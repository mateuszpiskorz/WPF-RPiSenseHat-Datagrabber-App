using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiHatWPF.ViewModel;
using PiHatWPF.Model;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using PiHatWPF.Commands;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PiHatWPF.ViewModel
{
    class DataListViewModel : BaseViewModel
    {
        private IoTServer _server;
        private ConfigParams _config;
        private ObservableCollection<SensorDataModel> _dataList;
        private Timer _requestTimer;

        #region Properties
        public ObservableCollection<SensorDataModel> DataList
        {
            get
            {
                return _dataList;
            }
            private set
            {
                _dataList = value;
                OnPropertyChanged("DataList");
            }
        }

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        #endregion

        public DataListViewModel()
        {
            _config = new ConfigParams();
            _server = new IoTServer(_config.IpAddress, _config.IpPort);
            _dataList = null;
            StartCommand = new ConfigButtonCommand(StartTransfer);
            StopCommand = new ConfigButtonCommand(StopTransfer);
        }

        public void StartTransfer()
        {
            if (_requestTimer == null)
            {
                _requestTimer = new Timer(_config.SampleTime);
                _requestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElaped);
                _requestTimer.Enabled = true;
            }
        }

        private async void RequestTimerElaped(object sender, ElapsedEventArgs e)
        {
            string responseText = await _server.GETData();
            // Debug.WriteLine(responseText);

            try
            {
                var responseJson = await GetResponseCollection(responseText);
                DataList = new ObservableCollection<SensorDataModel>(responseJson);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Json parsing error: ");
                Debug.WriteLine(exc);
            }

        }

        private async Task<List<SensorDataModel>> GetResponseCollection(string responseString)
        {
            List<SensorDataModel> data = null;

            try
            {
                data = await Task.Run(() => JsonConvert.DeserializeObject<List<SensorDataModel>>(responseString));

            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine("Err: Json Collection deserializing");
                Debug.WriteLine(e);
            }

            return data;
        }

        public void StopTransfer()
        {
            if (_requestTimer != null)
            {
                _requestTimer.Enabled = false;
                _requestTimer = null;
            }
        }
    }
}
