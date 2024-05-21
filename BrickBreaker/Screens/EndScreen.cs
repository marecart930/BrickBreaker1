﻿using System;
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
    public partial class EndScreen : UserControl
    {
        public EndScreen()
        {
            InitializeComponent();
            if (GameScreen.blocks.Count > 0)
            {
                pictureBox1.BackgroundImage = Properties.Resources.YOU_DIED_4_25_2024_removebg_preview;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.YOU_WIN_4_25_2024_removebg_preview;
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen
            LevelScreen ls = new LevelScreen();
            Form form = this.FindForm();

            form.Controls.Add(ls);
            form.Controls.Remove(this);

            ls.Location = new Point((form.Width - ls.Width) / 2, (form.Height - ls.Height) / 2);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Goes to the menu screen
            MenuScreen ms = new MenuScreen();
            Form form = this.FindForm();

            form.Controls.Add(ms);
            form.Controls.Remove(this);

            ms.Location = new Point((form.Width - ms.Width) / 2, (form.Height - ms.Height) / 2);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
