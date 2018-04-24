using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawablesUI
{
    public class GraphicsDrawer : IDrawer, IDisposable
    {
        private readonly Graphics _graph;
        private float _pointWidth;
        private Pen _pen = new Pen(Color.Black, 2);

        public GraphicsDrawer(Graphics g)
        {
            _graph = g;
            g.PageUnit = GraphicsUnit.Pixel;
            g.PageScale = 1;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public void SelectPen(Color color, int width = 2)
        {
            _pen.Dispose();
            _pen = new Pen(color, width);
            _pointWidth = width / _graph.PageScale;
        }

        public void DrawPoint(PointF point)
        {
            using (var b = new SolidBrush(_pen.Color))
            {
                _graph.FillEllipse(b, new RectangleF(
                    new PointF(point.X - _pointWidth / 2, point.Y - _pointWidth / 2),
                    new SizeF(_pointWidth, _pointWidth)
                    ));
            }
        }

        public void DrawLine(PointF start, PointF end)
        {
            _graph.DrawLine(_pen, start, end);
        }

        public void DrawEllipseArc(PointF center, SizeF sizes, 
            float startAngle = 0, float endAngle = 360, float rotate = 0)
        {
            _graph.TranslateTransform(center.X, center.Y);
            _graph.RotateTransform(rotate);
            _graph.TranslateTransform(-center.X, -center.Y);
            _graph.DrawArc(_pen, new RectangleF(
                new PointF(center.X - sizes.Width / 2, center.Y - sizes.Height / 2), sizes),
                startAngle, endAngle);
            _graph.ResetTransform();
        }

        public void Dispose()
        {
            _pen.Dispose();
        }
    }
}
