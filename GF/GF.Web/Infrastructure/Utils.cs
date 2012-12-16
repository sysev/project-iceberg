using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GF.Web.Infrastructure
{
    public class Utils
    {
       
        public static string XMLHeader = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>";

        public static string XmlElement(KeyValuePair<string, string> item)
        {
            return Tag(item.Key, false) + item.Value + Tag(item.Key, true);
        }


        internal static string TagStart(string name)
        {
            return Tag(name, false);
        }

        internal static String TagEnd(string name)
        {
            return Tag(name, true);
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