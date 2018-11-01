using System.Collections.Generic;

namespace PeopleNet.MazeSolver.Models
{
	/// <summary>
	/// This class is used by the maze algorithm to traverse the maze.
	/// </summary>
	public class Point
	{
		#region Private members

		private int x;
		private int y;
		private Point parent;

		#endregion

		#region Public properties

		public Point Parent
		{
			get { return parent; }
		}

		public int X
		{
			get { return x; }
			set { value = x; }
		}

		public int Y
		{
			get { return y; }
			set { value = y; }
		}

		#endregion

		#region Constructors

		public Point( int x, int y )
		{
			this.x = x;
			this.y = y;
			parent = null;
		}

		public Point( int x, int y, Point parent )
		{
			this.x = x;
			this.y = y;
			this.parent = parent ?? new Point( -1, -1 );
		}

		#endregion

		#region Overrides

		public override string ToString()
		{
			return $"({x}, {y})";
		}

		#endregion

		#region Output methods

		public List<Point> TracePath()
		{
			var path = new List<Point>();
			var curPoint = this;

			path.Add( curPoint );

			while( curPoint.Parent != null && curPoint.Parent.X != -1 )
			{
				path.Add( curPoint.Parent );
				curPoint = curPoint.Parent;
			}

			path.Reverse();

			return path;
		}

		#endregion
	}
}