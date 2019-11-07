namespace SQLRegistration
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conversationList = new System.Windows.Forms.ListView();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.conversationPanel = new System.Windows.Forms.Panel();
            this.SelectImageButton = new System.Windows.Forms.Button();
            this.messagesList = new System.Windows.Forms.ListBox();
            this.menuStrip.SuspendLayout();
            this.conversationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.addFriendToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip.Size = new System.Drawing.Size(314, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // addFriendToolStripMenuItem
            // 
            this.addFriendToolStripMenuItem.Name = "addFriendToolStripMenuItem";
            this.addFriendToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.addFriendToolStripMenuItem.Text = "Add Friend";
            this.addFriendToolStripMenuItem.Click += new System.EventHandler(this.addFriendToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // conversationList
            // 
            this.conversationList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conversationList.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conversationList.FullRowSelect = true;
            this.conversationList.GridLines = true;
            this.conversationList.Location = new System.Drawing.Point(9, 36);
            this.conversationList.Margin = new System.Windows.Forms.Padding(2);
            this.conversationList.MultiSelect = false;
            this.conversationList.Name = "conversationList";
            this.conversationList.Size = new System.Drawing.Size(297, 324);
            this.conversationList.TabIndex = 4;
            this.conversationList.TileSize = new System.Drawing.Size(228, 36);
            this.conversationList.UseCompatibleStateImageBehavior = false;
            this.conversationList.View = System.Windows.Forms.View.Tile;
            this.conversationList.SelectedIndexChanged += new System.EventHandler(this.conversationList_SelectedIndexChanged);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.messageTextBox.Location = new System.Drawing.Point(24, 304);
            this.messageTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(189, 20);
            this.messageTextBox.TabIndex = 6;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMessageButton.BackColor = System.Drawing.Color.White;
            this.sendMessageButton.Location = new System.Drawing.Point(217, 304);
            this.sendMessageButton.Margin = new System.Windows.Forms.Padding(2);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(79, 20);
            this.sendMessageButton.TabIndex = 7;
            this.sendMessageButton.Text = "SEND";
            this.sendMessageButton.UseVisualStyleBackColor = false;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // conversationPanel
            // 
            this.conversationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conversationPanel.BackColor = System.Drawing.Color.Transparent;
            this.conversationPanel.Controls.Add(this.messagesList);
            this.conversationPanel.Controls.Add(this.SelectImageButton);
            this.conversationPanel.Controls.Add(this.messageTextBox);
            this.conversationPanel.Controls.Add(this.sendMessageButton);
            this.conversationPanel.Location = new System.Drawing.Point(9, 36);
            this.conversationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.conversationPanel.Name = "conversationPanel";
            this.conversationPanel.Size = new System.Drawing.Size(296, 323);
            this.conversationPanel.TabIndex = 8;
            this.conversationPanel.Visible = false;
            // 
            // SelectImageButton
            // 
            this.SelectImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectImageButton.Location = new System.Drawing.Point(0, 304);
            this.SelectImageButton.Name = "SelectImageButton";
            this.SelectImageButton.Size = new System.Drawing.Size(19, 19);
            this.SelectImageButton.TabIndex = 8;
            this.SelectImageButton.UseVisualStyleBackColor = true;
            this.SelectImageButton.Click += new System.EventHandler(this.SelectImageButton_Click);
            // 
            // messagesList
            // 
            this.messagesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messagesList.FormattingEnabled = true;
            this.messagesList.ItemHeight = 24;
            this.messagesList.Location = new System.Drawing.Point(0, 0);
            this.messagesList.Name = "messagesList";
            this.messagesList.Size = new System.Drawing.Size(297, 292);
            this.messagesList.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::SQLRegistration.Properties.Resources.chatappcolours;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(314, 369);
            this.Controls.Add(this.conversationPanel);
            this.Controls.Add(this.conversationList);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.conversationPanel.ResumeLayout(false);
            this.conversationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem addFriendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ListView conversationList;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Panel conversationPanel;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.Button SelectImageButton;
        private System.Windows.Forms.ListBox messagesList;
    }
}