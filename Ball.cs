using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout {
    internal class Ball : GameObj {

        public Vector2 dir;

        public Ball(Vector2 Apos, Vector2 Adir, float Ascale) {
            pos = Apos;
            scale = new Vector2(Ascale, Ascale);
            dir = Adir * scale;
            tex = Texture2D.FromFile(Game1.gd, "imgs/boll2.png");

        }

        public void Draw() {
            sb.Begin();
            sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.End();
        }
       

        private void WallCollision(float width, float height) {
            if (pos.X < 0 || pos.X + tex.Width * scale.X > width) {
                pos -= dir * Helper.gametime;
                dir.X *= -1;
            }
            if (pos.Y < 0 || pos.Y + tex.Height * scale.Y > height) {
                pos -= dir * Helper.gametime;
                dir.Y *= -1;
            }
        }

        public void Update() {
            pos += dir * Helper.gametime;
            WallCollision(Helper.screenwidth, Helper.screenheight);
        }

    }
}
