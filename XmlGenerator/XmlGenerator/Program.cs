using XmlGenerator.BFSM;
using XmlGenerator.BFSM.GoalParser;

namespace XmlGenerator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using (var writer = new BfsmWriter("B.xml"))
            {
                writer.GoalSet.Circle("0", 10, 10);
                writer.State.GoToGoal("Walk", "unknown_path", "0");
                writer.Transition.SimpleContition("Walk", "Walk", "goal_reached");
            }

            new GoalParser(@"C:\Users\prut\Downloads\maze1\path.svg");
        }
    }
}