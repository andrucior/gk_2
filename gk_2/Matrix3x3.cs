using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace gk_2
{
    public struct Matrix3x3
    {
        // Each row of the matrix
        public Vector3 Row1;
        public Vector3 Row2;
        public Vector3 Row3;

        // Constructor
        public Matrix3x3(Vector3 row1, Vector3 row2, Vector3 row3)
        {
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public static Matrix3x3 Multiply(Matrix3x3 a, Matrix3x3 b)
        {
            return new Matrix3x3(
                new Vector3(
                    a.Row1.X * b.Row1.X + a.Row1.Y * b.Row2.X + a.Row1.Z * b.Row3.X,
                    a.Row1.X * b.Row1.Y + a.Row1.Y * b.Row2.Y + a.Row1.Z * b.Row3.Y,
                    a.Row1.X * b.Row1.Z + a.Row1.Y * b.Row2.Z + a.Row1.Z * b.Row3.Z
                ),
                new Vector3(
                    a.Row2.X * b.Row1.X + a.Row2.Y * b.Row2.X + a.Row2.Z * b.Row3.X,
                    a.Row2.X * b.Row1.Y + a.Row2.Y * b.Row2.Y + a.Row2.Z * b.Row3.Y,
                    a.Row2.X * b.Row1.Z + a.Row2.Y * b.Row2.Z + a.Row2.Z * b.Row3.Z
                ),
                new Vector3(
                    a.Row3.X * b.Row1.X + a.Row3.Y * b.Row2.X + a.Row3.Z * b.Row3.X,
                    a.Row3.X * b.Row1.Y + a.Row3.Y * b.Row2.Y + a.Row3.Z * b.Row3.Y,
                    a.Row3.X * b.Row1.Z + a.Row3.Y * b.Row2.Z + a.Row3.Z * b.Row3.Z
                )
            );
        }

        public Vector3 Transform(Vector3 vector)
        {
            return new Vector3(
                Row1.X * vector.X + Row1.Y * vector.Y + Row1.Z * vector.Z,
                Row2.X * vector.X + Row2.Y * vector.Y + Row2.Z * vector.Z,
                Row3.X * vector.X + Row3.Y * vector.Y + Row3.Z * vector.Z
            );
        }

        public float Determinant()
        {
            return Row1.X * (Row2.Y * Row3.Z - Row2.Z * Row3.Y)
                 - Row1.Y * (Row2.X * Row3.Z - Row2.Z * Row3.X)
                 + Row1.Z * (Row2.X * Row3.Y - Row2.Y * Row3.X);
        }

        public static Matrix3x3 CreateRotationZ(float radians)
        {
            float cos = MathF.Cos(radians);
            float sin = MathF.Sin(radians);
            return new Matrix3x3(
                new Vector3(cos, -sin, 0),
                new Vector3(sin, cos, 0),
                new Vector3(0, 0, 1)
            );
        }

    }
}
