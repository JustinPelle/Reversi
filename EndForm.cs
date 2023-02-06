using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
    class EndForm : Form
    {
        ReversiGameForm GameForm;
        public EndForm(ReversiGameForm gameForm)
        {
            this.GameForm = gameForm;
            this.Winner_label = new System.Windows.Forms.Label();
            this.Rematchbutton = new System.Windows.Forms.Button();
            this.Newgame_button = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // Winner_label
            // 
            int[] winnerNums = gameForm.Game.getWinningPlayerNums();
            string playerWinnersString = winnerNums[0].ToString();
            for (int i = 1; i<winnerNums.Length; i++)
            {
                if (winnerNums[i] != 0)
                    playerWinnersString += (" and " + winnerNums[i].ToString());
            }

            this.Winner_label.AutoSize = true;
            this.Winner_label.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Winner_label.Location = new System.Drawing.Point(97, 52);
            this.Winner_label.Name = "Winner_label";
            this.Winner_label.Size = new System.Drawing.Size(156, 24);
            this.Winner_label.TabIndex = 0;
            this.Winner_label.Text = "Winner: player " + playerWinnersString; // de winnaar
            // 
            // Rematchbutton
            // 
            this.Rematchbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rematchbutton.Location = new System.Drawing.Point(72, 98);
            this.Rematchbutton.Name = "Rematchbutton";
            this.Rematchbutton.Size = new System.Drawing.Size(102, 53);
            this.Rematchbutton.TabIndex = 2;
            this.Rematchbutton.Text = "Rematch";
            this.Rematchbutton.UseVisualStyleBackColor = true;
            this.Rematchbutton.MouseClick += this.processRematchbuttonClick;
            // 
            // Newgame_button
            // 
            this.Newgame_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Newgame_button.Location = new System.Drawing.Point(180, 98);
            this.Newgame_button.Name = "Newgame_button";
            this.Newgame_button.Size = new System.Drawing.Size(102, 53);
            this.Newgame_button.TabIndex = 3;
            this.Newgame_button.Text = "New Game";
            this.Newgame_button.UseVisualStyleBackColor = true;
            this.Newgame_button.MouseClick += this.processNewgame_buttonClick;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(382, 303);
            this.Controls.Add(this.Newgame_button);
            this.Controls.Add(this.Rematchbutton);
            this.Controls.Add(this.Winner_label);
            this.Name = "EndForm";
            this.Text = "EndForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label Winner_label;
        private System.Windows.Forms.Button Rematchbutton;
        private System.Windows.Forms.Button Newgame_button;

        public void processRematchbuttonClick(object sender, MouseEventArgs mea)
        {
            this.GameForm.resetGame();
            this.GameForm.Visible = true;
            this.Close();
        }

        public void processNewgame_buttonClick(object sender, MouseEventArgs mea)
        {
            new LaunchForm().Show();
            this.GameForm.Close();
            this.Close();
        }

    }
}