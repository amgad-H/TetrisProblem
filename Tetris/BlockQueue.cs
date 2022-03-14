using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class BlockQueue
    {
        //List of all available blocks
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };

        private readonly Random random = new Random();
        public Block NextBlock { get; private set; }

        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }

        //gets a random block from the array
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        //gives the random block to output in the game,
        //if the newly generated one doesn't match with the one given before
        public Block GetAndUpdate()
        {
            Block block = NextBlock;
            do
            {
                NextBlock = RandomBlock();
            }while (block.Id == NextBlock.Id);

            return block;
        }
    }
}
