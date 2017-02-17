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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.editMenu = new System.Windows.Forms.ContextMenu();
            this.addPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // addPanel
            // 
            this.addPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.addPanel.Location = new System.Drawing.Point(12, 12);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(201-24, 30);
            this.addPanel.TabIndex = 0;
            this.addPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.addPanel_Click);
            //
            // 
            //
            //
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(18, 127);
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(218, 42);
            this.Controls.Add(this.addPanel);
            this.Name = "Form1";
            this.Opacity = 0.8D;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RightToLeftLayout = true;
            this.Text = "File Manager";
            this.addPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel addPanel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenu editMenu;
        #endregion

        private System.Windows.Forms.Label label1;
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

