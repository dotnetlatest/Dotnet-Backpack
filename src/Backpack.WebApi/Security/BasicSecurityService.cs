using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using Backpack.Core.Logging;

namespace Backpack.WebApi.Security
{
    public class BasicSecurityService : IBasicSecurityService
    {
        private readonly Log _log = Log.Get(typeof (BasicSecurityService));
        /// <summary>
        /// Used to construct a security principal
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public bool SetPrincipal(string username, string password)
        {
            var user = GetUser(username, password);
            IPrincipal principal;

            if (user == null || (principal = GetPrincipal(user)) == null)
            {
                _log.DebugFormat("System could not validate user {0}", username);
                return false;
            }
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
            return true;
        }

        /// <summary>
        /// Construct an IPrincipal User from credentials
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual IPrincipal GetPrincipal(User user)
        {
            var identity = new GenericIdentity(user.CompoundUserName);
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            identity.AddClaim(new Claim("Organization", user.Organization.Name));

            return new ClaimsPrincipal(identity);
        }

        public virtual User GetUser(string username, string password)
        {
            User userinfo = null;
            var credentials = username.Split('/').ToList();
            var organization = credentials.Take(1).FirstOrDefault();
            var email = credentials.Skip(1).FirstOrDefault();
            bool authenticated = Membership.ValidateUser(username, password);
            if (authenticated)
            {
               // var currentUserManager = UserManager.GetMembershipUser(email, organization);
                //userinfo = UserManager.CreateInstance(currentUserManager).User;
            }

            return userinfo;

        }
    }
}