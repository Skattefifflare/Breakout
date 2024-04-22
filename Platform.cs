using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout {
    internal class Platform : GameObj {

        float speed;
        float acceleration;
        bool goingRight;

        public Platform(Vector2 Apos, Vector2 Ascale, float Aacceleration) {
            pos = Apos;
            scale = Ascale;
            tex = Texture2D.FromFile(Game1.gd, "imgs/block.png");
            speed = 0;
            acceleration = Aacceleration;
            goingRight = false;

        }

        public void Update(KeyboardState Akstate) {
            if (Akstate.IsKeyDown(Keys.Left)) {

            }
            if (speed < 400) {
                speed += acceleration;
            }

        }

        public void Draw() {

        }
    }
}
