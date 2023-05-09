namespace BrainTraining {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Footer = new BrainTraining.Controls.PanelDoubleBuffered();
            this.Content = new BrainTraining.Controls.PanelDoubleBuffered();
            this.Header = new BrainTraining.Controls.PanelDoubleBuffered();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.SystemColors.Control;
            this.Footer.Location = new System.Drawing.Point(144, 517);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(399, 121);
            this.Footer.TabIndex = 2;
            // 
            // Content
            // 
            this.Content.BackColor = System.Drawing.SystemColors.Control;
            this.Content.Location = new System.Drawing.Point(144, 127);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(399, 384);
            this.Content.TabIndex = 1;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.Control;
            this.Header.Location = new System.Drawing.Point(144, 6);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(399, 115);
            this.Header.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 641);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Content);
            this.Controls.Add(this.Header);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Brain Training";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Footer;
        private System.Windows.Forms.Panel Content;
        private System.Windows.Forms.Panel Header;
    }
}

