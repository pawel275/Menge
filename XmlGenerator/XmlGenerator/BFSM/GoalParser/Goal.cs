using System;
using System.Collections.Generic;

namespace XmlGenerator.BFSM.GoalParser
{
    public class Goal
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Goal Next { get; set; }

        public List<Goal> Adjacent { get; set; }

        public Goal(int x, int y)
        {
            X = x;
            Y = y;
            Adjacent = new List<Goal>();
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public bool Crosses(Goal g1, Goal g2)
        {
            int xMin = Math.Min(g1.X, g2.X),
                yMin = Math.Min(g1.Y, g2.Y),
                xMax = Math.Max(g1.X, g2.X),
                yMax = Math.Max(g1.Y, g2.Y);
            return xMin < X && xMax > X || yMin < Y && yMax > Y;
        }

        public static int Dist(Goal g1, Goal g2)
        {
            if (g1.X != g2.X && g1.Y != g2.Y)
            {
                throw new ArgumentException("Goals are not orthogonal");
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

        public static bool operator ==(Goal g1, Goal g2) => g1.X == g2.X && g1.Y == g2.Y;

        public static bool operator !=(Goal g1, Goal g2) => g1.X != g2.X || g1.Y != g2.Y;
    }
}