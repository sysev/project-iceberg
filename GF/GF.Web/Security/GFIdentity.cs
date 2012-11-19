using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GF.Web.Security
{
    public class GFIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int CustomerID
        {
            get;
            set;
        }
    }
}