using System;
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
    public class ReportModel
    {
        [DataMember(Name = "criteria")]
        Criteria criteria { get; set; }

        [DataMember(Name = "dataTable")]
        DataTableDataMember dataTable { get; set; }

        public ReportModel() { }

        public ReportModel(Criteria criteria, DataTableDataMember dataTable)
        {
            this.criteria = criteria;
            this.dataTable = dataTable;
        }
    }
}