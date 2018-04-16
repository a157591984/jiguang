using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rc.Web.Payment.Core
{
    public class LogHandler
    {
        /// <summary>
        /// 保存支付请求 author：Ethan
        /// </summary>
        /// <param name="orderNum">订单号</param>
        /// <param name="con">内容</param>
        /// <param name="serialNum">交易号</param>
        public static void WriteLogForPay(string orderNum, string con, string serialNum)
        {
            string logPath = HttpContext.Current.Server.MapPath("/Upload/PayLog");
            DateTime CurrTime = DateTime.Now;

            //拼接日志完整路径
            logPath = logPath + "\\" + CurrTime.ToString("yyyy-MM") + "\\" + CurrTime.Day + "\\" + orderNum + ".txt";

            //日志内容
            string content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + serialNum + "\r\n" + con + "\r\n";
            Rc.Common.LogContext.AddLogInfo(logPath, content, true);
        }
    }
}