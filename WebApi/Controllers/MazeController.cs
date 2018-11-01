using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using PeopleNet.MazeSolver.Models;

namespace PeopleNet.MazeSolver.Controllers
{
    public class SolveMazeController : System.Web.Http.ApiController
	{
		// POST /SolveMaze
		[HttpPost]
		public async Task<MazeSolution> Default()
		{
			var maze = await Request.Content.ReadAsStringAsync();

			try
			{
				var solver = new Solver( maze );

				return solver.Solve();

			}
			catch( Exception ex )
			{
				return new MazeSolution( 0, ex.Message );
			}
		}
	}
}
