// /*
//  * -----------------------------------------------------------------
//  * © 2006-2015  MineCloud, Inc (http://www.minecloud.com)
//  * -----------------------------------------------------------------
//  */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backpack.Web.Rest
{
    public class RestRequest
    {
        public RestMethod Method { get; set; }
        public string TargetUrl { get; set; }
        public string TargetPath { get; set; }
        public List<KeyValuePair<string, string>> ParamList { get; set; }
        public string ContentType { get; set; }
        public int TimeOut { get; set; }

        public byte[] Body { get; set; }
        public string TextBody
        {
            set
            {
                if (value != null)
                {
                    Body = Encoding.UTF8.GetBytes(value);
                }
                else
                {
                    Body = null;
                }
            }
        }

        /// <summary>
        /// Retrieves the full request URL.
        /// </summary>
        /// <returns>The full request URL.</returns>
        public string GetRequestUrl()
        {
            string query = BuildQueryString(ParamList);
            query = (String.IsNullOrEmpty(query)) ? "" : "?" + query;

            return String.Format("{0}{1}{2}", TargetUrl, TargetPath, query);
        }

        /// <summary>
        /// Creates the query string using the entries in the parameter list.
        /// </summary>
        /// <param name="paramList">The parameter list.</param>
        /// <returns>The query string.</returns>
        public static string BuildQueryString(List<KeyValuePair<string, string>> paramList)
        {
            return BuildQueryString(paramList, true, true);
        }

        /// <summary>
        /// Creates the query string using the entries in the parameter list.
        /// </summary>
        /// <param name="paramList">The parameter list.</param>
        /// <param name="appendDelim">Determines whether parameter values are delimited by ampersands.</param>
        /// <param name="doEscape">Determines whether parameter values should be URI encoded/escaped.</param>
        /// <returns>The query string.</returns>
        public static string BuildQueryString(List<KeyValuePair<string, string>> paramList, bool appendDelim, bool doEscape)
        {
            string result = string.Empty;

            if ((paramList != null) && (paramList.Count > 0))
            {
                result = paramList
                    .Aggregate("", (s, e) =>
                        string.Format("{0}{1}{2}={3}",
                            s,
                            (!string.IsNullOrEmpty(s) && appendDelim) ? "&" : "",
                            doEscape ? Uri.EscapeDataString(e.Key) : e.Key,
                            doEscape ? Uri.EscapeDataString(e.Value ?? "") : e.Value ?? "")
                    );
            }

            return result;
        }
    }
}