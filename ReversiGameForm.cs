using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;



namespace Reversi
{

    // MAIN FORM FOR DISPLAYING AND INTERACTING WITH THE REVERSI GAME
    class ReversiGameForm : Form
    {
        int tileSize;
        public ReversiGame Game;

        public PictureBox GameFrame;
        public Panel[] PlayerPanels;
        public Panel StatusPanel;

        public bool hintsEnabled = false;

        public Color[] playerColors = new Color[4] { Color.Blue, Color.Orange, Color.Purple, Color.ForestGreen };

        public Font baseFont = new Font("Calibri", 10);
        public Font headerFont = new Font("Calibri", 11, FontStyle.Bold);


        public ReversiGameForm(int tableWidth, int tableHeight, int nPlayers, int nPcPlayers, int tileSize = 60)
        {
            // set primary instance variables
            this.tileSize = tileSize;
            this.Game = new ReversiGame(tableWidth, tableHeight, nPlayers, nPcPlayers);

            // determine sizes to be used for form and frames configuration
            int playerPanelWidth = 150,
                statusPanelHeight = 150;
            Size gameFrameSize = new Size(tableWidth * tileSize, tableHeight * tileSize);

            Size clientSize = new Size(2 * playerPanelWidth + gameFrameSize.Width, statusPanelHeight + gameFrameSize.Height);
            Size playerPanelSize = new Size(playerPanelWidth, clientSize.Height / 2);
            Size statusPanelSize = new Size(gameFrameSize.Width, statusPanelHeight);

            // initialize form components
            this.InitializeComponents(tableWidth, tableHeight, gameFrameSize, clientSize, playerPanelSize, statusPanelSize);
        }

        // initialize form components
        public void InitializeComponents(int tableWidth, int tableHeight, Size gameFrameSize, Size clientSize, Size playerPanelSize, Size statusPanelSize)
        {
            // set form properties
            this.Text = "Reversi Game";
            this.ClientSize = clientSize;


            // set game-frame picturebox by setting bitmap game background to its Image and drawing foreground in paint event
            this.GameFrame = new PictureBox();
            this.GameFrame.Location = new Point(150, 0);
            this.GameFrame.ClientSize = gameFrameSize;
            this.GameFrame.Image = (Image)makeGameBackground(tableWidth, tableHeight, gameFrameSize, Color.Black, Color.White);
            this.GameFrame.Paint += this.drawGameForeground;
            this.GameFrame.MouseClick += this.processGameFrameClick;


            // set status panel to display game statuses and hints- and newgame buttons
            this.StatusPanel = new Panel();
            this.StatusPanel.Location = new Point(playerPanelSize.Width + 1, gameFrameSize.Height);
            statusPanelSize.Width -= 2;
            this.StatusPanel.Size = statusPanelSize;
            this.StatusPanel.Paint += this.updateStatusPanel;

            int buttonWidth = statusPanelSize.Width / 3,
                buttonWidthOffset = buttonWidth / 3;

            Button hintsButton = new Button();
            hintsButton.Text = "Hints";
            hintsButton.Location = new Point(buttonWidthOffset, 80);
            hintsButton.Size = new Size(buttonWidth, 50);
            hintsButton.MouseClick += (object s, MouseEventArgs mea) => {
                this.hintsEnabled = this.hintsEnabled ? false : true;
                this.resetFormControls();
            };

            Button newGameButton = new Button();
            newGameButton.Text = "New game";
            newGameButton.Location = new Point(2 * buttonWidthOffset + buttonWidth, 80);
            newGameButton.Size = new Size(buttonWidth, 50);
            newGameButton.MouseClick += (object s, MouseEventArgs mea) => {
                new EndForm(this).Show();
                this.Visible = false;
            };

            this.StatusPanel.Controls.Add(hintsButton);
            this.StatusPanel.Controls.Add(newGameButton);


            // set player panels to display player statistics
            this.PlayerPanels = new Panel[4];
            Point[] playerPanelLocations = new Point[4]
            {
                    new Point(0,0),
                    new Point(playerPanelSize.Width+gameFrameSize.Width, 0),
                    new Point(1, playerPanelSize.Height),
                    new Point(playerPanelSize.Width + gameFrameSize.Width, playerPanelSize.Height)
            };
            for (int i = 0; i < 4; i++)
            {
                Panel playerPanel = new Panel();
                playerPanel.Location = playerPanelLocations[i];
                playerPanel.Size = playerPanelSize;
                playerPanel.BorderStyle = BorderStyle.FixedSingle;
                if (i < this.Game.Status.playersAmt)
                    playerPanel.Paint += this.updatePlayerPanel;
                this.PlayerPanels[i] = playerPanel;
            }

            // add the main controls to the form
            this.Controls.Add(this.GameFrame);
            this.Controls.Add(this.StatusPanel);
            this.Controls.AddRange(this.PlayerPanels);

        }


