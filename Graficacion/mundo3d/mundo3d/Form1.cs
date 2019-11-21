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
        double xmax = 10.0, ymax = 10.0, zmax = 10.0;//TAMAÑO DEL MUNDO
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

           // GL.ShadeModel(ShadingModel.Flat); //MODELO DE INTERPOLACIÓN
            GL.ShadeModel(ShadingModel.Smooth); //MODELO GOUNRAUD

            GL.Material(MaterialFace.Front, MaterialParameter.Ambient,mat_ambient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse,mat_diffuse);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular,mat_specular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess,mat_shinisess);



            //GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            //GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            //GL.Light(LightName.Light0, LightParameter.Specular, light_specular);
            //GL.Light(LightName.Light0, LightParameter.Position, light_position);
            //GL.Material(LightName.Front, MaterialParameter.Ambient, mat_ambient);

            //Transparencia
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrc1Alpha);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.RescaleNormal);

            GL.ClearColor(Color.Black);
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

            tetraedroTrunked(C, 200.0);

            GL.Disable(EnableCap.Blend);
            GL.PopMatrix();
            glControl1.SwapBuffers();
        }
     
        void tetraedroTrunked(Punto c, double r)
        {
            Punto[] Vertices = new Punto[12];
            Punto Normal = new Punto(0, 0, 0);
            //double x, y, z;


            Vertices[0] = new Punto(1 / Math.Sqrt(6), -2 / Math.Sqrt(3), 2);        //a
            Vertices[1] = new Punto(1 / Math.Sqrt(6), -2 / Math.Sqrt(3), -2);       //b
            Vertices[2] = new Punto(1 / Math.Sqrt(6), 4 / Math.Sqrt(3), 0);         //c
            Vertices[3] = new Punto(-3 / Math.Sqrt(6), 0, +2);                      //d
            Vertices[4] = new Punto(-3 / Math.Sqrt(6), 0, -2);                      //e
            Vertices[5] = new Punto(-3 / Math.Sqrt(6), +3 / Math.Sqrt(3), +1);      //f
            Vertices[6] = new Punto(-3 / Math.Sqrt(6), -3 / Math.Sqrt(3), 1);       //g
            Vertices[7] = new Punto(-3 / Math.Sqrt(6), +3 / Math.Sqrt(3), -1);      //h
            Vertices[8] = new Punto(-3 / Math.Sqrt(6), -3 / Math.Sqrt(3), -1);      //i
            Vertices[9] = new Punto(5 / Math.Sqrt(6), -1 / Math.Sqrt(3), +1);       //j
            Vertices[10] = new Punto(5 / Math.Sqrt(6), -1 / Math.Sqrt(3), -1);      //k
            Vertices[11] = new Punto(5 / Math.Sqrt(6), 2 / Math.Sqrt(3), 0);         //l


            /*
             * (1/math.sqrt(6), -2/math.sqrt(3),+2)     //a     //0
             * (1/math.sqrt(6), -2/math.sqrt(3),-2)     //b     //1
             * (1/math.sqrt(6), 4/math.sqrt(3),0)       //c     //2
             * (-3/math.sqrt(6), 0,+2)                  //d     //3
             * (-3/math.sqrt(6), 0,-2)                  //e     //4
             * (-3/math.sqrt(6),+3/math.sqrt(3),+1)     //f     //5
             * (-3/math.sqrt(6),-3/math.sqrt(3),+1)     //g     //6
             * (-3/math.sqrt(6),+3/math.sqrt(3),-1)     //h     //7
             * (-3/math.sqrt(6),-3/math.sqrt(3),-1)     //i     //8
             * (5/math.sqrt(6), +1/math.sqrt(3),+1)     //j     //9
             * (5/math.sqrt(6), -1/math.sqrt(3),-1)     //k     //10
             * (5/math.sqrt(6), 2/math.sqrt(3),0)       //l     //11
             * 
              */
            GL.Begin(PrimitiveType.Triangles);//Triangulos equilateros 
            GL.Color3(Color.Cyan);

            Normal = normal(Vertices[0], Vertices[6], Vertices[3]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);   //a
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);   //g
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);   //d

            GL.Color3(Color.Purple);
            Normal = normal(Vertices[8], Vertices[1], Vertices[4]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);

            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);   //i
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);   //b
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);   //e

            GL.Color3(Color.Pink);
            Normal = normal(Vertices[5], Vertices[7], Vertices[2]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);

            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);    //f
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);    //h
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);    //c

            GL.Color3(Color.Brown);
            Normal = normal(Vertices[0], Vertices[5], Vertices[11]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);

            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);    //j
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z); //l
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z); //k

            GL.End();

           


            GL.DepthMask(false);
            GL.Enable(EnableCap.Blend);

            GL.Begin(PrimitiveType.Polygon);//Hexagonos Base
            GL.Color4(0.0f, 0.0f, 1.0f, 1.0f);
            Normal = normal(Vertices[0], Vertices[5], Vertices[11]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);            
            

            //Cara 1 -- a,j,k,b,i,g
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);    //a
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);    //j
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);    //k
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z);    //b
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);    //i
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);    //g
           
            GL.End();

            GL.Disable(EnableCap.Blend);
            GL.DepthMask(true);

            GL.Begin(PrimitiveType.Polygon);//Hexagonos Base
            GL.Color3(Color.Red);
            Normal = normal(Vertices[0], Vertices[5], Vertices[11]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);

            //Cara 2 -- a,d,f,c,l,j
            GL.Vertex3(Vertices[0].x, Vertices[0].y, Vertices[0].z);    //a
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);    //d
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);    //f
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);    //c
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z); //l
            GL.Vertex3(Vertices[9].x, Vertices[9].y, Vertices[9].z);    //j
            GL.End();

            GL.Begin(PrimitiveType.Polygon);//Hexagonos Base
            GL.Color3(Color.White);
            Normal = normal(Vertices[0], Vertices[5], Vertices[11]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);

            //Cara 3 -- d,g,i,e,h,f
            GL.Vertex3(Vertices[3].x, Vertices[3].y, Vertices[3].z);   //d
            GL.Vertex3(Vertices[6].x, Vertices[6].y, Vertices[6].z);   //g
            GL.Vertex3(Vertices[8].x, Vertices[8].y, Vertices[8].z);   //i
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);   //e
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);   //h
            GL.Vertex3(Vertices[5].x, Vertices[5].y, Vertices[5].z);   //f
            GL.End();

            GL.Begin(PrimitiveType.Polygon);//Hexagonos Base
            GL.Color3(Color.Green);
            Normal = normal(Vertices[0], Vertices[5], Vertices[11]);
            GL.Normal3(Normal.x, Normal.y, Normal.z);

            //Cara 4 -- l,c,h,e,b,k
            GL.Vertex3(Vertices[11].x, Vertices[11].y, Vertices[11].z); //l
            GL.Vertex3(Vertices[2].x, Vertices[2].y, Vertices[2].z);    //c
            GL.Vertex3(Vertices[7].x, Vertices[7].y, Vertices[7].z);    //h
            GL.Vertex3(Vertices[4].x, Vertices[4].y, Vertices[4].z);    //e
            GL.Vertex3(Vertices[1].x, Vertices[1].y, Vertices[1].z);    //b
            GL.Vertex3(Vertices[10].x, Vertices[10].y, Vertices[10].z); //k


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