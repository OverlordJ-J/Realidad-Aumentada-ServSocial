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

namespace I_wanna_die
{
    public partial class Form1 : Form
    {
        double xmax = 500.0, ymax = 500.0, zmax = 1.0;
        int iteracion = 0;

        public struct Punto
        {
            public double x, y;
        }

        public struct Cursor
        {
            public Punto P;
            public double angulo;

            public Cursor(double x, double y, double angulo)
            {
                this.P.x = x;
                this.P.y = y;
                this.angulo = angulo;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Ortho(-xmax, xmax, -ymax, ymax, -zmax, zmax);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.ClearColor(Color.Black);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            //double h = 300.0 + (600.0 * Math.Sin(-2.0 * Math.PI / 3.0));

            Cursor C1 = new Cursor(300.0 , 0.0 , Math.PI);         
            Cursor C2 = new Cursor(-0.0, 300.0, -2.0*Math.PI / 2.0);
            Cursor C3 = new Cursor(0.0, -0.0, Math.PI/2.0);
            //Cursor C4 = new Cursor(200.0, -0.0, 2.0*Math.PI / 2.0);
            //Cursor C5 = new Cursor(200.0, -0.0, Math.PI / 2.0);
            //C1.P.x = -450.0; C1.P.y = 0.0; C1.angulo = 0.0;
            //Cursor C2 = new Cursor( 200.0, 200.0, -2.0 * Math.PI/3.0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Koch(iteracion, C1, 600.0);
            //Koch(iteracion, C2, 600.0);
            //Koch(iteracion, C3, 600.0);

            Peano(iteracion, C1, 150.0);
            //Peano(iteracion, C2, 200.0);
            //Peano(iteracion, C3, 300.0);
            //Peano(iteracion, C4, 200.0);
            //Peano(iteracion, C5, 300.0);

            glControl1.SwapBuffers();
        }

        Cursor Koch(int n, Cursor C, double L)
        {
            Cursor Cur = C;
            Punto P1;
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
                Cur = Koch(n - 1, Cur, L / 2.0); //F
                Cur.angulo += Math.PI / 2.0;
                Cur.angulo += Math.PI / 2.0;
                Cur = Koch(n - 1, Cur, L / 2.0);
                Cur.angulo += Math.PI / 2.0;
                Cur.angulo += Math.PI / 2.0;
                //Cur = Koch(n - 1, Cur, L / 2.0);
                Cur.angulo -= Math.PI / 2.0;
                //Cur = Koch(n - 1, Cur, L / 2.0);
            }
            return Cur;
        }

        Cursor Peano(int n, Cursor C, double L)
        {
            Cursor Cur = C;
            Punto P1;
            if (n == 0)
            {
                P1.x = C.P.x + (L * Math.Cos(C.angulo));
                P1.y = C.P.y + (L * Math.Sin(C.angulo));
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.Aqua);
                GL.Vertex2(C.P.x, C.P.y);
                GL.Vertex2(P1.x, P1.y);
                GL.End();
                Cur.P = P1;
            }
            else
            {
                // ff+f+f+ff+f+f-f
                // deg 90 
                
                Cur = Peano(n - 1, Cur, L / 2.0); //F
                Cur = Peano(n - 1, Cur, L / 2.0);//F
                //Cur.angulo -= Math.PI / 2.0;//-
                Cur.angulo += Math.PI / 2.0;//+
                Cur = Peano(n - 1, Cur, L / 2.0); //F
                //Cur.angulo -= Math.PI / 2.0;//-
                Cur.angulo += Math.PI / 2.0;//+
                Cur = Peano(n - 1, Cur, L / 2.0); //F
                Cur.angulo += Math.PI / 2.0;//+
                Cur = Peano(n - 1, Cur, L / 2.0); //F
                Cur = Peano(n - 1, Cur, L / 2.0);//F
                Cur.angulo += Math.PI / 2.0;//+
                Cur = Peano(n - 1, Cur, L / 2.0);//F
                Cur.angulo += Math.PI / 2.0;//+
                Cur = Peano(n - 1, Cur, L / 2.0);//F
                Cur.angulo -= Math.PI / 2.0;//-
                Cur = Peano(n - 1, Cur, L / 2.0); //F
               
                //Cur = Peano(n - 1, Cur, L / 4.0);
                //Cur.angulo += Math.PI / 4.0;
                //Cur.angulo += Math.PI / 4.0;
                //Cur.angulo -= Math.PI / 4.0;
                //Cur = Peano(n - 1, Cur, L / 4.0);
            }
            return Cur;
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Z:
                    if(iteracion > 0){
                        --iteracion;
                    }
                    break;
                case Keys.A:
                    iteracion++;
                    break;
            }
            glControl1.Invalidate();
        }
    }
}
