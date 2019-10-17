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
        double xmax = 500.0, ymax = 500.0, zmax = 1.0;
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

            Punto AP = new Punto(100.0, 100.0, 0.0);
            Punto BP = new Punto(-100.0, 100.0, 0.0);
            Punto CP = new Punto(-100.0, -100.0, 0.0);
            Punto DP = new Punto(100.0, -100.0, 0.0);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //sierpinsky(iteracion, A, B, C);
            // Pitagoras(iteracion, AP, BP, CP, DP);
            sierpinskyCarpet(iteracion, AP, BP, CP, DP);
            glControl1.SwapBuffers();
        }

        void sierpinskyCarpet(double n, Punto A, Punto B, Punto C,Punto D)
        {
            Punto AB = new Punto(), BC = new Punto(), CD = new Punto(), DA=new Punto(), AC2 = new Punto(), AC = new Punto(), BD = new Punto(), BD2 = new Punto();
            if (n == 0)
            {
                GL.Begin(PrimitiveType.Polygon);
                GL.Color3(Color.Green);
                GL.Vertex2(A.x, A.y);
                GL.Vertex2(B.x, B.y);
                GL.Vertex2(C.x, C.y);
                GL.Vertex2(D.x, D.y);
                GL.End();
            }
            else
            {
                
                AC = PuntoMedio2(A, B);
                BD = PuntoMedio2(B, D);
                AC2 = PuntoMedio3(A, C);
                BD2 = PuntoMedio3(B, D);


                sierpinskyCarpet(n - 1, A, B, AC, BD);
                sierpinskyCarpet(n - 1, AC, B, AC2, BD);
                sierpinskyCarpet(n - 1, AC2, B, C, BD);
                sierpinskyCarpet(n - 1, A, BD, AC, BD2);
                sierpinskyCarpet(n - 1, AC2, BD, C, BD2);
                sierpinskyCarpet(n - 1, A, BD2, AC, D);
                sierpinskyCarpet(n - 1,AC,BD2,AC2,D);
                sierpinskyCarpet(n - 1,AC2,BD2,C,D);


                //////////////Code////////////////
                //alfombrizar(g, x1, y1, (x2 - x1) / 3 + x1, (y2 - y1) / 3 + y1, fases????? );
                //alfombrizar(g, (x2 - x1) / 3 + x1, y1, (x2 - x1) * 2 / 3 + x1, (y2 - y1) / 3 + y1, f);
                //alfombrizar(g, (x2 - x1) * 2 / 3 + x1, y1, x2, (y2 - y1) / 3 + y1, f);
                //alfombrizar(g, x1, (y2 - y1) / 3 + y1, (x2 - x1) / 3 + x1, (y2 - y1) * 2 / 3 + y1, f);
                //alfombrizar(g, (x2 - x1) * 2 / 3 + x1, (y2 - y1) / 3 + y1, x2, (y2 - y1) * 2 / 3 + y1, f);
                //alfombrizar(g, x1, (y2 - y1) * 2 / 3 + y1, (x2 - x1) / 3 + x1, y2, f);
                //alfombrizar(g, (x2 - x1) / 3 + x1, (y2 - y1) * 2 / 3 + y1, (x2 - x1) * 2 / 3 + x1, y2, f);
                //alfombrizar(g, (x2 - x1) * 2 / 3 + x1, (y2 - y1) * 2 / 3 + y1, x2, y2, f);

            }

        }

        Punto PuntoMedio3(Punto P0, Punto P1)
        {
            Punto P = new Punto(0, 0, 0);

            P.x = ((P1.x - P0.x) *2 / 3.0 + P0.x);
            P.y = ((P1.y - P0.y) *2 / 3.0 + P0.y);
            //P.z = P0.z + ((P1.z - P0.z) / 2.0);

            return P;
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

            PE.x = p1.x + Math.Cos(Math.PI / 3) * Math.Cos(Math.PI / 3) * Math.Sqrt(p1.x * p1.x) + Math.Sqrt(p2.x * p2.x);
            //PE.x = p1.x + Math.Cos(alpha) * Math.Cos(alpha) * Math.Sqrt(p1.x * p1.x) - Math.Sqrt(p2.x * p2.x);

            PE.y = p1.y + Math.Sin(Math.PI / 3) * Math.Sin(Math.PI / 3) * Math.Sqrt(p1.y * p1.y) + Math.Sqrt(p2.y * p2.y);
            //PE.y = p1.y + Math.Sin(alpha) * Math.Sin(alpha) * Math.Sqrt(p1.y * p1.y) - Math.Sqrt(p2.y * p2.y);

            return PE;
        }

        Punto F(Punto p1, Punto PE)
        {
            Punto PF = new Punto(0, 0, 0);

            PF.x = p1.x + (Math.Cos((Math.PI / 3) + (Math.PI / 2))) * Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p1.x * p1.x);
            //PF.x = p1.x + Math.Cos((alpha) + (Math.PI / 2)) * Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p1.x * p1.x);

            PF.y = p1.y + (Math.Sin((Math.PI / 3) + (Math.PI / 2))) * Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p1.y * p1.y);
            //PF.y = p1.y + Math.Sin((alpha) + (Math.PI / 2)) * Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p1.y * p1.y);

            return PF;
        }

        Punto G(Punto p1, Punto PE)
        {
            Punto PG = new Punto(0, 0, 0);

            PG.x = PE.x + (Math.Cos((Math.PI / 3) + (Math.PI / 2))) * Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p1.x * p1.x);
            PG.y = PE.y + (Math.Sin((Math.PI / 3) + (Math.PI / 2))) * Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p1.y * p1.y);

            return PG;
        }

        Punto H(Punto p2, Punto PE)
        {
            Punto PH = new Punto(0, 0, 0);

            PH.x = p2.x + Math.Cos(Math.PI / 3) * Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p2.x * p2.x);
            PH.y = p2.y + Math.Sin(Math.PI / 3) * Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p2.y * p2.y);

            return PH;
        }

        Punto I(Punto p2, Punto PE)
        {
            Punto PI = new Punto(0, 0, 0);

            PI.x = PE.x + Math.Cos(Math.PI / 3) * Math.Sqrt(PE.x * PE.x) + Math.Sqrt(p2.x * p2.x);
            PI.y = PE.y + Math.Sin(Math.PI / 3) * Math.Sqrt(PE.y * PE.y) + Math.Sqrt(p2.y * p2.y);

            return PI;
        }

        Punto PuntoMedio2(Punto P0, Punto P1)
        {
            Punto P = new Punto(0, 0, 0);

            P.x = ((P1.x - P0.x) / 3.0 + P0.x);
            P.y = ((P1.y - P0.y) / 3.0 + P0.y); 
            //P.z = P0.z + ((P1.z - P0.z) / 2.0);

            return P;
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
