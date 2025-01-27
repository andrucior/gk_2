using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk_2
{
    public class Tetrahedron
    {
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }
        public Vertex Vertex3 { get; set; }
        public Vertex Vertex4 {  get; set; }
        public Triangle Triangle1 { get; set; }
        public Triangle Triangle2 { get; set; }
        public Triangle Triangle3 { get; set; }
        public Triangle Triangle4 { get; set; }
        public List<Triangle> Triangles { get; set; }
        public List<Vertex> Vertices { get; set; }
        public void CalculateTriangles()
        {
            Triangle1 = new Triangle(Vertex1, Vertex2, Vertex3);
            Triangle2 = new Triangle(Vertex1, Vertex2, Vertex4);
            Triangle3 = new Triangle(Vertex2, Vertex3, Vertex4);
            Triangle4 = new Triangle(Vertex1, Vertex3, Vertex4);
            Triangles = new List<Triangle> { Triangle1, Triangle2, Triangle3, Triangle4 };
            Vertices = new List<Vertex> { Vertex1,  Vertex2, Vertex3, Vertex4 };
        }
    }
}