        // process a click on the gameframe
        public void processGameFrameClick(object sender, MouseEventArgs mea)
        {
            if (this.Game.Status.gameStatus == GameStatus.GAME_OVER)
            {
                new EndForm(this).Show();
                this.Visible = false;
                return;
            }
            int i = (int)mea.Y / this.tileSize,
                j = (int)mea.X / this.tileSize;
            this.Game.processGamePlayerTurn(i, j);
            this.resetFormControls();
        }

        // reset game (with same table board dimensions and player amount)
        public void resetGame()
        {
            int nPcPlayers = this.Game.Players.Count((v) => v is ReversiGameComputerPlayer);
            int nPlayers = this.Game.Status.playersAmt - nPcPlayers;
            this.Game = new ReversiGame(this.Game.Board.width, this.Game.Board.height, nPlayers, nPcPlayers);
            this.resetFormControls();
        }

        // flush main game form controls
        public void resetFormControls()
        {
            this.GameFrame.Invalidate();
            this.StatusPanel.Invalidate();
            for (int i = 0; i < 4; i++)
                this.PlayerPanels[i].Invalidate();
        }



        // create bitmap with background-color fill and foreground-color rasters
        public static Bitmap makeGameBackground(int tableWidth, int tableHeight, Size frameSize, Color bgColor, Color fgColor)
        {
            int h = frameSize.Height, w = frameSize.Width;
            Bitmap bmp = new Bitmap(w, h);
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                gfx.FillRectangle(bgBrush, 0, 0, w, h); // fill background
                rasterizeGameBackground(gfx, new Pen(fgColor), tableWidth, tableHeight, frameSize); // make table rasters
            }
            return bmp;
        }

        // draw rasterized graphics (onto the game backgound bitmap)
        public static void rasterizeGameBackground(Graphics gfx, Pen pen, int tableWidth, int tableHeight, Size frameSize)
        {
            int tileHeight = frameSize.Height / tableHeight,
                tileWidth = frameSize.Width / tableWidth;
            int lineY, lineX;
            for (int i = 1; i < tableHeight; i++)
            {
                lineY = i * tileHeight;
                gfx.DrawLine(pen, 0, lineY, frameSize.Width, lineY);
            }
            for (int j = 1; j < tableWidth; j++)
            {
                lineX = j * tileWidth;
                gfx.DrawLine(pen, lineX, 0, lineX, frameSize.Height);
            }
        }

        // draw the foreground of the game frame corresponding to the board tiles and player colors
        public void drawGameForeground(object sender, PaintEventArgs pea)
        {
            Graphics gfx = pea.Graphics;
            GameTile playerTile;
            SolidBrush[] playerBrushes = new SolidBrush[4]
            {
                new SolidBrush(this.playerColors[0]), new SolidBrush(this.playerColors[1]),
                new SolidBrush(this.playerColors[2]), new SolidBrush(this.playerColors[3])
            };
            int h = this.Game.Board.height, w = this.Game.Board.width;
            for (int i = 0; i < h; i++) for (int j = 0; j < w; j++)
                {
                    playerTile = this.Game.Board.Table[i, j];
                    if (playerTile != GameTile.EMPTY)
                        gfx.FillEllipse(playerBrushes[(int)playerTile - 1], j * this.tileSize + 1, i * this.tileSize + 1, tileSize - 2, tileSize - 2);
                }
            if (this.hintsEnabled)
            {
                this.drawCurrentValidPlayerTiles(gfx);
            }
        }

