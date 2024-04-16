using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Breakout {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        Ball ball;
        Block block;

        SpriteFont font;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D balltex = Texture2D.FromFile(GraphicsDevice, "imgs/boll.png");
            Texture2D blocktex = Texture2D.FromFile(GraphicsDevice, "imgs/block.png");

            ball = new Ball(new Vector2(10, 10), new Vector2(50, 50), balltex);
            block = new Block(new Vector2(50, 50), blocktex);

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            base.Update(gameTime);
        }

        void SphereAABBCollision(Ball ball, Block block) {

            float bax = ball.pos.X;
            float bay = ball.pos.Y;
            float blx = block.pos.X;
            float bly = block.pos.Y;
            float blw = block.tex.Width;
            float blh = block.tex.Height;
            // inte glömma scale


            if (bax > blx && bax < blx + blw) {
                if (bay > bly && bay < bly + blh) {
                    Vector2 clampPoint = new Vector2(Math.Clamp(bax, blx, blx+blw), Math.Clamp(bay, bly, bly+blh));
                    Vector2 diff = ball.pos - clampPoint;


                }
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(39, 42, 53));


            ball.Draw(_spriteBatch, 1f);
            block.Draw(_spriteBatch);


            base.Draw(gameTime);
        }
    }
}
