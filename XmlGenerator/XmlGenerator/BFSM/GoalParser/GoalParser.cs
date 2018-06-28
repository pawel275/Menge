using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XmlGenerator.BFSM.GoalParser
{
    public class GoalParser
    {
        private List<List<Goal>> polylines;

        public GoalParser(string filePath)
        {
            var xml = XDocument.Load(filePath);
            var solution = ReadGoals(xml.Root.Elements().First(x => x.Name.LocalName == "polyline")).ToList();
            solution.RemoveAt(0);
            solution.RemoveAt(solution.Count - 1);

            List<Goal> paths = InitializePaths(solution);

            // TODO: initialize polylines

            foreach (var path in paths)
            {
                LookUp(path);
            }
        }

        private IEnumerable<Goal> ReadGoals(XElement polyline)
        {
            return polyline
                .Attributes()
                .First(x => x.Name.LocalName == "points")
                .Value
                .Split()
                .Select(x =>
                {
                    var pos = x.Split(',').Select(p => int.Parse(p)).ToArray();
                    return new Goal(pos[0], pos[1]);
                });
        }

        private List<Goal> InitializePaths(List<Goal> solution)
        {
            List<Goal> paths = new List<Goal>(2);

            int halfWayLength = solution.Zip(solution.Skip(1), (a, b) => Goal.Dist(a, b)).Sum() / 2;
            int s = 0;
            for (int i = 1; i < solution.Count - 1; i++)
            {
                int oldS = s;
                s += Goal.Dist(solution[i - 1], solution[i]);

                solution[i].Next = s < halfWayLength ? solution[i - 1] : solution[i + 1];

                solution[i - 1].Adjacent.Add(solution[i]);
                solution[i + 1].Adjacent.Add(solution[i]);
                solution[i].Adjacent.Add(solution[i - 1]);
                solution[i].Adjacent.Add(solution[i + 1]);

                if (oldS < halfWayLength && s > halfWayLength) // passing half way point
                {
                    paths.Add(solution[i - 1]);
                    paths.Add(solution[i]);
                }
            }

            return paths;
        }

        private void Attach(List<Goal> polyline, Goal node)
        {
            for (int i = 1; i < polyline.Count - 1; i++)
            {
                polyline[i - 1].Adjacent.Add(polyline[i]);
                polyline[i + 1].Adjacent.Add(polyline[i]);
                polyline[i].Adjacent.Add(polyline[i - 1]);
                polyline[i].Adjacent.Add(polyline[i + 1]);

                polyline[i].Next = polyline[i + 1];
            }

            polyline[0].Next = polyline[1];

            LookUp(polyline.First());

            polyline.Last().Adjacent.Add(node);
            node.Adjacent.Add(polyline.Last());
            polyline.Last().Next = node;
        }

        private void LookUp(Goal path)
        {
            Goal current = path;
            while (current != null)
            {
                // paths leading to existing node
                var lastOnCurrent = polylines.Where(p => p.Last() == current);
                var firstOnCurrent = polylines.Where(p => p.First() == current);
                foreach (var p in firstOnCurrent)
                {
                    p.Reverse();
                }

                var nodeToAttach = current;
                var polylinesToAttach = lastOnCurrent.Concat(firstOnCurrent);

                foreach (var p in polylinesToAttach)
                {
                    polylines.Remove(p);
                    p.Remove(p.Last());
                }
                foreach (var p in polylinesToAttach)
                {
                    Attach(p, nodeToAttach);
                }

                // paths crossing current path
                if (current.Next != null)
                {
                    var crossingPath = polylines.FirstOrDefault(p => p.Last().Crosses(current, current.Next));

                    if (crossingPath == null)
                    {
                        crossingPath = polylines.FirstOrDefault(p => p.First().Crosses(current, current.Next));
                        if (crossingPath == null)
                        {
                            current = current.Next;
                            continue;
                        }
                        else
                        {
                            crossingPath.Reverse();
                        }
                    }

                    polylines.Remove(crossingPath);

                    var lastNode = crossingPath.Last();
                    crossingPath.Remove(lastNode);

                    lastNode.Next = current.Next;
                    current.Next = lastNode;

                    Attach(crossingPath, lastNode);
                }
                else
                {
                    current = current.Next;
                }
            }
        }
    }
}