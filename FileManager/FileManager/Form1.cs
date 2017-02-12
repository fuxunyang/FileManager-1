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

        public Form2()
        {
            InitializeComponent();
        }
        //Form2's Set when Load////////////
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Controls.Add(userTab.tabControl); //Make TabControl in Form2
        }
        //Form Close///////////////////////
        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //UserEdit Method/////////////////
        private void Edit_Add_Click(object sender, EventArgs e)
        {
            this.userTab.MakeTabPage();
        }
        private void Edit_Delete_Click(object sender, EventArgs e)
        {
            //if(sender.Equals(userTab.tabControl.SelectedTab))
            userTab.tabControl.TabPages.Remove(userTab.tabControl.SelectedTab);
        }
        private void Edit_NameChange_Click(object sender, EventArgs e)
        {
            //Need Update!
            userTab.tabControl.SelectedTab.Text = "Changed";
        }
        //////////////////////////////////

    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////// 
    // Class for Main Form2
    ////////////////////////////////////////////////////////////////////
    public class UserTab  // Make a new Tab
    {
        public List<TabPage> tabList = new List<TabPage>();
        public System.Windows.Forms.TabControl tabControl = new System.Windows.Forms.TabControl();
        private int tabListIndex;

        public UserTab()  //Setting tabControl
        {
            tabControl.Location = new System.Drawing.Point(39, -2);
            tabControl.Name = "tabControl1";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(547, 264);
            tabControl.TabIndex = 0;
        }

        public int MakeTabPage() //Make a TabPage in Tab
        {
            System.Windows.Forms.TabPage tab = new System.Windows.Forms.TabPage();
            System.Windows.Forms.VScrollBar vScrollBar = new System.Windows.Forms.VScrollBar();
            /////////////////////////////////////////////// Add a TabPage in List<TabPag> 
            tabList.Add(tab);
            tabListIndex = tabList.Count - 1;
            /////////////////////////////////////////////// VScrollBar Sertting
            vScrollBar.Location = new System.Drawing.Point(520, 0);
            vScrollBar.Name = "vScrollBar" + tabList.Count;
            vScrollBar.Size = new System.Drawing.Size(17, 238);
            vScrollBar.TabIndex = tabList.Count;
            /////////////////////////////////////////////// tabPage Setting
            tab.Location = new System.Drawing.Point(4, 22);
            tab.Name = "tabPage" + tabList.Count;
            tab.Padding = new System.Windows.Forms.Padding(3);
            tab.Size = new System.Drawing.Size(520, 300);
            tab.TabIndex = tabListIndex;
            tab.Text = "tabPage";
            tab.UseVisualStyleBackColor = true;
            tab.Controls.Add(vScrollBar);
            tabControl.Controls.Add(tab);
            ///////////////////////////////////////////////
            return tabListIndex;
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
        public System.Windows.Forms.ToolStripMenuItem 추가ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.추가ToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // 추가ToolStripMenuItem
            // 
            this.추가ToolStripMenuItem.Name = "추가ToolStripMenuItem";
            this.추가ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.추가ToolStripMenuItem.Text = "추가";
        }
    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////
}
