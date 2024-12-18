namespace HospitalSystemProposal
{
    partial class barcodForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(116, 65);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(190, 204);
            this.listBox1.TabIndex = 39;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(420, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 110);
            this.button1.TabIndex = 40;
            this.button1.Text = "ANASAYFA";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // barcodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 365);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Name = "barcodForm";
            this.Text = "barcodForm";
            this.Load += new System.EventHandler(this.barcodForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
    }
}