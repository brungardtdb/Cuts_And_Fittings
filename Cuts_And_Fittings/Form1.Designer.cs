namespace Cuts_And_Fittings
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
            this.btnWorkPlane = new System.Windows.Forms.Button();
            this.btnApplyFitting = new System.Windows.Forms.Button();
            this.btnPartCut = new System.Windows.Forms.Button();
            this.btnPolygonCut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWorkPlane
            // 
            this.btnWorkPlane.Location = new System.Drawing.Point(12, 12);
            this.btnWorkPlane.Name = "btnWorkPlane";
            this.btnWorkPlane.Size = new System.Drawing.Size(99, 23);
            this.btnWorkPlane.TabIndex = 0;
            this.btnWorkPlane.Text = "Set Work Plane";
            this.btnWorkPlane.UseVisualStyleBackColor = true;
            this.btnWorkPlane.Click += new System.EventHandler(this.btnWorkPlane_Click);
            // 
            // btnApplyFitting
            // 
            this.btnApplyFitting.Location = new System.Drawing.Point(12, 41);
            this.btnApplyFitting.Name = "btnApplyFitting";
            this.btnApplyFitting.Size = new System.Drawing.Size(99, 23);
            this.btnApplyFitting.TabIndex = 1;
            this.btnApplyFitting.Text = "Apply Fitting";
            this.btnApplyFitting.UseVisualStyleBackColor = true;
            this.btnApplyFitting.Click += new System.EventHandler(this.btnApplyFitting_Click);
            // 
            // btnPartCut
            // 
            this.btnPartCut.Location = new System.Drawing.Point(12, 70);
            this.btnPartCut.Name = "btnPartCut";
            this.btnPartCut.Size = new System.Drawing.Size(99, 23);
            this.btnPartCut.TabIndex = 2;
            this.btnPartCut.Text = "Part Cut";
            this.btnPartCut.UseVisualStyleBackColor = true;
            this.btnPartCut.Click += new System.EventHandler(this.btnPartCut_Click);
            // 
            // btnPolygonCut
            // 
            this.btnPolygonCut.Location = new System.Drawing.Point(12, 99);
            this.btnPolygonCut.Name = "btnPolygonCut";
            this.btnPolygonCut.Size = new System.Drawing.Size(99, 23);
            this.btnPolygonCut.TabIndex = 3;
            this.btnPolygonCut.Text = "Polygon Cut";
            this.btnPolygonCut.UseVisualStyleBackColor = true;
            this.btnPolygonCut.Click += new System.EventHandler(this.btnPolygonCut_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 132);
            this.Controls.Add(this.btnPolygonCut);
            this.Controls.Add(this.btnPartCut);
            this.Controls.Add(this.btnApplyFitting);
            this.Controls.Add(this.btnWorkPlane);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWorkPlane;
        private System.Windows.Forms.Button btnApplyFitting;
        private System.Windows.Forms.Button btnPartCut;
        private System.Windows.Forms.Button btnPolygonCut;
    }
}

