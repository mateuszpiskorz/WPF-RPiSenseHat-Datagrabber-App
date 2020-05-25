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

        public IoTServer(string _ip)
        {
            ip = _ip;
        }

        private KeyValuePair<string, string> GetKeyValuePair(Dictionary<Tuple<int, int>, byte[]> data)
        {
            KeyValuePair<string, string> keyValuePairData = new KeyValuePair<string, string>();

            return keyValuePairData;
        }

        private string GetFileUrl()
        {
            return "http://" + ip + "/chartdata.json";
        }

        private string GetScriptUrl()
        {
            return "http://" + ip + "/serverscript.php";
        }

        public async Task<string> GETData()
        {
            string responseText = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(GetFileUrl());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);


            }

            return responseText;
        }

        public async Task<string> POSTData(Dictionary<Tuple<int,int>, byte[]> data)
        {
            string responsetext = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // post request data
                    var requestdatacollection = new List<KeyValuePair<string,string>>();
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Tuple<int, int> pos = new Tuple<int, int>(i,j);
                            requestdatacollection.Add(new KeyValuePair<string, string>("[" + i.ToString() + "," + j.ToString()+"]", data[pos].ToString()));
                        }
                    }
                    
                    var requestdata = new FormUrlEncodedContent(requestdatacollection);
                    // sent post request
                    var result = await client.PostAsync(GetScriptUrl(), requestdata);
                    // read response content
                    responsetext = await result.Content.ReadAsStringAsync();

                }
            }
            catch (Exception e)
            {
                Debug.writeline("network error");
                Debug.writeline(e);
            }

            return responsetext;
        }
    }
}
