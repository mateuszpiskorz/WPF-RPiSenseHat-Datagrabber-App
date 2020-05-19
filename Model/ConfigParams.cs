using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHatWPF.Model
{
    class ConfigParams
    {
        static readonly string defaultIpAdress = "192.168.0.14";
        static readonly string defaultIpPort = "8000";
        static readonly string defaultApiVersion = "1.0.0";
        public string ApiVersion;
        public string IpAddress;
        public string IpPort;
        static readonly int defaultSampleTime = 500;
        public int SampleTime;
        public readonly int defaultMaxSamples = 100;
        public int MaxSamples;

        public double XAxisMax
        {
            get
            {
                return defaultMaxSamples * SampleTime / 1000.0;

            }
            private set { }
        }

        public ConfigParams()
        {
            IpAddress = defaultIpAdress;
            IpPort = defaultIpPort;
            SampleTime = defaultSampleTime;
            MaxSamples = defaultMaxSamples;
            ApiVersion = defaultApiVersion;

        }

        public ConfigParams(string _ip, string _ipport, int _st)
        {
            IpAddress = _ip;
            IpPort = _ipport; 
            SampleTime = _st;
        }
    }
}
