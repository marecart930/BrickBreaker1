using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker.Screens
{
    public partial class Pause_screen : UserControl
    {
        public Pause_screen()
        {
            InitializeComponent();
            pauselabel.Location = new Point(this.Width / 5, this.Height / 4);
        }
    }
}
