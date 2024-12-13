using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace gk_2
{
    public class Vertex
    {
        // Position, tangent, and normal vectors before rotation
        public Vector3 P_before { get; set; }
        public Vector3 Pu_before { get; set; }
        public Vector3 Pv_before { get; set; }
        public Vector3 N_before { get; set; }

        // Position, tangent, and normal vectors after rotation
        public Vector3 P_after { get; set; }
        public Vector3 Pu_after { get; set; }
        public Vector3 Pv_after { get; set; }
        public Vector3 N_after { get; set; }

        // Parameters (u, v) for the vertex
        public float U { get; set; }
        public float V { get; set; }
        // Constructor
        public Vertex(Vector3 p_before, Vector3 pu_before, Vector3 pv_before, Vector3 n_before,
                      Vector3 p_after, Vector3 pu_after, Vector3 pv_after, Vector3 n_after, float u, float v)
        {
            P_before = p_before;
            Pu_before = pu_before;
            Pv_before = pv_before;
            N_before = n_before;

            P_after = p_after;
            Pu_after = pu_after;
            Pv_after = pv_after;
            N_after = n_after;

            U = u;
            V = v;
        }
    }
}
