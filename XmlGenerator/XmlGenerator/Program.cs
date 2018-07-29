using XmlGenerator.BFSM;
using XmlGenerator.Scene;

namespace XmlGenerator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            double dx = -162;
            double dy = -162;
            double scale = .1;
 
            using (var writer = new BfsmWriter("B.xml"))
            {
                writer.GoalSet.Parsed(args[0], "0", 16, scale, dx, dy);
                writer.State.GoToGoal("Walk", "known_path", "0");
                writer.Transition.Simple("Walk", "Walk", "goal_reached");
            }

            using (var writer = new SceneWriter("S.xml"))
            {
                writer.AgentProfile.Default("group1", "1", "1");
                //writer.AgentGroup.Single(0, 0, "group1", "Walk");
                double d = 150;
                writer.AgentGroup.RandomInArea(-d * scale, -d * scale, d * scale, d * scale, 20, "group1", "Walk");
                writer.ObstacleSet.Parsed(args[1], "1", scale, dx, dy);
            }
        }
    }
}