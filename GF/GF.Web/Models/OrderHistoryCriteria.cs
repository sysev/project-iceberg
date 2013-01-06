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
    public class OrderHistoryCriteria : Criteria
    {
        [DataMember(Name = "materialNumber")]
        public string MaterialNumber { get; set; }

        [DataMember(Name = "rollNumber")]
        public string RollNumber { get; set; }

        [DataMember(Name = "customerPartNumber")]
        public string CustomerPartNumber { get; set; }

        [DataMember(Name = "customerPONumber")]
        public string CustomerPONumber { get; set; }

        [DataMember(Name = "orderStatus")]
        public bool OrderStatus { get; set; }

        [DataMember(Name = "orderStatusFromDate")]
        public string OrderStatusFromDate { get; set; }

        [DataMember(Name = "orderStatusToDate")]
        public string OrderStatusToDate { get; set; }
    }
}