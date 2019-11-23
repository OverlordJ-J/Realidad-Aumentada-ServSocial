using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;


namespace CurveBProyecto
{
    public partial class Form1 : Form
    {

        double xmax = 800.0, ymax = 800.0, zmax = 800.0; //Tamaño del mundo
        double alpha = 30.0, betha = -30.0, gamma = 0.0; //Angulos de rotacion por eje
        int Fila=0, Columna=0;

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Z:
                    alpha += 1;
                    break;
                case Keys.A:
                    alpha -= 1;
                    break;
                case Keys.X:
                    betha += 1;
                    break;
                case Keys.S:
                    betha -= 1;
                    break;
                case Keys.C:
                    gamma += 1;
                    break;
                case Keys.D:
                    gamma -= 1;
                    break;
            }
            glControl1.Invalidate();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            double t;
            Punto PA0, PA1, PB0, PB1, Normal;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Enable(EnableCap.DepthTest); //Habilitar el buffer de profundidad
            GL.Rotate(alpha, 1, 0, 0); //Alpha rota X
            GL.Rotate(betha, 0, 1, 0); //Betha rota Y
            GL.Rotate(gamma, 0, 0, 1); //Gamma rota Z
            #region EJES
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(1.0f, 0.0f, 0.0f); //ROJO
            GL.Vertex3(0.0, 0.0, 0.0);
            GL.Vertex3(xmax, 0.0, 0.0); //EJE X
            GL.Color3(0.0f, 1.0f, 0.0f); //VERDE
            GL.Vertex3(0.0, 0.0, 0.0);
            GL.Vertex3(0.0, ymax, 0.0); //EJE y
            GL.Color3(0.0f, 0.0f, 1.0f); //AZUL
            GL.Vertex3(0.0, 0.0, 0.0);
            GL.Vertex3(0.0, 0.0, zmax); //EJE Z
            GL.End();
            #endregion
            GL.Color3(Color.DarkRed);
            //Bezier(pBezier[0, 0], pBezier[0, 1], pBezier[0, 2], pBezier[0, 3], pBezier[0, 4]);
            PA0 = pBezier[0, 0];
            PB0 = pBezier[1, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[0, 0], pBezier[0, 1], pBezier[0, 2], pBezier[0, 3], pBezier[0, 4]);
                PB1 = Bezier4(t, pBezier[1, 0], pBezier[1, 1], pBezier[1, 2], pBezier[1, 3], pBezier[1, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[1, 0];
            PB0 = pBezier[2, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[1, 0], pBezier[1, 1], pBezier[1, 2], pBezier[1, 3], pBezier[1, 4]);
                PB1 = Bezier4(t, pBezier[2, 0], pBezier[2, 1], pBezier[2, 2], pBezier[2, 3], pBezier[2, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[2, 0];
            PB0 = pBezier[3, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[2, 0], pBezier[2, 1], pBezier[2, 2], pBezier[2, 3], pBezier[2, 4]);
                PB1 = Bezier4(t, pBezier[3, 0], pBezier[3, 1], pBezier[3, 2], pBezier[3, 3], pBezier[3, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[3, 0];
            PB0 = pBezier[4, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[3, 0], pBezier[3, 1], pBezier[3, 2], pBezier[3, 3], pBezier[3, 4]);
                PB1 = Bezier4(t, pBezier[4, 0], pBezier[4, 1], pBezier[4, 2], pBezier[4, 3], pBezier[4, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[4, 0];
            PB0 = pBezier[5, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[4, 0], pBezier[4, 1], pBezier[4, 2], pBezier[4, 3], pBezier[4, 4]);
                PB1 = Bezier4(t, pBezier[5, 0], pBezier[5, 1], pBezier[5, 2], pBezier[5, 3], pBezier[5, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[5, 0];
            PB0 = pBezier[6, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[5, 0], pBezier[5, 1], pBezier[5, 2], pBezier[5, 3], pBezier[5, 4]);
                PB1 = Bezier4(t, pBezier[6, 0], pBezier[6, 1], pBezier[6, 2], pBezier[6, 3], pBezier[6, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[6, 0];
            PB0 = pBezier[7, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[6, 0], pBezier[6, 1], pBezier[6, 2], pBezier[6, 3], pBezier[6, 4]);
                PB1 = Bezier4(t, pBezier[7, 0], pBezier[7, 1], pBezier[7, 2], pBezier[7, 3], pBezier[7, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[7, 0];
            PB0 = pBezier[8, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[7, 0], pBezier[7, 1], pBezier[7, 2], pBezier[7, 3], pBezier[7, 4]);
                PB1 = Bezier4(t, pBezier[8, 0], pBezier[8, 1], pBezier[8, 2], pBezier[8, 3], pBezier[8, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[8, 0];
            PB0 = pBezier[9, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[8, 0], pBezier[8, 1], pBezier[8, 2], pBezier[8, 3], pBezier[8, 4]);
                PB1 = Bezier4(t, pBezier[9, 0], pBezier[9, 1], pBezier[9, 2], pBezier[9, 3], pBezier[9, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[9, 0];
            PB0 = pBezier[10, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[9, 0], pBezier[9, 1], pBezier[9, 2], pBezier[9, 3], pBezier[9, 4]);
                PB1 = Bezier4(t, pBezier[10, 0], pBezier[10, 1], pBezier[10, 2], pBezier[10, 3], pBezier[10, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            PA0 = pBezier[10, 0];
            PB0 = pBezier[11, 0];
            for (t = 0.0; t <= 1.0; t += 0.01)
            {
                PA1 = Bezier4(t, pBezier[10, 0], pBezier[10, 1], pBezier[10, 2], pBezier[10, 3], pBezier[10, 4]);
                PB1 = Bezier4(t, pBezier[11, 0], pBezier[11, 1], pBezier[11, 2], pBezier[11, 3], pBezier[11, 4]);

                GL.Begin(PrimitiveType.Quads);
                Normal = normal(PB1, PA1, PB0);
                GL.Normal3(Normal.x, Normal.y, Normal.z);
                GL.Vertex3(PA0.x, PA0.y, PA0.z);
                GL.Vertex3(PA1.x, PA1.y, PA1.z);
                GL.Vertex3(PB1.x, PB1.y, PB1.z);
                GL.Vertex3(PB0.x, PB0.y, PB0.z);
                GL.End();
                PA0 = PA1;
                PB0 = PB1;
            }
            
            GL.PopMatrix();

            glControl1.SwapBuffers();
        }

        public struct Punto
        {
            public double x, y, z;
            public Punto(double x, double y, double z)
            {
                this.x = x; this.y = y; this.z = z;
            }
        }

        Punto[,] pBezier = new Punto[12, 5];

        private void Form1_Load(object sender, EventArgs e)
        {
            int i;
            for(i=0; i<12; i++)
            {
                cBoxLinea.Items.Add("Linea " + (i + 1).ToString());
            }
        }

        private void cBoxCoor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Columna = (int)cBoxCoor.SelectedIndex;
            NumUDX.Value = (decimal)pBezier[Fila, Columna].x;
            NumUDY.Value = (decimal)pBezier[Fila, Columna].y;
            NumUDZ.Value = (decimal)pBezier[Fila, Columna].z;
        }

        private void cBoxLinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fila = (int)cBoxLinea.SelectedIndex;
            NumUDX.Value = (decimal)pBezier[Fila, Columna].x;
            NumUDY.Value = (decimal)pBezier[Fila, Columna].y;
            NumUDZ.Value = (decimal)pBezier[Fila, Columna].z;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ModificarPunto();
            glControl1.Invalidate();
        }

        void ModificarPunto()
        {
            pBezier[Fila, Columna].x = (double)NumUDX.Value;
            pBezier[Fila, Columna].y = (double)NumUDY.Value;
            pBezier[Fila, Columna].z = (double)NumUDZ.Value;
        }

        public Form1()
        {
            InitializeComponent();
            //---Puntos---
            pBezier[0, 0] = new Punto(-500, 0, -600);
            pBezier[0, 1] = new Punto(-450, 200, -600);
            pBezier[0, 2] = new Punto(0, 200, -600);
            pBezier[0, 3] = new Punto(300, -400, -600);
            pBezier[0, 4] = new Punto(500, 0, -600);

            pBezier[1, 0] = new Punto(-500, 0, -500);
            pBezier[1, 1] = new Punto(-450, 200, -500);
            pBezier[1, 2] = new Punto(0, 200, -500);
            pBezier[1, 3] = new Punto(300, -400, -500);
            pBezier[1, 4] = new Punto(500, 0, -500);



            pBezier[2, 0] = new Punto(-500, 0, -400);
            pBezier[2, 1] = new Punto(-450, 200, -400);
            pBezier[2, 2] = new Punto(0, 200, -400);
            pBezier[2, 3] = new Punto(300, -400, -400);
            pBezier[2, 4] = new Punto(500, 0, -400);

            pBezier[3, 0] = new Punto(-500,0, -300);
            pBezier[3, 1] = new Punto(-450, 200, -300);
            pBezier[3, 2] = new Punto(0, 200, -300);
            pBezier[3, 3] = new Punto(300, -400, -300);
            pBezier[3, 4] = new Punto(500, 0, -300);

            pBezier[4, 0] = new Punto(-500, 0, -200);
            pBezier[4, 1] = new Punto(-450, 200, -200);
            pBezier[4, 2] = new Punto(0, 200, -200);
            pBezier[4, 3] = new Punto(300, -400, -200);
            pBezier[4, 4] = new Punto(500, 0, -200);

            pBezier[5, 0] = new Punto(-500, 0, -100);
            pBezier[5, 1] = new Punto(-450, 200, -100);
            pBezier[5, 2] = new Punto(0, 200, -100);
            pBezier[5, 3] = new Punto(300, -400, -100);
            pBezier[5, 4] = new Punto(500, 0, -100);

            pBezier[6, 0] = new Punto(-500, 0, 0);
            pBezier[6, 1] = new Punto(-450, 200, 0);
            pBezier[6, 2] = new Punto(0, 200, 0);
            pBezier[6, 3] = new Punto(300, -400, 0);
            pBezier[6, 4] = new Punto(500, 0, 0);

            pBezier[7, 0] = new Punto(-500, 0, 100);
            pBezier[7, 1] = new Punto(-450, 200, 100);
            pBezier[7, 2] = new Punto(0, 200, 100);
            pBezier[7, 3] = new Punto(300, -400, 100);
            pBezier[7, 4] = new Punto(500, 0, 100);

            pBezier[8, 0] = new Punto(-500, 0, 200);
            pBezier[8, 1] = new Punto(-450, 200, 200);
            pBezier[8, 2] = new Punto(0, 200, 200);
            pBezier[8, 3] = new Punto(300, -400, 200);
            pBezier[8, 4] = new Punto(500, 0, 200);

            pBezier[9, 0] = new Punto(-500, 0, 300);
            pBezier[9, 1] = new Punto(-450, 200, 300);
            pBezier[9, 2] = new Punto(0, 200, 300);
            pBezier[9, 3] = new Punto(300, -400, 300);
            pBezier[9, 4] = new Punto(500, 0, 300);

            pBezier[10, 0] = new Punto(-500, 0, 400);
            pBezier[10, 1] = new Punto(-450, 200, 400);
            pBezier[10, 2] = new Punto(0, 200, 400);
            pBezier[10, 3] = new Punto(300, -400, 400);
            pBezier[10, 4] = new Punto(500, 0, 400);

            pBezier[11, 0] = new Punto(-500, 0, 500);
            pBezier[11, 1] = new Punto(-450, 200, 500);
            pBezier[11, 2] = new Punto(0, 200, 500);
            pBezier[11, 3] = new Punto(300, -400, 500);
            pBezier[11, 4] = new Punto(500, 0, 500);
            
           
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            float[] light_ambient = { 0.2f, 0.2f, 0.2f, 0.2f };
            float[] light_diffuse = { 0.5f, 0.5f, 0.5f, 0.5f };
            float[] light_specular = { 0.5f, 0.5f, 0.5f, 0.5f };
            float[] light_position = { (float)xmax, (float)ymax, (float)zmax, 0.0f };

            float[] mat_ambient = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] mat_diffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shininess = { 100.0f };
            float[] high_shininess = { 100.0f };

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-xmax, xmax, -ymax, ymax, -zmax, zmax);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.ColorMaterial);

            //GL.ShadeModel(ShadingModel.Flat); //Modelo de Interpolacion "Flat"
            GL.ShadeModel(ShadingModel.Smooth); //Modelo Gouraud

            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, light_specular);
            GL.Light(LightName.Light0, LightParameter.Position, light_position);

            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, mat_ambient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, mat_diffuse);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_specular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shininess);

            GL.ClearColor(Color.Black);
        }

        //Punto Bezier(Punto P0, Punto P1, Punto P2, Punto P3, Punto P4)
        //{
        //    Punto Pi = new Punto(0, 0, 0), Pf = new Punto(0,0,0);
        //    double t;

        //    Pi = P0;
        //    for(t = 0; t <= 1; t+=0.01)
        //    {
        //        Pf.x = (P0.x * Math.Pow(1 - t, 4)) + (P1.x * 4 * t * Math.Pow(1 - t, 3)) + (P2.x * 6 * Math.Pow(t, 2) * Math.Pow(1 - t, 2)) + (P3.x * 4 * Math.Pow(t, 3) * (1 - t)) + (P4.x * Math.Pow(t, 4));
        //        Pf.y = (P0.y * Math.Pow(1 - t, 4)) + (P1.y * 4 * t * Math.Pow(1 - t, 3)) + (P2.y * 6 * Math.Pow(t, 2) * Math.Pow(1 - t, 2)) + (P3.y * 4 * Math.Pow(t, 3) * (1 - t)) + (P4.y * Math.Pow(t, 4));
        //        Pf.z = (P0.z * Math.Pow(1 - t, 4)) + (P1.z * 4 * t * Math.Pow(1 - t, 3)) + (P2.z * 6 * Math.Pow(t, 2) * Math.Pow(1 - t, 2)) + (P3.z * 4 * Math.Pow(t, 3) * (1 - t)) + (P4.z * Math.Pow(t, 4));
        //        GL.Begin(PrimitiveType.Lines);
        //            GL.Vertex3(Pi.x, Pi.y, Pi.z);
        //            GL.Vertex3(Pf.x, Pf.y, Pf.z);
        //        GL.End();
        //        Pi = Pf;
        //    }
        //    return Pf;
        //}

        Punto Bezier4(double t, Punto P0, Punto P1, Punto P2, Punto P3, Punto P4)
        {
            Punto Pf = new Punto(0, 0, 0);

            Pf.x = (P0.x * Math.Pow(1 - t, 4)) + (P1.x * 4 * t * Math.Pow(1 - t, 3)) + (P2.x * 6 * Math.Pow(t, 2) * Math.Pow(1 - t, 2)) + (P3.x * 4 * Math.Pow(t, 3) * (1 - t)) + (P4.x * Math.Pow(t, 4));
            Pf.y = (P0.y * Math.Pow(1 - t, 4)) + (P1.y * 4 * t * Math.Pow(1 - t, 3)) + (P2.y * 6 * Math.Pow(t, 2) * Math.Pow(1 - t, 2)) + (P3.y * 4 * Math.Pow(t, 3) * (1 - t)) + (P4.y * Math.Pow(t, 4));
            Pf.z = (P0.z * Math.Pow(1 - t, 4)) + (P1.z * 4 * t * Math.Pow(1 - t, 3)) + (P2.z * 6 * Math.Pow(t, 2) * Math.Pow(1 - t, 2)) + (P3.z * 4 * Math.Pow(t, 3) * (1 - t)) + (P4.z * Math.Pow(t, 4));

            return Pf;
        }

        #region Normales
        Punto normal(Punto p0, Punto p1, Punto p2)
        {
            Punto n, v1, v2, aux;

            v1.x = (p1.x - p0.x); v1.y = (p1.y - p0.y); v1.z = (p1.z - p0.z);
            v2.x = (p2.x - p0.x); v2.y = (p2.y - p0.y); v2.z = (p2.z - p0.z);

            aux.x = (v1.y * v2.z) - (v2.y * v1.z);
            aux.y = (v1.x * v2.z) - (v2.x * v1.z);
            aux.z = (v1.x * v2.y) - (v2.x * v1.y);

            n.x = aux.x;
            n.y = -aux.y;
            n.z = aux.z;

            return (n);
        }
        #endregion

    }
}