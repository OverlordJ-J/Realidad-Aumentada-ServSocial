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

namespace Mundo3D
{
    public partial class Form1 : Form
    {
        double xmax = 500.0, ymax = 500.0, zmax = 500.0;//TAMAÑO DEL MUNDO
        double alpha = -30.0, betha = 30.0, gamma = 0.0; //ANGULOS DE ROTACIÓN POR EJE

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Punto normal(Punto p0,Punto p1,Punto p2)
        {
            Punto n, v1, v2, aux;
            v1.x = (p1.x - p0.x); v1.y = (p1.y - p0.y);v1.z = (p1.z - p0.z);
            v2.x = (p2.x - p0.x); v2.y = (p2.y - p0.y); v2.z = (p2.z - p0.z);

            aux.x = (v1.y - v2.z) - (v2.y * v1.z);
            aux.y = (v1.x - v2.z) - (v2.x * v1.z);
            aux.z = (v1.x - v2.y) - (v2.x * v1.y);

            n.x = aux.x;
            n.y = -aux.y;
            n.z = aux.z;

            return (n);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            float[] light_ambient = { 0.2f, 0.2f, 0.2f };
            float[] light_diffuse = { 0.5f, 0.5f, 0.5f, 0.5f };
            float[] light_spectacular = { 0.5f, 0.5f, 0.5f, 0.5f };
            //  float[] light_position = { (float)xmax, (float)ymax, (float)zmax, 0.0f };
            float[] light_position = { (float)xmax, (float)ymax, (float)zmax, 0.0f };
            float[] mat_ambient = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] mat_diffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shinisess = { 100.0f };
            float[] high_shinisess = { 100.0f };


            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Ortho(-xmax, xmax, -ymax, ymax, -zmax, zmax);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();


            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.ColorMaterial);

            //GL.ShadeModel(ShadingModel.Flat); //MODELO DE INTERPOLACIÓN
            GL.ShadeModel(ShadingModel.Smooth); //MODELO GOUNRAUD


            //GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            //GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            //GL.Light(LightName.Light0, LightParameter.Specular, light_specular);
            //GL.Light(LightName.Light0, LightParameter.Position, light_position);
            //GL.Material(LightName.Front, MaterialParameter.Ambient, mat_ambient);



            GL.ClearColor(Color.Purple);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Punto C = new Punto(0.0, 0.0, 0.0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            GL.PushMatrix();

            GL.Enable(EnableCap.DepthTest);//ATEST DE PROFUNDIDA XD

            GL.Rotate(alpha, 1, 0, 0);//ALPHA ROTA X
            GL.Rotate(betha, 0, 1, 0);//BETHA ROTA Y
            GL.Rotate(gamma, 0, 0, 1);//GAMMA ROTA Z

            int l = 300, r = 300;
            //Punto V0 =new Punto ((r * Math.Cos((3 * Math.PI) / 4)), r * Math.Sin((3 * Math.PI) / 4), l);

            GL.Begin(PrimitiveType.Quads);
            //cara1
            GL.Vertex3((r * Math.Cos((3 * Math.PI) / 4)), r * Math.Sin((3 * Math.PI) / 4), l);//v0
            GL.Vertex3(r * Math.Cos((5 * Math.PI) / 4), r * Math.Sin((5 * Math.PI) / 4), l);//v1
            GL.Vertex3(r * Math.Cos((7 * Math.PI) / 4), r * Math.Sin((7 * Math.PI) / 4), l);//v2
            GL.Vertex3(r * Math.Cos((Math.PI) / 4), r * Math.Sin((Math.PI) / 4), l);//v3
            //cara2
            GL.Vertex3(r * Math.Cos(( Math.PI) / 4), r * Math.Sin(( Math.PI) / 4), l);//v3
            GL.Vertex3(l, r * Math.Sin(3 *(Math.PI) / 4), r * Math.Cos((3 * Math.PI) / 4));//v4
            GL.Vertex3(l, r * Math.Sin(5 * (Math.PI) / 4), r * Math.Cos((5 * Math.PI) / 4));//v5
            GL.Vertex3(r * Math.Cos((7*Math.PI) / 4), r * Math.Sin((7*Math.PI) / 4), l);//v2
            //cara3
            GL.Vertex3((r * Math.Cos((3 * Math.PI) / 4)), r * Math.Sin((3 * Math.PI) / 4), -l);//v0
            GL.Vertex3(r * Math.Cos((5 * Math.PI) / 4), r * Math.Sin((5 * Math.PI) / 4), -l);//v1
            GL.Vertex3(r * Math.Cos((7 * Math.PI) / 4), r * Math.Sin((7 * Math.PI) / 4), -l);//v2
            GL.Vertex3(r * Math.Cos((Math.PI) / 4), r * Math.Sin((Math.PI) / 4), -l);//v3
            //cara4
            GL.Vertex3(r * Math.Cos((Math.PI) / 4), r * Math.Sin((Math.PI) / 4), -l);//v3
            GL.Vertex3(-l, r * Math.Sin(3 * (Math.PI) / 4), r * Math.Cos((3 * Math.PI) / 4));//v4
            GL.Vertex3(-l, r * Math.Sin(5 * (Math.PI) / 4), r * Math.Cos((5 * Math.PI) / 4));//v5
            GL.Vertex3(r * Math.Cos((7 * Math.PI) / 4), r * Math.Sin((7 * Math.PI) / 4), -l);//v2


            GL.Vertex3(r * Math.Cos((3 * Math.PI) / 4), r * Math.Sin(3 * (Math.PI) / 4), -l);//v6
            GL.Vertex3(r * Math.Cos((5 * Math.PI) / 4), r * Math.Sin(5 * (Math.PI) / 4), -l);//v7
            GL.End();
            GL.Color3(1.0f, 1.0f, 0.0f);
            //Tetraedro(C, 400.0);

            GL.PopMatrix();
            glControl1.SwapBuffers();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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

        void Tetraedro(Punto C, double r)
        {
            Punto[] Vertices = new Punto[4];
            Punto Normal = new Punto(0, 0, 0);
            double x, y, z;

            x = C.x + r;


            Vertices[0] = new Punto(x, C.y, C.z);

            x = C.x + (r * Math.Cos(2.0 * Math.PI / 3.0));
            z = C.z + (r * Math.Sin(2.0 * Math.PI / 3.0));

            Vertices[1] = new Punto(x, C.y, z);

            x = C.x + (r * Math.Cos(4.0 * Math.PI / 3.0));
            z = C.z + (r * Math.Sin(4.0 * Math.PI / 3.0));

            Vertices[2] = new Punto(x, C.y, z);

            y = C.y + r;

            Vertices[3] = new Punto(C.x, y, C.z);


            GL.Begin(PrimitiveType.Triangles);
            //BASE
            Normal = normal(Vertices[0], Vertices[1], Vertices[2]);
            //GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);

            //CARA 1
            Normal = normal(Vertices[3], Vertices[1], Vertices[0]);
//            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);

            //CARA 2
            Normal = normal(Vertices[3], Vertices[2], Vertices[1]);

            //          GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);

            //CARA 3
            Normal = normal(Vertices[3], Vertices[0], Vertices[2]);

            //            GL.Color3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);

            GL.End();
        }

    }
}
