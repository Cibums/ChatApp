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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.friendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conversationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conversationList = new System.Windows.Forms.ListView();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.conversationPanel = new System.Windows.Forms.Panel();
            this.AddFriendsButton = new System.Windows.Forms.Button();
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
            this.menuStrip.Size = new System.Drawing.Size(318, 24);
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
            this.addFriendToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.friendToolStripMenuItem,
            this.conversationToolStripMenuItem});
            this.addFriendToolStripMenuItem.Name = "addFriendToolStripMenuItem";
            this.addFriendToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.addFriendToolStripMenuItem.Text = "Add...";
            // 
            // friendToolStripMenuItem
            // 
            this.friendToolStripMenuItem.Name = "friendToolStripMenuItem";
            this.friendToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.friendToolStripMenuItem.Text = "Friend";
            this.friendToolStripMenuItem.Click += new System.EventHandler(this.friendToolStripMenuItem_Click);
            // 
            // conversationToolStripMenuItem
            // 
            this.conversationToolStripMenuItem.Name = "conversationToolStripMenuItem";
            this.conversationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.conversationToolStripMenuItem.Text = "Conversation";
            this.conversationToolStripMenuItem.Click += new System.EventHandler(this.conversationToolStripMenuItem_Click);
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
            this.conversationList.Size = new System.Drawing.Size(301, 324);
            this.conversationList.TabIndex = 4;
            this.conversationList.TileSize = new System.Drawing.Size(228, 36);
            this.conversationList.UseCompatibleStateImageBehavior = false;
            this.conversationList.View = System.Windows.Forms.View.Tile;
            this.conversationList.SelectedIndexChanged += new System.EventHandler(this.conversationList_SelectedIndexChanged);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(24, 305);
            this.messageTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(193, 20);
            this.messageTextBox.TabIndex = 6;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMessageButton.BackColor = System.Drawing.Color.White;
            this.sendMessageButton.Location = new System.Drawing.Point(221, 304);
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
            this.conversationPanel.Controls.Add(this.AddFriendsButton);
            this.conversationPanel.Controls.Add(this.messagesList);
            this.conversationPanel.Controls.Add(this.messageTextBox);
            this.conversationPanel.Controls.Add(this.sendMessageButton);
            this.conversationPanel.Location = new System.Drawing.Point(9, 36);
            this.conversationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.conversationPanel.Name = "conversationPanel";
            this.conversationPanel.Size = new System.Drawing.Size(300, 323);
            this.conversationPanel.TabIndex = 8;
            this.conversationPanel.Visible = false;
            // 
            // AddFriendsButton
            // 
            this.AddFriendsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddFriendsButton.Location = new System.Drawing.Point(0, 304);
            this.AddFriendsButton.Name = "AddFriendsButton";
            this.AddFriendsButton.Size = new System.Drawing.Size(19, 19);
            this.AddFriendsButton.TabIndex = 10;
            this.AddFriendsButton.UseVisualStyleBackColor = true;
            this.AddFriendsButton.Click += new System.EventHandler(this.AddFriendsButton_Click);
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
            this.messagesList.Size = new System.Drawing.Size(301, 292);
            this.messagesList.TabIndex = 9;
            this.messagesList.SelectedIndexChanged += new System.EventHandler(this.messagesList_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::SQLRegistration.Properties.Resources.chatappcolours;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(318, 369);
            this.Controls.Add(this.conversationPanel);
            this.Controls.Add(this.conversationList);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
        private System.Windows.Forms.ListBox messagesList;
        private System.Windows.Forms.ToolStripMenuItem friendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conversationToolStripMenuItem;
        private System.Windows.Forms.Button AddFriendsButton;
    }
}