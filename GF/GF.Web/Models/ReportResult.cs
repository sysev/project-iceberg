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
    public class ReportResult : ActionResult
    {
        ReportModel reportModel { get; set; }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        public ReportResult() { }

        public ReportResult(ReportModel reportModel)
        {
            this.reportModel = reportModel;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Get not allowed");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
               // response.ContentEncoding = this.ContentEncoding;
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ReportModel), new List<Type>() { typeof(ReportModel), typeof(OrderHistoryCriteria), typeof(DataTableDataMember)});
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, reportModel);
            string json = response.ContentEncoding.GetString(ms.ToArray());

            response.Write(json);
        }
    }
}