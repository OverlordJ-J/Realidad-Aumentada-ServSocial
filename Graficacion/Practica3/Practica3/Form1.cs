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

namespace Practica3
{
    public partial class Form1 : Form
    {
        double xmax = 100.0, ymax = 100.0, zmax = 1.0;
        double iteracion = 0;
        public struct Punto
        {
            public double x, y, z;
            public Punto(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Ortho(-xmax, xmax, -ymax, ymax, -zmax, zmax);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.ClearColor(Color.White);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Punto A = new Punto(-50.0, -50.0, 0.0);
            Punto B = new Punto(50.0, -50.0, 0.0);
            Punto C = new Punto(0.0, 50.0, 0.0);

            Punto AP = new Punto(25.0, 25.0, 0.0);
            Punto BP = new Punto(-25.0, 25.0, 0.0);
            Punto CP = new Punto(-25.0, -25.0, 0.0);
            Punto DP = new Punto(25.0, -25.0, 0.0);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //sierpinsky(iteracion, A, B, C);
            Pitagoras(iteracion, AP, BP, CP, DP);

            glControl1.SwapBuffers();
        }

        void sierpinsky(double n, Punto A, Punto B, Punto C)
        {
            Punto AB = new Punto(), AC = new Punto(), CB = new Punto();
            if (n == 0)
            {
                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(Color.Green);
                GL.Vertex2(A.x, A.y);
                GL.Vertex2(B.x, B.y);
                GL.Vertex2(C.x, C.y);
                GL.End();
            }
            else
            {
                AB = PuntoMedio(A, B);
                AC = PuntoMedio(A, C);
                CB = PuntoMedio(C, B);

                sierpinsky(n - 1, A, AB, AC); //Triangulo 1
                sierpinsky(n - 1, AB, B, CB); //Triangulo 2
                sierpinsky(n - 1, AC, CB, C); //Triangulo 3
            }
        }

        void Pitagoras(double n, Punto A, Punto B, Punto C, Punto D)
        {
            if (n == 0)
            {
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.Red);
                GL.Vertex2(A.x, A.y);
                GL.Vertex2(B.x, B.y);
                GL.Vertex2(C.x, C.y);
                GL.Vertex2(D.x, D.y);

                GL.End();
            }
            else
            {
                //AB = PuntoMedio(A, B);
                //AC = PuntoMedio(A, C);
                //CB = PuntoMedio(C, B);

                //sierpinsky(n - 1, A, AB, AC); //Triangulo 1
                //sierpinsky(n - 1, AB, B, CB); //Triangulo 2
                //sierpinsky(n - 1, AC, CB, C); //Triangulo 3
            }
        }

        Punto Hipo(Punto p1, Punto p2)
        {
            Punto PE = new Punto(0, 0, 0);


        }

        Punto PuntoMedio(Punto P0, Punto P1)
        {
            Punto P = new Punto(0, 0, 0);

            P.x = P0.x + ((P1.x - P0.x) / 2.0);
            P.y = P0.y + ((P1.y - P0.y) / 2.0);
            //P.z = P0.z + ((P1.z - P0.z) / 2.0);

            return P;
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.A:
                    ++iteracion;
                    break;
                case Keys.Z:
                    if (iteracion > 0)
                    {
                        --iteracion;
                    }
                    break;
            }
            glControl1.Invalidate();
        }
    }
}
