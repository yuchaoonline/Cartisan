using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Cartisan.Components.Image {
    public class ImageCut {
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        public ImageCut(int x, int y, int width, int height) {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }

        public Bitmap Cut(System.Drawing.Image sourceImage) {
            // 如果没有源图，则不能剪切
            if (sourceImage == null) {
                return null;
            }

            // 源图的宽与高
            var sourceWidth = sourceImage.Width;
            var sourceHeight = sourceImage.Height;

            // 如果剪切的起点超出了图像范围，则不能剪切
            if (this._x >= sourceWidth || this._y >= sourceHeight) {
                return null;
            }

            // 如果剪切图像的宽度超出源图的范围，则宽度为从剪切的基点到图像的右边
            if (this._x + this._width > sourceWidth) {
                this._width = sourceWidth - this._x;
            }

            // 如果剪切图像的高度超出源图的范围，则高度为从剪切的基点到图像的底部
            if (this._y + this._height > sourceHeight) {
                this._height = sourceHeight - this._y;
            }

            try {
                // 创建剪切的图
                var bmpOut = new Bitmap(this._width, this._height, PixelFormat.Format64bppPArgb);
                var graphic = Graphics.FromImage(bmpOut);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 在指定范围并且按指定位置绘制源图
                graphic.DrawImage(sourceImage,
                    new Rectangle(0, 0, this._width, this._height),
                    new Rectangle(this._x, this._y, this._width, this._height),
                    GraphicsUnit.Pixel);

                graphic.Dispose();
                return bmpOut;
            }
            catch (Exception) {
                return null;
            }
        } 
    }
}