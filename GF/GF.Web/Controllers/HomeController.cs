using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GF.Data;
using GF.Data.Helpers;
using GF.Data.Models;
using GF.Web.Infrastructure;
using GF.Web.Models;
using GF.Web.Security;

namespace GF.Web.Controllers
{
    /// <summary>
    ///   Home Controller Class for proto
    ///      
    /// </summary> 
    public class HomeController : Controller
    {
        private IGFRepository _gfRepository;
        private GFPrincipal _gfPrincipal;
       
        public HomeController(IGFRepository GfRepository, GFPrincipal GfPrincipal)
        {
            _gfRepository = GfRepository;
            _gfPrincipal = GfPrincipal;
        }

        private int CustomerID
        {
            get
            { 
                return (_gfPrincipal.Identity as GFIdentity).CustomerID; 
            }
        }

        public ActionResult Index()
        {
            return View(); 
        }

        #region  Report Page Actions
         
        public ActionResult HistorySummaryReport(int? CustomerID)
        {
            var model = new HistorySummaryViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();
            return View(new HistorySummaryViewModel(this.GetGuid(), colList));
        }
          
        public ActionResult HistoryDetail(int? CustomerID)
        {
            var HistoryDetails = _gfRepository.GetOrderRolls(CustomerID.Value); 
            return View(HistoryDetails);
        }
         
        public ActionResult HistoryDetailReport(int? CustomerID)
        {

            var model = new HistoryDetailViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new HistoryDetailViewModel(this.GetGuid(), colList));
        }
         

