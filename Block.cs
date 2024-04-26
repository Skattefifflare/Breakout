using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    internal class Block : GameObj {

        private (float, float, float) RGB;

        public bool isMagic;

        public Block(Vector2 Acoords, Vector2 Ascale) {
            tex = Texture2D.FromFile(Game1.gd, "../../../imgs/block.png");
            scale = Ascale;
            pos = Acoords * new Vector2(tex.Width * scale.X, tex.Height * scale.Y);

            RGB = (Helper.random.Next(40, 256), Helper.random.Next(40, 256), Helper.random.Next(40, 256));
            isMagic = (Helper.random.Next(1, 16) == 1) ? true : false;            
        }

        override public void Draw() {
            Helper.shader.Parameters["R"].SetValue(RGB.Item1 / 255);
            Helper.shader.Parameters["G"].SetValue(RGB.Item2 / 255);
            Helper.shader.Parameters["B"].SetValue(RGB.Item3 / 255);
            Helper.shader.Parameters["isMagic"].SetValue(isMagic);

            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, Helper.shader, null);

            sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.End();
        }
    }
}