        // draw the hints of current player's valid placement tiles
        public void drawCurrentValidPlayerTiles(Graphics gfx)
        {
            int playerIndex = this.Game.Status.playerTurn - 1;
            ReversiGamePlayer currentPlayer = this.Game.Players[playerIndex];
            int[][] placementLocs = this.Game.Board.getValidPlacementLocs(currentPlayer.playerTile);
            for (int locIndex = 0; locIndex < placementLocs.GetLength(0); locIndex++)
            {
                if (placementLocs[locIndex] is Array)
                {
                    int[] loc = placementLocs[locIndex];
                    int i = loc[0], j = loc[1];
                    gfx.DrawEllipse(Pens.White, j * this.tileSize, i * this.tileSize, this.tileSize, this.tileSize);
                }
            }
        }

        // determine brush and variables to draw the status panel
        public void updateStatusPanel(object sender, PaintEventArgs pea)
        {

            GameStatus gameStatus = this.Game.Status.gameStatus;
            Brush statusBrush;
            if (gameStatus == GameStatus.NEXT_TURN)
                statusBrush = Brushes.Green;
            else if (gameStatus == GameStatus.INVALID_PLACEMENT)
                statusBrush = Brushes.Red;
            else if (gameStatus == GameStatus.NO_OPTIONS_LEFT_TURN_SKIPPED)
                statusBrush = Brushes.Gold;
            else if (gameStatus == GameStatus.GAME_OVER)
                statusBrush = Brushes.Crimson;
            else
                statusBrush = Brushes.Black;
            int playerTurn = this.Game.Status.playerTurn;
            this.drawStatusPanel(pea.Graphics, playerTurn, statusBrush, new SolidBrush(this.playerColors[playerTurn - 1]));
        }

        // draw the current status of the game
        public void drawStatusPanel(Graphics gfx, int playerTurn, Brush statusBrush, Brush playerBrush)
        {
            gfx.DrawString("Status: " + this.Game.Status.getGameStatusString(), headerFont, statusBrush, 15, 15);
            gfx.DrawString("Player turn: " + playerTurn.ToString(), baseFont, playerBrush, 15, 30);
            gfx.DrawString("Game turn: " + this.Game.Status.gameTurn.ToString(), baseFont, Brushes.Black, 15, 45);
        }

        // determine brush and player to draw the player panel
        public void updatePlayerPanel(object sender, PaintEventArgs pea)
        {
            Graphics gfx = pea.Graphics;
            int playerNum = Array.IndexOf(this.PlayerPanels, (Panel)sender) + 1;
            SolidBrush playerBrush = new SolidBrush(this.playerColors[playerNum - 1]);
            ReversiGamePlayer player = this.Game.Players[playerNum - 1];
            this.drawPlayerPanel(gfx, player, playerBrush);
        }

        // draw specific player side-panel depending on sender
        public void drawPlayerPanel(Graphics gfx, ReversiGamePlayer player, SolidBrush playerBrush)
        {
            GameTile playerTile = player.playerTile;
            gfx.FillEllipse(playerBrush, 15, 15, 15, 15);
            gfx.DrawString("Player " + ((int)playerTile).ToString(), headerFont, playerBrush, 34, 13);
            gfx.DrawString("Tiles current: " + this.Game.Board.countTiles(playerTile).ToString(), baseFont, playerBrush, 15, 50);
            gfx.DrawString("Tiles placed: " + player.tilesPlaced, baseFont, playerBrush, 15, 68);
            gfx.DrawString("Tiles won: " + player.tilesWon, baseFont, playerBrush, 15, 86);
            gfx.DrawString("Tiles lost: " + player.tilesLost, baseFont, playerBrush, 15, 104);
        }

    }
}
