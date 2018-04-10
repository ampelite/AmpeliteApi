using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AmpeliteReport.Reports.PoDaily
{
    public partial class Index : System.Web.UI.Page
    {
        private string connTaap = ConfigurationManager.ConnectionStrings["db_Ampelite"].ConnectionString;
        private SqlConnection conn = null;
        private ReportDocument rptDoc;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["GroupCode"]))
            {
                string groupCode = Request.QueryString["GroupCode"];
                string teamName = Request.QueryString["TeamName"];
                DateTime sDate = DateTime.Parse(Request.QueryString["SDate"]);
                string reportType = Request.QueryString["RptType"];
                bool byCustomerOrder = bool.Parse(Request.QueryString["ByCustomerOrder"].ToString());

                switch (groupCode)
                {
                    case "fibre":
                    case "screw":
                        ExportProduct(groupCode, teamName, sDate, byCustomerOrder, reportType);
                        break;

                    case "saleteam":
                        ExportTeamSale(groupCode, teamName, sDate, reportType);
                        break;
                }
            }
        }

        private void ExportProduct(string groupCode, string teamName, DateTime sDate, bool byCustomerOrder, string reportType)
        {
            conn = new SqlConnection(connTaap);
            var cmd = new SqlCommand();
            var dt = new DataTable();
            var da = new SqlDataAdapter();
            rptDoc = new ReportDocument();

            try
            {
                conn.Open();

                cmd.CommandText = "dbo.sp_DAILYPO_RptByProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupCode", groupCode);
                cmd.Parameters.AddWithValue("@TeamName", teamName);
                cmd.Parameters.AddWithValue("@SDate", sDate.ToString("yyyy-MM-dd"));
                cmd.Connection = conn;

                da.SelectCommand = cmd;
                da.Fill(dt);
                                
                var rptReponse = (reportType == "excel"
                    ? ExportFormatType.Excel
                    : ExportFormatType.PortableDocFormat);

                string filePath = "";
                string fileName = "";

                if ( byCustomerOrder == false)
                {
                    filePath = teamName == "all" ? "./AllProduct.rpt" : "./ByProduct.rpt";
                    fileName = teamName == "all" ? "Report-All-Product" : "Report-Product";
                } else
                {
                    filePath = "./ByCustomerOrder.rpt";
                    fileName = "Report-By-Customer-Order";
                }                

                rptDoc.Load(Server.MapPath(filePath));
                rptDoc.SetDataSource(dt);
                rptDoc.SetParameterValue("@GroupCode", groupCode);
                rptDoc.SetParameterValue("@TeamName", teamName);
                rptDoc.SetParameterValue("@SDate", sDate.ToString("yyyy-MM-dd"));
                rptDoc.ExportToHttpResponse(rptReponse, Response, true, fileName);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void ExportTeamSale(string groupCode, string teamName, DateTime sDate, string reportType)
        {
            conn = new SqlConnection(connTaap);
            var cmd = new SqlCommand();
            var dt = new DataTable();
            var da = new SqlDataAdapter();
            rptDoc = new ReportDocument();

            try
            {
                conn.Open();

                cmd.CommandText = "dbo.sp_DAILYPO_RptByTeamSale";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupCode", groupCode);
                cmd.Parameters.AddWithValue("@TeamName", teamName);
                cmd.Parameters.AddWithValue("@SDate", sDate.ToString("yyyy-MM-dd"));
                cmd.Connection = conn;

                da.SelectCommand = cmd;
                da.Fill(dt);

                var rptReponse = (reportType == "excel"
                    ? ExportFormatType.Excel
                    : ExportFormatType.PortableDocFormat);

                rptDoc.Load(Server.MapPath("./ByTeamSale.rpt"));
                rptDoc.SetDataSource(dt);
                rptDoc.SetParameterValue("@GroupCode", groupCode);
                rptDoc.SetParameterValue("@TeamName", teamName);
                rptDoc.SetParameterValue("@SDate", sDate.ToString("yyyy-MM-dd"));
                rptDoc.ExportToHttpResponse(rptReponse, Response, true, "Report-TeamSale");

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}