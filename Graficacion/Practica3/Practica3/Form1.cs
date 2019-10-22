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
        int iteracion = 0;
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
            //Punto A = new Punto(-50.0, -50.0, 0.0);
            //Punto B = new Punto(50.0, -50.0, 0.0);
            //Punto C = new Punto(0.0, 50.0, 0.0);

            Punto AP = new Punto(25.0, 25.0, 0.0);
            Punto BP = new Punto(-25.0, 25.0, 0.0);
            Punto CP = new Punto(-25.0, -25.0, 0.0);
            Punto DP = new Punto(25.0, -25.0, 0.0);

            Punto AS = new Punto(300.0, 300.0, 0.0);
            Punto BS = new Punto(-300.0, 300.0, 0.0);
            Punto CS = new Punto(-300.0, -300.0, 0.0);
            Punto DS = new Punto(300.0, -300.0, 0.0);

            Punto AC = new Punto(-50.0, -50.0, 0.0);

            Punto A = new Punto(-0.0, -0.0, 0.0);
            Punto B = new Punto(0, 0, 0);

            double side = 15;
            A.x = 6 * side / 2 - side / 2;
            A.y = 4 * side;
            B.x = 6 * side / 2 + side / 2;
            B.y = 4 * side;


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //sierpinsky(iteracion, A, B, C);
            //Pitagoras(iteracion, AP, BP, CP, DP);
            //Carpet(iteracion, AS, BS, CS, DS);
            PitTree(iteracion, A, B);
            //SierCarpet(iteracion, AC, 100.0);
            //Cuadrado(AP, (double)iteracion, Math.PI / 4);

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
            Punto PE = new Punto(), PF = new Punto(), PG = new Punto(), PH = new Punto(), PI = new Punto();
            // if (n == 0)

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.Vertex2(A.x, A.y);
            GL.Vertex2(B.x, B.y);
            GL.Vertex2(C.x, C.y);
            GL.Vertex2(D.x, D.y);
            GL.End();
            //}
            if (n != 0)
            {
                PE = E(B, A);
                PF = F(B, PE);
                PG = G(B, PE);
                PH = H(A, PE);
                PI = I(A, PE);

                Pitagoras(n - 1, PG, PF, B, PE);
                Pitagoras(n - 1, PH, PI, PE, A);
            }
        }

        void Carpet(double n, Punto A, Punto B, Punto C, Punto D)
        {

            Punto AB = new Punto(), BC = new Punto(), CD = new Punto(), DA = new Punto();

            if (n == 0)
            {
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.Black);
                GL.Vertex2(A.x, A.y);
                GL.Vertex2(B.x, B.y);
                GL.Vertex2(C.x, C.y);
                GL.Vertex2(D.x, D.y);
                GL.End();
            }
            else
            {
                AB = CuadradoMid(A, B);
                BC = CuadradoMid(B, C);
                CD = CuadradoMid(C, D);
                DA = CuadradoMid(D, A);

                Carpet(n - 1, AB, BC, CD, DA);
            }
        }

        void PitTree(int n, Punto A, Punto B)
        {
            Punto a = A;
            Punto b = B;
            Punto c = new Punto();
            Punto d = new Punto();
            Punto e = new Punto();

            c.x = b.x - (a.y - b.y);
            c.y = b.y - (b.x - a.x);

            d.x = a.x - (a.y - b.y);
            d.y = a.y - (b.x - a.x);

            e.x = d.x + (b.x - a.x - (a.y - b.y)) / 2;
            e.y = d.y - (b.x - a.x + a.y - b.y) / 2;

            if (n > 0)
            {
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.Navy);
                GL.Vertex2(a.x * -1, a.y * -1);
                GL.Vertex2(b.x * -1, b.y * -1);
                GL.Vertex2(c.x * -1, c.y * -1);
                GL.Vertex2(d.x * -1, d.y * -1);
                GL.End();


                PitTree(n - 1, d, e);
                PitTree(n - 1, e, c);
            }
        }

        void SierCarpet(int n, Punto A, double tam)
        {
            if (n == 0)
            {
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.Green);
                GL.Vertex2(A.x, A.y);
                GL.Vertex2(A.x, A.y + tam);
                GL.Vertex2(A.x + tam, A.y + tam);
                GL.Vertex2(A.x + tam, A.y);
                GL.End();

            }
            else
            {
                Punto temP = A;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 1 && i == 1)
                        {

                        }
                        else
                            SierCarpet(n - 1, temP, tam / 3.0);
                            temP.y += tam / 3.0;
                    }
                    temP.y = A.y;
                    temP.x += tam / 3.0;
                }
            }
        }

        Punto CuadradoMid(Punto p1, Punto p2)
        {
            Punto Cuad = new Punto(0, 0, 0);
            Punto mid = new Punto(0, 0, 0);

            mid = PuntoMedio(p1, p2);

            Cuad.x = (mid.x - ((p1.x - p2.x) / 6)) + ((p1.x - p2.x) / 2);
            Cuad.y = (mid.y - ((p1.y - p2.y) / 6)) + ((p1.y - p2.y) / 2);

            return Cuad;
        }

        //Punto Hipo(Punto p1, Punto p2)
        //{
        //    Punto PE = new Punto(0, 0, 0);
        //    Punto PF = new Punto(0, 0, 0);
        //    Punto PG = new Punto(0, 0, 0);
        //    Punto PH = new Punto(0, 0, 0);
        //    Punto PI = new Punto(0, 0, 0);

        //    PE.x = p1.x + Math.Cos(Math.PI / 3) * Math.Cos(Math.PI / 3) * Math.Abs(p1.x - p2.x);
        //    PE.y = p1.y + Math.Sin(Math.PI / 3) * Math.Sin(Math.PI / 3) * Math.Abs(p1.y - p2.y);

        //    PF.x = p1.x + Math.Cos((Math.PI / 3) + (Math.PI / 2)) * Math.Abs(PE.x - p1.x);
        //    PF.x = p1.y + Math.Sin((Math.PI / 3) + (Math.PI / 2)) * Math.Abs(PE.y - p1.y);

        //    PG.x = PE.x + Math.Cos((Math.PI / 3) + (Math.PI / 2)) * Math.Abs(PE.x - p1.x);
        //    PG.y = PE.y + Math.Sin((Math.PI / 3) + (Math.PI / 2)) * Math.Abs(PE.y - p1.y);

        //    PH.x = p2.x + Math.Cos(Math.PI / 3) * Math.Abs(PE.x - p2.x);
        //    PH.y = p2.y + Math.Sin(Math.PI / 3) * Math.Abs(PE.y - p2.y);

        //    PI.x = PE.x + Math.Cos(Math.PI / 3) * Math.Abs(PE.x - p2.x);
        //    PI.y = PE.x + Math.Sin(Math.PI / 3) * Math.Abs(PE.y - p2.y);
        //}

        Punto E(Punto p1, Punto p2)
        {
            Punto PE = new Punto(0, 0, 0);

            PE.x = p1.x + Math.Cos(Math.PI / 4) * Math.Cos(Math.PI / 4) * (Math.Sqrt(p1.x * p1.x) + Math.Sqrt(p2.x * p2.x));
            //PE.x = p1.x + Math.Cos(alpha) * Math.Cos(alpha) * Math.Sqrt(p1.x * p1.x) - Math.Sqrt(p2.x * p2.x);

            PE.y = p1.y + Math.Sin(Math.PI / 4) * Math.Sin(Math.PI / 4) * (Math.Sqrt(p1.y * p1.y) + Math.Sqrt(p2.y * p2.y));
            //PE.y = p1.y + Math.Sin(alpha) * Math.Sin(alpha) * Math.Sqrt(p1.y * p1.y) - Math.Sqrt(p2.y * p2.y);

            return PE;
        }

        Punto F(Punto p1, Punto PE)
        {
            Punto PF = new Punto(0, 0, 0);

            PF.x = p1.x + (Math.Cos((Math.PI / 4) + (Math.PI / 2))) * (Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p1.x * p1.x));
            //PF.x = p1.x + Math.Cos((alpha) + (Math.PI / 2)) * Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p1.x * p1.x);

            PF.y = p1.y + (Math.Sin((Math.PI / 4) + (Math.PI / 2))) * (Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p1.y * p1.y));
            //PF.y = p1.y + Math.Sin((alpha) + (Math.PI / 2)) * Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p1.y * p1.y);

            return PF;
        }

        Punto G(Punto p1, Punto PE)
        {
            Punto PG = new Punto(0, 0, 0);

            PG.x = PE.x + (Math.Cos((Math.PI / 4) + (Math.PI / 2))) * (Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p1.x * p1.x));
            PG.y = PE.y + (Math.Sin((Math.PI / 4) + (Math.PI / 2))) * (Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p1.y * p1.y));

            return PG;
        }

        Punto H(Punto p2, Punto PE)
        {
            Punto PH = new Punto(0, 0, 0);

            PH.x = p2.x + Math.Cos(Math.PI / 4) * (Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p2.x * p2.x));
            PH.y = p2.y + Math.Sin(Math.PI / 4) * (Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p2.y * p2.y));

            return PH;
        }

        Punto I(Punto p2, Punto PE)
        {
            Punto PI = new Punto(0, 0, 0);

            PI.x = PE.x + Math.Cos(Math.PI / 4) * (Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p2.x * p2.x));
            PI.y = PE.y + Math.Sin(Math.PI / 4) * (Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p2.y * p2.y));

            return PI;
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

        void Cuadrado(Punto C, double r, double ang)
        {
            Punto[] V = new Punto[4];
            double theta = ang;
            int i;

            for (i = 0; i < 4; i++)
            {
                V[i].x = C.x + (r * Math.Cos(theta));
                V[i].y = C.y + (r * Math.Sin(theta));
                V[i].z = 0.0;
                theta += Math.PI / 2;
            }

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(V[0].x, V[0].y, V[0].z);
            GL.Vertex3(V[1].x, V[1].y, V[1].z);
            GL.Vertex3(V[2].x, V[2].y, V[2].z);
            GL.Vertex3(V[3].x, V[3].y, V[3].z);
            GL.End();
        }
    }
}
