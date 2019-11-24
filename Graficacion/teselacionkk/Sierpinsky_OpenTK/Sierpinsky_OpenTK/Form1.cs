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

namespace Sierpinsky_OpenTK
{
    public partial class Form1 : Form
    {
        double xmax = 3000.0, ymax = 1000.0, zmax = 1.0;
        int iteracion;
        double tamaño = 800.0;
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
        public struct Cursor1
        {
            public Punto P;
            public double angulo;
            public Cursor1(double x, double y, double angulo)
            {
                this.P.x = x;
                this.P.y = y;
                this.P.z = 0;
                this.angulo = angulo;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Ortho(-100, xmax, -ymax, ymax, -zmax, zmax);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.ClearColor(Color.Black);

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Punto A = new Punto(-50.0, -50.0, 0.0);
            Punto B = new Punto(50.0, -50.0, 0.0);
            Punto C = new Punto(0.0, 50.0, 0.0);
            Cursor1 C1 = new Cursor1();
            C1.P.x = 500.0;
            C1.P.y =-500.0;
            C1.angulo = 0.0;
            Cursor1 C2 = new Cursor1();
            C2.P.x = 0.0;
            C2.P.y = 0.0;
            C2.angulo = (Math.PI / 3.0) ;
            Cursor1 C3 = new Cursor1();
            C3.P.x = 600.0;
            C3.P.y = 000.0;
            C3.angulo = (Math.PI / 3.0) * 2;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            serp1(iteracion, C1, tamaño*5); //Se incrementa el tamaño con cada iteración para que pueda ser visible 

            glControl1.SwapBuffers();
        }
        void sierpinsky(int n, Punto A, Punto B, Punto C) 
        {
            Punto AB = new Punto(), AC =new Punto(), CB = new Punto();
            if(n==0)
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
                AB=PuntoMedio(A,B);
                AC=PuntoMedio(A,C);
                CB=PuntoMedio(C,B);

                sierpinsky(n-1, A, AB, AC);
                sierpinsky(n-1, AB, B, CB);
                sierpinsky(n-1, AC, CB, C);
            }
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
                    tamaño += 800;
                    ++iteracion;
                    break;
                case Keys.Z:
                    if (iteracion > 0)
                        tamaño -= 800;
                        --iteracion;
                    break;
            }
            glControl1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        void circulo(int lados, double r, Punto C)
        {
            double delta = 2.0 * Math.PI / lados;
            double angulo = 2.0 * Math.PI / lados;
            int i;
            Punto P0, P1;
            P0 = new Punto(C.x+r,C.y,0);
            P1 = new Punto(0, 0.0, 0);
            GL.Begin(PrimitiveType.Polygon);
            for (i = 0; i < lados; i++)
            
            {
                P1.x = C.x + (r * Math.Cos(angulo));
                P1.y = C.y + (r * Math.Sin(angulo));

                GL.Color3(Color.Black);
                GL.Vertex2(P0.x, P0.y);
                GL.Vertex2(P1.x, P1.y);
                //GL.End();
                P0 = P1;
                angulo += delta;
            }
            GL.End();
        }
        Cursor1 serp1(int n, Cursor1 C, double L)
        {
            Cursor1 Cur = C;
            
           Punto P1=new Punto();

            if (n == 0)
            {
                P1.x = C.P.x + (L * Math.Cos(C.angulo));
                P1.y = C.P.y + (L * Math.Sin(C.angulo));
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.White);
                GL.Vertex2(C.P.x, C.P.y);
                GL.Vertex2(P1.x, P1.y);
                GL.End();
                Cur.P = P1;
            }
            else
            {
                // si falla deja de contar las A
                Cur = fN(n - 1, Cur, L / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L /5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 5.0);


            }
            return Cur;
        }

