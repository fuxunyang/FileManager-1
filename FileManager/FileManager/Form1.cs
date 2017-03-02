using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
using System.IO;

/// Started on February 12, 2017.
/// Version 1.0 was completed on February 25, 2017.
/// by Dev NamKiHyun...

namespace FileManager
{
    ////////////////////////////////////////////////////////////////////
    /// <summary> Setting Main Form 1
    ////////////////////////////////////////////////////////////////////
    public partial class Form1 : Form
    {
        //
        //To put Control Instance on the List
        //
        private List<Panel> panel_List = new List<Panel>(); 
        private List<Label> label_List = new List<Label>();
        //
        //To show the currently clicked Control Instance
        //        
        private Panel panel_Click;
        private Label label_Click;
        //
        //To access the data text file
        //
        private StreamWriter write_txt;
        private StreamReader read_txt;
        private string data_Path = System.IO.Directory.GetCurrentDirectory();
        //
        //State of Form1
        //
        private UserColor user_Color = new UserColor();
        private Rectangle form_Rectange;
        private bool check_Init = true;
        private bool form1_open = true;
        private bool mouse_Enter = false;
        private bool mouse_Leave = true;
        private int panel_Index = 0;
        private Point mousePoint;

        public Form1()
        {
            InitializeComponent();
            //
            // Set Menu function
            //
            editMenu.MenuItems.Add("삭제", new System.EventHandler(this.Edit_Delete_Click));
            editMenu.MenuItems.Add("이름 바꾸기", new System.EventHandler(this.Edit_NameChange_Click));
            editMenu.MenuItems.Add("종료", new System.EventHandler(this.Form_End));
            this.editMenu_2.MenuItems.Add("종료", new System.EventHandler(this.Form_End));
            this.editMenu_2.MenuItems.Add("색 변경", new System.EventHandler(this.Form1_Color));
            //
            // Set Color
            //
            this.Set_Color();

            this.Data_Check();
            //if have save data
            if (check_Init == false)
            {
                this.panel_List.Add(addPanel);
                this.Load_Data();
                this.ClientSize = new System.Drawing.Size(201, (102) * (panel_List.Count - 1) + 55);
                this.write_txt = new StreamWriter(new FileStream(data_Path + @"\Setting.txt", FileMode.Append, FileAccess.Write));
            }
            //if have no data
            else
            {
                this.panel_List.Add(addPanel);
            }
            form_Rectange.Size = new Size(this.Width, this.Height);
        }
        //Data Check//
        private void Data_Check()
        {
            data_Path = data_Path + @"\Data";
            if (File.Exists(data_Path + @"\Setting.txt"))
            {
                this.check_Init = false;
                this.read_txt = new StreamReader(new FileStream(data_Path + @"\Setting.txt", FileMode.Open, FileAccess.Read));
            }
            else
            {
                this.write_txt = new StreamWriter(new FileStream(data_Path + @"\Setting.txt", FileMode.Append, FileAccess.Write));
            }
        }
        //Load Form1//
        private void Load_Data()
        {
            List<string> title_List = new List<string>();
            string line;
            while ((line = read_txt.ReadLine()) != null)
            {
                String[] title = line.Split();
                title_List.Add(title[1].ToString());
            }
            for(int index = 0; index < title_List.Count ; index++)
            {
                //
                //Insert Label and Panel in List
                //
                this.panel_List.Insert(index,new System.Windows.Forms.Panel());
                this.label_List.Insert(index,new System.Windows.Forms.Label());
                //
                //Handle Click Panel
                //
                this.panel_List[index].MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserPanel_Click);
                this.label_List[index].MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserPanel_Click);
                this.panel_List[index].ContextMenu = editMenu;
                //
                //Set panel and label in Form1
                //
                this.panel_List[index].Name = title_List[index];
                this.Set_Panel(index);
                this.Form_SizeChange();
                this.Locate_Panel();
                this.Controls.Add(panel_List[index]);
                this.panel_List[index].Controls.Add(label_List[index]);
                //this.AutoScrollPosition = new System.Drawing.Point(0, 30);
            }
            this.read_txt.Close();
        }
        //Set a Panel//
        private void Set_Panel(int index)
        {
            //
            // Panel
            //
            this.panel_List[index].BackColor = user_Color.panel_Color[Properties.Settings.Default.Color];
            this.panel_List[index].Size = new System.Drawing.Size(177, 90);
            this.panel_List[index].MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_Mouse_Move);
            //
            // Label
            //
            this.label_List[index].AutoSize = true;
            this.label_List[index].Location = new System.Drawing.Point(10, 70);
            this.label_List[index].Name = "label" + index;
            this.label_List[index].Size = new System.Drawing.Size(38, 12);
            this.label_List[index].TabIndex = index;
            this.label_List[index].Text = panel_List[index].Name;
            this.label_List[index].MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_Mouse_Move);
        }
        // Set Color //
        private void Set_Color()
        {
            if (Properties.Settings.Default.Color == 0)
            {
                this.BackColor = this.user_Color.form_Color[0];
                this.addPanel.BackColor = this.user_Color.add_Color[0];
            }
            else
            {
                this.BackColor = this.user_Color.form_Color[1];
                this.addPanel.BackColor = this.user_Color.add_Color[1];
            }
        }
        //Location of Panels//
        private void Locate_Panel()
        {
            int Y = 0;
            for (int index = 0; index < panel_List.Count; index++)
            {
                Y = (12 + 90) * index + 12;
                this.panel_List[index].Location = new System.Drawing.Point(12, Y);
                this.panel_List[index].TabIndex = index;
            }
        }
        //Form size change dynamically//
        private void Form_SizeChange()
        {
            if (panel_List.Count > 1)
                this.ClientSize = new System.Drawing.Size(201, (102) * (panel_List.Count - 1) + 54);
            else
                this.ClientSize = new System.Drawing.Size(201, 54);
            //this.AutoScrollPosition = new System.Drawing.Point(0, 0);
            //this.AutoScrollMinSize = new System.Drawing.Size(18, (102) * (panel_List.Count - 1) + 55);
        }
        //Make a Panel by Add_Panel//
        private void addPanel_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //
                //Insert Label and Panel in List
                //
                this.panel_Index = panel_List.IndexOf(addPanel);
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
                this.panel_List[panel_Index].Name = "제목" + panel_Index;
                this.Form_SizeChange();
                this.Set_Panel(panel_Index);
                this.Locate_Panel();
                this.Controls.Add(panel_List[panel_Index]);
                this.panel_List[panel_Index].Controls.Add(label_List[panel_Index]);
                //this.AutoScrollPosition = new System.Drawing.Point(0, 30);
                //
                // Panel_Directory
                //
                if(Directory.Exists(data_Path + @"\" + label_List[panel_Index].Text))
                {
                    //When a folder with the same name exists
                    panel_List[panel_Index].Name = "New Folder" + panel_Index;
                    label_List[panel_Index].Text = "New Folder" + panel_Index;
                }
                Directory.CreateDirectory(data_Path + @"\" + label_List[panel_Index].Text);
                this.write_txt.WriteLine(panel_Index + " " + label_List[panel_Index].Text);
                this.write_txt.Flush();
            }
        }
        //When click Panel & Label, Run Form2//
        private void UserPanel_Click(object sender, MouseEventArgs e)
        {
            //
            // handling click event anywhere in a panel
            //
            if (sender is Panel) // if click on the panel except for the label
            {
                panel_Click = sender as Panel;
                string path_Click = data_Path + @"\" + panel_Click.Name;

                // handling click event Right or Left Click.
                if (e.Button == MouseButtons.Left)
                {
                    Form2 form2 = new Form2(label_List[panel_List.IndexOf(panel_Click)].Text, path_Click);
                    form2.TopMost = true;
                    form2.Show();
                    if ((this.Location.X + form2.Size.Width) > System.Windows.Forms.SystemInformation.VirtualScreen.Width)
                    {
                        form2.Location = new Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - form2.Size.Width, this.Location.Y);
                    }
                    else
                    {
                        form2.Location = this.Location;
                    }
                }
                else
                {
                    panel_Click.ContextMenu = editMenu;
                }
            }
            else // if click a lable on the panel
            {
                this.label_Click = sender as Label;
                this.panel_Click = panel_List[label_Click.TabIndex];
                string path_Click = data_Path + @"\" + panel_Click.Name;

                // handling click event Right or Left Click.
                if (e.Button == MouseButtons.Left)
                {
                    Form2 form2 = new Form2(label_Click.Text, path_Click);
                    form2.TopMost = true;
                    form2.Show();
                    form2.Location = this.Location;
                    if ((this.Location.X + form2.Size.Width) > System.Windows.Forms.SystemInformation.VirtualScreen.Width)
                    {
                        form2.Location = new Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - form2.Size.Width, this.Location.Y);
                    }
                    else
                    {
                        form2.Location = this.Location;
                    }
                }
                else
                {
                    panel_Click.ContextMenu = editMenu;
                }
            }
        }
        ////////     Event Handler    ////////
        //////////////////////////////////////
        //------Right Click Function--------// 
            //Edit - Delete//
        private void Edit_Delete_Click(object sender,EventArgs e)
        {
            int current_click_index = panel_List.IndexOf(panel_Click);
            string delete_dir = data_Path + @"\" + label_List[current_click_index].Text;
            //
            // Delete Panel
            //
            this.Controls.Remove(panel_Click);
            this.panel_List.Remove(panel_Click);
            this.label_List.RemoveAt(current_click_index);
            this.Form_SizeChange();
            this.Locate_Panel();
            //
            // Delete Data
            //
            Directory.Delete(delete_dir, true);
            this.write_txt.Close();
            this.write_txt = new StreamWriter(data_Path + @"\Setting.txt");
            foreach (Label item in label_List)
            {
                write_txt.WriteLine(label_List.IndexOf(item) + " " + item.Text);
            }
            this.write_txt.Flush();
        }
            //Edit - NameChange//
        private void Edit_NameChange_Click(object sender, EventArgs e)
        {
            this.panel_Click.Controls.Remove(label_List[panel_List.IndexOf(panel_Click)]);
            this.textBox = new System.Windows.Forms.TextBox();
            this.textBox.Location = new System.Drawing.Point(10, 68);
            this.textBox.Text = label_List[panel_List.IndexOf(panel_Click)].Text;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Name_Change); //Data Change algorithm is in Name_Change method.
            this.panel_Click.Controls.Add(textBox);
        }
            //Name_Change//
        private void Name_Change(object sender, KeyEventArgs e)
        {
            //
            //Press Enter
            //
            if (e.KeyData == Keys.Enter) 
            {
                if (label_List[panel_List.IndexOf(panel_Click)].Text != textBox.Text)
                {
                    if (!Directory.Exists(data_Path + @"\" + textBox.Text)) //When a folder with the same name exists
                    {
                        // Change Data & Label //
                        String[] title = textBox.Text.Split();
                        if (title.Length >= 2)
                        {
                            MessageBox.Show("제목에 띄어쓰기가 존재합니다!");
                            string item = title[0];
                            for(int i = 1; i < title.Length; i++)
                            {
                                item = item + "_" + title[i];
                            }
                            textBox.Text = item;
                        }
                        Directory.Move(data_Path + @"\" + label_List[panel_List.IndexOf(panel_Click)].Text, data_Path + @"\" + textBox.Text);
                        this.label_List[panel_List.IndexOf(panel_Click)].Text = textBox.Text;
                        this.panel_Click.Name = textBox.Text;
                        this.write_txt.Close();
                        this.write_txt = new StreamWriter(data_Path + @"\Setting.txt");
                        foreach(Label item in label_List)
                        {
                            write_txt.WriteLine(label_List.IndexOf(item) + " " + item.Text);
                        }
                        this.write_txt.Flush();
                        this.panel_Click.Controls.Remove(textBox);
                    }
                    else
                    {
                        MessageBox.Show("A folder with the same name exists!","Sorry");
                        this.panel_Click.Controls.Remove(textBox);
                    }
                }
                else
                {
                    this.panel_Click.Controls.Remove(textBox);
                }
            }
            //
            //Press ESC
            //
            else if (e.KeyData == Keys.Escape) 
            {
                this.panel_Click.Controls.Remove(textBox);
            }
            this.panel_Click.Controls.Add(label_List[panel_List.IndexOf(panel_Click)]);
        }
        //----------Drag move Form----------//
            //Form_Mouse_Down//
        private void Form_Mouse_Down(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }
            //Form_Mouse_move//
        private void Form_Mouse_Move(object sender, MouseEventArgs e)
        {
            if((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Location = new Point(this.Left - (mousePoint.X - e.X), this.Top - (mousePoint.Y - e.Y));
            }
        }
        //----------Slide Effect------------//
            //Form_Mouse_Enter//
        private void Form_Mouse_Enter(object sender, EventArgs e)
        {
            if (Cursor.Position.X > this.Location.X 
                && Cursor.Position.Y > this.Location.Y
                && Cursor.Position.X < this.Location.X + this.Width
                && Cursor.Position.Y < this.Location.Y + this.Height)
            {
                if(mouse_Leave == true && form1_open == false)
                {
                    for(int i = 0; i <= (102) * (panel_List.Count - 1) + 54; i++)
                    {
                        this.Size = new Size(201, i);
                    }
                    mouse_Leave = false;
                    mouse_Enter = true;
                    form1_open = true;
                    System.GC.Collect();
                }
            }
        }
            //Form_Mouse_Leave//
        private void Form_Mouse_Leave(object sender, EventArgs e)
        {
            if(Cursor.Position.X < this.Location.X 
                || Cursor.Position.Y < this.Location.Y
                || Cursor.Position.X > this.Location.X + this.Width
                || Cursor.Position.Y > this.Location.Y + this.Height)
            {
                if(mouse_Enter = true && form1_open == true)
                {
                    for (int i = (102) * (panel_List.Count - 1) + 54; i >= 12; i--)
                    {
                        this.Size = new Size(201 ,i);
                    }
                    mouse_Enter = false;
                    mouse_Leave = true;
                    form1_open = false;
                    System.GC.Collect();
                }
            }
        }
        //-----Form1_Color Event---------//
        private void Form1_Color(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.Color == 0)
            {
                this.BackColor = user_Color.form_Color[1];
                foreach(Panel item in panel_List)
                {
                    item.BackColor = user_Color.panel_Color[1];
                }
                this.addPanel.BackColor = user_Color.add_Color[1];
                Properties.Settings.Default.Color = 1;
            }
            else
            {
                this.BackColor = user_Color.form_Color[0];
                foreach (Panel item in panel_List)
                {
                    item.BackColor = user_Color.panel_Color[0];
                }
                this.addPanel.BackColor = user_Color.add_Color[0];
                Properties.Settings.Default.Color = 0;
            }
        }
        //-----Form1_Close Event---------//
        private void Form1_Close(object sender, FormClosingEventArgs e)
        {
            if (panel_List.Count == 1)
            {
                this.write_txt.Close();
                File.Delete(data_Path + @"\Setting.txt");
            }
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();
        }
        //-----Program End Event---------//
        private void Form_End(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //////----------End----------//////
    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////
    /// <summary> Setting Main Form 2
    ////////////////////////////////////////////////////////////////////
    public partial class Form2 : Form
    {
        //
        // Control Instance
        //
        private Panel title_Panel = new System.Windows.Forms.Panel();
        private Label title_Label = new System.Windows.Forms.Label();
        private TabPage tabPage1 = new TabPage();
        private TextBox textbox;
        private Form textbox_form;
        //
        // Control List
        //
        private List<TabPage> tab_List = new List<TabPage>();
        private List<TreeView> treeView_List = new List<TreeView>();
        //
        // For Read & Write Data
        //
        private StreamWriter write_txt;
        private StreamReader read_txt;
        //
        // Form2 State
        //
        private string data_Path;
        private string dir_file_Path;
        private int lastIndex;
        private Point scrollbar;
        private bool check_Init = true;
        //
        // For Add and Close Button
        //
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int TCM_SETMINTABWIDTH = 0x1300 + 49;

        public Form2(string title, string path)
        {
            InitializeComponent();

            this.data_Path = path;
            this.editMenu.MenuItems.Add("불러오기", new System.EventHandler(this.Edit_Load_Click));
            this.editMenu.MenuItems.Add("이름 바꾸기", new System.EventHandler(this.Edit_NameChange_Click));
            this.Controls.Add(tabControl); //Add TabControl in Form2
            this.MakeTitlePanel(title);
            this.Data_Check();
            //if have save data
            if (check_Init == false)
            {
               this.Load_Data();
               this.tabControl.Controls.Add(tabPage1);
               this.SetTabPage(tabPage1);
               this.tabPage1.Text = "";
               this.read_txt.Close();
            }
            //if have no save data
            else
            {
                this.tabControl.Controls.Add(tabPage1);
                this.SetTabPage(tabPage1);
                this.tabPage1.Text = "";
            }
        }
        //Check Data//
        private void Data_Check()
        {
            if (File.Exists(data_Path + @"\Setting.txt"))
            {
                this.check_Init = false;
                this.read_txt = new StreamReader(new FileStream(data_Path + @"\Setting.txt", FileMode.Open, FileAccess.Read));
            }
            else
            {
                this.write_txt = new StreamWriter(new FileStream(data_Path + @"\Setting.txt", FileMode.Append, FileAccess.Write));
            }
        }
        //Load Data//
        private void Load_Data()
        {
            string line;
            while((line = read_txt.ReadLine()) != null)
            {
                //
                // Get Path
                //
                lastIndex = tabControl.TabCount ;
                String[] data_Line = line.Split();
                StringBuilder path = new StringBuilder();
                for (int i = data_Line[0].Length+1; i < line.Length; i++)
                {
                    path.Append(line[i]);
                }
                //
                // Load Tab
                //
                this.tabControl.TabPages.Add("Add_Tab");
                this.tab_List.Add(tabControl.TabPages[lastIndex]);
                this.treeView_List.Add(new TreeView());
                this.treeView_List[treeView_List.Count-1].NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
                this.SetTabPage(tabControl.TabPages[lastIndex]);
                this.SetTreeView();
                this.tabControl.TabPages[lastIndex].Text = data_Line[0];
                this.tabControl.TabPages[lastIndex].Name = path.ToString();
                //
                // Load Directory
                //
                DirectoryInfo root_dir = new DirectoryInfo(this.tabControl.TabPages[lastIndex].Name);
                TreeNode Main_node = new TreeNode(root_dir.Name);
                this.treeView_List[lastIndex].Nodes.Add(Main_node);
                foreach (FileInfo file in root_dir.GetFiles())
                {
                    this.treeView_List[lastIndex].Nodes.Add(file.Name);
                }
                this.treeView_List[lastIndex].ExpandAll();
                this.Sub_Dir(root_dir, Main_node);
            }
            this.read_txt.Close();
            this.write_txt = new StreamWriter(new FileStream(data_Path + @"\Setting.txt", FileMode.Append, FileAccess.Write));
            this.check_Init = false;
            System.GC.Collect();
        }
        //Form2_Title_Panel//
        private void MakeTitlePanel(string title)
        {
            //
            //Panel
            //
            this.title_Panel.Location = new System.Drawing.Point(0, 21);
            this.title_Panel.Size = new System.Drawing.Size(40, 240);
            this.title_Panel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.title_Panel.Name = title;
            this.title_Panel.TabIndex = 0;
            this.title_Panel.Click += new System.EventHandler(this.Title_Panel_Click);
            this.Controls.Add(title_Panel);
            //
            //Label
            //
            this.title_Label.AutoSize = true;
            this.title_Label.Name = title;
            this.title_Label.Size = new System.Drawing.Size(1,38);
            this.title_Label.Text = "";
            for (int i = 0; i< title.Length; i ++)
                this.title_Label.Text = title_Label.Text + title[i] + "\n\n";
            this.title_Label.Location = new System.Drawing.Point(15, 240/title.Length);
            this.title_Label.Click += new System.EventHandler(this.Title_Panel_Click);
            this.title_Panel.Controls.Add(title_Label);
        }
        //Setting Tab_Page//
        private void SetTabPage(TabPage tab)
        {
            //
            //TabPage Setting
            //
            tab.Location = new System.Drawing.Point(1, 22);
            tab.Name = "TabPage";
            tab.Padding = new System.Windows.Forms.Padding(3);
            tab.TabIndex = lastIndex;
            tab.Text = ("New_Tab" + lastIndex);
            tab.UseVisualStyleBackColor = true;
        }
        //Setting TreeView//
        private void SetTreeView()
        {
            //
            //TreeView Setting
            //
            this.scrollbar = this.treeView_List[lastIndex].AutoScrollOffset;
            this.treeView_List[lastIndex].Location = new System.Drawing.Point(0, 0);
            this.treeView_List[lastIndex].Name = "TreeView" + tab_List[lastIndex].TabIndex;
            this.treeView_List[lastIndex].Size = new System.Drawing.Size(500, 237);
            this.treeView_List[lastIndex].TabIndex = tab_List[lastIndex].TabIndex;
            this.treeView_List[lastIndex].Scrollable = true;
            this.tab_List[lastIndex].Controls.Add(treeView_List[lastIndex]);
        }

        //Sub_Directory in TreeView//
        private void Sub_Dir(DirectoryInfo dir, TreeNode node)
        {
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo dir2 in dirs)
            {
                TreeNode present_node = new TreeNode(dir2.Name);
                node.Nodes.Add(present_node);
                foreach(FileInfo file in dir2.GetFiles())
                {
                    dir_file_Path = file.DirectoryName;
                    present_node.Nodes.Add(file.Name);
                }
                if ((dir2.GetDirectories()).Length > 0)
                {
                    this.Sub_Dir(dir2, present_node);
                }
            }
        }
        ////////     Event Handler    ////////        
        //////////////////////////////////////
        //Node Click//
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // When node double clicked, start program.
            if (e.Node.Text.Contains("."))
            {
                try
                {
                    string dir_Path = tabControl.SelectedTab.Name.Replace(treeView_List[tabControl.SelectedIndex].Nodes[0].Text, null);
                    string file_Path = e.Node.FullPath;
                    System.Diagnostics.Process.Start(dir_Path + file_Path);
                }
                catch
                {
                    MessageBox.Show("파일이 존재하지 않거나 경로를 찾지 못했습니다.");
                }
            }
        }
        //Edit Button in tab//
        //Handle Click on Close Button and Add Button
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if(check_Init ==false)
            {
                check_Init = true;
            }
            //
            //Handle Left Click on Add Button
            //
            lastIndex = this.tabControl.TabCount - 1;
            if (this.tabControl.GetTabRect(lastIndex).Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                FolderBrowserDialog open_Folder = new FolderBrowserDialog();
                if(open_Folder.ShowDialog() == DialogResult.OK)
                {
                    //
                    //Add Tab_Page
                    //
                    this.tabControl.TabPages.Insert(lastIndex, "New_Tab");
                    this.tabControl.SelectedIndex = lastIndex;
                    this.tab_List.Insert(lastIndex, tabControl.TabPages[lastIndex]);
                    this.treeView_List.Add(new TreeView());
                    this.SetTabPage(tabControl.SelectedTab);
                    this.SetTreeView();
                    //
                    //Open Directory Browser
                    //
                    DirectoryInfo root_dir = new DirectoryInfo(open_Folder.SelectedPath);
                    TreeNode Main_node = new TreeNode(root_dir.Name);
                    this.treeView_List[lastIndex].Nodes.Add(Main_node);
                    this.treeView_List[lastIndex].NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
                    foreach (FileInfo file in root_dir.GetFiles())
                    {
                        this.treeView_List[lastIndex].Nodes.Add(file.Name);
                    }
                    this.treeView_List[lastIndex].ExpandAll();
                    this.Sub_Dir(root_dir, Main_node);
                    //
                    //Change Tab_Name,Text & Data
                    //
                    this.tabControl.SelectedTab.Name = open_Folder.SelectedPath;
                    this.tabControl.SelectedTab.Text = root_dir.Name;
                    String[] name = root_dir.Name.Split(); //Check for space.
                    if (name.Length >= 2)
                    {
                        StringBuilder tab_name = new StringBuilder();
                        tab_name.Append(name[0]);
                        MessageBox.Show("메인 디렉토리의 이름에 띄어쓰기가 있습니다!");
                        for (int i = 1; i < name.Length; i++)
                        {
                            tab_name.Append("_");
                            tab_name.Append(name[i]);
                        }
                        this.tabControl.SelectedTab.Text = tab_name.ToString();
                    }
                    //
                    //Write Data
                    //
                    this.write_txt.WriteLine(tab_List[lastIndex].Text + " " + tab_List[lastIndex].Name);
                    this.write_txt.Flush();
                    System.GC.Collect();
                }
                open_Folder.Dispose();
            }
            else
            {
                //
                //Handle Left Click on Close Button
                //
                for (int i = 0; i < this.tabControl.TabPages.Count; i++)
                {
                    Rectangle tabRect = this.tabControl.GetTabRect(i);
                    tabRect.Inflate(-2, -2);
                    Bitmap closeImage = Properties.Resources.DeleteButton_Image;
                    Rectangle imageRect = new Rectangle(
                        (tabRect.Right - closeImage.Width),
                        tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                        closeImage.Width,
                        closeImage.Height);
                    if (imageRect.Contains(e.Location) && e.Button == MouseButtons.Left)
                    {
                        this.tab_List.RemoveAt(i);
                        this.tabControl.TabPages.RemoveAt(i);
                        this.treeView_List.RemoveAt(i);
                        this.write_txt.Close();
                        this.write_txt = new StreamWriter(data_Path + @"\Setting.txt");
                        foreach (TabPage tab_Page in tab_List)
                        {
                            write_txt.WriteLine(tab_Page.Text + " " + tab_Page.Name);
                        }
                        write_txt.Flush();
                        System.GC.Collect();
                        break;
                    }
                }
            }
            //
            //Handle Right on Tab Header
            //
            if ( e.Button == MouseButtons.Right)
            {
                if(this.tabControl.GetTabRect(this.tabControl.SelectedIndex).Contains(e.Location))
                    editMenu.Show(this.tabControl, e.Location);
            }
        }
        //Prevent selection last tab//
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == this.tabControl.TabCount - 1)
                e.Cancel = true;
        }
        //Draw Close Buttton and Add Button//
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tabPage = this.tabControl.TabPages[e.Index];
            Rectangle tabRect = this.tabControl.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            if (e.Index == this.tabControl.TabCount - 1)
            {
                Bitmap addImage = Properties.Resources.AddButton_Image;
                e.Graphics.DrawImage(addImage,
                    tabRect.Left + (tabRect.Width - addImage.Width-1) ,
                    tabRect.Top + (tabRect.Height - addImage.Height));
            }
            else
            {
                Bitmap closeImage = Properties.Resources.DeleteButton_Image;
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
            SendMessage(this.tabControl.Handle, TCM_SETMINTABWIDTH, IntPtr.Zero, (IntPtr)16);
        }
        //Edit - Load Directory//
        private void Edit_Load_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open_Folder = new FolderBrowserDialog();
            if (open_Folder.ShowDialog() == DialogResult.OK)
            {
                //
                //Open Directory Browser
                //
                this.treeView_List[tabControl.SelectedIndex].Nodes.Clear();
                DirectoryInfo root_dir = new DirectoryInfo(open_Folder.SelectedPath);
                TreeNode Main_node = new TreeNode(root_dir.Name);
                this.treeView_List[tabControl.SelectedIndex].Nodes.Add(Main_node);
                foreach (FileInfo file in root_dir.GetFiles())
                {
                    this.treeView_List[tabControl.SelectedIndex].Nodes.Add(file.Name);
                }
                this.treeView_List[tabControl.SelectedIndex].ExpandAll();
                this.Sub_Dir(root_dir, Main_node);
                //
                //Change Tab_Name,Text & Data
                //
                this.tabControl.SelectedTab.Name = open_Folder.SelectedPath;
                this.tabControl.SelectedTab.Text = root_dir.Name; this.write_txt.Close();
                String[] name = root_dir.Name.Split(); //Check for space.
                if (name.Length >= 2)
                {
                    StringBuilder tab_name = new StringBuilder();
                    tab_name.Append(name[0]);
                    MessageBox.Show("메인 디렉토리의 이름에 띄어쓰기가 있습니다!");
                    for (int i = 1; i < name.Length; i++)
                    {
                        tab_name.Append("_");
                        tab_name.Append(name[i]);
                    }
                    this.tabControl.SelectedTab.Text = tab_name.ToString();
                }
                this.write_txt = new StreamWriter(data_Path + @"\Setting.txt");
                foreach (TabPage tab_Page in tab_List)
                {
                    write_txt.WriteLine(tab_Page.Text + " " + tab_Page.Name);
                }
                write_txt.Flush();
            }
            open_Folder.Dispose();
        }
        //Edit - Name Change//
        private void Edit_NameChange_Click(object sender, EventArgs e)
        {
            this.textbox_form = new Form();
            this.textbox_form.Size = new System.Drawing.Size(216, 93);
            this.textbox_form.Text = "Change Name";
            this.textbox = new TextBox();
            this.textbox.Size = new System.Drawing.Size(177, 30);
            this.textbox.Location = new System.Drawing.Point(12, 12);
            this.textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Textbox_key);

            this.textbox_form.Controls.Add(textbox);
            this.textbox_form.Show();
        }
        //Textbox to change Name//
        private void Textbox_key(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                tabControl.SelectedTab.Text = textbox.Text;
                this.write_txt.Close();
                this.write_txt = new StreamWriter(data_Path + @"\Setting.txt");
                foreach (TabPage tab_Page in tab_List)
                {
                    write_txt.WriteLine(tab_Page.Text + " " + tab_Page.Name);
                }
                write_txt.Flush();
                textbox_form.Dispose();
            }
            else if (e.KeyData == Keys.Escape)
            {
                textbox_form.Dispose();
            }
        }
        //Close Form2 by Title//
        private void Title_Panel_Click(object sender, EventArgs e)
        {
            this.write_txt.Close();
            this.Dispose();
            System.GC.Collect();
        }
        //Short Key//
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(!base.ProcessCmdKey(ref msg, keyData))
            {
                //Short Key - ESC//
                if(keyData.Equals(Keys.Escape))
                {
                    this.write_txt.Close();
                    this.Dispose();
                    System.GC.Collect();
                    return true;
                }
                //Short Key - Tab//
                else if (keyData.Equals(Keys.Tab))
                {
                    if (this.tabControl.SelectedIndex != tabControl.TabCount - 2)
                    {
                        this.tabControl.SelectedTab = this.tabControl.TabPages[this.tabControl.SelectedIndex + 1];
                    }
                    else
                    {
                        this.tabControl.SelectedTab = this.tabControl.TabPages[0];
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        //Close Form2//
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.write_txt.Close();
            System.GC.Collect();
        }
        /////----------End----------//////
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
        private System.Windows.Forms.ToolStripMenuItem 정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem(); 

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
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.정보ToolStripMenuItem}); 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 24);
            this.editToolStripMenuItem.Text = "Edit";
            //
            // 정보
            //
            this.정보ToolStripMenuItem.Name = "정보ToolStripMenuItem";
            this.정보ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.정보ToolStripMenuItem.Text = "정보";
            this.정보ToolStripMenuItem.Click += new System.EventHandler(this.정보ToolStripMenuItem_Click);
        }
        private void 정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Started on February 12, 2017.\nVersion 1.0 was completed on February 25, 2017.\n\nby Dev NamKiHyun...");
        }
    }
    public class UserColor 
    {
        public List<Color> form_Color = new List<Color>();
        public List<Color> panel_Color = new List<Color>();
        public List<Color> add_Color = new List<Color>();

        public UserColor()
        {
            this.form_Color.Add(ColorTranslator.FromHtml("#181717")); //Dark
            this.form_Color.Add(ColorTranslator.FromHtml("#e7e7e7")); //Bright
            this.panel_Color.Add(ColorTranslator.FromHtml("#999999"));//Gray
            this.panel_Color.Add(ColorTranslator.FromHtml("#80397B"));//Purple
            this.add_Color.Add(ColorTranslator.FromHtml("#4285F4"));  //Blue
            this.add_Color.Add(ColorTranslator.FromHtml("#A4C639")); //Green
            this.add_Color.Add(ColorTranslator.FromHtml("#FF3366"));  //Red
        }
    }

    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////
}
