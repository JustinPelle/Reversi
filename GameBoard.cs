using System;

namespace Reversi
{

    // ENUM OF MAIN GAME-TILES
    public enum GameTile
    {
        EMPTY,
        PLAYER1,
        PLAYER2,
        PLAYER3,
        PLAYER4
    }


    // CLASS HANDLING THE TABLE AND TILES OF THE REVERSI GAME AND CORRESPONDING LOGIC
    public class ReversiBoard
    {

        public int width, height;
        public GameTile[,] Table;

        public ReversiBoard(int tableWidth, int tableHeight, int playersAmt)
        {

            this.width = tableWidth;
            this.height = tableHeight;
            this.Table = createLoadTableTiles(tableWidth, tableHeight, playersAmt);
        }


        // #################### TABLE CREATION AND TILE LOADING #######################

        // creates and fills a 2D array with empty tiles
        private static GameTile[,] createTable(int w, int h)
        {
            GameTile[,] table = new GameTile[h, w];
            for (int i = 0; i < h; i++) for (int j = 0; j < w; j++)
                    table[i, j] = GameTile.EMPTY;
            return table;
        }

        // creates and loads a 2D table of tiles in startconfiguration dependent on even/odd width and height, and player amount
        private static GameTile[,] createLoadTableTiles(int w, int h, int playersAmt)
        {
            if (w < 4 || h < 4)
                throw new Exception("Assumption-error: can only load tile configuration with table width and -height of >=4");
            GameTile[,] table = createTable(w, h);
            if (playersAmt == 2)
                return Load2PlayersTableTiles(w, h, table);
            else if (playersAmt == 3)
                return Load3PlayersTableTiles(w, h, table);
            else if (playersAmt == 4)
                return Load4PlayersTableTiles(w, h, table);
            throw new Exception("Assumption-error: can only load table tiles for 2-4 players");
        }

