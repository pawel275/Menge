using System;
using System.Collections.Generic;

namespace XmlGenerator.BFSM
{
    public class Goal
    {
        private static int ID_COUNT = 1;

        public int Id { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Goal Next { get; set; }

        public HashSet<Goal> Adjacent { get; set; }

        public Goal(int x, int y)
        {
            X = x;
            Y = y;
            Adjacent = new HashSet<Goal>();
            Id = ID_COUNT++;
            Instances.Add(this);
        }

        public static List<Goal> Instances = new List<Goal>();

        public override string ToString()
        {
            return $"{Id}[{X}, {Y}]";
        }

        public bool Crosses(Goal g1, Goal g2)
        {
            int xMin = Math.Min(g1.X, g2.X),
                yMin = Math.Min(g1.Y, g2.Y),
                xMax = Math.Max(g1.X, g2.X),
                yMax = Math.Max(g1.Y, g2.Y);

            if (xMin == xMax)
            {
                return X == xMin && yMin < Y && yMax > Y;
            }

            if (yMin == yMax)
            {
                return Y == yMin && xMin < X && xMax > X;
            }

            throw new ArgumentException("Goals should have common x or y coordinate");
        }

        public static int Dist(Goal g1, Goal g2)
        {
            if (g1.X != g2.X && g1.Y != g2.Y)
            {
                throw new ArgumentException("Goals should have common x or y coordinate");
            }

            return Math.Abs(g1.X - g2.X) + Math.Abs(g1.Y - g2.Y);
        }

        public override bool Equals(object obj)
        {
            Goal g = obj as Goal;
            return this == g;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private static bool _AreEqual(Goal g1, Goal g2)
        {
            if (g1 is null ^ g2 is null)
            {
                return false;
            }

            if (g1 is null && g2 is null)
            {
                return true;
            }

            return g1.X == g2.X && g1.Y == g2.Y;
        }

        public static bool operator ==(Goal g1, Goal g2) => _AreEqual(g1, g2);

        public static bool operator !=(Goal g1, Goal g2) => !_AreEqual(g1, g2);
    }
}