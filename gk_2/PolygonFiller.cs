using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace gk_2
{
    public class PolygonFiller
    {
        private float kd, ks, m;
        public int[,] depth {  get; set; }
        public Vector3? lightColorVector, lightDirection, objectColor, viewDirection;
        public DirectBitmap? normalMapBitmap {  get; set; }
        public BezierSurface? bezierSurface { get; set; }
        public Matrix3x3 M {  get; set; }
        private Color[,]? normalMapData;
        private ConcurrentBag<(int, int, Color)> pixelUpdates = new ConcurrentBag<(int, int, Color)>();
        public PolygonFiller(float m, float ks, float kd, Vector3 lightColorVector, Vector3 lightDirection, Vector3? objectColor, Vector3 viewDirection)
        {
            this.kd = kd;
            this.ks = ks;
            this.m = m;
            this.lightColorVector = lightColorVector;
            this.lightDirection = lightDirection;
            this.objectColor = objectColor;
            this.viewDirection = viewDirection;

        }
        private Vector3 PrecomputedBarycentric(Point p1, Point p2, Point p3, Point p)
        {
            float denominator = (p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y);
            float a = ((p2.Y - p3.Y) * (p.X - p3.X) + (p3.X - p2.X) * (p.Y - p3.Y)) / denominator;
            float b = ((p3.Y - p1.Y) * (p.X - p3.X) + (p1.X - p3.X) * (p.Y - p3.Y)) / denominator;
            float c = 1 - a - b;
            return new Vector3(a, b, c);
        }
        public void LoadNormalMap(DirectBitmap normalMapBitmap)
        {
            normalMapData = new Color[normalMapBitmap.Width, normalMapBitmap.Height];
            for (int x = 0; x < normalMapBitmap.Width; x++)
            {
                for (int y = 0; y < normalMapBitmap.Height; y++)
                {
                    normalMapData[x, y] = normalMapBitmap.GetPixel(x, y);
                }
            }
        }
        public void FillPolygon(DirectBitmap bitmap, List<Vertex> polygon3D, int offsetX, int offsetY, bool modifiedNormalVector, bool reflector, float ml)
        {
            List<Point> polygon2D = new List<Point>();
            if (modifiedNormalVector)
            {
                LoadNormalMap(normalMapBitmap);
            }
            foreach (var vertex in polygon3D)
            {
                Point projectedPoint = Project3DTo2D(vertex.P_after);
                polygon2D.Add(projectedPoint);
            }

            List<ProjectedEdge> edges = GetEdges(polygon2D);

            edges.Sort((e1, e2) => e1.Start.Y.CompareTo(e2.Start.Y));

            int minY = polygon2D[0].Y, maxY = polygon2D[0].Y;
            foreach (var p in polygon2D)
            {
                minY = Math.Min(minY, p.Y);
                maxY = Math.Max(maxY, p.Y);
            }

            Parallel.For(minY, maxY + 1, y =>
            {
                List<int> intersections = new List<int>();

                foreach (var edge in edges)
                {
                    if (edge.IntersectsAtY(y))
                    {
                        int intersectionX = edge.GetIntersectionXAtY(y);
                        intersections.Add(intersectionX);
                    }
                }
                BucketSort(intersections);

                for (int i = 0; i < intersections.Count; i += 2)
                {
                    int startX = intersections[i];
                    int endX = intersections[i + 1];

                    for (int x = startX; x <= endX; x++)
                    {
                        Vector3 barycentricCoord = PrecomputedBarycentric(polygon2D[0], polygon2D[1], polygon2D[2], new Point(x, y));
                        if (barycentricCoord.X >= 0 && barycentricCoord.Y >= 0 && barycentricCoord.Z >= 0)
                        {
                            Vector3 normal;
                            float u = polygon3D[0].U * barycentricCoord.X + polygon3D[1].U * barycentricCoord.Y + polygon3D[2].U * barycentricCoord.Z;
                            float v = polygon3D[0].V * barycentricCoord.X + polygon3D[1].V * barycentricCoord.Y + polygon3D[2].V * barycentricCoord.Z;

                            if (!modifiedNormalVector)
                            {
                                normal = polygon3D[0].N_after * barycentricCoord.X +
                                                 polygon3D[1].N_after * barycentricCoord.Y +
                                                 polygon3D[2].N_after * barycentricCoord.Z;

                            }
                            else
                            {
                                normal = GetModifiedNormal(u, v, polygon3D, barycentricCoord);
                            }
                            Vector3 position = polygon3D[0].P_after * barycentricCoord.X +
                                      polygon3D[1].P_after * barycentricCoord.Y +
                                      polygon3D[2].P_after * barycentricCoord.Z;

                            Vector3 toLight = Vector3.Normalize((Vector3)lightDirection - position);
                            normal = Vector3.Normalize(normal);

                            Vector3 color = ColorCalculator.CalculateColor(normal, (Vector3)lightColorVector, (Vector3)objectColor, (Vector3)toLight, (Vector3)lightDirection, (Vector3)viewDirection, kd, ks, (int)m, reflector, ml);

                            int red = Math.Clamp((int)(color.X * 255), 0, 255);
                            int green = Math.Clamp((int)(color.Y * 255), 0, 255);
                            int blue = Math.Clamp((int)(color.Z * 255), 0, 255);

                            bitmap.SetPixel(x + offsetX / 2, -y + offsetY / 2, Color.FromArgb(red, green, blue));
                        }
                    }
                }
            });
        }

        public void FillPolygon(DirectBitmap bitmap, DirectBitmap texture, List<Vertex> polygon3D, int offsetX, int offsetY, bool modifiedNormalVector, bool reflector, float ml, bool triangle)
        {
            List<Point> polygon2D = new List<Point>();
            foreach (var vertex in polygon3D)
            {
                Point projectedPoint = Project3DTo2D(vertex.P_after);
                polygon2D.Add(projectedPoint);
            }

            List<ProjectedEdge> edges = GetEdges(polygon2D);
            edges.Sort((e1, e2) => e1.Start.Y.CompareTo(e2.Start.Y));

            int minY = polygon2D.Min(p => p.Y);
            int maxY = polygon2D.Max(p => p.Y);
            if (modifiedNormalVector)
            {
                LoadNormalMap(normalMapBitmap);
            }

            Parallel.For(minY, maxY + 1, y =>
            {
                List<int> intersections = new List<int>();
                foreach (var edge in edges)
                {
                    if (edge.IntersectsAtY(y))
                    {
                        int intersectionX = edge.GetIntersectionXAtY(y);
                        intersections.Add(intersectionX);
                    }
                }

                BucketSort(intersections);

                for (int i = 0; i < intersections.Count; i += 2)
                {
                    int startX = intersections[i];
                    int endX = intersections[i + 1];

                    for (int x = startX; x <= endX; x++)
                    {
                        Vector3 barycentricCoord = PrecomputedBarycentric(polygon2D[0], polygon2D[1], polygon2D[2], new Point(x, y));
                        int z = (int)(polygon3D[0].P_after.Z * barycentricCoord.X + polygon3D[1].P_after.Z * barycentricCoord.Y + polygon3D[2].P_after.Z * barycentricCoord.Z);

                        if (((z < depth[x + offsetX / 2, -y + offsetY / 2] && triangle) || !triangle)  && barycentricCoord.X >= 0 && barycentricCoord.Y >= 0 && barycentricCoord.Z >= 0)
                        {
                            depth[x + offsetX / 2, -y + offsetY / 2] = z;

                            float u = polygon3D[0].U * barycentricCoord.X + polygon3D[1].U * barycentricCoord.Y + polygon3D[2].U * barycentricCoord.Z;
                            float v = polygon3D[0].V * barycentricCoord.X + polygon3D[1].V * barycentricCoord.Y + polygon3D[2].V * barycentricCoord.Z;

                            int texX = (int)(u * (texture.Width - 1));
                            int texY = (int)(v * (texture.Height - 1));
                            Color textureColor;
                            if (!triangle)
                                textureColor = texture.GetPixel(texX, texY);
                            else
                                textureColor = Color.FromArgb((int)objectColor.Value.X, (int)objectColor.Value.Y, (int)objectColor.Value.Z);
                            Vector3 normal;
                            if (!modifiedNormalVector)
                            {
                                normal = polygon3D[0].N_after * barycentricCoord.X +
                                         polygon3D[1].N_after * barycentricCoord.Y +
                                         polygon3D[2].N_after * barycentricCoord.Z;

                                normal = Vector3.Normalize(normal);
                            }
                            else
                            {
                                normal = GetModifiedNormal(u, v, polygon3D, barycentricCoord);
                            }


                            Vector3 position = polygon3D[0].P_after * barycentricCoord.X +
                                      polygon3D[1].P_after * barycentricCoord.Y +
                                      polygon3D[2].P_after * barycentricCoord.Z;

                            Vector3 toLight = Vector3.Normalize((Vector3)lightDirection - position);
                            Vector3 colorVector = new Vector3(textureColor.R, textureColor.G, textureColor.B);
                            Vector3 color = ColorCalculator.CalculateColor(normal, (Vector3)lightColorVector, colorVector,
                                toLight, (Vector3)lightDirection, (Vector3)viewDirection, kd, ks, (int)m, reflector, ml);

                            int red = Math.Clamp((int)(color.X * 255), 0, 255);
                            int green = Math.Clamp((int)(color.Y * 255), 0, 255);
                            int blue = Math.Clamp((int)(color.Z * 255), 0, 255);
                            bitmap.SetPixel(x + offsetX / 2, -y + offsetY / 2, Color.FromArgb(red, green, blue));

                        }
                    }
                }
            });
        }
        private int FindZ()
        {
            throw new NotImplementedException();
        }

        private Vector3 GetModifiedNormal(float u, float v, List<Vertex> polygon3D, Vector3 barycentricCoord)
        {
            float texX = u * (normalMapData.GetLength(0) - 1);
            float texY = v * (normalMapData.GetLength(1) - 1);

            int x0 = (int)texX;
            int y0 = (int)texY;

            float dx = texX - x0;
            float dy = texY - y0;

            Color color00 = normalMapData[x0, y0];
            Color color10 = normalMapData[Math.Min(x0 + 1, normalMapData.GetLength(0) - 1), y0];
            Color color01 = normalMapData[x0, Math.Min(y0 + 1, normalMapData.GetLength(1) - 1)];
            Color color11 = normalMapData[Math.Min(x0 + 1, normalMapData.GetLength(0) - 1), Math.Min(y0 + 1, normalMapData.GetLength(1) - 1)];

            float r0 = (1 - dx) * color00.R + dx * color10.R;
            float r1 = (1 - dx) * color01.R + dx * color11.R;
            float r = (1 - dy) * r0 + dy * r1;

            float g0 = (1 - dx) * color00.G + dx * color10.G;
            float g1 = (1 - dx) * color01.G + dx * color11.G;
            float g = (1 - dy) * g0 + dy * g1;

            float b0 = (1 - dx) * color00.B + dx * color10.B;
            float b1 = (1 - dx) * color01.B + dx * color11.B;
            float b = (1 - dy) * b0 + dy * b1;

            float nx = (r / 255f) * 2 - 1;
            float ny = (g / 255f) * 2 - 1; 
            float nz = b / 255f;  

            Vector3 nTexture = new Vector3(nx, ny, nz);

            Vector3 pu = barycentricCoord.X * polygon3D[0].Pu_after + barycentricCoord.Y * polygon3D[1].Pu_after + barycentricCoord.Z * polygon3D[2].Pu_after;
            Vector3 pv = barycentricCoord.X * polygon3D[0].Pv_after + barycentricCoord.Y * polygon3D[1].Pv_after + barycentricCoord.Z * polygon3D[2].Pv_after;
            Vector3 n = barycentricCoord.X * polygon3D[0].N_after + barycentricCoord.Y * polygon3D[1].N_after + barycentricCoord.Z * polygon3D[2].N_after;

            Matrix3x3 M_pixel = new Matrix3x3(pu, pv, n);

            return -Vector3.Normalize(M_pixel.Transform(nTexture));
        }
        private Point Project3DTo2D(Vector3 position)
        {
            return new Point((int)position.X, (int)position.Y);
        }

        private void BucketSort(List<int> intersections)
        {
            if (intersections.Count <= 1) return;

            int minX = int.MaxValue;
            int maxX = int.MinValue;

            foreach (var x in intersections)
            {
                minX = Math.Min(minX, x);
                maxX = Math.Max(maxX, x);
            }

            int bucketCount = maxX - minX + 1;
            List<List<int>> buckets = new List<List<int>>(bucketCount);
            for (int i = 0; i < bucketCount; i++)
            {
                buckets.Add(new List<int>());
            }

            foreach (var x in intersections)
            {
                buckets[x - minX].Add(x);
            }

            intersections.Clear();
            foreach (var bucket in buckets)
            {
                foreach (var x in bucket)
                {
                    intersections.Add(x);
                }
            }
        }

        private List<ProjectedEdge> GetEdges(List<Point> polygon)
        {
            List<ProjectedEdge> edges = new List<ProjectedEdge>();

            for (int i = 0; i < polygon.Count; i++)
            {
                Point p1 = polygon[i];
                Point p2 = polygon[(i + 1) % polygon.Count]; 

                edges.Add(new ProjectedEdge(p1, p2));
            }

            return edges;
        }
    }
}







