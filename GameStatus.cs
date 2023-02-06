using System;
using System.Linq;

namespace Reversi
{
    // ENUM OF MAIN GAME-STATUSSES
    public enum GameStatus
    {
        NEXT_TURN,
        INVALID_PLACEMENT,
        NO_OPTIONS_LEFT_TURN_SKIPPED,
        GAME_OVER
    }


    // CLASS HANDLING THE GAME STATUS OF THE REVERSI GAME AND (TURN/PLAYERS BASED) STATISTICS
    public class ReversiGameStatus
    {

        public GameStatus gameStatus;
        public int playersAmt, gameTurn, playerTurn;

        public int playersMax = 4;
        private string[] gameStatusStrings = new string[4] {
            "Next turn",
            "Invalid placement, try again",
            "No options left, turn skip",
            "Game over"
        };

        public ReversiGameStatus(int playersAmt = 2)
        {
            if (playersAmt > playersMax)
                throw new Exception("Assumption-error: 4 players max allowed");
            this.playersAmt = playersAmt;
            this.gameTurn = 1;
            this.playerTurn = 1;
            this.gameStatus = GameStatus.NEXT_TURN;
        }

        // get the status of the game in string format (w.r.t current game-status enum)
        public string getGameStatusString() => this.gameStatusStrings[(int)this.gameStatus];

        // increment both the game- and player-turn; reset (cycle) player-turn if it exceeds total player amount
        public void IncrementTurns()
        {
            this.gameTurn += 1;
            int nextPlayerTurn = this.playerTurn + 1;
            bool playerTurnOverflow = nextPlayerTurn > this.playersAmt;
            this.playerTurn = playerTurnOverflow ? nextPlayerTurn - this.playersAmt : nextPlayerTurn;
        }


        // function handling the cycle to the next player after a turn has taken place;
        // including game- and player-turn incrementing game-status updating and checking for end-game (game-over) and turn-skip conditions
        public bool cycleCheckNextPlayers(ReversiGame game)
        {
            // switch to next player; increment (game/player) turn and set game status to next turn
            this.IncrementTurns();
            this.gameStatus = GameStatus.NEXT_TURN;

            // retrieve the valid placement tile locations of the next player
            ReversiGamePlayer nextPlayer = game.Players[this.playerTurn - 1];
            int[][] placementTiles = game.Board.getValidPlacementLocs(nextPlayer.playerTile);

            // check for and process turn-skip and end-game (game-over) conditions;
            // skip turn of next player if it has no valid placement tile options and
            // game-over if every player has their turn skipped (return false if game-over, else true)
            int turnsSkipped = 0;
            while (placementTiles.Count((v) => (v is Array)) == 0)
            {
                if (turnsSkipped >= game.Status.playersAmt)
                {
                    this.gameStatus = GameStatus.GAME_OVER;
                    return false;
                }

                this.gameStatus = GameStatus.NO_OPTIONS_LEFT_TURN_SKIPPED;
                this.IncrementTurns();
                turnsSkipped += 1;

                nextPlayer = game.Players[this.playerTurn - 1];
                placementTiles = game.Board.getValidPlacementLocs(nextPlayer.playerTile);
            }
            return true;

        }

        // function handling cycling to the next player (+skipturn/gameover conditions)
        // and processing the next-players turn if it is a computer-player
        public bool updateGameTurn(ReversiGame game)
        {
            bool notGameOver = this.cycleCheckNextPlayers(game);
            if (notGameOver)
            {
                var nextPlayer = game.Players[this.playerTurn - 1];
                if (nextPlayer is ReversiGameComputerPlayer)
                    return ((ReversiGameComputerPlayer)nextPlayer).processComputerPlayerTurn(game);
            }
            return notGameOver;
        }

    }
}