        Cursor1 fN(int n, Cursor1 C, double L)
        {
            Cursor1 Cur = C;
            Punto P1 = new Punto();

            if (n == 0)
            {
                P1.x = C.P.x + (L * Math.Cos(C.angulo));
                P1.y = C.P.y + (L * Math.Sin(C.angulo));
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.White);
                GL.Vertex2(C.P.x, C.P.y);
                GL.Vertex2(P1.x, P1.y);
                GL.End();
                Cur.P = P1;
            }
            else
            {
                // si falla deja de contar las A
                Cur.angulo += (Math.PI / 5.0);
                Cur = fO(n - 1, Cur, L / 4.0);
                 
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fP(n - 1, Cur, L / 4.0);
                Cursor1 temp = Cur;
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fM(n - 1, Cur, L / 4.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 4.0);
                Cur = temp;
                Cur.angulo += (Math.PI / 5.0);


            }
            return Cur;
        }
        Cursor1 fO(int n, Cursor1 C, double L)
        {
            Cursor1 Cur = C;
            Punto P1 = new Punto();

            if (n == 0)
            {
                P1.x = C.P.x + (L * Math.Cos(C.angulo));
                P1.y = C.P.y + (L * Math.Sin(C.angulo));
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.White);
                GL.Vertex2(C.P.x, C.P.y);
                GL.Vertex2(P1.x, P1.y);
                GL.End();
                Cur.P = P1;
            }
            else
            {
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fM(n - 1, Cur, L / 4.0);
                 
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 4.0);
                Cursor1 temp = Cur;
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fO(n - 1, Cur, L / 4.0);
                 
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fP(n - 1, Cur, L / 4.0);
                Cur = temp;
                Cur.angulo -= (Math.PI / 5.0);


            }
            return Cur;
        }
        Cursor1 fM(int n, Cursor1 C, double L)
        {
            Cursor1 Cur = C;
            Punto P1 = new Punto();

            if (n == 0)
            {
                P1.x = C.P.x + (L * Math.Cos(C.angulo));
                P1.y = C.P.y + (L * Math.Sin(C.angulo));
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.White);
                GL.Vertex2(C.P.x, C.P.y);
                GL.Vertex2(P1.x, P1.y);
                GL.End();
                Cur.P = P1;
            }
            else
            {

                Cur = fO(n - 1, Cur, L / 5.0);
                
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fP(n - 1, Cur, L / 5.0);
                
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 5.0);
                Cursor1 temp = Cur;
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fO(n - 1, Cur, L / 5.0);
                
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fM(n - 1, Cur, L / 5.0);
                Cur = temp;
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);


            }
            return Cur;
        }
        Cursor1 fP(int n, Cursor1 C, double L)
        {
            Cursor1 Cur = C;
            Punto P1 = new Punto();

            if (n == 0)
            {
                P1.x = C.P.x + (L * Math.Cos(C.angulo));
                P1.y = C.P.y + (L * Math.Sin(C.angulo));
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.White);
                GL.Vertex2(C.P.x, C.P.y);
                GL.Vertex2(P1.x, P1.y);
                GL.End();
                Cur.P = P1;
            }
            else
            {
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fO(n - 1, Cur, L / 5.0);
                
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fM(n - 1, Cur, L / 5.0);
                Cursor1 temp = Cur;
                Cur.angulo += (Math.PI / 5.0);
                Cur = fP(n - 1, Cur, L / 5.0);
                
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur.angulo += (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 5.0);
                Cur = temp;
                Cur.angulo -= (Math.PI / 5.0);
                Cur.angulo -= (Math.PI / 5.0);
                Cur = fN(n - 1, Cur, L / 5.0);
                

            }
            return Cur;
        }
        Cursor1 fA(int n, Cursor1 C, double L)
        {
            Cursor1 Cur = C;
            Punto P1 = new Punto();

            /*

                P1.x = C.P.x + (L * Math.Cos(C.angulo));
                P1.y = C.P.y + (L * Math.Sin(C.angulo));
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.White);
                GL.Vertex2(C.P.x, C.P.y);
                GL.Vertex2(P1.x, P1.y);
                GL.End();
                Cur.P = P1;

            */
            return Cur;
        }
    }
}
