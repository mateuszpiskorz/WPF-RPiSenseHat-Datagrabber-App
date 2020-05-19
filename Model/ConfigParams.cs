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
        public string IpAddress;
        public string IpPort;
        static readonly int sampleTimeDefault = 500;
        public int SampleTime;
        public readonly int MaxSampleNumber = 100;

        public double XAxisMax
        {
            get
            {
                return MaxSampleNumber * SampleTime / 1000.0;

            }
            private set { }
        }

        public ConfigParams()
        {
            IpAddress = defaultIpAdress;
            IpPort = defaultIpPort;
            SampleTime = sampleTimeDefault;

        }

        public ConfigParams(string _ip, string _ipport, int _st)
        {
            IpAddress = _ip;
            IpPort = _ipport; 
            SampleTime = _st;
        }
    }
}
