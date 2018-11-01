using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleNet.MazeSolver.Models
{
	public class Solver
	{
		#region Private members

		private string maze;
		private string[] mazeLines;
		private int height;
		private int width;
		private char[,] map;
		private bool[,] blocked;
		private int startRow = -1;
		private int startCol = -1;
		private int endRow = -1;
		private int endCol = -1;

		#endregion

		#region Constructor

		public Solver( string maze )
		{
			this.maze = maze;
		}

		#endregion

		#region Primary operations

		public MazeSolution Solve()
		{
			// Parse and validate input maze
			Initialize();

			// Call the maze solver
			var path = Traverse();

			// Process the maze solver output
			return RenderSolution( path );
		}

		#endregion

		#region Support  code

		private void Initialize()
		{
			if( string.IsNullOrWhiteSpace( maze ) )
				throw new Exception( "No maze provided" );

			mazeLines = maze.Split( '\n' ).Select( s => s.Replace( "\r", string.Empty ) ).ToArray();
			height = mazeLines.Length;

			if( height < 5 )
				throw new Exception( "Maze must be at least 5 rows" );

			width = mazeLines[0].Length;

			if( width < 5 )
				throw new Exception( "Maze must be at least 5 columns" );

			if( mazeLines.Any( ml => ml.Length != width ) )
				throw new Exception( "All maze rows must be the same length" );

			map = new char[width, height];
			blocked = new bool[width, height];

			for( int row = 0; row < height; row++ )
			{
				string curLine = mazeLines[row];

				for( int column = 0; column < width; column++ )
				{
					if( !"AB#.".Contains( curLine[column] ) )
						throw new Exception( "Maze can only contain the characters 'A', 'B', '#' and '.'" );

					map[column, row] = curLine[column];
					blocked[column, row] = curLine[column] == '#';

					if( curLine[column] == 'A' )
					{
						if( startRow != -1 || startCol != -1 )
							throw new Exception( "Start position can only be specified once" );

						startRow = row;
						startCol = column;
					}

					if( curLine[column] == 'B' )
					{
						if( endRow != -1 || endCol != -1 )
							throw new Exception( "End position can only be specified once" );

						endRow = row;
						endCol = column;
					}
				}
			}

			if( startRow == -1 || startCol == -1 )
				throw new Exception( "No starting position specified" );

			if( endRow == -1 || endCol == -1 )
				throw new Exception( "No ending position specified" );
		}

		private List<Point> Traverse()
		{
			var queue = new Queue<Point>();

			queue.Enqueue( new Point( startCol, startRow, null ) );

			blocked[startCol, startRow] = true; // mark current position as true, to show algorithm's already been here
								// array is same size as maze grid
								// and is true where there is wall
								// or where the algorithm has already been

			while( queue.Count > 0 )
			{
				var curPoint = queue.Dequeue();

				// Check if at end
				if( curPoint.X == endCol && curPoint.Y == endRow )
					return curPoint.TracePath();

				// Check Left
				if( curPoint.X > 0 && !blocked[curPoint.X - 1, curPoint.Y] )
				{
					queue.Enqueue( new Point( curPoint.X - 1, curPoint.Y, curPoint ) );
					blocked[curPoint.X - 1, curPoint.Y] = true;
				}
				// Check Right
				if( curPoint.X < blocked.GetLength(0) && !blocked[curPoint.X + 1, curPoint.Y] )
				{
					queue.Enqueue( new Point( curPoint.X + 1, curPoint.Y, curPoint ) );
					blocked[curPoint.X + 1, curPoint.Y] = true;
				}
				// Check Above
				if( curPoint.Y > 0 && !blocked[curPoint.X, curPoint.Y - 1] )
				{
					queue.Enqueue( new Point( curPoint.X, curPoint.Y - 1, curPoint ) );
					blocked[curPoint.X, curPoint.Y - 1] = true;
				}
				// Check Below
				if( curPoint.Y < blocked.GetLength(1) - 1 && !blocked[curPoint.X, curPoint.Y + 1] )
				{
					queue.Enqueue( new Point( curPoint.X, curPoint.Y + 1, curPoint ) );
					blocked[curPoint.X, curPoint.Y + 1] = true;
				}
			}

			return null;
		}

		private MazeSolution RenderSolution( List<Point> path )
		{
			if( path == null )
				throw new Exception( "No possible path found!" );

			// Process the maze solver output
			foreach( var step in path )
				if( step != path.First() && step != path.Last() )   // don't replace 'A' or 'B' in map
					map[step.X, step.Y] = '@';

			var sb = new StringBuilder();

			for( int row = 0; row < height; row++ )
			{
				for( int column = 0; column < width; column++ )
					sb.Append( map[column, row] );

				if( row < height - 1 )
					sb.AppendLine();
			}

			return new MazeSolution( path.Count - 1, sb.ToString() );
		}

		#endregion
	}
}