using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESTUtil
{
    public class RestUtil
    {
        private string baseUri = null;  //   for example: "http://ist.rit.edu/api";
        public RestUtil(string _uri)
        {
            baseUri = _uri;
        }

        public string getRESTData(string url)
        {
            // const string baseUri = "http://ist.rit.edu/api";

            // connect to the API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUri + url);

            try
            {
                // Waits and gets the response from the REST reqeust
                WebResponse response = request.GetResponse();

                // Using the response stream from the web request
                using (Stream responseStream = response.GetResponseStream())
                {
                    // read the response from the api
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                // get the response
                Console.WriteLine("Error ");
                throw;
            }
        }
    }
}
