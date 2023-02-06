using System;
using System.Linq;

namespace Reversi
{

    // CLASS HANDLING A PLAYER OF THE REVERSI GAME AND ITS (TILE BASED) STATISTICS
    public class ReversiGamePlayer
    {

        public GameTile playerTile;
        public int tilesCurrent, tilesPlaced, tilesWon, tilesLost;


        public ReversiGamePlayer(GameTile playerTile, int startTiles = 2)
        {
            this.playerTile = playerTile;
            this.tilesCurrent = startTiles;
            this.tilesPlaced = 0;
            this.tilesWon = 0;
            this.tilesLost = 0;
        }

        // function that handles the update of player statistics after a turn has passed
        public void updateStatistics(ReversiGame game)
        {
            ReversiGamePlayer currentPlayer = game.Players[game.Status.playerTurn - 1];
            if (this == currentPlayer)
            {
                int tilesPlaced = 1,
                    tilesCurrent = game.Board.countTiles(this.playerTile),
                    tilesWon = (tilesCurrent - this.tilesCurrent) - tilesPlaced;
                this.tilesCurrent = tilesCurrent;
                this.tilesPlaced += tilesPlaced;
                this.tilesWon += tilesWon;

            }
            else
            {
                int tilesCurrent = game.Board.countTiles(this.playerTile),
                    tilesLost = this.tilesCurrent - tilesCurrent;
                this.tilesCurrent = tilesCurrent;
                this.tilesLost += tilesLost;
            }
        }
    }

    // CLASS HANDLING A REVERSI COMPUTER PLAYER
    public class ReversiGameComputerPlayer : ReversiGamePlayer
    {

        public ReversiGameComputerPlayer(GameTile playerTile, int startTiles = 2)
            : base(playerTile, startTiles)
        { }

        // process this players turn by selecting a valid placement tile (random) and processing its placement
        public bool processComputerPlayerTurn(ReversiGame game)
        {
            int[][] placementTiles = game.Board.getValidPlacementLocs(this.playerTile);
            int[] tile = placementTiles[new Random().Next(placementTiles.Count((v) => (v is Array)))];
            int i = tile[0], j = tile[1];
            return game.processGamePlayerTurn(i, j);

        }

    }
}
