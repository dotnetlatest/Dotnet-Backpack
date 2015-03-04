// /*
//  * -----------------------------------------------------------------
//  * © 2006-2015  MineCloud, Inc (http://www.minecloud.com)
//  * -----------------------------------------------------------------
//  */ 

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Backpack.Core.Tools.IO
{
    public enum XmlVersion
    {
        Xml10 = 0,
        Xml11 = 1
    }

    /// <summary>
    /// XML Serialization Utilities and Extension Methods
    /// </summary>
    public static  class XmlTransformer
    {
        private static Regex _xml10RegEx = new Regex(@"[^\u0009\u000a\u000d\u0020-\ud7ff\ue000-\ufffd\u10000-\u10ffff]",
                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex _xml11RegEx = new Regex(@"[\u0001-\u0008\u000b\u000c\u000e-\u001f\u007f-\u0084\u0086-\u009f\ufdd0-\ufddf]",
                                                      RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Serializes the target object into it's JSON form.
        /// </summary>
        /// <param name="targetObj">The object to serialize.</param>
        /// <returns>The serialized JSON form of the object.</returns>
        public static string Serialize(object targetObj)
        {
            string result = null;

            if (targetObj != null)
            {
                XmlSerializer serializer = new XmlSerializer(targetObj.GetType());
                using (MemoryStream memStream = new MemoryStream())
                {
                    serializer.Serialize(memStream, targetObj);
                    result = Encoding.UTF8.GetString(memStream.ToArray());
                }
            }

            return result;
        }

        /// <summary>
        /// Deserializes the target XML string to the target class.
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The deserialized instance of the target class.</returns>
        public static T Deserialize<T>(string xml) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(xml))
            {
                return serializer.Deserialize(reader) as T;
            }
        }

        /// <summary>
        /// Serializes the target object into it's XML form.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized XML form of the object.</returns>
        public static string ToXml(this object obj)
        {
            return ToXml(obj, null);
        }

        /// <summary>
        /// Serializes the target object into it's XML form.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>The serialized XML form of the object.</returns>
        public static string ToXml(this object obj, string defaultValue)
        {
            string result = defaultValue;

            if (obj != null)
            {
                result = Serialize(obj);
            }

            return result;
        }

        /// <summary>
        /// Removes illegal XML characters according to the XML version.
        /// XML 1.0: http://www.w3.org/TR/2006/REC-xml-20060816/#charsets
        /// XML 1.1: http://www.w3.org/TR/xml11/#charsets
        /// </summary>
        /// <param name="value">The XML string.</param>
        /// <param name="version">The XML version.</param>
        public static string RemoveIllegalXMLCharacters(this string value, XmlVersion version)
        {
            if (value == null)
            {
                return null;
            }

            switch (version)
            {
                case XmlVersion.Xml10:
                    return _xml10RegEx.Replace(value, String.Empty);

                case XmlVersion.Xml11:
                    return _xml11RegEx.Replace(value, String.Empty);

                default:
                    throw new Exception("Unsupported XML version.");
            }
        }
    }
}