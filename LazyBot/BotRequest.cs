using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LazyBot
{
    public class BotRequest
    {
        public string Put(RequestKeys keys, string document)
        {
            WebRequest request = WebRequest.Create(GetUrl(keys.Bot));

            byte[] byteArray = Encoding.UTF8.GetBytes(document);

            //Set the correct request properties
            request.Method = "PUT";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("Authorization", GetHeader(keys.Key));

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the document to the request stream, then close it
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = null;

            try
            {
                // Get the response.
                response = request.GetResponse();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally 
            {
                dataStream.Close();
                if (response != null)
                {
                    response.Close();
                }
            }

            return "Success";
        }

        private string GetUrl(string bot)
        {
            return string.Format("http://gosuarena.erikojebo.se/api/bot/{0}", bot);       
        }

        private string GetHeader(string key)
        {
            return string.Format("GosuArenaApiKey {0}", key);
        }
    }
}
