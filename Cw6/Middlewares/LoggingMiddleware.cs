using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cw6.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            string path = httpContext.Request.Path;
            string method = httpContext.Request.Method;
            string queryString = httpContext.Request.QueryString.ToString();

            using (StreamReader reader
                 = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                string bodyStr = await reader.ReadToEndAsync();
                string createText = method + "\n" + path + "\n" + queryString + "\n" + bodyStr;
                httpContext.Request.Body.Position = 0;

                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                filePath=filePath+ @"\log.txt";
                using (StreamWriter writetext = new StreamWriter(filePath,true))
                {
                    writetext.WriteLine(createText);
                }
            }

            await _next(httpContext);
        }
    }
}
