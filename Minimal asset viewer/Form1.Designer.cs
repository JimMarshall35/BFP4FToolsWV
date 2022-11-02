
namespace Minimal_asset_viewer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rendererTimer = new System.Windows.Forms.Timer(this.components);
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.MeshFilesCombo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.geomatIdxPicker = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.geomatIdxPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(795, 444);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // rendererTimer
            // 
            this.rendererTimer.Interval = 50;
            this.rendererTimer.Tick += new System.EventHandler(this.rendererTimer_Tick);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.BackColor = System.Drawing.Color.Transparent;
            this.DescriptionLabel.Location = new System.Drawing.Point(3, 13);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(35, 13);
            this.DescriptionLabel.TabIndex = 1;
            this.DescriptionLabel.Text = "label1";
            this.DescriptionLabel.Click += new System.EventHandler(this.DescriptionLabel_Click);
            this.DescriptionLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.DescriptionLabel_Paint);
            // 
            // MeshFilesCombo
            // 
            this.MeshFilesCombo.FormattingEnabled = true;
            this.MeshFilesCombo.Location = new System.Drawing.Point(12, 12);
            this.MeshFilesCombo.Name = "MeshFilesCombo";
            this.MeshFilesCombo.Size = new System.Drawing.Size(121, 21);
            this.MeshFilesCombo.TabIndex = 2;
            this.MeshFilesCombo.SelectedIndexChanged += new System.EventHandler(this.MeshFilesCombo_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.DescriptionLabel);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 399);
            this.panel1.TabIndex = 3;
            // 
            // geomatIdxPicker
            // 
            this.geomatIdxPicker.Location = new System.Drawing.Point(140, 13);
            this.geomatIdxPicker.Name = "geomatIdxPicker";
            this.geomatIdxPicker.Size = new System.Drawing.Size(120, 20);
            this.geomatIdxPicker.TabIndex = 4;
            this.geomatIdxPicker.ValueChanged += new System.EventHandler(this.geomatIdxPicker_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.geomatIdxPicker);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MeshFilesCombo);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.geomatIdxPicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer rendererTimer;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.ComboBox MeshFilesCombo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown geomatIdxPicker;
    }
}

