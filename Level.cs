using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using static System.Formats.Asn1.AsnWriter;


namespace Breakout {
    internal class Level {
        public int levelindex;
        public List<Block> blocks;

        public Level(int Alevelindex) {
            levelindex = Alevelindex;

            blocks = new List<Block>();          
        }

        public void CreateBlocks() {
            Vector2 scale = new Vector2(2f, 1f);
            
            for (int i = 0; i < Helper.screenwidth / 40 * scale.X; i++) {
                for (int j = 0; j < 6; j++) {

                    blocks.Add(new Block(new Vector2(i, j), scale));
                   // Thread.Sleep(50);
                }
            }
        }
        public void CreateBlock(float Ax, float Ay, Vector2 Ascale) {
            blocks.Add(new Block(new Vector2(Ax, Ay), Ascale));
        }
        public void Draw(Effect shader) {
            shader.Parameters["time"].SetValue(Helper.totalgametime);
            
            foreach (Block block in blocks) {               
                shader.Parameters["isMagic"].SetValue(block.isMagic);
                shader.Parameters["R"].SetValue(block.RGB.Item1/255);
                shader.Parameters["G"].SetValue(block.RGB.Item2 / 255);
                shader.Parameters["B"].SetValue(block.RGB.Item3 / 255);

                block.Draw(shader);
                
            }           
        }
    }
}
