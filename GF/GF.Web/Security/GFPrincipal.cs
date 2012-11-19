using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GF.Web.Security
{
    public class GFPrincipal : IPrincipal
    {

        private IIdentity _identity;
        public GFPrincipal(IIdentity Identity)
        {
            _identity = Identity;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}