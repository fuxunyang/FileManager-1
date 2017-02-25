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
            this.components = new System.ComponentModel.Container();
            this.addPanel = new System.Windows.Forms.Panel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.editMenu = new System.Windows.Forms.ContextMenu();
            this.editMenu_2 = new System.Windows.Forms.ContextMenu();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // addPanel
            // 
            this.addPanel.Location = new System.Drawing.Point(12, 12);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(177, 30);
            this.addPanel.TabIndex = 0;
            this.addPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.addPanel_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(100, 21);
            this.textBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(201, 54);
            this.Controls.Add(this.addPanel);
            this.ContextMenu = this.editMenu_2;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.TopMost = true;
            this.Location = global::FileManager.Properties.Settings.Default.Location;
            this.Name = "Form1";
            this.Opacity = 0.8D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "File Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Close);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_Mouse_Down);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_Mouse_Move);
            this.MouseEnter += new System.EventHandler(this.Form_Mouse_Enter);
            this.MouseLeave += new System.EventHandler(this.Form_Mouse_Leave);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel addPanel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenu editMenu;
        private System.Windows.Forms.ContextMenu editMenu_2;
        #endregion

        private System.Windows.Forms.Timer timer;
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
            this.editMenu = new System.Windows.Forms.ContextMenu();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            //
            //Edit button in tabControl
            //
            this.tabControl.Location = new System.Drawing.Point(39, -2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(547, 264);
            this.tabControl.TabIndex = 0;
            this.tabControl.Padding = new System.Drawing.Point(12, 4);
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.DrawItem += tabControl_DrawItem;
            this.tabControl.MouseDown += tabControl_MouseDown;
            this.tabControl.Selecting += tabControl_Selecting;
            this.tabControl.HandleCreated += tabControl_HandleCreated;
            //
            //Form2
            //
            this.components = new System.ComponentModel.Container();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Name = "Form2";
            this.Text = "sub FileManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            // edit_2 is User Class.
            this.Controls.Add(this.edit_2.menuStrip);
            this.MainMenuStrip = this.edit_2.menuStrip;
            this.edit_2.menuStrip.ResumeLayout(false);
            this.edit_2.menuStrip.PerformLayout();
            //
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ContextMenu editMenu;
        private UserEdit edit_2;
        #endregion
    }
}

