using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace XPlanner
{
    public partial class ToolPanelItem : UserControl
    {
        [Description("Path to the image of the item"), Category("Data")]
        public string Image {
            get { return pbxImage.ImageLocation; }
            set
            {
                //if (File.Exists(value))
                //{
                    pbxImage.ImageLocation = value;
                    //pbxImage.Load();
                //}
                //else
                //{
                //    MessageBox.Show("Image doesn't exist!");
                //}
            } }

        [Description("Name of the item"), Category("Data")]
        public string Description
        {
            get { return lblItemText.Text; }
            set
            {
                lblItemText.Text = value;
            }
        }

        public ToolPanelItem()
        {
            InitializeComponent();
        }
    }
}
