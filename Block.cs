using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    internal class Block : GameObj {

        public bool isMagic;

        public (float, float, float) RGB;

        public Effect shader;

        public Block(Vector2 Acoords, Vector2 Ascale) {
            tex = Texture2D.FromFile(Game1.gd, "imgs/block.png");
            scale = Ascale;
            pos = Acoords * new Vector2(tex.Width * scale.X, tex.Height * scale.Y);

            if (Helper.random.Next(1, 16) == 1) {
                isMagic = true;
            }
            else {
                isMagic = false;
            }
            RGB = (Helper.random.Next(40, 256), Helper.random.Next(40, 256), Helper.random.Next(40, 256));
        }

        override public void Draw() {
            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, shader, null);
            sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.End();
        }
    }
}