        public ActionResult InventoryStatusReport(int? CustomerID)
        {
            var model = new InventoryStatusViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new InventoryStatusViewModel(this.GetGuid(), colList));
        }

        public ActionResult InventoryAgingReport(int? CustomerID)
        {
            var model = new InventoryAgingViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new InventoryAgingViewModel(this.GetGuid(), colList));
        }

        public ActionResult AutoReplenishmentReport(int? CustomerID)
        {
            var model = new AutoReplenishmentViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new AutoReplenishmentViewModel(this.GetGuid(), colList));
        }

        public ActionResult Help(int? CustomerID)
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetOrderRollHistory(OrderHistoryCriteria criteria, DataTable dataTable)
        {
            var model = new HistoryDetailViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<OrderRoll> HistoryDetails = _gfRepository.GetOrderRolls(CustomerID).OrderBy(orderByClause).ToList<OrderRoll>();

            //var criteria = new OrderHistoryCriteria();
            return GetResults<OrderRoll>(criteria, dataTable, HistoryDetails, model.Columns);
        }

        [HttpPost]
        public ActionResult GetOrderRollHistorySummary(OrderHistoryCriteria criteria, DataTable dataTable)
        {
            var model = new HistorySummaryViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<OrderRoll> HistoryDetails = _gfRepository.GetOrderRolls(CustomerID).OrderBy(orderByClause).ToList<OrderRoll>();
            return GetResults<OrderRoll>(criteria, dataTable, HistoryDetails, model.Columns);
        }

        [HttpPost]
        public ActionResult GetInventory(EmptyCriteria criteria, DataTable dataTable)
        {
            var model = new InventoryStatusViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<MaterialAvailability> MaterialAvailability = _gfRepository.GetMaterialAvailability(CustomerID).OrderBy(orderByClause).ToList<MaterialAvailability>();

            return GetResults<MaterialAvailability>(criteria, dataTable, MaterialAvailability, model.Columns);
        }

        [HttpPost]
        public ActionResult GetInventoryAging(EmptyCriteria criteria, DataTable dataTable)
        {
            var model = new InventoryAgingViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<MaterialAvailability> MaterialAvailability = _gfRepository.GetMaterialAvailability(CustomerID).OrderBy(orderByClause).ToList<MaterialAvailability>();

            return GetResults<MaterialAvailability>(criteria, dataTable, MaterialAvailability, model.Columns);
        }

        [HttpPost]
        public ActionResult GetAutoReplenishment(EmptyCriteria criteria, DataTable dataTable)
        {
            var model = new AutoReplenishmentViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<OrderRoll> OrderRoll = _gfRepository.GetOrderRolls(CustomerID).OrderBy(orderByClause).ToList<OrderRoll>();

            return GetResults<OrderRoll>(criteria, dataTable, OrderRoll, model.Columns);
        }

        [HttpPost]
        public ActionResult GetMaterialAvailability(DataTable dataTable)
        {

            var model = new MaterialAvailabilityViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<MaterialAvailability> items = _gfRepository.GetMaterialAvailability(CustomerID).OrderBy(orderByClause).ToList<MaterialAvailability>();
            var criteria = new EmptyCriteria();
            return GetResults<MaterialAvailability>(criteria, dataTable, items, model.Columns);
        }

        private ReportResult GetResults<T>(Criteria criteria, DataTable dataTable, IList<T> Objects, IList<ColumnDef<T>> Columns)
        {
            var end = dataTable.iDisplayStart + dataTable.iDisplayLength;
            if (end > Objects.Count - 1)
                end = Objects.Count;

            List<List<string>> table = new List<List<string>>();
            for (int i = dataTable.iDisplayStart; i < end; i++)
            {
                var row = new List<string>();
                foreach (var c in Columns)
                {
                    var obj = Objects[i];
                    var value = c.ValueFunction(obj);
                    row.Add(value);
                }
                table.Add(row);
            }
            var dataTableDataMember = new DataTableDataMember(dataTable, Objects.Count, Objects.Count, table);

            var result = new ReportResult(new ReportModel(criteria, dataTableDataMember));
            result.ContentEncoding = Encoding.UTF8;
            return result;
        }

        #endregion
         
        #region Export Actions
         
        /// <summary>
        ///   Below includes various ways exports can be created
        ///   there are some base object methods that help support exporting
        /// </summary>
        /// <returns></returns>

        public ActionResult ExportToExcel()
        {

            var model = new HistoryDetailViewModel();
            model.Results = _gfRepository.GetOrderRolls(1).Take(200).ToList();

            var bytes = ExportResultHelper.GetXLS(model.AsTable(true));
            return new ExcelResult(bytes, "filename.xls");
        }

        // EXCEL Export #2, just a CSV... but you don't get an annoying popup
        public ActionResult ExportToExcel2()
        {
            var model = new HistoryDetailViewModel();
            model.Results = _gfRepository.GetOrderRolls(1).Take(200).ToList();
            return ExportResultHelper.GetXLSAsCSVResult(model.AsTable(true));
        }

        //Uses default serializer to get the job done
        public ActionResult ExportToXml()
        {
            var model = new HistoryDetailViewModel();
            model.Results = _gfRepository.GetOrderRolls(1).Take(200).ToList();
            return File(model.AsStream(), "data/xml", "Results.xml");
        }



        public ActionResult ExportToCSV()
        {
            var model = new HistoryDetailViewModel();
            model.Results = _gfRepository.GetOrderRolls(1).Take(200).ToList();
            return ExportResultHelper.GetCSVResult(model.AsTable(true));
        }

        // XML Export #2
        //Performs a more customized XML format rather than the default
        public ActionResult ExportToXml2()
        {
            var model = new HistoryDetailViewModel();
            model.Results = _gfRepository.GetOrderRolls(1).Take(200).ToList();
            return ExportResultHelper.GetXMLResult(model.AsKeyValuePairList());
        }

        public ActionResult ExportToPDF()
        {
            var model = new HistoryDetailViewModel();
            model.Results = _gfRepository.GetOrderRolls(1).Take(200).ToList();
            return File(ExportResultHelper.GetPDF(model.AsTable(true)), "application/pdf", "Report.pdf");

        }


        #endregion

        
        public ActionResult About()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        
    }
}
