using PiHatWPF.Commands;
using PiHatWPF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHatWPF.ViewModel
{
    class ConfigViewModel:BaseViewModel
    {
        #region Properties
        private string ipAddress;
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if(ipAddress != value)
                {
                    ipAddress = value;
                    OnPropertyChanged("IpAdress");
                }
            }
        }

        private string ipPort;
        public string IpPort
        {
            get
            {
                return ipPort;
            }
            set
            {
                if (ipPort != value)
                {
                    ipPort = value;
                    OnPropertyChanged("IpPort");
                }
            }
        }

        private int sampleTime;
        public string SampleTime
        {
            get
            {
                return sampleTime.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int st))
                {
                    if (sampleTime != st)
                    {
                        sampleTime = st;
                        OnPropertyChanged("SampleTime");
                    }
                }
            }
        }
        private int maxSamples;
        public string MaxSamples
        {
            get
            {
                return maxSamples.ToString();
            }
            set
            {
                if(Int32.TryParse(value, out int ms))
                {
                    if (maxSamples != ms)
                    {
                        maxSamples = ms;
                        OnPropertyChanged("MaxSamples");
                    }
                }
            }
        }
        private string apiVersion;
        public string ApiVersion
        {
            get
            {
                return apiVersion;
            }
            set
            {
                if (apiVersion != value)
                {
                    apiVersion = value;
                    OnPropertyChanged("ApiValue");
                }
            }
        }
        public ConfigButtonCommand SaveButton { get; set; }
        public ConfigButtonCommand DefaultButton { get; set; }
        #endregion
        #region Fields
        private ConfigParams config = new ConfigParams();
        
        #endregion

        public ConfigViewModel()
        {
            ipAddress = config.IpAddress;
            ipPort = config.IpPort;
            sampleTime = config.SampleTime;
            maxSamples = config.MaxSamples;
            apiVersion = config.ApiVersion;

            SaveButton = new ConfigButtonCommand(SaveSettings);
            DefaultButton = new ConfigButtonCommand(DefaultSettings);
        }

        public void SaveSettings()
        {
            Debug.WriteLine("Save Button Works!");
            config = new ConfigParams(ipAddress, ipPort, apiVersion, maxSamples, sampleTime);
            config.SaveConfigToFile();
        }
        public void DefaultSettings()
        {
            Console.WriteLine("Default Button Works!");
            config.SetDefaultConfig();
            config.SaveConfigToFile();

            IpAddress = config.IpAddress;
            IpPort = config.IpPort;
            SampleTime = (config.SampleTime).ToString();
            MaxSamples = (config.MaxSamples).ToString();
            ApiVersion = config.ApiVersion;


        }


    }
}
