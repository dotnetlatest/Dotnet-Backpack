using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Backpack.Core.Logging;

namespace Backpack.WebApi.Security
{
    /// <summary>
    /// Responsible for checking authenticated users and set the IPrincipal on the HttpRequest object.
    /// The handler overrides the SendAsync method. This allows it to pass a request to the next handler in the ASP.NET Web API processing pipeline, in cases where processing is allowed to continue, by calling
    ///base.SendAsync. It also allows it to return an error response, in cases where processing is not allowed to continue, by calling CreateUnauthorizedResponse.
    /// </summary>
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {

        public const char AuthorizationHeaderSeparator = ':';
        private const int UsernameIndex = 0;
        private const int PasswordIndex = 1;
        private const int ExpectedCredentialCount = 2;

        private readonly IBasicSecurityService _basicSecurityService;
        private readonly Log _log = Log.Get();

        public BasicAuthenticationMessageHandler(IBasicSecurityService basicSecurityService)
        {
            _basicSecurityService = basicSecurityService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                _log.Debug("Already authenticated; passing on to next handler...");
                return await base.SendAsync(request, cancellationToken);
            }
            if (!CanHandleAuthentication(request))
            {
                _log.Debug("Not a basic auth request; passing on to next handler...");
                return await base.SendAsync(request, cancellationToken);
            }
            bool isAuthenticated;
            try
            {
                isAuthenticated = Authenticate(request);
            }
            catch (Exception e)
            {
                _log.Error("Failure in auth processing", e);
                return CreateUnauthorizedResponse();
            }
            if (isAuthenticated)
            {
                var response = await base.SendAsync(request, cancellationToken);
                return response.StatusCode == HttpStatusCode.Unauthorized ? CreateUnauthorizedResponse() :
                response;
            }
            return CreateUnauthorizedResponse();
        }

        /// <summary>
        /// CanHandleAuthentication examines the request and returns true if it contains an HTTP
        /// header indicating the Basic authorization scheme.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool CanHandleAuthentication(HttpRequestMessage request)
        {
            return (request.Headers != null
            && request.Headers.Authorization != null
            && request.Headers.Authorization.Scheme.ToLowerInvariant() == "basic");
        }

        /// <summary>
        /// Authenticate uses GetCredentials to extract the credentials from the request, and then it
        ///delegates the actual work of setting the principal to the security service we implemented earlier.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Authenticate(HttpRequestMessage request)
        {
            _log.Debug("Attempting to authenticate...");
            var authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                return false;
            }
            var credentialParts = GetCredentialParts(authHeader);
            if (credentialParts.Length != ExpectedCredentialCount)
            {
                return false;
            }
            return _basicSecurityService.SetPrincipal(credentialParts[UsernameIndex], credentialParts[PasswordIndex]);
        }
        /// <summary>
        /// GetCredentials parses the credentials from the request. The thing to remember here is that
        /// the credentials arrive base64-encoded and separated by a delimiter (":").
        /// </summary>
        /// <param name="authHeader"></param>
        /// <returns></returns>
        public string[] GetCredentialParts(AuthenticationHeaderValue authHeader)
        {
            var encodedCredentials = authHeader.Parameter;
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.ASCII.GetString(credentialBytes);
            var credentialParts = credentials.Split(AuthorizationHeaderSeparator);
            return credentialParts;
        }
        public HttpResponseMessage CreateUnauthorizedResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("basic"));
            return response;
        }

    }
}