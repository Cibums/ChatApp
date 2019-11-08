namespace SQLRegistration
{
    partial class TextInputDialog
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
            this.dialogInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dialogLabel = new System.Windows.Forms.Label();
            this.DialogOKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dialogInput
            // 
            this.dialogInput.Location = new System.Drawing.Point(13, 30);
            this.dialogInput.Margin = new System.Windows.Forms.Padding(2);
            this.dialogInput.Name = "dialogInput";
            this.dialogInput.Size = new System.Drawing.Size(191, 20);
            this.dialogInput.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "USERNAME:";
            // 
            // dialogLabel
            // 
            this.dialogLabel.AutoSize = true;
            this.dialogLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogLabel.Location = new System.Drawing.Point(9, 7);
            this.dialogLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dialogLabel.Name = "dialogLabel";
            this.dialogLabel.Size = new System.Drawing.Size(97, 20);
            this.dialogLabel.TabIndex = 2;
            this.dialogLabel.Text = "USERNAME:";
            // 
            // DialogOKButton
            // 
            this.DialogOKButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DialogOKButton.Location = new System.Drawing.Point(13, 53);
            this.DialogOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.DialogOKButton.Name = "DialogOKButton";
            this.DialogOKButton.Size = new System.Drawing.Size(191, 30);
            this.DialogOKButton.TabIndex = 3;
            this.DialogOKButton.Text = "Add Friend";
            this.DialogOKButton.UseVisualStyleBackColor = true;
            this.DialogOKButton.Click += new System.EventHandler(this.addFriendButton_Click);
            // 
            // TextInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 94);
            this.Controls.Add(this.DialogOKButton);
            this.Controls.Add(this.dialogLabel);
            this.Controls.Add(this.dialogInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TextInputDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Friend";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox dialogInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label dialogLabel;
        private System.Windows.Forms.Button DialogOKButton;
    }
}