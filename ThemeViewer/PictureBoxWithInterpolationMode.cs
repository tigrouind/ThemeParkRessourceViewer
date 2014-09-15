using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System.Windows.Forms
{
	/// <summary>
	/// Description of PictureBoxWithInterpolationMode.
	/// </summary>
	public class PictureBoxWithInterpolationMode : PictureBox
	{
	    public InterpolationMode InterpolationMode { get; set; }
	
	    protected override void OnPaint(PaintEventArgs paintEventArgs)
	    {
	        paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
	        base.OnPaint(paintEventArgs);
	    }
	  
	}
}
