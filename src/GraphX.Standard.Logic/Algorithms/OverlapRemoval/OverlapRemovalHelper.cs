using GraphX.Measure;

namespace GraphX.Logic.Algorithms.OverlapRemoval
{
	public static class OverlapRemovalHelper
	{
		public static GPoint GetCenter( this Rect r )
		{
			return new GPoint( r.Left + r.Width / 2, r.Top + r.Height / 2 );
		}
	}
}