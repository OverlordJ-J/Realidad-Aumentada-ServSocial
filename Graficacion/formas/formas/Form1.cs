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

namespace formas
{
    public partial class Form1 : Form
    {
        public struct Punto
        {
            public double x, y, z;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void Tetraedo(Punto C, double r)
        {
            Punto[] Vertices = new Punto[4];
            double x, y, z;
            x = C.x + r;

            Vertices[0] = new Punto(x, C.y, C.z);

            x = C.x + (r * Math.Cos(2.0 * Math.PI / 3.0));
            z = C.z + (r * Math.Sin(2.0 * Math.PI / 3.0));

            Vertices[1] = new Punto(x, C.y, z);

            x = C.x + (r * Math.Cos(4.0 * Math.PI / 3.0));
            z = C.Z + (r * Math.Cos(4.0 * Math.PI / 3.0));

            Vertices[2] = new Punto(x, C.y, z);

            y = C.y + r;

            Vertices[3] = new Punto(C.x, y, C.z);

            GL.Begin(PrimitiveType.Triangles);
            //base
                GL.Color3(1.0f, 0.0f, 0.0f);
                GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
                GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
                GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);

            //cara 1
            GL.Color3(0.0f, 1.0f, 0.0f);
                GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
                GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
                GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
   
            //cara 2
            GL.Color3(0.0f, 0.0f, 1.0f);
                GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
                GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
                GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);

            //cara 3
            GL.Color3(1.0f, 0.0f, 1.0f);
                GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
                GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
                GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);

            GL.End();
        }
    }
}

//push paint
//gl.enable(enablecap.depthtest); test de profundidad
