using System;
using System.Web;
using System.Drawing;

namespace Rc.WebControls.HttpHandler {

	/// <summary>
	/// 生成验证码图片
	/// </summary>
	public class BuildImageByText {

		private int _fontSize = 10;

		/// <summary>
		/// 设置或获取字体的大小,采用unit的单位
		/// </summary>
		public int FontSize {
			get { return _fontSize; }
			set { _fontSize = value; }
		}

		/// <summary>
		/// 高度
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// 宽度
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// 设置左右边距
		/// </summary>
		public int PaddingLeftRight { get; set; }

		/// <summary>
		/// 设置上下边距
		/// </summary>
		public int PaddingTopBottom { get; set; }

		/// <summary>
		/// 根据请求返回需要的验证码图片
		/// </summary>
		/// <param name="context">httpContext对象</param>
		/// <param name="strValue">生成图片的文本</param>
		public void Build(string strValue, ref HttpContext context) {

			if (Width == 0)
				Width = (int)(strValue.Length * _fontSize + 10) + PaddingLeftRight * 2;
			if (Height == 0)
				Height = _fontSize * 2 + PaddingTopBottom * 2;

			System.Drawing.Bitmap image = new System.Drawing.Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(image);
			g.Clear(Color.White);
			//定义颜色
			Color[] c = { Color.Black, Color.DarkRed, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
			//定义字体 
			string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "新宋体", "黑体" };
			Random rand = new Random();
			//随机输出噪点
			for (int i = 0; i < 50; i++) {
				int x = rand.Next(image.Width);
				int y = rand.Next(image.Height);
				g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
			}

			//输出不同字体和颜色的字符
			for (int i = 0; i < strValue.Length; i++) {
				int cindex = rand.Next(3);
				int findex = rand.Next(3);

				Font f = new System.Drawing.Font(font[findex], _fontSize + rand.Next(2));
				Brush b = new System.Drawing.SolidBrush(c[cindex]);
				g.DrawString(strValue.Substring(i, 1), f, b, i * _fontSize + PaddingLeftRight, PaddingTopBottom);
			}

			//输出到浏览器
			image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

			g.Dispose();
			image.Dispose();
		}
	}
}
