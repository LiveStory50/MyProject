using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            switch (action)
            {
                case "toExcel":
                    ToExcel();
                    break;
                default:
                    break;
            }
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ToExcel()
        {

        }
    }
}