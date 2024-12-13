using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace gk_2
{
    public class Mesh
    {
        public List<Triangle> Triangles { get; set; } = new List<Triangle>();
        public Vertex[,] ControlPoints { get; set; }
        public void AddTriangle(Triangle triangle)
        {
            Triangles.Add(triangle);
        }
        public Mesh(BezierSurface bezierSurface)
        {
            ControlPoints = bezierSurface.controlPoints;
        }
        public List<Triangle> Triangulate(List<Vertex> points, int resolution)
        {
            var mesh = new List<Triangle>();
            float step = 1.0f / resolution;

            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    float u0 = i * step;
                    float v0 = j * step;
                    float u1 = (i + 1) * step;
                    float v1 = (j + 1) * step;

                    Vertex p0 = ComputeBezierSurfacePoint(u0, v0);
                    Vertex p1 = ComputeBezierSurfacePoint(u1, v0);
                    Vertex p2 = ComputeBezierSurfacePoint(u0, v1);
                    Vertex p3 = ComputeBezierSurfacePoint(u1, v1);

                    mesh.Add(new Triangle(p0, p1, p3));
                    mesh.Add(new Triangle(p0, p3, p2));
                }
            }

            return mesh;
        }
        public Vertex ComputeBezierSurfacePoint(float u, float v)
        {
            Vector3 position = Vector3.Zero;
            Vector3 tangentU = Vector3.Zero; 
            Vector3 tangentV = Vector3.Zero; 

            float[] Bu = new float[4];
            float[] Bv = new float[4];
            float[] Bu_deriv = new float[4];
            float[] Bv_deriv = new float[4];

            Bu[0] = (1 - u) * (1 - u) * (1 - u);
            Bu[1] = 3 * u * (1 - u) * (1 - u);
            Bu[2] = 3 * u * u * (1 - u);
            Bu[3] = u * u * u;

            Bv[0] = (1 - v) * (1 - v) * (1 - v);
            Bv[1] = 3 * v * (1 - v) * (1 - v);
            Bv[2] = 3 * v * v * (1 - v);
            Bv[3] = v * v * v;

            Bu_deriv[0] = -3 * (1 - u) * (1 - u);
            Bu_deriv[1] = 3 * (1 - u) * (1 - 3 * u);
            Bu_deriv[2] = 3 * u * (2 - 3 * u);
            Bu_deriv[3] = 3 * u * u;

            Bv_deriv[0] = -3 * (1 - v) * (1 - v);
            Bv_deriv[1] = 3 * (1 - v) * (1 - 3 * v);
            Bv_deriv[2] = 3 * v * (2 - 3 * v);
            Bv_deriv[3] = 3 * v * v;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector3 controlPointPos = ControlPoints[i, j].P_before;
                    float basisU = Bu[i];
                    float basisV = Bv[j];
                    float basisU_deriv = Bu_deriv[i];
                    float basisV_deriv = Bv_deriv[j];

                    position += basisU * basisV * controlPointPos;

                    tangentU += basisU_deriv * basisV * controlPointPos;

                    tangentV += basisU * basisV_deriv * controlPointPos;
                }
            }

            Vector3 normal = Vector3.Cross(tangentU, tangentV);
            normal = Vector3.Normalize(normal);

            return new Vertex(
                position,                // P_before
                tangentU,                // Pu_before
                tangentV,                // Pv_before
                normal,                  // N_before
                position,                // P_after
                tangentU,                // Pu_after
                tangentV,                // Pv_after
                normal,                  // N_after
                u,                       // U parameter
                v                        // V parameter
            );
        }
    }
}
