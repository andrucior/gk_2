using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;

namespace gk_2
{
    public class Triangle
    {
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }
        public Vertex Vertex3 { get; set; }

        public Vector3 Normal { get; private set; }
        // Constructor
        public Triangle(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }
        public bool ContainsVertex(Vertex vertex)
        {
            return Vertex1 == vertex || Vertex2 == vertex || Vertex3 == vertex;
        }
        public void CalculateNormal()
        {
            var edge1 = Vertex2.P_after - Vertex1.P_after;
            var edge2 = Vertex3.P_after - Vertex1.P_after;
            Normal = Vector3.Normalize(Vector3.Cross(edge1, edge2));
        }
    }
}

