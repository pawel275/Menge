using System.Collections.Generic;
using System.Linq;
using XmlGenerator.BFSM;
using XmlGenerator.Scene;

namespace XmlGenerator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            double dx = 0;
            double dy = 0;
            double scale = .1;
            double size = 16;
            double goAwayDist = 60;

            var goalParser = new GoalParser(args[0]);
            var paths = goalParser.ParsePaths();

            var outGoals = _FindEnds(paths);
            IEnumerable<Goal> goals;

            using (var writer = new BfsmWriter("B.xml"))
            {
                goals = writer.GoalSet.Parsed(paths, 0, size, scale, dx, dy);

                using (var gw = writer.GoalSet.Explicit(1))
                {
                    var sorted = outGoals.OrderBy(g => g.Y);
                    var bottom = sorted.First();
                    var top = sorted.Last();

                    gw.Goal(1, bottom.X + dx, bottom.Y + dy - goAwayDist, size: size * 2, scale: scale);
                    gw.Goal(2, top.X + dx, top.Y + dy + goAwayDist, size: size * 2, scale: scale);
                }

                writer.State.GoToGoal("Walk", "known_path", 0);
                writer.State.GoToGoal("Out", "identity");
                writer.State.GoToGoal("GoAway", "nearest", 1, true);

                foreach (var outGoal in outGoals)
                {
                    writer.Transition.AABB("Walk", "Out", Utils.BoxPosition(outGoal.X + dx, outGoal.Y + dy, size, scale), true);
                }

                writer.Transition.Simple("Out", "GoAway", "auto");
                writer.Transition.Simple("Walk", "Walk", "goal_reached");
            }

            using (var writer = new SceneWriter("S.xml"))
            {
                writer.AgentProfile.Default("group1", "1", "1");
                //writer.AgentGroup.Single(0, 0, "group1", "Walk");
                var lines = writer.ObstacleSet.Parsed(args[1], "1", scale, dx, dy);
                writer.AgentGroup.Random(lines, goals, scale, 40, "group1", "Walk");
            }
        }

        private static Goal[] _FindEnds(List<Goal> paths)
        {
            var ends = new[] { paths[0], paths[1] };

            for (int i = 0; i < ends.Length; i++)
            {
                while (ends[i].Next != null)
                {
                    ends[i] = ends[i].Next;
                }
            }

            return ends;
        }
    }
}