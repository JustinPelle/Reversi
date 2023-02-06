using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
    class Program
    {
        static void Main(string[] args) => Application.Run(new LaunchForm());
    }

    public partial class LaunchForm : Form
    {

        ReversiGameForm gameForm;
        public LaunchForm()
        {
            InitializeComponent();
            NewGame_button.MouseClick += startGame;
        }

        public void startGame(object sender, MouseEventArgs mea)
        {
            int tableWidth, tableHeight, nPlayers, nPcPlayers;
            try
            {
                tableWidth = int.Parse(Columns_textbox.Text);
                tableHeight = int.Parse(Rows_textbox.Text);
                nPlayers = int.Parse(Players_textBox.Text);
                nPcPlayers = int.Parse(CPUPlayers_textBox.Text);
                if (tableWidth < 4 || tableHeight < 4)
                    throw new Exception("table size too small");
                else if (nPlayers + nPcPlayers > 4)
                    throw new Exception("total player amount too large");
                else if (nPlayers < 1)
                    throw new Exception("should at least select 1 non-computer player");
            }
            catch (Exception)
            {
                NewGame_button.ForeColor = Color.Red;
                return;
            }
            this.gameForm = new ReversiGameForm(tableWidth, tableHeight, nPlayers, nPcPlayers);
            this.gameForm.Show(this);
            this.Visible = false;
        }


    }


}
