// /*
//  * -----------------------------------------------------------------
//  * © 2006-2015  MineCloud, Inc (http://www.minecloud.com)
//  * -----------------------------------------------------------------
//  */ 

using System;
using System.IO;
using System.Net;
using System.Text;

namespace Backpack.Web.Rest
{
    public class RestClient
    {
        /// <summary>
        /// Executes a post to the specified URL with the parameters specified.
        /// </summary>
        /// <param name="reqItem">The REST request details.</param>
        /// <returns>The response for the request.</returns>
        public static HttpWebResponse DoRequest(RestRequest reqItem)
        {
            if (reqItem == null)
            {
                return null;
            }

            HttpWebRequest request = WebRequest.Create(reqItem.GetRequestUrl()) as HttpWebRequest;
            if (request == null)
            {
                throw new Exception(String.Format("Unable to create a request to the target URL: {0}", reqItem.TargetUrl));
            }

            request.Method = reqItem.Method.ToString();
            request.ContentType = reqItem.ContentType;
            request.ContentLength = (reqItem.Body != null) ? reqItem.Body.Length : 0;
            request.ServicePoint.Expect100Continue = false;

            if (reqItem.TimeOut > 0)
            {
                request.Timeout = reqItem.TimeOut;
            }

            if (reqItem.Body != null)
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(reqItem.Body, 0, reqItem.Body.Length);
                }
            }

            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// Executes a request to the specified URL with the parameters specified.
        /// </summary>
        /// <param name="reqItem">The REST request details.</param>
        /// <returns>The response as a string.</returns>
        public static string DoRequestToString(RestRequest reqItem)
        {
            string result = null;

            using (HttpWebResponse resp = DoRequest(reqItem))
            {
                if (resp != null)
                {
                    Encoding respEncoding = null;

                    if (!String.IsNullOrEmpty(resp.CharacterSet))
                    {
                        respEncoding = Encoding.GetEncoding(resp.CharacterSet);
                    }

                    if ((respEncoding == null) || (String.IsNullOrEmpty(respEncoding.EncodingName)))
                    {
                        //Default encoding
                        respEncoding = Encoding.UTF8;
                    }

                    using (StreamReader reader = new StreamReader(resp.GetResponseStream(), respEncoding))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Retrieves the HTTP response body.
        /// Note: the response body for protocol errors is limited to 64K, unless explicitly set during the request.
        /// </summary>
        /// <param name="resp">The HTTP response.</param>
        /// <returns>The HTTP response body, if one exists.</returns>
        public static string GetResponseBody(HttpWebResponse resp)
        {
            string result = String.Empty;

            if (resp != null)
            {
                Encoding respEncoding = null;

                if (!String.IsNullOrEmpty(resp.CharacterSet))
                {
                    respEncoding = Encoding.GetEncoding(resp.CharacterSet);
                }

                if ((respEncoding == null) || (String.IsNullOrEmpty(respEncoding.EncodingName)))
                {
                    //Default encoding
                    respEncoding = Encoding.UTF8;
                }

                using (StreamReader reader = new StreamReader(resp.GetResponseStream(), respEncoding))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}