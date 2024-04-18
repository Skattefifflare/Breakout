using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout {
    internal class Ball : GameObj{

        public Vector2 dir;       
        
        public Ball(Vector2 Apos, Vector2 Adir, float Ascale) {
            pos = Apos;
            dir = Adir;
            tex = Texture2D.FromFile(Game1.gd, "imgs/boll2.png");
            scale = new Vector2(Ascale, Ascale);
        }

        public void Draw() {
            sb.Begin();
            sb.Draw(tex ,pos, null,  Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            sb.End();
        }
        
        private void WallCollision(float width, float height) {
            if (pos.X < 0 || pos.X + tex.Width > width) {
                pos -= dir * Helper.gametime;
                dir.X *= -1;
            }
            if (pos.Y < 0 || pos.Y + tex.Height > height) {
                dir.Y *= -1;
            }
        }

        public void Update() {
            pos += dir * Helper.gametime;
            WallCollision(Helper.screenwidth, Helper.screenheight);
        }
        
    }
}
