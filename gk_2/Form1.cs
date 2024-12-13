using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace gk_2
{
    public partial class Form1 : Form
    {
        private BezierSurface? bezierSurface;
        private DirectBitmap? texture, normalMapBitmap, bitmap;
        private Mesh? mesh;
        private Color lightColor = Color.White;
        private float alpha, beta, lastAlpha = 0, lastBeta = 0;
        private int accuracy;
        private float kd, ks, m;
        private float lightAngle = 0f;
        private float lightRadius = 50f;
        private float lightZ = 100f;
        private bool isAnimating = false;
        private Vector3 lightDirection;
        private Vector3? objectColor;
        private System.Windows.Forms.Timer timer, timer2;
        private bool useMap = false;
        private Graphics? g;
        private int maxFlag = 0;
        private float ml;
        private bool reflector;
        public Form1()
        {
            // Falowanie
            // 

            float theta = 45f;
            float phi = 30f;
            float thetaRad = theta * (float)Math.PI / 180f;
            float phiRad = phi * (float)Math.PI / 180f;

            lightDirection = new Vector3(
                (float)(Math.Cos(phiRad) * Math.Sin(thetaRad)),
                (float)(Math.Sin(phiRad)),
                (float)(Math.Cos(phiRad) * Math.Cos(thetaRad))
            );

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            InitializeComponent();
            InitializeGraphics();
            accuracy = triangulationTrackBar.Value;
            InitializeBezierSurface();
            checkBox1.Checked = true;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (isAnimating)
            {
                lightAngle += (float)Math.PI / 30;


                lightRadius += 5f;
                lightRadius = lightRadius % 1000;
                float lightX = lightRadius * (float)Math.Cos(lightAngle);
                float lightY = lightRadius * (float)Math.Sin(lightAngle);

                lightDirection = new Vector3(lightX, lightY, lightZ);

                pictureBox1.Invalidate();
            }
        }
        private void Timer2_Tick(object? sender, EventArgs e)
        {
            maxFlag++;
            bezierSurface.WaveControlPoints(ref pictureBox1, maxFlag);
            UpdateMesh();
            pictureBox1.Invalidate();
        }

        private void InitializeBezierSurface()
        {
            bezierSurface = new BezierSurface(4, 4);
            bezierSurface.ReadControlPoints();
            UpdateMesh();
        }

        private void UpdateMesh()
        {
            List<Vertex> controlPointsList = bezierSurface.controlPoints.Cast<Vertex>().ToList();
            if (mesh == null)
                mesh = new Mesh(bezierSurface);

            mesh.Triangles = mesh.Triangulate(controlPointsList, accuracy);
            pictureBox1.Invalidate();
        }
        private void InitializeGraphics()
        {
            bitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap.Bitmap);
            g.ScaleTransform(1, -1);
            g.TranslateTransform(pictureBox1.Width / 2, -pictureBox1.Height / 2);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (bitmap == null || bitmap.Width != pictureBox1.Width || bitmap.Height != pictureBox1.Height)
            {
                bitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            }
            if (texture == null && objectColor == null)
            {
                Image image = Image.FromFile(".//xx.jpg");
                texture = new DirectBitmap(image);
            }
            g.Clear(Color.White);


            foreach (var triangle in mesh.Triangles)
            {
                DrawTriangle(g, triangle);
            }
            e.Graphics.DrawImage(bitmap.Bitmap, 0, 0);
        }
        private void DrawTriangle(Graphics g, Triangle triangle)
        {
            Point pt1 = Project3DTo2D(triangle.Vertex1.P_after);
            Point pt2 = Project3DTo2D(triangle.Vertex2.P_after);
            Point pt3 = Project3DTo2D(triangle.Vertex3.P_after);

            Vector3 viewDirection = new Vector3(0f, 0f, 1f);

            Vector3 lightColorVector = new Vector3(lightColor.R, lightColor.G, lightColor.B);

            if (!checkBox1.Checked)
            {
                var filler = new PolygonFiller(m, ks, kd, lightColorVector, lightDirection, objectColor, viewDirection);
                filler.normalMapBitmap = normalMapBitmap;
                filler.bezierSurface = bezierSurface;
                filler.M = new Matrix3x3(triangle.Vertex1.Pu_after, triangle.Vertex1.Pv_after, triangle.Vertex1.N_after);
                filler.lightDirection = lightDirection;

                if (texture == null)
                {
                    filler.FillPolygon(bitmap, new List<Vertex>() { triangle.Vertex1, triangle.Vertex2, triangle.Vertex3 }, pictureBox1.Width, pictureBox1.Height, useMap, reflector, ml);
                }
                else
                {
                    filler.FillPolygon(bitmap, texture, new List<Vertex>() { triangle.Vertex1, triangle.Vertex2, triangle.Vertex3 }, pictureBox1.Width, pictureBox1.Height, useMap, reflector, ml);
                }
            }
            else // (checkBox1.Checked)
            {
                using (Pen pen = new Pen(Color.Black, 1))
                {
                    lock (g)
                        g.DrawPolygon(pen, new[] { pt1, pt2, pt3 });
                }
            }
        }
        private void alphaTrackBar_Scroll(object sender, EventArgs e)
        {
            alpha = MathF.PI * alphaTrackBar.Value / 180f;
            RotateControlPoints(alpha - lastAlpha, true);
            lastAlpha = alpha;
        }
        private void betaTrackBar_Scroll(object sender, EventArgs e)
        {
            beta = MathF.PI * betaTrackBar.Value / 180f;
            RotateControlPoints(beta - lastBeta, false);
            lastBeta = beta;
        }

        private void RotateControlPoints(float angle, bool isAlpha)
        {
            bezierSurface.RotateControlPoints(angle, isAlpha, ref pictureBox1, ref normalMapBitmap);

            if (Math.Abs(angle) > 0.01f)
            {
                UpdateMesh();
            }
        }
        private Point Project3DTo2D(Vector3 point)
        {
            return new Point((int)(point.X), (int)(point.Y));
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void colorButton_Click(object sender, EventArgs e)
        {
            var myDialog = new ColorDialog();
            myDialog.Color = lightColor;
            myDialog.AllowFullOpen = false;
            myDialog.ShowHelp = true;
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                lightColor = myDialog.Color;
            }
            pictureBox1.Invalidate();
        }
        private void triangulationTrackBar_Scroll(object sender, EventArgs e)
        {
            accuracy = triangulationTrackBar.Value;
            UpdateMesh();
        }
        private void kdTrackBar_Scroll(object sender, EventArgs e)
        {
            kd = (float)kdTrackBar.Value / 10;
            pictureBox1.Invalidate();
        }
        private void mTrackBar_Scroll(object sender, EventArgs e)
        {
            m = mTrackBar.Value;
            pictureBox1.Invalidate();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ks = (float)ksTrackBar.Value / 10;
            pictureBox1.Invalidate();
        }
        private void animationTrackBar_Scroll(object sender, EventArgs e)
        {
            lightZ = animationTrackBar.Value;
            pictureBox1.Invalidate();
        }
        private void animationRadioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            isAnimating = animationRadioButton.Checked;
            timer.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "c:\\";

            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                texture = new DirectBitmap(Image.FromFile(fileDialog.FileName));
                objectColor = null;
            }
            pictureBox1.Invalidate();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var myDialog = new ColorDialog();
            myDialog.Color = lightColor;
            myDialog.AllowFullOpen = false;
            myDialog.ShowHelp = true;
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                objectColor = new Vector3(myDialog.Color.R, myDialog.Color.G, myDialog.Color.B);
                texture = null;
            }
            pictureBox1.Invalidate();
        }
        private void normalVectorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useMap = normalVectorCheckBox.Checked;
            pictureBox1.Invalidate();
        }
        private void normalMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(fileDialog.FileName);
                normalMapBitmap = new DirectBitmap(image);
            }
        }

        private void waveCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 50;
            timer2.Tick += Timer2_Tick;
            timer2.Start();
            maxFlag = 0;
        }

        private void mlTrackBar_Scroll(object sender, EventArgs e)
        {
            ml = mlTrackBar.Value;
            pictureBox1.Invalidate();
        }

        private void reflectorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            reflector = reflectorCheckBox.Checked;
            pictureBox1.Invalidate();
        }
    }
}
