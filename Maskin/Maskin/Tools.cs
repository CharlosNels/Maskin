using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace Maskin
{
    public static class Tools
    {
        public static GraphicsPath CreateCircle(Rectangle rect)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(rect, 0, 360);
            return gp;
        }
        /// <summary>
        /// 圆角矩形
        /// </summary>
        /// <param name="rect">绘制按钮的区域</param>
        /// <param name="radius">圆角半径</param>
        /// <returns></returns>
        public static GraphicsPath CreateRadianRectangle(Rectangle rect,int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            int l = rect.Left;
            int t = rect.Top;
            int w = rect.Width;
            int h = rect.Height;
            gp.AddArc(l, t, 2 * radius, 2 * radius, 180, 90);
            gp.AddLine(l + radius, t, l + w - radius, t);
            gp.AddArc(l + w - 2 * radius, t, 2 * radius, 2 * radius, 270, 90);
            gp.AddLine(l + w, t + radius, l + w, t + h - radius);
            gp.AddArc(l + w - 2 * radius, t + h - 2 * radius, 2 * radius, 2 * radius, 0, 90);
            gp.AddLine(l + radius, t + h, l + w - radius, t + h);
            gp.AddArc(l, t + h - 2 * radius, 2 * radius, 2 * radius, 90, 90);
            gp.AddLine(l, t + radius, l, t + h - radius);
            return gp;
        }
        public static GraphicsPath CreateTriangle()
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(new Point[] { new Point(5,17), new Point(5,12), new Point(10,17), new Point(5,17) });
            return gp;
        }
        /// <summary>
        /// 创建指定的三角形
        /// </summary>
        /// <param name="p1">定点坐标,以此类推</param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static GraphicsPath CreateTriangle(Point p1,Point p2,Point p3)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(new Point[] { p1, p2, p3, p1 });
            return gp;
        }
        public static double RadianToDegree(double radian)
        {
            return radian * 180 / Math.PI;
        }
        // ***************** GDI+ Effect函数的示例代码 *********************
        // 作者     ： laviewpbt 
        // 作者简介 ： 对图像处理（非识别）有着较深程度的理解
        // 使用语言 ： VB6.0/C#/VB.NET
        // 联系方式 ： QQ-33184777  E-Mail:laviewpbt@sina.com
        // 开发时间 ： 2012.12.10-2012.12.12
        // 致谢     ： Aaron Lee Murgatroyd
        // 版权声明 ： 复制或转载请保留以上个人信息
        // *****************************************************************
        /// <summary>
        /// 获取对象的私有字段的值，感谢Aaron Lee Murgatroyd
        /// </summary>
        /// <typeparam name="TResult">字段的类型</typeparam>
        /// <param name="obj">要从其中获取字段值的对象</param>
        /// <param name="fieldName">字段的名称.</param>
        /// <returns>字段的值</returns>
        /// <exception cref="System.InvalidOperationException">无法找到该字段.</exception>
        /// 
        internal static TResult GetPrivateField<TResult>(this object obj, string fieldName)
        {
            if (obj == null) return default(TResult);
            Type ltType = obj.GetType();
            FieldInfo lfiFieldInfo = ltType.GetField(fieldName, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (lfiFieldInfo != null)
                return (TResult)lfiFieldInfo.GetValue(obj);
            else
                throw new InvalidOperationException(string.Format("Instance field '{0}' could not be located in object of type '{1}'.", fieldName, obj.GetType().FullName));
        }
        public static IntPtr NativeHandle(this Bitmap Bmp)
        {
            return Bmp.GetPrivateField<IntPtr>("nativeImage");
            /*  用Reflector反编译System.Drawing.Dll可以看到Image类有如下的私有字段
                internal IntPtr nativeImage;
                private byte[] rawData;
                private object userData;
                然后还有一个 SetNativeImage函数
                internal void SetNativeImage(IntPtr handle)
                {
                    if (handle == IntPtr.Zero)
                    {
                        throw new ArgumentException(SR.GetString("NativeHandle0"), "handle");
                    }
                    this.nativeImage = handle;
                }
                这里在看看FromFile等等函数，其实也就是调用一些例如GdipLoadImageFromFile之类的GDIP函数，并把返回的GDIP图像句柄
                通过调用SetNativeImage赋值给变量nativeImage，因此如果我们能获得该值，就可以调用VS2010暂时还没有封装的GDIP函数
                进行相关处理了，并且由于.NET肯定已经初始化过了GDI+，我们也就无需在调用GdipStartup初始化他了。
             */
        }
        public struct BlurParameters
        {
            internal float Radius;
            internal bool ExpandEdges;
        }
        /// <summary>
        /// 对图像进行高斯模糊,参考：http://msdn.microsoft.com/en-us/library/ms534057(v=vs.85).aspx
        /// </summary>
        /// <param name="Rect">需要模糊的区域，会对该值进行边界的修正并返回.</param>
        /// <param name="Radius">指定高斯卷积核的半径，有效范围[0，255],半径越大，图像变得越模糊.</param>
        /// <param name="ExpandEdge">指定是否对边界进行扩展，设置为True，在边缘处可获得较为柔和的效果. </param>
        public static void GaussianBlur(this Bitmap Bmp, ref Rectangle Rect, float Radius = 10, bool ExpandEdge = false)
        {
            int Result;
            IntPtr BlurEffect;
            BlurParameters BlurPara;
            if ((Radius < 0) || (Radius > 255))
            {
                throw new ArgumentOutOfRangeException("半径必须在[0,255]范围内");
            }
            BlurPara.Radius = Radius;
            BlurPara.ExpandEdges = ExpandEdge;
            Result = GdipCreateEffect(BlurEffectGuid, out BlurEffect);
            if (Result == 0)
            {
                IntPtr Handle = Marshal.AllocHGlobal(Marshal.SizeOf(BlurPara));
                Marshal.StructureToPtr(BlurPara, Handle, true);
                GdipSetEffectParameters(BlurEffect, Handle, (uint)Marshal.SizeOf(BlurPara));
                GdipBitmapApplyEffect(Bmp.NativeHandle(), BlurEffect, ref Rect, false, IntPtr.Zero, 0);
                // 使用GdipBitmapCreateApplyEffect函数可以不改变原始的图像，而把模糊的结果写入到一个新的图像中
                GdipDeleteEffect(BlurEffect);
                Marshal.FreeHGlobal(Handle);
            }
            else
            {
                throw new ExternalException("不支持的GDI+版本，必须为GDI+1.1及以上版本，且操作系统要求为Win Vista及之后版本.");
            }
        }
        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipCreateEffect(Guid guid, out IntPtr effect);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipDeleteEffect(IntPtr effect);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipGetEffectParameterSize(IntPtr effect, out uint size);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipSetEffectParameters(IntPtr effect, IntPtr parameters, uint size);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipGetEffectParameters(IntPtr effect, ref uint size, IntPtr parameters);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipBitmapApplyEffect(IntPtr bitmap, IntPtr effect, ref Rectangle rectOfInterest, bool useAuxData, IntPtr auxData, int auxDataSize);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipBitmapCreateApplyEffect(ref IntPtr SrcBitmap, int numInputs, IntPtr effect, ref Rectangle rectOfInterest, ref Rectangle outputRect, out IntPtr outputBitmap, bool useAuxData, IntPtr auxData, int auxDataSize);

        private static Guid BlurEffectGuid = new Guid("{633C80A4-1843-482B-9EF2-BE2834C5FDD4}");
    }
}
