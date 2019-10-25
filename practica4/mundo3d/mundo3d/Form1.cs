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
            //Cubo(200.0);
            //Tetraedro(C, 400.0);
            //Octaedro(200.0);
            //Icosaedro(C, 200.0);
            Dodecaedro(C, 200.0);

            GL.PopMatrix();
            glControl1.SwapBuffers();
        }
        //A
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
        //B
        void Cubo (double l)
        {
            Punto[] Vertices = new Punto[8];
            Punto Normal = new Punto(0, 0, 0);
            
            Vertices[0] = new Punto(l, l, l);//v0       //M//
            Vertices[1] = new Punto(l, -l, l);//v1      //N//
            Vertices[2] = new Punto(-l, -l, l);//v2     //O//
            Vertices[3] = new Punto(-l, l, l);//v3      //P//
            Vertices[4] = new Punto(-l, l, -l);//v4     //Q//
            Vertices[5] = new Punto(l, l, -l);//v5      //R//
            Vertices[6] = new Punto(l, -l, -l);//v6     //S//
            Vertices[7] = new Punto(-l, -l, -l);//v7    //T//

            GL.Begin(PrimitiveType.Quads);
            Normal = normal(Vertices[0], Vertices[1], Vertices[2]);
            //cara1
            GL.Color3(1.0f, 0.0f, 0.0f);//rojo
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            //cara2
            GL.Color3(0.0f, 1.0f, 0.0f);//green
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            // cara3
            GL.Color3(0.0f, 0.0f, 1.0f);//azul
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //cara4
            GL.Color3(1.0f, 1.0f, 0.0f);//morado
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            //cara5
            GL.Color3(1.0f, 1.0f, 1.0f);//negro
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //cara6
            GL.Color3(0.0f, 0.0f, 0.0f);//blanca
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);


            GL.End();
            
        }
        //C
        void Octaedro(double tam)
        {
            GL.Begin(PrimitiveType.Triangles);
            //Cara 1
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(0, 0, 2 * tam);//G
            GL.Vertex3(2 * tam, 0, 0);//I
            GL.Vertex3(0, 2 * tam, 0);//J

            //Cara 2
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0, 0, 2 * tam);//G
            GL.Vertex3(0, 2 * tam, 0);//J
            GL.Vertex3(-2 * tam, 0, 0);//L

            //Cara 3
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(-2 * tam, 0, 0);//L
            GL.Vertex3(0, 0, 2 * tam);//G
            GL.Vertex3(0, -2 * tam, 0);//K

            //Cara 4
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex3(0, 0, 2 * tam);//G
            GL.Vertex3(0, -2 * tam, 0);//K
            GL.Vertex3(2 * tam, 0, 0);//I

            //Cara 5
            GL.Color3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(2 * tam, 0, 0);//I
            GL.Vertex3(0, 0, -2 * tam);//H
            GL.Vertex3(0, 2 * tam, 0);//J

            //Cara 6
            GL.Color3(0.0f, 1.0f, 1.0f);
            GL.Vertex3(0, 2 * tam, 0);//J
            GL.Vertex3(0, 0, -2 * tam);//H
            GL.Vertex3(-2 * tam, 0, 0);//L

            //Cara 7
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(-2 * tam, 0, 0);//L
            GL.Vertex3(0, 0, -2 * tam);//H
            GL.Vertex3(0, -2 * tam, 0);//K

            //Cara 8
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(0, -2 * tam, 0);//K
            GL.Vertex3(0, 0, -2 * tam);//H
            GL.Vertex3(2 * tam, 0, 0);//I

            GL.End();
        }
        //D //Pendiente...
        void Dodecaedro(Punto C, double r)
        {
            Punto[] Vertices = new Punto[20];

            double x, y, z;
            x = C.x + r;
            z = C.z + r;
            y = C.y + r;
            //vertives del cubo
            //vertices de arriba
            Vertices[0] = new Punto(x, y, -z);//Rojo
            Vertices[1] = new Punto(-x, y, -z);//Verde
            Vertices[2] = new Punto(-x, y, z);//Cafe
            Vertices[3] = new Punto(x, y, z);//Gris
            //vertisces de abajo
            Vertices[4] = new Punto(-x, -y, z);//Azul
            Vertices[5] = new Punto(x, -y, z);//Morado
            Vertices[6] = new Punto(x, -y, -z);//Amarillo
            Vertices[7] = new Punto(-x, -y, -z);//Rosa
            //Verice de la tablita || negra
            Vertices[8] = new Punto(x - 36.1, y + 40.4, 0);//Azul
            Vertices[9] = new Punto(-x + 36.1, y + 40.4, 0);//Gris
            Vertices[10] = new Punto(x - 36.1, -y - 40.4, 0);//Verde
            Vertices[11] = new Punto(-x + 36.1, -y - 40.4, 0);//Rojo
            //vertices para tablita = color zul
            Vertices[12] = new Punto(0, y - 36.1, z + 40.4);//Cafe
            Vertices[13] = new Punto(0, -y + 36.1, -z - 40.4);//rojo
            Vertices[14] = new Punto(0, y - 36.1, -z - 40.4);//Azul
            Vertices[15] = new Punto(0, -y + 36.1, z + 40.4);//Gris
            //Vertices de la tabla que esta sobre el plano de la x y de la z color rosa
            Vertices[16] = new Punto(x + 40.4, 0, -z + 36.1);//Verde
            Vertices[17] = new Punto(x + 40.4, 0, z - 36.1);//morado
            Vertices[18] = new Punto(-x - 40.4, 0, z - 36.1);//Gris
            Vertices[19] = new Punto(-x - 40.4, 0, -z + 36.1);//Rojo

            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(7.8f, 6.0f, 10.0f);
            //GL.Vertex3(Vertices[18].x, Vertices[18].y, Vertices[18].z);
            //GL.Vertex3(Vertices[19].x, Vertices[19].y, Vertices[19].z);
            //GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            //GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);
            //GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            //GL.End();
            
            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(1.8f, 0.4f, 0.10f);
            //GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);
            //GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);
            //GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            //GL.Vertex3(Vertices[13].x, Vertices[13].y, Vertices[13].z);
            //GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            
            //GL.End();
            
            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(7.8f, 0.0f, 0.0f);
            //GL.Vertex3(Vertices[13].x, Vertices[13].y, Vertices[13].z);
            //GL.Vertex3(Vertices[14].x, Vertices[14].y, Vertices[14].z);
            //GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            //GL.Vertex3(Vertices[16].x, Vertices[16].y, Vertices[16].z);
            //GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            //GL.End();
            
            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(7.8f, 0.0f, 0.0f);
            //GL.Vertex3(Vertices[15].x, Vertices[15].y, Vertices[15].z);
            //GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            //GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);
            //GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);
            //GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //GL.End();
            
            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(1.3f, 0.0f, 0.0f);
            //GL.Vertex3(Vertices[15].x, Vertices[15].y, Vertices[15].z);
            //GL.Vertex3(Vertices[12].x, Vertices[12].y, Vertices[12].z);
            //GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            //GL.Vertex3(Vertices[18].x, Vertices[18].y, Vertices[18].z);
            //GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            //GL.End();

            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(1.3f, 8.0f, 9.0f);
            //GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            //GL.Vertex3(Vertices[19].x, Vertices[19].y, Vertices[19].z);
            //GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            //GL.Vertex3(Vertices[13].x, Vertices[13].y, Vertices[13].z);
            //GL.Vertex3(Vertices[14].x, Vertices[14].y, Vertices[14].z);
            //GL.End();

            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(1.3f, 8.0f, 9.0f);
            //GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            //GL.Vertex3(Vertices[12].x, Vertices[12].y, Vertices[12].z);
            //GL.Vertex3(Vertices[15].x, Vertices[15].y, Vertices[15].z);
            //GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //GL.Vertex3(Vertices[17].x, Vertices[17].y, Vertices[17].z);
            //GL.End();



            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(1.3f, 8.0f, 0.0f);
            //GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            //GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            //GL.Vertex3(Vertices[19].x, Vertices[19].y, Vertices[19].z);
            //GL.Vertex3(Vertices[18].x, Vertices[18].y, Vertices[18].z);
            //GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            //GL.End();

            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(3.3f, 1.0f, 0.0f);
            //GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            //GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            //GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            //GL.Vertex3(Vertices[12].x, Vertices[12].y, Vertices[12].z);
            //GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);

            //GL.End();

            //GL.Begin(PrimitiveType.Polygon);

            //GL.Color3(3.3f, 1.0f, 8.0f);
            //GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            //GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            //GL.Vertex3(Vertices[14].x, Vertices[14].y, Vertices[14].z);
            //GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            //GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            //GL.End();

            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(9.3f, 1.0f, 0.0f);
            //GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            //GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            //GL.Vertex3(Vertices[17].x, Vertices[17].y, Vertices[17].z);
            //GL.Vertex3(Vertices[16].x, Vertices[16].y, Vertices[16].z);
            //GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            //GL.End();
            //GL.Begin(PrimitiveType.Polygon);
            //GL.Color3(1.3f, 8.0f, 0.0f);
            //GL.Vertex3(Vertices[17].x, Vertices[17].y, Vertices[17].z);
            //GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);
            //GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            //GL.Vertex3(Vertices[16].x, Vertices[16].y, Vertices[16].z);
            //GL.End();

            GL.Begin(PrimitiveType.Quads);
            // color negro
            //Tabla ||
            GL.Color3(0.3f, 0.0f, 0.0f);
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);

            //color azul
            //Tabla =
            GL.Color3(0.3f, 0.0f, 8.0f);
            GL.Vertex3(Vertices[12].x, Vertices[12].y, Vertices[12].z);
            GL.Vertex3(Vertices[15].x, Vertices[15].y, Vertices[15].z);
            GL.Vertex3(Vertices[13].x, Vertices[13].y, Vertices[13].z);
            GL.Vertex3(Vertices[14].x, Vertices[14].y, Vertices[14].z);

            //color rosa
            //Tabla soble el plano de la z y x, que no se mueve en y
            GL.Color3(10.3f, 0.0f, 8.0f);
            GL.Vertex3(Vertices[19].x, Vertices[19].y, Vertices[19].z);
            GL.Vertex3(Vertices[18].x, Vertices[18].y, Vertices[18].z);
            GL.Vertex3(Vertices[17].x, Vertices[17].y, Vertices[17].z);
            GL.Vertex3(Vertices[16].x, Vertices[16].y, Vertices[16].z);
            
            ////Color verde
            ////Cara de arriba del cubo
            //GL.Color3(0.3f, 5.0f, 0.0f);
            //GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            //GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            //GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            //GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            ////Cara de abajo del cubo
            //GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            //GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            //GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            ////Cara del cubo izquierdo
            //GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            //GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            //GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            //GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            ////Cara del cubo derecho
            //GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            //GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            //GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            ////Cara del cubo atras
            //GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            //GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            //GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            //GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            ////Cara del cubo adelante
            //GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            //GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            //GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            //GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);

            GL.End();
        }
        //E
        void Icosaedro(Punto C, double r)
        {
            Punto[] Vertices = new Punto[12];

            double x, y, z;

            x = C.x + r;
            z = C.z + r;
            y = C.y + r;

            Vertices[0] = new Punto(0, y, z - 100);
            Vertices[1] = new Punto(0, y, -z + 100);
            Vertices[2] = new Punto(0, -y, z - 100);
            Vertices[3] = new Punto(0, -y, -z + 100);

            Vertices[4] = new Punto(-x + 100, 0, z);
            Vertices[5] = new Punto(-x + 100, 0, -z);
            Vertices[6] = new Punto(x - 100, 0, z);
            Vertices[7] = new Punto(x - 100, 0, -z);

            Vertices[8] = new Punto(-x, y - 100, 0);
            Vertices[9] = new Punto(-x, -y + 100, 0);
            Vertices[10] = new Punto(x, y - 100, 0);
            Vertices[11] = new Punto(x, -y + 100, 0);

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(0.1f, 0.0f, 0.9f);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);

            GL.Color3(1.0f, 9.0f, 5.0f);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);

            GL.Color3(0.1f, 1.0f, 0.0f);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);

            GL.Color3(0.0f, 0.7f, 1.0f);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);

            GL.Color3(0.0f, 0.1f, 1.0f);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);

            GL.Color3(10.0f, 6.0f, 5.0f);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);

            GL.Color3(0.9f, 1.0f, 0.0f);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);

            GL.Color3(8.0f, 0.0f, 3.0f);
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);

            GL.Color3(0.8f, 0.0f, 0.0f);
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);

            GL.Color3(1.0f, 0.0f, 48.0f);
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);

            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);

            GL.Color3(3.3f, 0.9f, 5.0f);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);

            GL.Color3(3.0f, 15.0f, 1.3f);
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);

            GL.Color3(1.0f, 4.0f, 1.9f);
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);

            GL.Color3(0.0f, 9.0f, 5.0f);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);

            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);

            GL.Color3(9.0f, 0.0f, 1.0f);
            GL.Vertex3(Vertices[11].x, Vertices[9].y, Vertices[9].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);

            GL.Color3(0.0f, 8.0f, 1.0f);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);

            GL.Color3(0.98f, 0.0f, 1.0f);
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);

            GL.Color3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);

            GL.Color3(3.3f, 23.9f, 5.0f);
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);

            GL.Color3(2.0f, 67.0f, 1.0f);
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);

            GL.End();

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
                //case Keys.Escape:
                    
                //    break;
            }
            glControl1.Invalidate();
        }
        
    }
}
