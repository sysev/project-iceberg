using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GF.Web.Models
{
    /// <summary>
    /// Generic class to hold basic column definition
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColumnDef<T>
    { 
            /// <summary>
            ///    Name of the property of the target business object
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            ///   Holds display name of the column
            /// </summary>
            public string DisplayName { get; set; }

            /// <summary>
            ///   Holds the function which returns a string given an
            ///   instance of an object
            /// </summary>
            public Func<T, string> ValueFunction { get; set; }

            public ColumnDef(string DisplayName, string Name, Func<T, string> ValueFunction)
            {
                this.Name = Name;
                this.DisplayName = DisplayName;
                this.ValueFunction = ValueFunction;
            }
         
    }
}