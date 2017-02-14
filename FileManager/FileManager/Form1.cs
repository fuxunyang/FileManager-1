using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    ////////////////////////////////////////////////////////////////////
    /// <summary> Setting Main Form 1
    ////////////////////////////////////////////////////////////////////
    public partial class Form1 : Form
    {
        private UserPanel userPanel = new UserPanel();
        private bool check_Init = false;

        public Form1()
        {
            InitializeComponent();
        }

        //When click th button, show Form2//
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
        //EventHandler in Loading///////////
        // ->Check Save Data////////////////
        private void Form1_Load(object sender, EventArgs e)
        {
            if (check_Init) //Have no Save Data
                MakeInit();
            /*
              else
                CallData();
            */
        }
        //Set Init_Function in Form1_Load///
        private void MakeInit()
        {
            System.Windows.Forms.Label label_Init = new System.Windows.Forms.Label();
            label_Init.Location = new System.Drawing.Point(240, 116);
            label_Init.ForeColor = System.Drawing.SystemColors.ControlText;
            label_Init.Name = "label_Init";
            label_Init.Size = new System.Drawing.Size(118, 12);
            label_Init.TabIndex = 0;
            label_Init.Text = "Make new Manager";
            this.Controls.Add(label_Init);
        }
        ///////////////////////////////////

    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////// 
    // Class for Main Form1
    ////////////////////////////////////////////////////////////////////
    public class UserPanel //Make a new Panel 
    {
        public List<Panel> panelList = new List<Panel>();
        private int panelCurrentIndex;

        public int MakePanel() //Setting Panel
        {
            //
            //Panel Setting
            //
            System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
            panelList.Add(panel);
            panelCurrentIndex = panelList.Count - 1;

            panel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            panel.Location = new System.Drawing.Point(0, 90 * panelCurrentIndex);
            panel.Name = "항목" + panelList.Count;
            panel.Size = new System.Drawing.Size(583, 90);
            panel.TabIndex = panelCurrentIndex;

            return panelCurrentIndex;
        }
    }
    ////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////
    /// <summary> Setting Main Form 2
    ////////////////////////////////////////////////////////////////////
    public partial class Form2 : Form
    {
        private UserTab userTab = new UserTab();
        private bool checkData = true;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int TCM_SETMINTABWIDTH = 0x1300 + 49;

        public Form2()
        {
            InitializeComponent();
            if (checkData)
            {
                System.Windows.Forms.TabPage tabPage1 = new System.Windows.Forms.TabPage();
                userTab.tabControl.Controls.Add(tabPage1);
                userTab.MakeTabPage(tabPage1);
                tabPage1.Text = "";
            }
        }
        //Form2's Set when Load//
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Controls.Add(userTab.tabControl); //Make TabControl in Form2
        }
        //Form Close//
        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Edit Button in tab//
          //Handle Click on Close Button and Add Button
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            var lastIndex = this.userTab.tabControl.TabCount - 1;
            if (this.userTab.tabControl.GetTabRect(lastIndex).Contains(e.Location))
            {
                this.userTab.tabControl.TabPages.Insert(lastIndex, "New Tab");
                this.userTab.tabControl.SelectedIndex = lastIndex;
                this.userTab.MakeTabPage(this.userTab.tabControl.SelectedTab);
            }
            else
            {
                for (var i = 0; i < this.userTab.tabControl.TabPages.Count; i++)
                {
                    var tabRect = this.userTab.tabControl.GetTabRect(i);
                    tabRect.Inflate(-2, -2);
                    var closeImage = Properties.Resources.DeleteButton_Image;
                    var imageRect = new Rectangle(
                        (tabRect.Right - closeImage.Width),
                        tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                        closeImage.Width,
                        closeImage.Height);
                    if (imageRect.Contains(e.Location))
                    {
                        this.userTab.tabControl.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }
          //Prevent selection last tab
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == this.userTab.tabControl.TabCount - 1)
                e.Cancel = true;
        }
          //Draw Close Buttton and Add Button
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.userTab.tabControl.TabPages[e.Index];
            var tabRect = this.userTab.tabControl.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            if (e.Index == this.userTab.tabControl.TabCount - 1)
            {
                var addImage = Properties.Resources.AddButton_Image;
                e.Graphics.DrawImage(addImage,
                    tabRect.Left + (tabRect.Width - addImage.Width) ,
                    tabRect.Top + (tabRect.Height - addImage.Height));
            }
            else
            {
                var closeImage = Properties.Resources.DeleteButton_Image;
                e.Graphics.DrawImage(closeImage,
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                    tabRect, tabPage.ForeColor, TextFormatFlags.Left);
            }
        }
          //Adjust Tab Width individually.
        private void tabControl_HandleCreated(object sender, EventArgs e)
        {
            SendMessage(this.userTab.tabControl.Handle, TCM_SETMINTABWIDTH, IntPtr.Zero, (IntPtr)16);
        }
        /////////////////

    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////// 
    // Class for Main Form2
    ////////////////////////////////////////////////////////////////////
    public class UserTab  // Make a new Tab
    {
        public System.Windows.Forms.TabControl tabControl = new System.Windows.Forms.TabControl();

        public UserTab()  //Setting tabControl
        {
            tabControl.Location = new System.Drawing.Point(39, -2);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(547, 264);
            tabControl.TabIndex = 0;
        }

        public void MakeTabPage(TabPage tab) //Make a TabPage in Tab
        {
            //
            //VScrollBar Setting
            //
            System.Windows.Forms.VScrollBar vScrollBar = new System.Windows.Forms.VScrollBar();
            vScrollBar.Location = new System.Drawing.Point(524, 0);
            vScrollBar.Name = "vScrollBar";
            vScrollBar.Size = new System.Drawing.Size(17, 238);
            vScrollBar.TabIndex = tab.TabIndex;
            //
            //TabPage Setting
            //
            tab.Location = new System.Drawing.Point(4, 22);
            tab.Name = "TabPage";
            tab.Padding = new System.Windows.Forms.Padding(3);
            tab.Size = new System.Drawing.Size(520, 300);
            tab.TabIndex = tab.TabIndex;
            tab.Text = "New Tab";
            tab.UseVisualStyleBackColor = true;
            tab.Controls.Add(vScrollBar);
        }
    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////
    // Class for Form1 and Form2
    ////////////////////////////////////////////////////////////////////
    public class UserEdit
    {
        public System.Windows.Forms.MenuStrip menuStrip = new System.Windows.Forms.MenuStrip();
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

        public UserEdit() //Need Update!
        {
            // 
            // menuStrip
            // 
            this.menuStrip.AutoSize = false;
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(-6, -5);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(45, 28);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { }); 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 24);
            this.editToolStripMenuItem.Text = "Edit";
            
        }
    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////
}
