namespace FileManager
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.addPanel = new System.Windows.Forms.Panel();
            //this.panel = new System.Windows.Forms.Panel();
            this.editMenu = new System.Windows.Forms.ContextMenu();
            this.SuspendLayout();
            // 
            // addPanel
            // 
            this.addPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.addPanel.Location = new System.Drawing.Point(0, 0);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(567, 30);
            this.addPanel.TabIndex = 0;
            this.addPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.addPanel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(18, 720);
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.addPanel);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "File Manager";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel addPanel;
        //private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ContextMenu editMenu;
        #endregion
    }

    partial class Form2
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.edit_2 = new UserEdit();
            this.edit_2.menuStrip.SuspendLayout();
            this.SuspendLayout();
            //
            //Edit button in tabControl
            //
            this.userTab.tabControl.Padding = new System.Drawing.Point(12, 4);
            this.userTab.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.userTab.tabControl.DrawItem += tabControl_DrawItem;
            this.userTab.tabControl.MouseDown += tabControl_MouseDown;
            this.userTab.tabControl.Selecting += tabControl_Selecting;
            this.userTab.tabControl.HandleCreated += tabControl_HandleCreated;
            //
            //Form2
            //
            this.components = new System.ComponentModel.Container();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Name = "Form2";
            this.Text = "sub FileManager";
            this.Load += new System.EventHandler(this.Form2_Load);
            // edit_2 is User Class.
            this.Controls.Add(this.edit_2.menuStrip);
            this.MainMenuStrip = this.edit_2.menuStrip;
            this.edit_2.menuStrip.ResumeLayout(false);
            this.edit_2.menuStrip.PerformLayout();
            //
        }
        private UserEdit edit_2;
        #endregion
    }

}

