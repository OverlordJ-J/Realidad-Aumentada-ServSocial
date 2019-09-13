using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using SharpGL.Enumerations;

namespace Practica1
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {

        double xmax = 100.0;
        double ymax = 100.0;
        double zmax = 1.0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.ClearColor(0, 0, 0, 1);

            #region Lineas
            //Horizontal
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(-90, 90);
            gl.Vertex(-40, 90);
            gl.End();

            //Vertical
            gl.Color(0.0f, 1.0f, 1.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(-90, 80);
            gl.Vertex(-90, 30);
            gl.End();

            //Diagonal
            gl.Color(1.0f, 0.0f, 1.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(-75, 30);
            gl.Vertex(-25, 80);
            gl.End();
            #endregion

            #region Poligonos
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Vertex(-15, 60);
            gl.Vertex(15, 60);
            gl.Vertex(0, 90);
            gl.End();

            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(50, 90);
            gl.Vertex(20, 90);
            gl.Vertex(20, 60);
            gl.Vertex(50, 60);
            gl.End();
            #endregion
        }


        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(-xmax, xmax, -ymax, ymax, -zmax, zmax);
            gl.MatrixMode(MatrixMode.Modelview);
            gl.LoadIdentity();
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// The current rotation.
        /// </summary>

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }
    }
}
