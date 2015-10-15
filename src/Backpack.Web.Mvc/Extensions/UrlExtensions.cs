using System;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Backpack.Core.Extensions;
using Backpack.Web.Mvc.Configuration;

namespace Backpack.Web.Mvc.Extensions
{
    public static class UrlExtensions
    {
        private static readonly ObjectCache _Cache = MemoryCache.Default;

        private static readonly string _StaticRoot = BackpackMvcSection.Default.Ref(d => d.Static).Ref(s => s.Root);

        private static readonly Regex _HttpProtocol = new Regex("^http://", RegexOptions.IgnoreCase);

        public static string Static(this UrlHelper helper, string contentPath, string minPrefix = ".min", string tokenName = "cb", bool fixMixedContent = true)
        {
            bool releaseMode = !helper.RequestContext.HttpContext.IsDebuggingEnabled;

            if (releaseMode && !String.IsNullOrEmpty(minPrefix))
            {
                string extension = VirtualPathUtility.GetExtension(contentPath);

                if (!String.IsNullOrEmpty(extension) && !extension.Contains(minPrefix))
                {
                    int extensionLocation = contentPath.LastIndexOf(extension);
                    contentPath = contentPath.Insert(extensionLocation, minPrefix);
                }
            }

            string url = helper.Content(contentPath);

            if (fixMixedContent)
            {
                if (releaseMode)
                {
                    url = _HttpProtocol.Replace(url, "//"); // attempt to defeat mixed content-warnings
                }
                else
                {
                    if (_HttpProtocol.IsMatch(url))
                    {
                        string message = String.Format("contentPath value '{0}' includes a http: prefix, which can cause mixed-content warnings over ssl.  If this is intentional, pass 'false' for 'fixedMixedContent'.", contentPath);
                        throw new ArgumentException(message, "contentPath");
                    }
                }
            }

            if (releaseMode && !String.IsNullOrEmpty(tokenName))
            {
                string token = GetToken(contentPath);

                if (!String.IsNullOrEmpty(token))
                {
                    // wanted to use Uri and Uri builder, but they don't support protocol relative (//) or non-absolute urls, respectively.
                    url = String.Concat(url,
                        url.Contains('?') ? '&' : '?',
                        tokenName,
                        '=',
                        token);
                }
            }

            if (releaseMode && !String.IsNullOrEmpty(_StaticRoot))
            {
                const string protocolEnd = "//";
                int protocolIndex = url.IndexOf(protocolEnd);

                if (protocolIndex < 0)
                {
                    url = _StaticRoot + url;
                }
            }

            return url;

        }

        private static string GetToken(string contentPath)
        {
            string key = "TokenFor:" + contentPath;
            string token = _Cache.Get(key) as string;

            if (token != null)
                return token;

            string resolvedPath = VirtualPathUtility.ToAbsolute(contentPath);

            if (HostingEnvironment.VirtualPathProvider.FileExists(resolvedPath))
            {
                byte[] hash;
                using (var s = VirtualPathProvider.OpenFile(resolvedPath))
                {
                    var md5 = MD5.Create();
                    hash = md5.ComputeHash(s);
                }

                token = HttpServerUtility.UrlTokenEncode(hash);

                var policy = new CacheItemPolicy
                {
                    ChangeMonitors =
                    {
                        new HostFileChangeMonitor(new[] {
                            HostingEnvironment.MapPath(contentPath)
                        })
                    }
                };

                _Cache.Set(key, token, policy);
            }

            return token;
        }
    }
}