        // create and load the pieces for the 2D board table for 2 players w.r.t. even/odd dimension lengths
        private static GameTile[,] Load2PlayersTableTiles(int w, int h, GameTile[,] table)
        {
            bool evenHeight = h % 2 == 0,
                evenWidth = w % 2 == 0;
            int halfHeight = h / 2,
                halfWidth = w / 2;
            if (evenHeight && evenWidth)
            {
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight, halfWidth] = GameTile.PLAYER1;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER2;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER2;
            }
            else if (!evenHeight && !evenWidth)
            {
                table[halfHeight, halfWidth] = GameTile.PLAYER1;
                table[halfHeight-1, halfWidth+1] = GameTile.PLAYER1;
                table[halfHeight-1, halfWidth] = GameTile.PLAYER2;
                table[halfHeight, halfWidth+1] = GameTile.PLAYER2;

            }
            else if (evenHeight && !evenWidth)
            {
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER1;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight, halfWidth] = GameTile.PLAYER2;
                table[halfHeight - 1, halfWidth + 1] = GameTile.PLAYER2;
            }
            else if (!evenHeight && evenWidth)
            {
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER1;
                table[halfHeight, halfWidth] = GameTile.PLAYER2;
                table[halfHeight + 1, halfWidth - 1] = GameTile.PLAYER2;
            }
            return table;
        }

        // create and load the pieces for the 2D board table for 3 players w.r.t. even/odd dimension lengths
        private static GameTile[,] Load3PlayersTableTiles(int w, int h, GameTile[,] table)
        {
            bool evenHeight = h % 2 == 0,
                evenWidth = w % 2 == 0;
            int halfHeight = h / 2,
                halfWidth = w / 2;
            if (evenHeight && evenWidth)
            {
                table[halfHeight - 1, halfWidth - 2] = GameTile.PLAYER1;
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER2;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER3;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight, halfWidth] = GameTile.PLAYER2;
                table[halfHeight, halfWidth + 1] = GameTile.PLAYER3;
            }
            else if (!evenHeight && !evenWidth)
            {
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight, halfWidth] = GameTile.PLAYER2;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER3;
                table[halfHeight + 1, halfWidth] = GameTile.PLAYER1;
                table[halfHeight + 1, halfWidth + 1] = GameTile.PLAYER2;
                table[halfHeight, halfWidth + 1] = GameTile.PLAYER3;
            }
            else if (evenHeight && !evenWidth)
            {
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER2;
                table[halfHeight - 1, halfWidth + 1] = GameTile.PLAYER3;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER3;
                table[halfHeight, halfWidth] = GameTile.PLAYER1;
                table[halfHeight, halfWidth + 1] = GameTile.PLAYER2;
            }
            else if (!evenHeight && evenWidth)
            {
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER2;
                table[halfHeight + 1, halfWidth - 1] = GameTile.PLAYER3;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER3;
                table[halfHeight, halfWidth] = GameTile.PLAYER1;
                table[halfHeight + 1, halfWidth] = GameTile.PLAYER2;
            }
            return table;
        }

        // create and load the pieces for the 2D board table for 4 players w.r.t. even/odd dimension lengths
        private static GameTile[,] Load4PlayersTableTiles(int w, int h, GameTile[,] table)
        {
            bool evenHeight = h % 2 == 0,
                evenWidth = w % 2 == 0;
            int halfHeight = h / 2,
                halfWidth = w / 2;
            if (evenHeight && evenWidth)
            {
                table[halfHeight - 2, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight - 1, halfWidth + 1] = GameTile.PLAYER2;
                table[halfHeight + 1, halfWidth] = GameTile.PLAYER3;
                table[halfHeight, halfWidth - 2] = GameTile.PLAYER4;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER1;
                table[halfHeight, halfWidth] = GameTile.PLAYER2;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER3;
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER4;
            }
            else if (!evenHeight && !evenWidth)
            {
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER2;
                table[halfHeight - 1, halfWidth + 1] = GameTile.PLAYER3;
                table[halfHeight, halfWidth + 1] = GameTile.PLAYER4;
                table[halfHeight + 1, halfWidth] = GameTile.PLAYER1;
                table[halfHeight + 1, halfWidth + 1] = GameTile.PLAYER2;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER3;
                table[halfHeight + 1, halfWidth - 1] = GameTile.PLAYER4;
            }
            else if (evenHeight && !evenWidth)
            {
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER2;
                table[halfHeight, halfWidth] = GameTile.PLAYER3;
                table[halfHeight + 1, halfWidth] = GameTile.PLAYER4;
                table[halfHeight - 2, halfWidth] = GameTile.PLAYER1;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER2;
                table[halfHeight - 1, halfWidth + 1] = GameTile.PLAYER3;
                table[halfHeight, halfWidth + 1] = GameTile.PLAYER4;
            }
            else if (!evenHeight && evenWidth)
            {
                table[halfHeight - 1, halfWidth - 1] = GameTile.PLAYER1;
                table[halfHeight - 1, halfWidth] = GameTile.PLAYER2;
                table[halfHeight, halfWidth] = GameTile.PLAYER3;
                table[halfHeight, halfWidth + 1] = GameTile.PLAYER4;
                table[halfHeight, halfWidth - 2] = GameTile.PLAYER1;
                table[halfHeight, halfWidth - 1] = GameTile.PLAYER2;
                table[halfHeight + 1, halfWidth - 1] = GameTile.PLAYER3;
                table[halfHeight + 1, halfWidth] = GameTile.PLAYER4;
            }
            return table;
        }


        // #################### TILE CHECKS #######################

        // COLLECTION OF HELPER FUNCTIONS CHECKING THE PROPERTIES OF A CERTAIN TILE ON LOCATION I-,J-;
        // POSSIBLY W.R.T. TO THE CURRENT PLAYERTILE. ALSO INCLUDED: FUNCTION COUNTING THE AMOUNT OF A CERTAIN GAMETILE ON THE BOARD.

        public bool isBoardTile(int i, int j) => (0 <= i && i < this.height) && (0 <= j && j < this.width);
        public bool checkBoardTile(int i, int j, GameTile tile) => isBoardTile(i, j) && this.Table[i, j] == tile ? true : false;


        public bool isEmptyTile(int i, int j) => checkBoardTile(i, j, GameTile.EMPTY);
        public bool isPlayerTile(int i, int j, GameTile playerTile) => !isEmptyTile(i, j) && checkBoardTile(i, j, playerTile);
        public bool isOpponentTile(int i, int j, GameTile playerTile) => !isEmptyTile(i, j) && !checkBoardTile(i, j, playerTile);

        public int countTiles(GameTile tile)
        {
            int n = 0;
            for (int i = 0; i < this.height; i++) for (int j = 0; j < this.width; j++)
                    n += this.isPlayerTile(i, j, tile) ? 1 : 0;
            return n;
        }

        // #################### NEIGHBOR RETRIEVAL #######################

        // get all valid neighbor locations (array of i-,j- tuples representing row/column indices) respective to a certain location i-,j-
        public int[][] getNeighborLocs(int i, int j)
        {
            int[][] neighbors = new int[8][]
            {
                new int[2] {i-1, j-1},
                new int[2] {i+0, j-1},
                new int[2] {i+1, j-1},
                new int[2] {i-1, j+0},
                new int[2] {i+1, j+0},
                new int[2] {i-1, j+1},
                new int[2] {i+0, j+1},
                new int[2] {i+1, j+1}
            };
            return Array.FindAll(
                neighbors,
                (int[] ij) => isBoardTile(ij[0], ij[1])
            );
        }

        // get all valid opponent neighbor location respective to a certain location i-,j-
        public int[][] getNeighborOpponentLocs(int i, int j, GameTile playerTile)
        {
            int[][] neighbors = getNeighborLocs(i, j);
            return Array.FindAll(
                neighbors,
                (int[] ij) => isOpponentTile(ij[0], ij[1], playerTile)
            );
        }


        // #################### OPPONENT-ENCLOSEMENT OPS #######################

        // checks if a tile placement of playerTile on row/col location i-,j- encloses a line of opponents with another tile of itself
        public bool enclosesOpponentLine(int i, int j, int incrI, int incrJ, GameTile playerTile)
        {
            i += incrI;
            j += incrJ;
            if (!isBoardTile(i, j))
                return false;
            GameTile tile = this.Table[i, j];
            if (tile == GameTile.EMPTY)
                return false;
            else if (tile == playerTile)
                return true;
            return enclosesOpponentLine(i, j, incrI, incrJ, playerTile);
        }

        // converts playertile -enclosed opponent neighors to the playertile;
        // throws an error if tile placement on row/col i-,j-  does not enlcose a line of opponent tiles
        public void convertEnclosedOpponentLine(int i, int j, int incrI, int incrJ, GameTile playerTile)
        {
            i += incrI;
            j += incrJ;
            if (!isBoardTile(i, j))
                throw new Exception("Assumption-error: end-of-board reached without finding enclosing playerTile");
            GameTile tile = this.Table[i, j];
            if (tile == playerTile)
                return;
            else if (tile == GameTile.EMPTY)
                throw new Exception("Assumption-error: enclosed tile to be converted can never be 0 (empty)");
            this.Table[i, j] = playerTile;
            convertEnclosedOpponentLine(i, j, incrI, incrJ, playerTile);
        }

        // function that goes through all the neighbor locations of a tile placement row/col i-,j- of playerTile, 
        // and converts them if the neighbor partains to a line of opponents being enclosed by the playertile
        public void convertValidNeighborLines(int i, int j, GameTile playerTile, int[][] neighbors)
        {
            for (int neighborIndex = 0; neighborIndex < neighbors.Length; neighborIndex++)
            {
                int[] nb = neighbors[neighborIndex];
                int incrI = nb[0] - i,
                    incrJ = nb[1] - j;
                if (enclosesOpponentLine(i, j, incrI, incrJ, playerTile))
                    convertEnclosedOpponentLine(i, j, incrI, incrJ, playerTile);
            }
        }

        // #################### VALID TILE PLACEMENT (CHECKS, GET AND SET) #######################

        // check if a placement of playertile on row/col i-,j- is valid (w.r.t. its relation to its neighbors)
        public bool isValidTilePlacement(int i, int j, GameTile playerTile, int[][] neighbors)
        {
            if (!isEmptyTile(i, j) || neighbors.Length == 0)
                return false;
            for (int neighborIndex = 0; neighborIndex < neighbors.Length; neighborIndex++)
            {
                int[] nb = neighbors[neighborIndex];
                int nbI = nb[0],
                    nbJ = nb[1];
                if (enclosesOpponentLine(i, j, nbI - i, nbJ - j, playerTile))
                    return true;
            }
            return false;
        }

        // retrieve the valid placement locations of a certain playertile (array of i-,j- tuples)
        public int[][] getValidPlacementLocs(GameTile playerTile)
        {
            int[][] placementTiles = new int[this.height * this.width][];
            int placementIndex = 0;
            for (int i = 0; i < this.height; i++) for (int j = 0; j < this.width; j++)
                {
                    int[][] neighbors = getNeighborOpponentLocs(i, j, playerTile);
                    if (isValidTilePlacement(i, j, playerTile, neighbors))
                    {
                        placementTiles[placementIndex] = new int[2] { i, j };
                        placementIndex += 1;
                    }
                }
            return placementTiles;
        }

        // place a playertile on row/col location i-,j- if this is a valid placement for that tile/turn
        public bool placeTileIfValid(int i, int j, GameTile playerTile, int[][] neighbors)
        {
            if (isValidTilePlacement(i, j, playerTile, neighbors))
            {
                this.Table[i, j] = playerTile;
                return true;
            }
            return false;
        }
    }

}
