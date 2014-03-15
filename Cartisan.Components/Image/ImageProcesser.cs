using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Cartisan.Components.Image {
    public static class ImageProcesser {
        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="originalImage">原图</param>
        /// <param name="targetWidth">目标宽度</param>
        /// <param name="targetHeight">目标高度</param>
        /// <param name="sameOriginal">是否与原图等比例</param>
        /// <param name="tensile">是否拉伸</param>
        /// <returns>处理后的图片</returns>
        public static System.Drawing.Image CompressImage(System.Drawing.Image originalImage, int targetWidth, int targetHeight, bool sameOriginal, bool tensile) {
            // 原图的宽与高及宽高的比例
            var originalWidth = originalImage.Width;
            var originalHeight = originalImage.Height;
            float originalRatio = originalWidth / (float)originalHeight;

            // 如果不需要拉伸，并且原图大小都小于目标图大小，则不作处理
            if (!tensile && originalWidth <= targetWidth && originalHeight <= targetHeight) {
                return originalImage;
            }

            // 转换允许的最大宽与高及宽高的比例
            float targetRatio = targetWidth / (float)targetHeight;

            int thumbnailWidth;
            int thumbnailHeight;
            if (targetRatio > originalRatio) {
                // 当转换允许的最大宽高比例大于原图宽高比例，
                // 则先定高，高即为转换允许的最大高值，
                // 宽等于转换允许的最大高乘以原图宽高比例
                thumbnailHeight = targetHeight;
                thumbnailWidth = (int)Math.Floor(targetHeight * originalRatio);
            }
            else {
                // 当转换允许的最大宽高比例等于或小于原图宽高比例，
                // 则先定宽，宽即为转换允许的最大宽值，
                // 高等于转换允许的最大宽值除以原图比例
                thumbnailWidth = targetWidth;
                thumbnailHeight = (int)Math.Floor(targetWidth / originalRatio);
            }

            // 宽高比例修正，如果前面计算出的结果值大于允许转换的最大宽高值，
            // 则修正成为允许的最大宽高值。
            thumbnailWidth = thumbnailWidth > targetWidth ? targetWidth : thumbnailWidth;
            thumbnailHeight = thumbnailHeight > targetHeight ? targetHeight : thumbnailHeight;

            // 创建以允许转换的最大宽高值为宽高的预览图
            Bitmap thumbnailImage;
            int pasteX;
            int pasteY;
            if (sameOriginal) {
                // 与原图相同，即等比例缩放后，依然保持原来的形状。
                thumbnailImage = new Bitmap(thumbnailWidth, thumbnailHeight);

                // 绘图基点为0，即左上角的原点
                pasteX = 0;
                pasteY = 0;
            }
            else {
                // 原图等比例绽放后，新图大小按指定大小创建，不足部分以黑底显示
                thumbnailImage = new Bitmap(targetWidth, targetHeight);

                // 计算绘图的基点，居中绘制
                pasteX = (targetWidth - thumbnailWidth) / 2;
                pasteY = (targetHeight - thumbnailHeight) / 2;
            }

            // 在预览图上进行绘制
            var graphic = Graphics.FromImage(thumbnailImage);

            if (sameOriginal) {
                // 如果按指定大小创建给预览图，不足部分填充黑色
                graphic.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, targetWidth, targetHeight));
            }

            // 以最高质量转换图形
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // 以指定位置及指定宽高绘制指定图形
            graphic.DrawImage(originalImage, pasteX, pasteY, thumbnailWidth, thumbnailHeight);
            return thumbnailImage;
        }
    }
}