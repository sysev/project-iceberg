﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace GF.Web.Models
{
    [DataContract]
    public class DataTableDataMember
    {
        /// <summary>
        /// An unaltered copy of sEcho sent from the client side. 
        /// This parameter will change with each draw (it is basically a draw count) - 
        /// so it is important that this is implemented. Note that it strongly recommended 
        /// for security reasons that you 'cast' this parameter to an integer 
        /// in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>        
        [DataMember(Name = "sEcho")]
        public string sEcho { get; private set; }

        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        [DataMember(Name = "iTotalRecords")]
        public int iTotalRecords { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - 
        /// not just the number of records being returned in this result set)
        /// </summary>
        [DataMember(Name = "iTotalDisplayRecords")]
        public int iTotalDisplayRecords { get; set; }

        /// <summary>
        /// Optional - this is a string of column names, comma separated (used in combination with sName) 
        /// which will allow DataTables to reorder data on the client-side if required for display
        /// </summary>
        [DataMember(Name = "sColumns")]
        public string sColumns { get; set; }

        /// <summary>
        /// The data in a 2D array
        /// Fill this structure with the plain table data
        /// represented as string.
        /// </summary>
        [DataMember(Name = "aaData")]
        public List<List<string>> aaData { get; set; }

        public DataTableDataMember(string sEcho, int iTotalRecords, int iTotalDisplayRecords, List<List<string>> aaData)
        {
            this.sEcho = sEcho;
            this.iTotalRecords = iTotalRecords;
            this.iTotalDisplayRecords = iTotalDisplayRecords;
            this.aaData = aaData;
        }

        public DataTableDataMember(DataTable dataTable, int iTotalRecords, int iTotalDisplayRecords, List<List<string>> aaData)
            : this(dataTable.sEcho, iTotalRecords, iTotalDisplayRecords, aaData)
        {
        }

        public DataTableDataMember(DataTable dataTable)
            : this(dataTable.sEcho, 0, 0, null)
        {
        }

        private DataTableDataMember()
            : this(string.Empty, 0, 0, null)
        {
        }

    }
}