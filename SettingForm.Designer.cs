namespace RoL4YChecker
{
    partial class SettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxChatDir = new System.Windows.Forms.TextBox();
            this.buttonChatDir = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "チャットログ";
            // 
            // textBoxChatDir
            // 
            this.textBoxChatDir.Location = new System.Drawing.Point(75, 12);
            this.textBoxChatDir.Name = "textBoxChatDir";
            this.textBoxChatDir.Size = new System.Drawing.Size(100, 19);
            this.textBoxChatDir.TabIndex = 1;
            // 
            // buttonChatDir
            // 
            this.buttonChatDir.Location = new System.Drawing.Point(181, 11);
            this.buttonChatDir.Name = "buttonChatDir";
            this.buttonChatDir.Size = new System.Drawing.Size(75, 23);
            this.buttonChatDir.TabIndex = 2;
            this.buttonChatDir.Text = "参照";
            this.buttonChatDir.UseVisualStyleBackColor = true;
            this.buttonChatDir.Click += new System.EventHandler(this.buttonChatDir_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(262, 11);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 57);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonChatDir);
            this.Controls.Add(this.textBoxChatDir);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxChatDir;
        private System.Windows.Forms.Button buttonChatDir;
        private System.Windows.Forms.Button buttonOk;
    }
}