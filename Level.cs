using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout {
    internal class Level {
        public int levelindex;
        public List<Block> blocks;

        public Level(int Alevelindex) {
            levelindex = Alevelindex;

            blocks = new List<Block>();

            CreateBlocks();
        }

        void CreateBlocks() {
            Vector2 scale = new Vector2(2f, 1f);
            
            for (int i = 0; i < Helper.screenwidth / 40 * scale.X; i++) {
                for (int j = 0; j < 6; j++) {

                    blocks.Add(new Block(new Vector2(i * Block.tex.Width * scale.X, j * Block.tex.Height * scale.Y), scale));
                }
            }
        }

        public void Draw(SpriteBatch sb, Effect shader) {
            shader.Parameters["time"].SetValue(Helper.totalgametime);
            
            foreach (Block block in blocks) {               
                shader.Parameters["isMagic"].SetValue(block.isMagic);                                                
                sb.Begin(SpriteSortMode.Deferred, null, null, null, null, shader, null);
                block.Draw(sb);
                sb.End();
            }
            
        }
    }
}
