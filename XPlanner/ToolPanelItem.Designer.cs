namespace XPlanner
{
    partial class ToolPanelItem
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
            this.lblItemText = new System.Windows.Forms.Label();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblItemText
            // 
            this.lblItemText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblItemText.Location = new System.Drawing.Point(39, 3);
            this.lblItemText.Name = "lblItemText";
            this.lblItemText.Size = new System.Drawing.Size(106, 26);
            this.lblItemText.TabIndex = 0;
            this.lblItemText.Text = "label1";
            // 
            // pbxImage
            // 
            this.pbxImage.Image = global::XPlanner.Properties.Resources.microscope;
            this.pbxImage.ImageLocation = "";
            this.pbxImage.Location = new System.Drawing.Point(3, 3);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(30, 26);
            this.pbxImage.TabIndex = 1;
            this.pbxImage.TabStop = false;
            // 
            // ToolPanelItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbxImage);
            this.Controls.Add(this.lblItemText);
            this.Name = "ToolPanelItem";
            this.Size = new System.Drawing.Size(154, 36);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblItemText;
        private System.Windows.Forms.PictureBox pbxImage;
    }
}
