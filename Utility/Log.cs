using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Log
    {
        //public static void LogAdd(Exception exception)
        //{
        //    string WriteErrorToFile = "TRUE";
        //    try
        //    {
        //        if (WriteErrorToFile != null && WriteErrorToFile.ToUpper() == "TRUE")
        //        {
        //            var err = new CreateLogFiles();
        //            err.ErrorLog(httpconServer.MapPath("/ErrorLog"), HttpContext.Current.Request.Url + Environment.NewLine
        //                + (HttpContext.Current.Request.UrlReferrer != null ? "UrlRefer: " + HttpContext.Current.Request.UrlReferrer + Environment.NewLine : "")
        //                + exception.Message + Environment.NewLine + exception.StackTrace);
        //        }
        //    }
        //    catch
        //    {
        //        if (WriteErrorToFile != null && WriteErrorToFile.ToUpper() == "TRUE")
        //        {
        //            if (!string.IsNullOrEmpty(exception.Message))
        //            {
        //                CreateLogFiles Err = new CreateLogFiles();
        //                Err.ErrorLog(HttpContext.Current.Server.MapPath("/ErrorLog"), Environment.NewLine + Environment.NewLine + HttpContext.Current.Request.RawUrl.ToString() + Environment.NewLine + exception.Message + Environment.NewLine + exception.StackTrace + " - From:" + HttpContext.Current.Request.UrlReferrer);
        //            }
        //        }
        //    }
        //}
        //public class CreateLogFiles
        //{
        //    private string sLogFormat;
        //    private string sErrorTime;

        //    public CreateLogFiles()
        //    {
        //        //sLogFormat used to create log files format :

        //        // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message

        //        sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

        //        //this variable used to create log filename format "

        //        //for example filename : ErrorLogYYYYMMDD

        //        string sYear = DateTime.Now.Year.ToString();
        //        string sMonth = DateTime.Now.Month.ToString();
        //        string sDay = DateTime.Now.Day.ToString();
        //        sErrorTime = sYear + sMonth + sDay;
        //    }
        //    public void ErrorLog(string sPathName, string sErrMsg)
        //    {
        //        try
        //        {
        //            StreamWriter sw = new StreamWriter(sPathName + "\\Error_" + sErrorTime + ".txt", true);
        //            sw.WriteLine(sLogFormat + sErrMsg);
        //            sw.Flush();
        //            sw.Close();
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //}
    }
}
