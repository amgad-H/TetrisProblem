using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    //Class where game objects interactions is handled
    internal class GameState
    {
        private Block currentBlock;
        
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for(int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }
        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
        }

        //Checks if block is at legal position
        private bool BlockFits()
        {
            bool blockFits = true;
            foreach(Position p in CurrentBlock.TilesPosition())
            {
                if(!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    blockFits = false;
                }
            }

            return blockFits;
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        //Check if 2 at the top hidden rows have ANY of their cells occupied to end the game
        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        //Visualizes the block falling down
        //When block is set, it orders rows to be checked and cleared, then it gets another block
        //Stops game if game is over
        private void PlaceBlock()
        {
            foreach(Position p in CurrentBlock.TilesPosition())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score += GameGrid.ClearFullRows();
            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
        }

        //Moves the actual block object
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        //Calculates the drop distance of a tile of a block
        private int TileDropDistance(Position p)
        {
            int drop = 0;
            while(GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }
            return drop;
        }

        //Calculates drop distance of a whole block
        public int BlockDropDistance()
        {
            int drop = GameGrid.Rows;
            foreach(Position p in CurrentBlock.TilesPosition())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }
            return drop;
        }

        //Moves block down as much as allowed (puts block instantly down)
        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}
