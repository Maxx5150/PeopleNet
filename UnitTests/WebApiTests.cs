using System.IO;
using System.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeopleNet.MazeSolver.Models;

namespace PeopleNet.MazeSolver.UnitTests
{
	[TestClass]
	public class WebApiTests
	{
		[TestMethod]
		public void SolveMaze1()
		{
			var maze = File.ReadAllText( "../../TestFiles/maze1.txt" );

			using( var webClient = new WebClient() )
			{
				var solver = new Solver( maze );

				var mazeSolution = solver.Solve();

				Assert.AreEqual( mazeSolution.steps, 14 );

				var expectedSolution = File.ReadAllText( "../../TestFiles/Solution1.txt" );

				Assert.AreEqual( mazeSolution.solution.Replace( "\r", string.Empty ), expectedSolution );
			}
		}

		[TestMethod]
		public void SolveMaze2()
		{
			var maze = File.ReadAllText( "../../TestFiles/maze2.txt" );

			using( var webClient = new WebClient() )
			{
				var solver = new Solver( maze );

				var mazeSolution = solver.Solve();

				Assert.AreEqual( mazeSolution.steps, 219 );

				var expectedSolution = File.ReadAllText( "../../TestFiles/Solution2.txt" );

				Assert.AreEqual( mazeSolution.solution.Replace( "\r", string.Empty ), expectedSolution );
			}
		}
		[TestMethod]
		public void SolveMaze3()
		{
			var maze = File.ReadAllText( "../../TestFiles/maze3.txt" );

			using( var webClient = new WebClient() )
			{
				var solver = new Solver( maze );

				var mazeSolution = solver.Solve();

				Assert.AreEqual( mazeSolution.steps, 302 );

				var expectedSolution = File.ReadAllText( "../../TestFiles/Solution3.txt" );

				Assert.AreEqual( mazeSolution.solution.Replace( "\r", string.Empty ), expectedSolution );
			}
		}
	}
}
