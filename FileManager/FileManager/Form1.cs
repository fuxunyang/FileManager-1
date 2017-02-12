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
        ///////////////////////////////////

    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////
    /// <summary> Setting Main Form 2
    ////////////////////////////////////////////////////////////////////
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //Form2 Close/////////
        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /////////////////////

    }
    ////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////
}
