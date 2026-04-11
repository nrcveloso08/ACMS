using System.Security.Claims;
using System.Security.Principal;

namespace ATS.Web.Extensions
{
    public static class UserExtension
    {
        public static int ClientId(this IPrincipal user)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            int clientId;
            int.TryParse(identity?.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value, out clientId);

            return clientId;
        }

        public static string GetLogin(this IPrincipal user)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            return identity?.Claims.FirstOrDefault(c => c.Type == "ad_login")?.Value;
        }

        public static IList<string> GetRoles(this IPrincipal user)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;

            IList<string> userRoles = identity?.Claims
               .Where(c => c.Type.ToLower().Contains("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Select(x => x.Value.ToLower()).ToList();

            if (user.Is247User())
            {
                userRoles.Add("247user");
            }
            return userRoles;
        }

        public static bool IsFromClient(this IPrincipal user, string clientname)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;

            IList<string> userRoles = identity?.Claims
               .Where(c => c.Type.ToLower().Contains("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Select(x => x.Value.ToLower()).ToList();

            return userRoles.Contains(clientname.ToLower() + "users");
        }

        public static bool Is247User(this IPrincipal user)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            string authorityFrom = identity?.Claims
               .Where(c => c.Type.ToLower().Contains("authenticationmethod")).Select(x => x.Value).FirstOrDefault();

            return authorityFrom.Contains("master") ? true : false;
        }

        public static bool IsRoleAuthorized(this IPrincipal user, string roles)
        {
            bool authorized = false;
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            IList<string> userRoles = user.GetRoles();

            foreach (string role in userRoles)
            {
                if (roles.ToLower().Contains(role))
                {
                    authorized = true;
                    break;
                }
            }

            return authorized;
        }

        public static string FullName(this IPrincipal user)
        {
            return string.Format("{0} {1}"
                , user.FirstName()
                , user.LastName());
        }

        public static string FirstName(this IPrincipal user)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            return identity?.Claims
                .FirstOrDefault(c => c.Type.ToLower().Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;
        }

        public static string LastName(this IPrincipal user)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            return identity?.Claims
                .FirstOrDefault(c => c.Type.ToLower().Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"))?.Value;
        }

        public static string EmailAddress(this IPrincipal user)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            return identity?.Claims
                .FirstOrDefault(c => c.Type.ToLower().Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"))?.Value;
        }
    }
}
