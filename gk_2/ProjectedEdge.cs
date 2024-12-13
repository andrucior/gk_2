using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk_2
{
    internal class ProjectedEdge
    {
        public Point Start {  get; set; }
        public Point End { get; set; }
        public ProjectedEdge(Point start, Point end)
        {
            Start = start;
            End = end;
        }
        public bool IntersectsAtY(int y)
        {
            return (Start.Y <= y && End.Y > y) || (Start.Y > y && End.Y <= y);
        }

        // Calculate the intersection X-coordinate at a given Y-coordinate
        public int GetIntersectionXAtY(int y)
        {
            if (Start.Y == End.Y) return Start.X; // Horizontal line

            float t = (float)(y - Start.Y) / (End.Y - Start.Y);
            return (int)(Start.X + t * (End.X - Start.X));
        }
    }
}
