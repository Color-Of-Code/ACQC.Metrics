using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ACQC.Metrics.Controls {

	using Data;

	public partial class KiviatForm : Form {

		private const float LIMITMIN = 0.1f;
		private const float PREF_MIN = 0.1f;
		private const float PREF_MAX = 0.7f;

		public KiviatForm ()
		{
			InitializeComponent ();
		}

		private void pictureBox_Paint (object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.Clear (Color.White);
			if (Model == null)
				return;
			Render (g);
		}

		internal IKiviatModel Model { get; set; }

		private Color _ringColor = Color.LightGreen;
		private Color _okColor = Color.Green;
		private Color _ngColor = Color.Red;

		//:	ring_color(0.88, 1.0, 0.85)	// default color for the inner ring
		//,	ok_color(0.2, 0.8, 0)		// default color for a point in range
		//,	ng_color(0.95, 0.3, 0.3)	// default color for a point out of range
		//,	element_line(0.1, 0.1, 0.2)
		//,	element_value(0.0, 0.0, 0.0)

		private static float CenterValue
		{
			get { return -PREF_MIN / (PREF_MAX - PREF_MIN); }
		}

		private static float BorderValue
		{
			get { return (1.0f - PREF_MIN) / (PREF_MAX - PREF_MIN); }
		}

		// 0.0 -> PREF_MIN
		// 1.0 -> PREF_MAX
		private static float ScaleAndCropToCircle (float value)
		{
			// clip value
			if (value <= CenterValue)
				value = CenterValue;
			if (value >= BorderValue)
				value = BorderValue;
			return PREF_MIN + value * (PREF_MAX - PREF_MIN);
		}

		private static float Value (ValueRange r, IKiviatElement e)
		{
			float result = 0;
			float val = e.GetValue (r.Name);
			result = (val - r.MinPreferred) / r.PreferredRange;
			if (result > 1.0) {
				// result must not be bigger than get_border_value()
				float max_valk = BorderValue - 1.0f;
				float max_valv = r.MaxRange - r.MaxPreferred;
				result = (float)(Math.Log10 (1.0 + (val - r.MaxPreferred) / max_valv) / Math.Log10 (2.0) * max_valk);
				result += 1.0f;
			}
			if (result < 0.0) {
				// result must not be smaller than get_center_value()
				float min_valk = -CenterValue;
				float min_valv = r.MinPreferred - r.MinRange;
				result = (float)(Math.Log10 (1.0 + (r.MinPreferred - val) / min_valv) / Math.Log10 (2.0) * min_valk);
				result = -result;
			}
			result = ScaleAndCropToCircle (result);
			Debug.Assert (result >= 0.0f);
			Debug.Assert (result <= 1.0f);
			return result;
		}

		private Font _font = new Font (FontFamily.GenericSansSerif, 10.0f);
		private Brush _textBrush = new SolidBrush (Color.Black);

		private void RenderAxes (Graphics dc, IList<ValueRange> axes)
		{
			PolarConverter polar = GetPolarConverter (axes.Count);

			Pen axesPen = new Pen (Color.Gray, 1.0f);

			// min limit ring
			// outer ring
			dc.DrawEllipse (axesPen, polar.ToPos (0), GetRadius ());

			// max preferred ring
			Brush pRingColor = new SolidBrush (_ringColor);
			dc.FillEllipse (pRingColor, polar.ToPos (0), GetRadius () * PREF_MAX);

			// min preferred ring
			Brush pInner = new SolidBrush (Color.White);
			//dc.setColor(1, 1, 1);
			dc.FillEllipse (pInner, polar.ToPos (0), GetRadius () * PREF_MIN);

			for (int i = 0; i < axes.Count; i++) {
				dc.DrawLine (axesPen, polar.ToPos (0), polar.AxisPos (1.0f, i));
			}
		}

		private void RenderNames (Graphics dc, IList<ValueRange> axes)
		{
			int num_axes = axes.Count;
			PolarConverter polar = GetPolarConverter (num_axes);
			int i = 0;
			foreach (ValueRange axis in axes) {
				PointF pos = polar.AxisPos (1.2f, i);
				SizeF size = dc.MeasureString (axis.Description, _font);
				pos.X -= size.Width / 2;
				pos.Y -= size.Height / 2;
				dc.DrawString (axis.Description, _font, _textBrush, pos);
				++i;
			}
		}

		private PolarConverter GetPolarConverter (int count)
		{
			return new PolarConverter (count, pictureBox.Width, pictureBox.Height, GetRadius ());
		}

		private float GetRadius ()
		{
			return Math.Min (pictureBox.Width, pictureBox.Height) / 2.5f;
		}

		private void RenderElementLines (Graphics dc, Pen p, IList<ValueRange> axes, IKiviatElement e)
		{
			PolarConverter polar = GetPolarConverter (axes.Count);

			float lastValue = Value (axes.Last (), e);
			PointF lastPos = polar.AxisPos (lastValue, -1);

			int i = 0;
			foreach (ValueRange axis in axes) {
				// draw the line
				float curValue = Value (axis, e);
				PointF ctrlPos1 = polar.TangentPos (lastValue, (i - 1), 0.4f);
				PointF ctrlPos2 = polar.TangentPos (curValue, i, -0.4f);
				PointF curPos = polar.AxisPos (curValue, i);
				dc.DrawBezier (p, lastPos, ctrlPos1, ctrlPos2, curPos);
				lastPos = curPos;
				lastValue = curValue;
				i++;
			}
		}

		private void RenderElementPoints (Graphics dc, IList<ValueRange> axes, IKiviatElement e)
		{
			PolarConverter polar = GetPolarConverter (axes.Count);
			int i = 0;
			foreach (ValueRange axis in axes) {
				// draw the point
				float v = Value (axis, e);
				Brush b = null;
				if (v >= PREF_MIN && v <= PREF_MAX)
					b = new SolidBrush (_okColor);
				else
					b = new SolidBrush (_ngColor);
				dc.FillEllipse (b, polar.AxisPos (v, i), 4.0f);
				i++;
			}
		}

		private void RenderElementValues (Graphics dc, IList<ValueRange> axes, IKiviatElement e)
		{
			PolarConverter polar = GetPolarConverter (axes.Count);
			int i = 0;
			foreach (ValueRange axis in axes) {
				// draw the value
				float value = e.GetValue (axis.Name);
				String text = String.Format ("{0:0.#}", value);
				PointF pos = polar.AxisPos (Value (axis, e)+0.1f, i);
				SizeF size = dc.MeasureString (text, _font);
				pos.X -= size.Width / 2;
				pos.Y -= size.Height / 2;
				dc.DrawString (text, _font, _textBrush, pos);
				i++;
			}
		}

		//void KiviatView::renderElementMean(AbstractDC& dc,
		//                               IList<ValueRange> axes,
		//                               const Element& e)
		//{
		//    dc.setColor(0.2, 0.2, 0.9);
		//    dc.setLineWidth(4.0);
		//    dc.setLineDashes(2.0, 2.0);
		//    renderElementLines(dc, axes, e);
		//}

		//void KiviatView::renderElementMin(AbstractDC& dc,
		//                                   IList<ValueRange> axes,
		//                                   const Element& e)
		//{
		//    dc.setColor(0.9, 0.2, 0.2);
		//    dc.setLineWidth(2.0);
		//    dc.setLineDashes(4.0, 4.0);
		//    renderElementLines(dc, axes, e);
		//}

		//void KiviatView::renderElementMax(AbstractDC& dc,
		//                                   IList<ValueRange> axes,
		//                                   const Element& e)
		//{
		//    dc.setColor(0.9, 0.2, 0.2);
		//    dc.setLineWidth(2.0);
		//    dc.setLineDashes(4.0, 4.0);
		//    renderElementLines(dc, axes, e);
		//}

		private void RenderElement (Graphics dc, Pen p, IList<ValueRange> axes, IKiviatElement e, bool light)
		{
			if (light) {
				RenderElementLines (dc, p, axes, e);
			} else {
				RenderElementLines (dc, p, axes, e);
				RenderElementPoints (dc, axes, e);
				RenderElementValues (dc, axes, e);
			}
		}

		private void Render (Graphics dc)
		{
			RenderAxes (dc, Model.Axes);
			//Axes axes = model->getValueRanges();

			//// draw the min value
			//Element emin = model->getMinimum();
			//renderElementMin(dc, axes, emin);

			//// draw the min value
			//Element emax = model->getMaximum();
			//renderElementMax(dc, axes, emax);

			//// draw the mean value
			//Element emean = model->getMean();
			//renderElementMean(dc, axes, emean);

			// draw value lines and value points
			Pen p = new Pen (Color.Black, 1.1f);
			if (Model.Elements.Count == 1) {
				IKiviatElement e = Model.Elements[0];
				RenderElement (dc, p, Model.Axes, e, false);
			} else {
				foreach (IKiviatElement e in Model.Elements)
					RenderElement (dc, p, Model.Axes, e, true);
			}

			// draw the title
			//dc.SetColor(0.2, 0.2, 0.2);
			//dc.SetFontSize(40);
			//dc.DrawString(WIDTH / 2, HEIGHT - 60, e.getName(), AbstractDC::CENTER);
			RenderNames (dc, Model.Axes);
		}

		private void KiviatForm_ResizeEnd (object sender, EventArgs e)
		{
			pictureBox.Invalidate ();
		}

		public void Redraw ()
		{
			pictureBox.Invalidate ();
		}
	}
}
