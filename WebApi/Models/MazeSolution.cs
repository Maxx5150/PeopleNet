namespace PeopleNet.MazeSolver.Models
{
	/// <summary>
	/// This class contains the solution for a maze traversal.
	/// If the maze cannot be traversed, the "steps" counter is set to zero
	/// and the "solution" string contains an explanation.
	/// </summary>
	public class MazeSolution
	{
		public int steps { get; private set; }
		public string solution { get; private set; }

		public MazeSolution( int steps, string solution )
		{
			this.steps = steps;
			this.solution = solution;
		}
	}
}