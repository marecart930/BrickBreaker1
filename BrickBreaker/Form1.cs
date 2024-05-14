using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace BrickBreaker
{
    public partial class Form1 : Form
    {
        public static System.Windows.Media.MediaPlayer backSound = new System.Windows.Media.MediaPlayer();

        public Form1()
        {
            InitializeComponent();
            backSound.Open(new Uri(Application.StartupPath + "\\Resources\\Randy Newman - You've Got A Friend In Me (minus 2).wav"));
            backSound.MediaEnded += new EventHandler(backSoundEnded);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Start the program centred on the Menu Screen
            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);

            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);
        }

        private void backSoundEnded(object sender, EventArgs e)
        {
            backSound.Stop();
            backSound.Play();
        }
    }
}