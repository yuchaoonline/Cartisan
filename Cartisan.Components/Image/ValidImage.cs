using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Cartisan.Components.Image {
    public class ValidImage {
        /// <summary>
        /// 无参构造
        /// </summary>
        public ValidImage() { }
        /// <summary>
        /// 带有生成字符个数的构造
        /// </summary>
        /// <param name="charNum">验证码中包含随机字符的个数</param>
        public ValidImage(int charNum) {
            this.CharNum = charNum;
        }
        /// <summary>
        /// 带有验证码图片宽度和高度的构造
        /// </summary>
        /// <param name="width">验证码图片宽度</param>
        /// <param name="height">验证码图片高度</param>
        public ValidImage(int width, int height) {
            this.width = width;
            this.height = height;
        }
        /// <summary>
        /// 带有生成字符个数，验证码图片宽度和高度的构造
        /// </summary>
        /// <param name="charNum">验证码中包含随机字符的个数</param>
        /// <param name="width">验证码图片宽度</param>
        /// <param name="height">验证码图片高度</param>
        public ValidImage(int charNum, int width, int height) {
            this.CharNum = charNum;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// 验证码中字符个数
        /// </summary>
        int charNum = 5; //默认字符个数为5

        public int CharNum {
            get { return this.charNum; }
            set { this.charNum = value; }
        }
        /// <summary>
        /// 字号
        /// </summary>
        int fontSize = 20;

        public int FontSize {
            get { return this.fontSize; }
        }
        /// <summary>
        /// 图片宽度
        /// </summary>
        int width = 200;

        public int Width {
            get { return this.width; }
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        int height = 45;

        public int Height {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// 随机生成的字符串
        /// </summary>
        string validStr = "";

        public string ValidStr {
            get { return this.validStr; }
            set { this.validStr = value; }
        }

        /// <summary>
        /// 产生指定个数的随机字符串，默认字符个数为5
        /// </summary>
        void GetValidateCode() {
            Random rd = new Random(); //创建随机数对象            

            //产生由 charNum 个字母或数字组成的一个字符串
            string str = "abcdefghijkmnpqrstuvwyzABCDEFGHJKLMNPQRSTUVWYZ23456789田国兴";//共57个字符，除 l,o,x,I,O,X,1,0 的所有数字和大写字母
            for (int i = 0; i < this.charNum; i++) {
                this.validStr = this.validStr + str.Substring(rd.Next(57), 1);//返回0到56共57个
            }

        }

        /// <summary>
        /// 由随机字符串，随即颜色背景，和随机线条产生的Image
        /// </summary>
        /// <returns>Image</returns>
        public System.Drawing.Image GetImgWithValidateCode()//返回 Image
        {
            //产生随机字符串
            this.GetValidateCode();

            //声明一个位图对象
            Bitmap bitMap = null;
            //声明一个绘图画面
            Graphics gph = null;
            //创建内存流
            MemoryStream memStream = new MemoryStream();

            Random random = new Random();

            //由给定的需要生成字符串中字符个数 CharNum， 图片宽度 Width 和高度 Height 确定字号 FontSize，
            //确保不因字号过大而不能全部显示在图片上
            int fontWidth = (int)Math.Round(this.width / (this.charNum + 2) / 1.3);
            int fontHeight = (int)Math.Round(this.height / 1.5);
            //字号取二者中小者，以确保所有字符能够显示，并且字符的下半部分也能显示
            this.fontSize = fontWidth <= fontHeight ? fontWidth : fontHeight;

            //创建位图对象
            bitMap = new Bitmap(this.width + this.FontSize, this.height);
            //根据上面创建的位图对象创建绘图图面
            gph = Graphics.FromImage(bitMap);

            //设定验证码图片背景色
            gph.Clear(this.GetControllableColor(200));
            //产生随机干扰线条
            for (int i = 0; i < 10; i++) {
                Pen backPen = new Pen(this.GetControllableColor(100), 2);
                //线条起点
                int x = random.Next(this.width);
                int y = random.Next(this.height);
                //线条终点
                int x2 = random.Next(this.width);
                int y2 = random.Next(this.height);
                //划线
                gph.DrawLine(backPen, x, y, x2, y2);
            }

            //定义一个含10种字体的数组
            String[] fontFamily ={ "Arial", "Verdana", "Comic Sans MS", "Impact", "Haettenschweiler", 
                                                "Lucida Sans Unicode", "Garamond", "Courier New", "Book Antiqua", "Arial Narrow" };
            SolidBrush sb = new SolidBrush(this.GetControllableColor(0));
            //通过循环,绘制每个字符,
            for (int i = 0; i < this.validStr.Length; i++) {
                Font textFont = new Font(fontFamily[random.Next(10)], this.fontSize, FontStyle.Bold);//字体随机,字号大小30,加粗 

                //每次循环绘制一个字符,设置字体格式,画笔颜色,字符相对画布的X坐标,字符相对画布的Y坐标
                int space = (int)Math.Round((double)((this.width - this.fontSize * (this.CharNum + 2)) / this.CharNum));
                //纵坐标
                int y = (int)Math.Round((double)((this.height - this.fontSize) / 3));
                gph.DrawString(this.validStr.Substring(i, 1), textFont, sb, this.fontSize + i * (this.fontSize + space), y);
            }
            //扭曲图片
            bitMap = this.TwistImage(bitMap, true, random.Next(3, 5), random.Next(3));

            try {

                bitMap.Save(memStream, ImageFormat.Gif);

            }
            catch (Exception ex) {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            //gph.Dispose();
            bitMap.Dispose();

            System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
            gph.DrawImage(img, 50, 20, this.width, 10);

            return img;
        }
        /// <summary>
        /// 产生一种 R,G,B 均大于 colorBase 随机颜色，以确保颜色不会过深
        /// </summary>
        /// <returns>背景色</returns>
        Color GetControllableColor(int colorBase) {
            Color color = Color.Black;
            if (colorBase > 200) {
                //System.Windows.Forms.MessageBox.Show("可控制颜色参数大于200，颜色默认位黑色");
            }
            Random random = new Random();
            //确保 R,G,B 均大于 colorBase，这样才能保证背景色较浅
            color = Color.FromArgb(random.Next(56) + colorBase, random.Next(56) + colorBase, random.Next(56) + colorBase);
            return color;
        }

        /// <summary>
        /// 扭曲图片
        /// </summary>
        /// <param name="srcBmp"></param>
        /// <param name="bXDir"></param>
        /// <param name="dMultValue"></param>
        /// <param name="dPhase"></param>
        /// <returns></returns>
        Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase) {
            int leftMargin = 0;
            int rightMargin = 0;
            int topMargin = 0;
            int bottomMargin = 0;
            //float PI = 3.14159265358979f;
            float PI2 = 6.28318530717959f;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            double dBaseAxisLen = bXDir ? Convert.ToDouble(destBmp.Height) : Convert.ToDouble(destBmp.Width);
            for (int i = 0; i < destBmp.Width; i++) {
                for (int j = 0; j < destBmp.Height; j++) {
                    double dx = 0;
                    dx = bXDir ? PI2 * Convert.ToDouble(j) / dBaseAxisLen : PI2 * Convert.ToDouble(i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    //取得当前点的颜色        
                    int nOldX = 0;
                    int nOldY = 0;
                    nOldX = bXDir ? i + Convert.ToInt32(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + Convert.ToInt32(dy * dMultValue);
                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= leftMargin && nOldX < destBmp.Width - rightMargin && nOldY >= bottomMargin && nOldY < destBmp.Height - topMargin) {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }






        /// <summary>
        /// 判断验证码是否正确
        /// </summary>
        /// <param name="inputValCode">待判断的验证码</param>
        /// <returns>正确返回 true,错误返回 false</returns>
        public bool IsRight(string inputValCode) {

            if (this.validStr.ToUpper().Equals(inputValCode.ToUpper()))//无论输入大小写都转换为大些判断
                        {
                return true;
            }
            else {
                return false;
            }
        }

    } 
}