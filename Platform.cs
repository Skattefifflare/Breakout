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
            if (speed < 600) {
                speed += acceleration;
            }

            if (Akstate.IsKeyDown(Keys.Left)) {
                if (goingRight) {
                    speed = 100;
                    goingRight = false;
                }
                pos.X -= speed * Helper.gametime;
            }

            else if (Akstate.IsKeyDown(Keys.Right)) {
                if (!goingRight) {
                    speed = 100;
                    goingRight = true;
                }
                pos.X += speed * Helper.gametime;
            }
            else {
                speed = 100;
            }
            WallCollision();
        }
        private void WallCollision() {
            if (pos.X < 0) {
                pos.X = 0;
            }
            if (pos.X + tex.Width * scale.X > Helper.screenwidth) {
                pos.X = Helper.screenwidth- tex.Width*scale.X;
            }           
        }
       

        // gör till gemensam
        public void Reset() {
            //pos = OGpos;
        }
    }
}
