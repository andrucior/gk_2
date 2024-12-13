using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace gk_2
{
    internal class Edge
    {
        public Vertex P1 { get; }
        public Vertex P2 { get; }

        public Edge(Vertex p1, Vertex p2)
        {
            P1 = p1;
            P2 = p2;
        }

    }
}
