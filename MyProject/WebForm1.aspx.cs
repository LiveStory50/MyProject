using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using MyProject;
using System.Data;

namespace MyProject
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public DataTable dataTable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            //var ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            //SqlConnection sqlConnection = new SqlConnection(ConStr);
            //sqlConnection.Open();
            //var k = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var sql = $"insert into Student(Name,Phone,Sex) values('振威{i}号','{i}','0')";
            //    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            //    k = sqlCommand.ExecuteNonQuery();
            //    k += k == 0 ? 0 : 1;
            //}
            //sqlConnection.Close();
           DataTable dataTable=  SqlHelper.GetDatatable("select * from student");

            ;
        }
        public void ToExcel()
        {

        }
    }
}