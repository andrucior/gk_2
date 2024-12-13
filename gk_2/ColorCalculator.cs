using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace gk_2
{
    static internal class ColorCalculator
    {
        public static Color CalculateLighting(Vector3 normal, Vector3 L, Vector3 V, Color IL, Color IO, float kd, float ks, int m)
        {
            normal = Vector3.Normalize(normal);
            L = Vector3.Normalize(L);
            V = Vector3.Normalize(V);

            float cosNL = Math.Max(0, Vector3.Dot(normal, L));
            Vector3 R = 2 * cosNL * normal - L;
            R = Vector3.Normalize(R);
            float cosVR = Math.Max(0, Vector3.Dot(V, R));
            float specular = (float)Math.Pow(cosVR, m);

            float r = (kd * IL.R * IO.R * cosNL + ks * IL.R * IO.R * specular) / 255;
            float g = (kd * IL.G * IO.G * cosNL + ks * IL.G * IO.G * specular) / 255;
            float b = (kd * IL.B * IO.B * cosNL + ks * IL.B * IO.B * specular) / 255;

            r = Math.Min(255, Math.Max(0, r * 255));
            g = Math.Min(255, Math.Max(0, g * 255));
            b = Math.Min(255, Math.Max(0, b * 255));

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        public static Vector3 CalculateColor(Vector3 N, Vector3 lightColor, Vector3 objectColor, Vector3 lightDirection, Vector3 lightPosition, Vector3 viewDirection, float kd, float ks, float m, bool reflector, float ml)
        {
            Vector3 L = Vector3.Normalize(lightDirection);  
            Vector3 V = viewDirection;                      

            Color IL = Color.FromArgb((int)(lightColor.X), (int)(lightColor.Y), (int)(lightColor.Z));
            if (reflector)
            {
                lightPosition = Vector3.Normalize(lightPosition);
                var tmp = Math.Pow(Vector3.Dot(lightDirection, lightPosition), ml);
                tmp = Math.Abs(tmp);
                IL = Color.FromArgb((int)(IL.R * tmp), (int)(IL.G * tmp), (int)(IL.B * tmp));
            }
            Color IO = Color.FromArgb((int)(objectColor.X), (int)(objectColor.Y), (int)(objectColor.Z));  
            
            Color finalColor = CalculateLighting(N, L, V, IL, IO, kd, ks, (int)m);

            return new Vector3(finalColor.R / 255f, finalColor.G / 255f, finalColor.B / 255f);
        }
    }

}
