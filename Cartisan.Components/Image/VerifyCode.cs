using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Cartisan.Components.Image {
    /// <summary>
    /// 验证码操作类
    /// </summary>
    public class VerifyCode {
        private string _generateCode = string.Empty;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        ///绘制的符号数量
        /// </summary>
        /// <value>The code count.</value>
        public int CodeCount { get; set; }

        /// <summary>
        /// 生成的验证码
        /// </summary>
        /// <value>The generate code.</value>
        public string GenerateCode {
            get { return this._generateCode; }
        }

        /// <summary>
        /// 验证码字体
        /// </summary>
        /// <value>The font family.</value>
        public FontFamily FontFamily { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        /// <value>The size of the font.</value>
        public int FontSize { get; set; }

        /// <summary>
        ///是否扭曲
        /// </summary>
        /// <value><c>true</c> if wave; otherwise, <c>false</c>.</value>
        public bool Wave { get; set; }

        /// <summary>
        /// 扭曲方向,true为纵向,false为横向
        /// </summary>
        /// <value><c>true</c> if [wave dir]; otherwise, <c>false</c>.</value>
        public bool WaveDirc { get; set; }

        /// <summary>
        /// 波形扭曲幅度
        /// </summary>
        /// <value>The wave value.</value>
        public int WaveValue { get; set; }

        /// <summary>
        /// 波形起始相位
        /// </summary>
        /// <value>The wave phase.</value>
        public double WavePhase { get; set; }

        public VerifyCode(int codeCount) {
            this.CodeCount = codeCount;
            this.FontFamily = new FontFamily("宋体");
            this.FontSize = 20;
            this.Wave = true;
            this.WaveDirc = false;
            this.WaveValue = 6;
            this.WavePhase = 2;
        }

        /// <summary>
        /// 创建图片并输出
        /// </summary>
        /// <param name="context">The context.</param>
//        public void CreateImage(HttpContext context) {
//            CreateCheckCodeImage(this.CreateRandomCode(this.CodeCount), context);
//        }

        public byte[] CreateImage() {
            return this.CreateCheckCodeImage(this.CreateRandomCode(this.CodeCount));
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="codeCount">The code count.</param>
        /// <returns></returns>
        private string CreateRandomCode(int codeCount) {
            var random = new Random();

            for (var i = 0; i < codeCount; i++) {
                //随机整数
                var number = random.Next();

                //字符从0-9,A-Z中随机产生,对应的ASCII码为 48-57,65-90
                number = number % 36;

                if (number < 10) {
                    number += 48;
                }
                else {
                    number += 55;
                }
                this._generateCode += ((char)number).ToString();
            }
            return this._generateCode;
        }

        private byte[] CreateCheckCodeImage(string checkCode) {
            //若验证码为空,直接返回
            if (string.IsNullOrEmpty(checkCode))
                return null;
            //根据验证码的长度确定输出图片的长度
            //var image = new Bitmap((int)Math.Ceiling((checkCode.Length * (FontSize + 4.5))), FontSize * 2);
            var image = new Bitmap((int)Math.Ceiling(90.0), 30);
            //创建Graphs对象
            var g = Graphics.FromImage(image);

            try {
                var random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的背景噪音线25条
                for (var i = 0; i < 20; i++) {
                    //噪音线的坐标(x1,y1),(x2,y2)
                    var x1 = random.Next(image.Width);
                    var x2 = random.Next(image.Width);
                    var y1 = random.Next(image.Height);
                    var y2 = random.Next(image.Height);
                    //用银色画出噪音线
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                //输出图片中的验证码
                var font = new Font(this.FontFamily, this.FontSize, (FontStyle.Bold | FontStyle.Italic));
                //线性渐变笔刷
                var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Green, this.RandomColor(), 1.2f, true);
                g.DrawString(checkCode, font, brush, 0, 0);

                //图片的前景噪音点 
                for (var i = 0; i < 50; i++) {
                    var x = random.Next(image.Width);
                    var y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Peru), 0, 0, image.Width - 1, image.Height - 1);

                if (this.Wave)//是否扭曲
                    image = this.TwistImage(image, this.WaveDirc, this.WaveValue, this.WavePhase);

                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);

                return ms.ToArray();
            }
            finally {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="checkCode">The check code.</param>
        /// <param name="context">The context.</param>
//        private void CreateCheckCodeImage(string checkCode, HttpContext context) {
//            var msByte = this.CreateCheckCodeImage(checkCode);
//            context.Response.ClearContent();
//            context.Response.ContentType = "image/Jpeg";
//            context.Response.BinaryWrite(msByte);
//        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">源图片.</param>
        /// <param name="bxDir">扭曲方向</param>
        /// <param name="dMultValue">波形的幅度倍数,越大扭曲的程度越高,一般为3</param>
        /// <param name="dPhase">波形的起始相位,取值区间[0-2*PI]</param>
        /// <returns></returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bxDir, double dMultValue, double dPhase) {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            //将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            var dBaseAxisLen = bxDir ? destBmp.Height : (double)destBmp.Width;

            for (var i = 0; i < destBmp.Width; i++) {
                for (var j = 0; j < destBmp.Height; j++) {
                    var dx = bxDir ? (PI2 * j) / dBaseAxisLen : (PI2 * i) / dBaseAxisLen;
                    dx += dPhase;
                    var dy = Math.Sin(dx);

                    //取得当前点的颜色
                    var nOldX = bxDir ? i + (int)(dy * dMultValue) : i;
                    var nOldY = bxDir ? j : j + (int)(dy * dMultValue);

                    var color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height) {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }

        /// <summary>
        /// 随机颜色
        /// </summary>
        /// <returns></returns>
        private Color RandomColor() {
            var randomNumFirst = new Random(DateTime.Now.Millisecond);
            var randomNumSencond = new Random(DateTime.Now.Millisecond);

            var red = randomNumFirst.Next(256);
            var green = randomNumSencond.Next(256);
            var blue = (red + green > 400) ? 0 : 400 - red - green;
            blue = blue > 255 ? 255 : blue;
            return Color.FromArgb(red, green, blue);
        }
    }
}