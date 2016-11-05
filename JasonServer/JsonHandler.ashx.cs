using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace JasonServer
{
    /// <summary>
    /// Summary description for JasonHandler
    /// </summary>
    public class JasonHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream);
            
            context.Response.ContentType = "text/plain";
            context.Response.Write(reader.ReadToEnd());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}