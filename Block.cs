using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    internal class Block {
        public Vector2 pos;     
        public Vector2 scale;
        public static Texture2D tex = Texture2D.FromFile(Game1.gd, "imgs/block.png");
        public bool isMagic;

        public Block(Vector2 Apos, Vector2 Ascale) {
            pos = Apos;
            scale = Ascale;
            if (Helper.random.Next(1, 16) == 1) {
                isMagic = true;
            }
            else {
                isMagic = false;
            }
        }
        
        public void Draw(SpriteBatch sb) {
            sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            // ingen begin och end då Block.Draw endast ska kallas via Level.Draw
            //tillåter att rita alla block i en batch
        }       
    }
}
