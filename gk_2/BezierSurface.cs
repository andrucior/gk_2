using gk_2;
using System;
using System.Numerics;
using System.Windows.Forms;

public class BezierSurface
{
    public Vertex[,] controlPoints = new Vertex[4, 4]; 
    private int degreeU;
    private int degreeV;
    private int sign = 1;

    public BezierSurface(int degreeU, int degreeV)
    {
        this.degreeU = degreeU;
        this.degreeV = degreeV;
    }
    private float Bernstein(int i, int n, float u)
    {
        return BinomialCoefficient(n, i) * (float)Math.Pow(u, i) * (float)Math.Pow(1 - u, n - i);
    }
    private int BinomialCoefficient(int n, int i)
    {
        int result = 1;
        for (int k = 1; k <= i; k++)
        {
            result *= n - (i - k);
            result /= k;
        }
        return result;
    }
    public Vector3 GetSurfacePoint(float u, float v)
    {
        Vector3 point = Vector3.Zero;

        for (int i = 0; i < degreeU; i++)
        {
            for (int j = 0; j < degreeV; j++)
            {
                float Bu = Bernstein(i, degreeU, u);
                float Bv = Bernstein(j, degreeV, v);
                point += Bu * Bv * controlPoints[i, j].P_after;
            }
        }

        return point;
    }
    public Vector3 GetPartialDerivativeU(float u, float v)
    {
        Vector3 derivativeU = Vector3.Zero;

        for (int i = 0; i < degreeU; i++)
        {
            for (int j = 0; j < degreeV; j++)
            {
                float BuPrime = degreeU * (Bernstein(i, degreeU - 1, u) - Bernstein(i + 1, degreeU - 1, u));
                float Bv = Bernstein(j, degreeV, v);
                derivativeU += BuPrime * Bv * controlPoints[i, j].P_after;
            }
        }

        return derivativeU;
    }
    public Vector3 GetPartialDerivativeV(float u, float v)
    {
        Vector3 derivativeV = Vector3.Zero;

        for (int i = 0; i < degreeU; i++)
        {
            for (int j = 0; j < degreeV; j++)
            {
                float Bu = Bernstein(i, degreeU, u);
                float BvPrime = degreeV * (Bernstein(j, degreeV - 1, v) - Bernstein(j + 1, degreeV - 1, v));
                derivativeV += Bu * BvPrime * controlPoints[i, j].P_after;
            }
        }

        return derivativeV;
    }
    public Vector3 GetNormal(float u, float v)
    {
        Vector3 derivativeU = GetPartialDerivativeU(u, v);
        Vector3 derivativeV = GetPartialDerivativeV(u, v);
        Vector3 normal = Vector3.Cross(derivativeU, derivativeV);

        return Vector3.Normalize(normal);
    }
    
    public void ReadControlPoints()
    {
        using (StreamReader sr = new StreamReader(".\\dane.txt"))
        {
            string? line;
            string[] cords;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    line = sr.ReadLine();
                    cords = line.Split(',');
                    float x = Convert.ToSingle(cords[0]);
                    float y = Convert.ToSingle(cords[1]);
                    float z = Convert.ToSingle(cords[2]);
                    float u = Convert.ToSingle(cords[3]);   
                    float v = Convert.ToSingle(cords[4]);
                    controlPoints[i, j] = new Vertex(
                        new Vector3(x, y, z),  
                        Vector3.Zero,                       
                        Vector3.Zero,        
                        Vector3.Zero,                      
                        new Vector3(x, y, z), 
                        Vector3.Zero, 
                        Vector3.Zero,                      
                        Vector3.Zero,                        
                        u,                             
                        v                                    
                    );
                    
                }
            }
            for (int i = 0; i < 4;i++) 
            {
                for ( int j = 0;j < 4;j++)
                {
                    controlPoints[i, j].Pu_after = GetPartialDerivativeU(controlPoints[i, j].U, controlPoints[i, j].V);
                    controlPoints[i, j].Pv_after = GetPartialDerivativeV(controlPoints[i, j].U, controlPoints[i, j].V);
                    controlPoints[i, j].N_after = GetNormal(controlPoints[i, j].U, controlPoints[i, j].V);
                }
            }
        }
    }
    public void RotateControlPoints(float angle, bool isAlpha, ref PictureBox pictureBox, ref DirectBitmap normalMap)
    {
        Matrix3x3 matrix = new Matrix3x3();
        if (isAlpha)
        {
            matrix = Matrix3x3.CreateRotationZ(angle);
        }
        else
        {
            matrix = new Matrix3x3(
                new Vector3(1, 0, 0),
                new Vector3(0, MathF.Cos(angle), -MathF.Sin(angle)),
                new Vector3(0, MathF.Sin(angle), MathF.Cos(angle))
            );
        }
        foreach (var vertex in controlPoints)
        {
            vertex.P_before = vertex.P_after;
            vertex.Pu_before = vertex.Pu_after;
            vertex.Pv_before = vertex.Pv_after;
            vertex.N_before = vertex.N_after;
            vertex.P_after = matrix.Transform(vertex.P_before);
            vertex.Pu_after = GetPartialDerivativeU(vertex.U, vertex.V);
            vertex.Pv_after = GetPartialDerivativeV(vertex.U, vertex.V);
            vertex.N_after = GetNormal(vertex.U, vertex.V);
        }
        pictureBox.Invalidate();
    }
    public void WaveControlPoints(ref PictureBox pictureBox, int maxFlag)
    {
        if (maxFlag % 20 == 0)
            sign = -sign;

        // Do poprawy
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var x = controlPoints[i, j].P_after.X;
                var y = controlPoints[i, j].P_after.Y;
                var z = controlPoints[i, j].P_after.Z;

                var waveEffect = sign * Math.Sin((i + (i + 1) * j) * Math.PI / 15);
                var tmpSign = 1;
                
                if ((i + j) % 2 == 0)
                    tmpSign = -1;

                var newX = (float)(x + 5 * tmpSign * waveEffect);
                var newY = (float)(y + 5 * tmpSign * waveEffect);
                controlPoints[i, j].P_before = controlPoints[i, j].P_after;
                controlPoints[i, j].P_after = new Vector3(newX, newY, z);
            }
        }

        pictureBox.Invalidate();
    }


}
