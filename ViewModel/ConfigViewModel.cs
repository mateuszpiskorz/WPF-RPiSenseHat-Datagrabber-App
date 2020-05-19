﻿using PiHatWPF.Model;
using System;
using System.Collections.Generic;
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
                   // OnPropertyChanged("IpAdress");
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
                    ipAddress = value;
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
        }

    }
}
