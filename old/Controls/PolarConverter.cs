using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ACQC.Metrics.Controls {

	public static class GraphicsExtensiosn
	{
		public static void DrawEllipse(this Graphics g, Pen pen, PointF center, float radius)
		{
			RectangleF rect = new RectangleF(center.X - radius, center.Y - radius, radius*2.0f, radius*2.0f);
			g.DrawEllipse(pen, rect);
		}

		public static void FillEllipse(this Graphics g, Brush brush, PointF center, float radius)
		{
			RectangleF rect = new RectangleF(center.X - radius, center.Y - radius, radius*2.0f, radius*2.0f);
			g.FillEllipse(brush, rect);
		}
	}


	internal class PolarConverter {

		public PolarConverter (int count, float width, float height, float radius)
		{
			_axes = count;
			_width = width;
			_height = height;
			_radius = radius;
		}

		// Converts a polar coordinate representation to position coordinates.
		// d: 0.0 - center, 1.0 - outer edge of circle
		// c: 0.0 - top, 0.5 - bottom, 1.0 - top
		public PointF ToPos (float d)
		{
			return ToPos (d, 0);
		}

		public PointF ToPos (float d, float c)
		{
			PointF center = new PointF (_width / 2.0f, _height / 2.1f);

			float vx = _radius * (float)Math.Sin (Math.PI * 2 * c);
			float vy = _radius * (float)Math.Cos (Math.PI * 2 * c);

			return new PointF (center.X + vx * d, center.Y + vy * d);
		}

		public PointF AxisPos (float d, int axis)
		{
			return AxisPos (d, axis, false);
		}

		public PointF AxisPos (float d, int axis, bool text)
		{
			float angle = (float)axis / (float)_axes;
			if (text) {
				// compute something adapted to display the text
				// without overlaying the lines
				if (angle < 0.5f)
					angle -= 0.02f;
				else
					angle += 0.02f;
				d += 0.05f;
			}
			return ToPos (d, angle);
		}

		public PointF TangentPos (float d, int axis, float length)
		{
			PointF ret = AxisPos (d, axis);
			float angle = (float)axis / (float)_axes;
			float x = (float)Math.Cos (angle * 2.0 * Math.PI) * length * d * _radius;
			float y = -(float)Math.Sin (angle * 2.0 * Math.PI) * length * d * _radius;
			ret.X += x;
			ret.Y += y;
			return ret;
		}

		private int _axes;
		private float _width;
		private float _height;
		private float _radius;
	};
}
