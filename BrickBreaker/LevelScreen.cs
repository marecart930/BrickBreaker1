using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class LevelScreen : UserControl
    {
        public LevelScreen()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GameScreen.level = "firstLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
            gs.BackgroundImage = Properties.Resources.andysRoom;
            gs.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            GameScreen.level = "secLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
            gs.BackgroundImage = Properties.Resources.pizzaplanet_disney_ok_1523876897;
            gs.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void levelBox3_Click(object sender, EventArgs e)
        {
            GameScreen.level = "thirdLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
            gs.BackgroundImage = Properties.Resources.clawMachine;
            gs.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void levelBox4_Click(object sender, EventArgs e)
        {
            GameScreen.level = "fourthLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);

            gs.BackgroundImage = Properties.Resources.venting;
            gs.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void levelBox5_Click(object sender, EventArgs e)
        {
            GameScreen.level = "fifthLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
            gs.BackgroundImage = Properties.Resources.zergFight;
            gs.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void levelBox6_Click(object sender, EventArgs e)
        {
            GameScreen.level = "sixthLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
            gs.BackgroundImage = Properties.Resources.sidsRoomBackground;
            gs.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void levelBox7_Click(object sender, EventArgs e)
        {
            GameScreen.level = "seventhLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
            gs.BackgroundImage = Properties.Resources.TruckBackground;
        }

        private void levelBox8_Click(object sender, EventArgs e)
        {
            GameScreen.level = "eighth Level";
            
            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);

            gs.BackgroundImage = Properties.Resources.TrashBackground;
        }

        private void levelBox9_Click(object sender, EventArgs e)
        {
            GameScreen.level = "ninthLevel";

            //go to gamescreen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
            gs.BackgroundImage = Properties.Resources.lotso;

        }
    }
}
