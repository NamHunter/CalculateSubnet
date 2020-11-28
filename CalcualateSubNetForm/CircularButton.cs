using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.Xpo;

namespace CalcualateSubNetForm
{

    public class CircularButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }

}