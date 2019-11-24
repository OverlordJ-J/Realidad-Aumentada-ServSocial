namespace CurveBProyecto
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenTK.GLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cBoxLinea = new System.Windows.Forms.ComboBox();
            this.cBoxCoor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NumUDX = new System.Windows.Forms.NumericUpDown();
            this.NumUDY = new System.Windows.Forms.NumericUpDown();
            this.NumUDZ = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumUDX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUDY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUDZ)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(13, 13);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(600, 600);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(643, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Linea";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("News706 BT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(686, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Interface  para modificar";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(619, 493);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(113, 20);
            this.textBox1.TabIndex = 3;
            // 
            // cBoxLinea
            // 
            this.cBoxLinea.FormattingEnabled = true;
            this.cBoxLinea.Location = new System.Drawing.Point(646, 118);
            this.cBoxLinea.Name = "cBoxLinea";
            this.cBoxLinea.Size = new System.Drawing.Size(121, 21);
            this.cBoxLinea.TabIndex = 4;
            this.cBoxLinea.SelectedIndexChanged += new System.EventHandler(this.cBoxLinea_SelectedIndexChanged);
            // 
            // cBoxCoor
            // 
            this.cBoxCoor.FormattingEnabled = true;
            this.cBoxCoor.Items.AddRange(new object[] {
            "Punto 0",
            "Punto 1",
            "Punto 2",
            "Punto 3",
            "Punto 4",
            "Punto 5",
            "Punto 6"});
            this.cBoxCoor.Location = new System.Drawing.Point(828, 118);
            this.cBoxCoor.Name = "cBoxCoor";
            this.cBoxCoor.Size = new System.Drawing.Size(121, 21);
            this.cBoxCoor.TabIndex = 5;
            this.cBoxCoor.SelectedIndexChanged += new System.EventHandler(this.cBoxCoor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(828, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cordenada";
            // 
            // NumUDX
            // 
            this.NumUDX.Location = new System.Drawing.Point(691, 257);
            this.NumUDX.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NumUDX.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.NumUDX.Name = "NumUDX";
            this.NumUDX.Size = new System.Drawing.Size(120, 20);
            this.NumUDX.TabIndex = 7;
            // 
            // NumUDY
            // 
            this.NumUDY.Location = new System.Drawing.Point(691, 299);
            this.NumUDY.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NumUDY.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.NumUDY.Name = "NumUDY";
            this.NumUDY.Size = new System.Drawing.Size(120, 20);
            this.NumUDY.TabIndex = 8;
            // 
            // NumUDZ
            // 
            this.NumUDZ.Location = new System.Drawing.Point(691, 348);
            this.NumUDZ.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NumUDZ.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.NumUDZ.Name = "NumUDZ";
            this.NumUDZ.Size = new System.Drawing.Size(120, 20);
            this.NumUDZ.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(671, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(671, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(671, 355);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Z";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(729, 393);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 13;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 661);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NumUDZ);
            this.Controls.Add(this.NumUDY);
            this.Controls.Add(this.NumUDX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cBoxCoor);
            this.Controls.Add(this.cBoxLinea);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.glControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumUDX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUDY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUDZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cBoxLinea;
        private System.Windows.Forms.ComboBox cBoxCoor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumUDX;
        private System.Windows.Forms.NumericUpDown NumUDY;
        private System.Windows.Forms.NumericUpDown NumUDZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnActualizar;
    }
}

