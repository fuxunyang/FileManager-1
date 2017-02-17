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
        private List<Panel> panel_List = new List<Panel>();
        private List<Label> label_List = new List<Label>();

        private Panel panel_Click;
        private Label label_Click;

        private int panel_Index = 0;

        private bool check_Init = false;

        public Form1()
        {
            InitializeComponent();
            editMenu.MenuItems.Add("삭제", new System.EventHandler(this.Edit_Delete_Click));
            editMenu.MenuItems.Add("이름 바꾸기", new System.EventHandler(this.Edit_NameChange_Click));
            this.panel_List.Add(addPanel);
        }
        //Make a Panel by Add_Panel//
        private void addPanel_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //
                //Inser Label and Panel in List
                //
                panel_Index = panel_List.IndexOf(addPanel);
                this.panel_List.Insert(panel_Index, new System.Windows.Forms.Panel());
                this.label_List.Insert(panel_Index, new System.Windows.Forms.Label());
                //
                //Handle Click Panel
                //
                this.panel_List[panel_Index].MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserPanel_Click);
                this.label_List[panel_Index].MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserPanel_Click);
                this.panel_List[panel_Index].ContextMenu = editMenu;
                //
                //Set panel and label in Form1
                //
                this.Form_SizeChange();
                this.Make_Panel();
                this.Locate_Panel();
                this.Controls.Add(panel_List[panel_Index]);
                this.panel_List[panel_Index].Controls.Add(label_List[panel_Index]);
                this.AutoScrollPosition = new System.Drawing.Point(0, 30);
            }
        }
        //When click Panel & Label, Run Form2//
        private void UserPanel_Click(object sender, MouseEventArgs e)
        {
            //
            // handling click event anywhere in a panel
            //
            if (sender is Panel)
            {
                panel_Click = sender as Panel;

                // handling click event Right or Left Click.
                if (e.Button == MouseButtons.Left)
                {
                    Form2 form2 = new Form2(label_List[panel_List.IndexOf(panel_Click)].Text);
                    form2.Show();
                }
                else
                {
                    panel_Click.ContextMenu = editMenu;
                }
            }
            else
            {
                label_Click = sender as Label;
                panel_Click = panel_List[label_Click.TabIndex];

                // handling click event Right or Left Click.
                if (e.Button == MouseButtons.Left)
                {
                    Form2 form2 = new Form2(label_Click.Text);
                    form2.Show();
                }
                else
                {
                    panel_Click.ContextMenu = editMenu;
                }
            }
        }
        //Make a Panel//
        private void Make_Panel()
        {
            //
            //Panel
            //
            panel_List[panel_Index].BackColor = System.Drawing.SystemColors.ButtonShadow;
            panel_List[panel_Index].Name = "제목" + panel_Index;
            panel_List[panel_Index].Size = new System.Drawing.Size(201 - 24, 90);
            //
            //Label
            //
            label_List[panel_Index].AutoSize = true;
            label_List[panel_Index].Location = new System.Drawing.Point(10, 70);
            label_List[panel_Index].Name = "label" + panel_Index;
            label_List[panel_Index].Size = new System.Drawing.Size(38, 12);
            label_List[panel_Index].TabIndex = panel_Index;
            label_List[panel_Index].Text = panel_List[panel_Index].Name;
        }
        //Location of Panels//
        private void Locate_Panel()
        {
            int size = 0;
            for (int index = 0; index < panel_List.Count; index++)
            {
                size = (12 + 90) * index +12;
                this.panel_List[index].Location = new System.Drawing.Point(12, size);
                this.panel_List[index].TabIndex = index;
            }
        }
        //Form size change dynamically//
        private void Form_SizeChange()
        {
            if (panel_List.Count > 1)
                this.ClientSize = new System.Drawing.Size(218, (102) * (panel_List.Count - 1) + 12);
            else
                this.ClientSize = new System.Drawing.Size(218, 42);
            this.AutoScrollPosition = new System.Drawing.Point(0, 0);
        }
        //Edit-Delete//
        private void Edit_Delete_Click(object sender,EventArgs e)
        {
            this.Controls.Remove(panel_Click);
            this.panel_List.Remove(panel_Click);
            this.Form_SizeChange();
            this.Locate_Panel();
        }

        //Edit-NameChange//
        private void Edit_NameChange_Click(object sender, EventArgs e)
        {
            this.panel_Click.Controls.Remove(label_List[panel_List.IndexOf(panel_Click)]);
            textBox.Location = new System.Drawing.Point(10, 68);
            textBox.Text = label_List[panel_List.IndexOf(panel_Click)].Text;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Name_Change);
            this.panel_Click.Controls.Add(textBox);
        }
        //Name_Change//
        private void Name_Change(Object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label_List[panel_List.IndexOf(panel_Click)].Text = textBox.Text;

                this.panel_Click.Controls.Remove(textBox);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.panel_Click.Controls.Remove(textBox);
            }
            this.panel_Click.Controls.Add(label_List[panel_List.IndexOf(panel_Click)]);
        }
        ///////////////////////////////////
    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////
    /// <summary> Setting Main Form 2
    ////////////////////////////////////////////////////////////////////
    public partial class Form2 : Form
    {
        private UserTab userTab = new UserTab();
        private Panel title_Panel = new System.Windows.Forms.Panel();
        private Label title_Label = new System.Windows.Forms.Label();
        private bool checkData = true;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int TCM_SETMINTABWIDTH = 0x1300 + 49;

        public Form2(string title)
        {
            InitializeComponent();
            this.MakeTitlePanel(title);
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
            this.Controls.Add(userTab.tabControl); //Add TabControl in Form2
        }
        //Form2_Title_Panel
        private void MakeTitlePanel(string title)
        {
            //
            //Panel
            //
            title_Panel.Location = new System.Drawing.Point(0, 21);
            title_Panel.Size = new System.Drawing.Size(40, 240);
            title_Panel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            title_Panel.Name = title;
            title_Panel.TabIndex = 0;
            title_Panel.Click += new System.EventHandler(this.Title_Panel_Click);
            this.Controls.Add(title_Panel);
            //
            //Label
            //
            title_Label.Location = new System.Drawing.Point(20, 42);
            title_Label.AutoSize = true;
            title_Label.Name = title;
            title_Label.Size = new System.Drawing.Size(1,38);
            title_Label.Text = "";
            for (int i = 0; i< title.Length; i ++)
                title_Label.Text = title_Label.Text + title[i] + "\n\n";
            this.title_Label.Click += new System.EventHandler(this.Title_Panel_Click);
            this.title_Panel.Controls.Add(title_Label);
        }
        //Close Form2//
        private void Title_Panel_Click(object sender, EventArgs e)
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
                    tabRect.Left + (tabRect.Width - addImage.Width-1) ,
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
            this.menuStrip.Size = new System.Drawing.Size(45, 26);
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
