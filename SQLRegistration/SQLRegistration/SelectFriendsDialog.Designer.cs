namespace SQLRegistration
{
    partial class SelectFriendsDialog
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
            this.dialogLabel = new System.Windows.Forms.Label();
            this.friendListBox = new System.Windows.Forms.CheckedListBox();
            this.addFriendsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dialogLabel
            // 
            this.dialogLabel.AutoSize = true;
            this.dialogLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogLabel.Location = new System.Drawing.Point(11, 9);
            this.dialogLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dialogLabel.Name = "dialogLabel";
            this.dialogLabel.Size = new System.Drawing.Size(238, 20);
            this.dialogLabel.TabIndex = 3;
            this.dialogLabel.Text = "SELECT ONE OR MORE FRIENDS";
            // 
            // friendListBox
            // 
            this.friendListBox.FormattingEnabled = true;
            this.friendListBox.Items.AddRange(new object[] {
            "gryhfyjhryh",
            "rhjryjryjh",
            "ryjryjj",
            "jyjjhrtt",
            "tjtyjyjyjyjy",
            "yjyjyjyjdfef"});
            this.friendListBox.Location = new System.Drawing.Point(15, 33);
            this.friendListBox.Name = "friendListBox";
            this.friendListBox.Size = new System.Drawing.Size(315, 214);
            this.friendListBox.TabIndex = 4;
            // 
            // addFriendsButton
            // 
            this.addFriendsButton.Location = new System.Drawing.Point(15, 254);
            this.addFriendsButton.Name = "addFriendsButton";
            this.addFriendsButton.Size = new System.Drawing.Size(125, 23);
            this.addFriendsButton.TabIndex = 5;
            this.addFriendsButton.Text = "Add Selected Friends";
            this.addFriendsButton.UseVisualStyleBackColor = true;
            this.addFriendsButton.Click += new System.EventHandler(this.addFriendsButton_Click);
            // 
            // SelectFriendsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 288);
            this.Controls.Add(this.addFriendsButton);
            this.Controls.Add(this.friendListBox);
            this.Controls.Add(this.dialogLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectFriendsDialog";
            this.Text = "Select Friends";
            this.Load += new System.EventHandler(this.SelectFriendsDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dialogLabel;
        private System.Windows.Forms.CheckedListBox friendListBox;
        private System.Windows.Forms.Button addFriendsButton;
    }
}