namespace BrickBreaker.Screens
{
    partial class Pause_screen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pauselabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pauselabel
            // 
            this.pauselabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.pauselabel.Image = global::BrickBreaker.Properties.Resources.Pause;
            this.pauselabel.Location = new System.Drawing.Point(57, 37);
            this.pauselabel.Name = "pauselabel";
            this.pauselabel.Size = new System.Drawing.Size(738, 297);
            this.pauselabel.TabIndex = 0;
            // 
            // Pause_screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pauselabel);
            this.DoubleBuffered = true;
            this.Name = "Pause_screen";
            this.Size = new System.Drawing.Size(1067, 677);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label pauselabel;
    }
}
