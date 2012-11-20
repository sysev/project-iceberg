using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GF.Web.Controllers
{
    public static class ControllerExtensionMethods
    {
        public static string GetGuid(this Controller controller)
        {
            return controller.RouteData.Values["guid"].ToString();
        }

        public static void SetGuidSession(this Controller controller, string name, object value)
        {
            string guid = controller.GetGuid();
            var key = guid + "_" + name;
            controller.Session[key] = value;
        }

        public static void SetGuidSession(this Controller controller, string guid, string name, object value)
        { 
            var key = guid + "_" + name;
            controller.Session[key] = value;
        }

        public static object GetGuidSession(this Controller controller, string name)
        {
            string guid = controller.GetGuid();
            var key = guid + "_" + name;
            return controller.Session[key];
        }



    }
}