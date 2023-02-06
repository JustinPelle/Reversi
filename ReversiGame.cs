using System.Linq;

namespace Reversi
{
    // MAIN GAME CLASS HANDLING THE INTERACTION WITH THE LOGIC OF THE REVERSI GAME
    public class ReversiGame
    {
        public ReversiGameStatus Status;
        public ReversiGamePlayer[] Players;
        public ReversiBoard Board;

        public ReversiGame(int tableWidth, int tableHeight, int nPlayers, int nPcPlayers)
        {
            int nTotalPlayers = nPlayers + nPcPlayers;
            this.Board = new ReversiBoard(tableWidth, tableHeight, playersAmt: nTotalPlayers);
            this.Status = new ReversiGameStatus(playersAmt: nTotalPlayers); ;
            this.Players = new ReversiGamePlayer[nTotalPlayers];

            GameTile playerTile;
            int startTiles;
            for (int playerNum = 1; playerNum <= nTotalPlayers; playerNum++)
            {
                playerTile = (GameTile)playerNum;
                startTiles = this.Board.countTiles(playerTile);
                if (playerNum <= nPlayers)
                    this.Players[playerNum - 1] = new ReversiGamePlayer(playerTile, startTiles);
                else
                    this.Players[playerNum - 1] = new ReversiGameComputerPlayer(playerTile, startTiles);
            }
        }

        // method handling the game turn of an interactive (non-pc) player
        public bool processGamePlayerTurn(int i, int j)
        {
            // return negatively if game-over
            if (this.Status.gameStatus == GameStatus.GAME_OVER)
                return false;

            // retrieve current player and his corresponding game tile
            ReversiGamePlayer currentPlayer = this.Players[this.Status.playerTurn - 1];
            GameTile playerTile = currentPlayer.playerTile;

            // process player tile placement by retrieving neighbors, then checking if placement is valid
            // - and if valid, place the tile and convert valid neighbor opponent lines;
            // lastly update the player- and game statuses and statistics
            int[][] neighbors = this.Board.getNeighborOpponentLocs(i, j, playerTile);
            if (this.Board.placeTileIfValid(i, j, playerTile, neighbors))
            {
                this.Board.convertValidNeighborLines(i, j, playerTile, neighbors);

                for (int playerIndex = 0; playerIndex < this.Status.playersAmt; playerIndex++)
                    this.Players[playerIndex].updateStatistics(this);
                bool notGameOver = this.Status.updateGameTurn(this);
                return notGameOver;
            }
            else
                this.Status.gameStatus = GameStatus.INVALID_PLACEMENT;

            // return positively if game not over
            return true;
        }


        // get the player-numbers of the winners having the most tiles on the board at the moment of calling (typically at game-over status)
        public int[] getWinningPlayerNums()
        {
            int[] tileCounts = new int[4];
            int tileCount, maxCount = 0;
            for (int i = 0; i < this.Status.playersAmt; i++)
            {
                tileCount = this.Board.countTiles(this.Players[i].playerTile);
                tileCounts[i] = tileCount;
                maxCount = tileCount > maxCount ? tileCount : maxCount;
            }
            int[] playerNums = new int[4];
            int numIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                if (tileCounts[i] == maxCount)
                {
                    playerNums[numIndex] = (int)this.Players[i].playerTile;
                    numIndex += 1;
                }
            }
            return playerNums;
        }
    }


}