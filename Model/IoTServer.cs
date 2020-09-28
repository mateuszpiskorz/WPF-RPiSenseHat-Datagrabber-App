using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace PiHatWPF.Model
{
    class IoTServer
    {
        private string ip;
        private string port;

        public IoTServer(string _ip, string _port)
        {
            ip = _ip;
            port = _port;
        }

        private KeyValuePair<string, string> GetKeyValuePair(Dictionary<Tuple<int, int>, byte[]> data)
        {
            KeyValuePair<string, string> keyValuePairData = new KeyValuePair<string, string>();

            return keyValuePairData;
        }

        private string GetDataUri()
        {
            return "http://" + ip +":"+ port + "/?request=GETSens";
        }

        private string GetScriptUrl()
        {
            return "http://" + ip + ":"+ port;
        }

        private string GetJoystickUri()
        {
            return "http://" + ip + ":" + port + "/?request=GETJoy";
        }

        public async Task<string> GETData()
        {
            string responseText = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(GetDataUri());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);


            }

            return responseText;
        }

        public async Task<string> GETJoystick()
        {
            string responseText = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(GetJoystickUri());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);


            }

            return responseText;
        }

        public async Task<string> POSTData(int[] data)
        {
            string responsetext = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // post request data
                    var jsonLed = JsonConvert.SerializeObject(data);
                    var requestdatacollection = new Dictionary<string, string>{ {"ledData", jsonLed } };

                    //Debug.WriteLine(requestdatacollection.ToString());
                    var requestdata = new FormUrlEncodedContent(requestdatacollection);
                    //sent post request
                    var result = await client.PostAsync(GetScriptUrl(), requestdata);
                    //read response content
                    responsetext = await result.Content.ReadAsStringAsync();
                    if (String.IsNullOrEmpty(responsetext)) Debug.WriteLine("Empty");

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Network Error");
                Debug.WriteLine(e);
            }

            return responsetext;
        }
    }
}
