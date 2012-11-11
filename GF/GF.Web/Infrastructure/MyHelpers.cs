using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GF.Web.Infrastructure
{
    public static class MyHelpers
    {
        public static string SimpleDate(this HtmlHelper helper, DateTime date) {
            if (date != null)
                return date.ToString("d");
            return "";
        }

    }
}