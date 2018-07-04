using XmlGenerator.BFSM;
using XmlGenerator.Scene;

namespace XmlGenerator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using (var writer = new BfsmWriter("B.xml"))
            {
                writer.GoalSet.Parsed(args[0], "0", .5, .3);
                //writer.GoalSet.Circle("0", 10, 10);
                writer.State.GoToGoal("Walk", "known_path", "0");
                writer.Transition.SimpleContition("Walk", "Walk", "goal_reached");
            }

            using (var writer = new SceneWriter("S.xml"))
            {
                writer.AgentProfile.Default("group1", "1", "1");
                writer.AgentGroup.Single("50", "50", "group1", "Walk");
                writer.ObstacleSet.Parsed(args[1], "1", .3);
            }
        }
    }
}