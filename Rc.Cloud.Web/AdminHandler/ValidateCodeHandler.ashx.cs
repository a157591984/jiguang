using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;

namespace PowerCreditMS.Web.Admin.AdminHandler
{
    /// <summary>
    /// ValidateCodeHandler 的摘要说明
    /// </summary>
    public class ValidateCodeHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/png";
            string code = string.Empty;
            byte[] buffer = GenerateVerifyImage(23, 4, ref code);
            context.Session["AdminValidateCode"] = code;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }

        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <param name="nLen">验证码的长度</param>
        /// <param name="strKey">输出参数，验证码的内容</param>
        /// <returns>图片字节流</returns>
        private byte[] GenerateVerifyImage(int heitht, int nLen, ref string strKey)
        {
            int nBmpWidth = 13 * nLen + 5;
            int nBmpHeight = heitht;
            Bitmap bmp = new Bitmap(nBmpWidth, nBmpHeight);

            // 1. 生成随机背景颜色
            int nRed, nGreen, nBlue;  // 背景的三元色
            System.Random rd = new Random(unchecked((int)System.DateTime.Now.Ticks));
            nRed = 218;// rd.Next(255) % 128 + 128;
            nGreen = 229;// rd.Next(255) % 128 + 128;
            nBlue = 234;// rd.Next(255) % 128 + 128;

            // 2. 填充位图背景
            Graphics graph = Graphics.FromImage(bmp);
            graph.FillRectangle(new SolidBrush(Color.FromArgb(nRed, nGreen, nBlue))
             , 0
             , 0
             , nBmpWidth
             , nBmpHeight);


            // 3. 绘制干扰线条，采用比背景略深一些的颜色
            int nLines = 3;
            SolidBrush sb = new SolidBrush(Color.FromArgb(nRed - 17, nGreen - 17, nBlue - 17));
            Pen pen = new Pen(sb, 3);
            for (int a = 0; a < nLines; a++)
            {
                int x1 = rd.Next() % nBmpWidth;
                int y1 = rd.Next() % nBmpHeight;
                int x2 = rd.Next() % nBmpWidth;
                int y2 = rd.Next() % nBmpHeight;
                graph.DrawLine(pen, x1, y1, x2, y2);
            }

            // 采用的字符集，可以随即拓展，并可以控制字符出现的几率
            string strCode = "ABCDEFGHJKLMNPQRSTUVWXYZ";

            // 4. 循环取得字符，并绘制
            string strResult = "";
            for (int i = 0; i < nLen; i++)
            {
                int x = (i * 13 + rd.Next(3));
                int y = rd.Next(4) + 1;

                // 确定字体
                Font font = new Font("Courier New",
                 12 + rd.Next() % 4,
                 FontStyle.Bold);
                char c = strCode[rd.Next(strCode.Length)];  // 随机获取字符
                strResult += c.ToString();

                // 绘制字符
                graph.DrawString(c.ToString(),
                 font,
                    //new SolidBrush(Color.FromArgb(nRed - 60 + y * 3, nGreen - 60 + y * 3, nBlue - 40 + y * 3)),
                 new SolidBrush(Color.FromArgb(225, 123, 8)),
                 x,
                 y);
            }

            // 5. 输出字节流
            System.IO.MemoryStream bstream = new System.IO.MemoryStream();
            bmp.Save(bstream, ImageFormat.Png);
            bmp.Dispose();
            graph.Dispose();

            strKey = strResult;
            byte[] byteReturn = bstream.ToArray();
            bstream.Close();

            return byteReturn;
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