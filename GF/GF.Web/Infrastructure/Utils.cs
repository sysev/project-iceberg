using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GF.Web.Infrastructure
{
    public class Utils
    {
       

        public static string XmlElement(KeyValuePair<string, string> item)
        {
            return Tag(item.Key, false) + item.Value + Tag(item.Key, true);
        }


        internal static string TagStart(Type type)
        {
            return Tag(type.Name, false);
        }

        internal static String TagEnd(Type type)
        {
            return Tag(type.Name, true);
        }

        private static string Tag(string Value, bool End){
            return  "<" + (End ? "/" : "") + Value + ">";
        }

        public static string Quote(string str)
        {
            return @"""" + str + @"""";
        }
    }
}