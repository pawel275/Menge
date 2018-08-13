using System.Collections.Generic;
using System.IO;
using XmlGenerator.BFSM;

namespace XmlGenerator.Map
{
    public static class MapWriter
    {
        public static void WriteMap(string filePath, IList<Goal> goals, double scale = 1)
        {
            var vertices = _CreateVertices(goals);

            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine(goals.Count);

                int edgeCount = 0;
                for (int i = 0; i < goals.Count; i++)
                {
                    int e = _CountEdges(vertices, i);
                    edgeCount += e;
                    file.WriteLine($"{e} {goals[i].X * scale} {goals[i].Y * scale}".Replace(',', '.'));
                }
                edgeCount /= 2;

                file.WriteLine(edgeCount);

                for (int i = 0; i < goals.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (vertices[i, j])
                        {
                            file.WriteLine($"{i} {j}");
                        }
                    }
                }
            }
        }

        private static bool[,] _CreateVertices(IList<Goal> goals)
        {
            bool[,] edges = new bool[goals.Count, goals.Count];
            foreach (var g1 in goals)
            {
                foreach (var g2 in g1.Adjacent)
                {
                    edges[goals.IndexOf(g1), goals.IndexOf(g2)] = true;
                }
            }
            return edges;
        }

        private static int _CountEdges(bool[,] vertices, int idx)
        {
            int count = 0;
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                if (vertices[idx, i])
                {
                    ++count;
                }
            }
            return count;
        }
    }
}