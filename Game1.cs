using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Breakout {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public static GraphicsDevice gd;


        SpriteFont font;
        Effect blockshader;
        List<Level> levels;        
        Level level1;
        MenuButton startButton;


        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            gd = GraphicsDevice;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            Helper.screenwidth = _graphics.PreferredBackBufferWidth;
            Helper.screenheight = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font1");            
            blockshader = Content.Load<Effect>("blockshader");

            startButton = new MenuButton(new Vector2(230, 300), "START GAME", font, 4f);
            level1 = new Level(1);
            level1.LevelReader();
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Helper.gametime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Helper.totalgametime += Helper.gametime;

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            
            level1.ball.Update();
            BatchCollision();


            base.Update(gameTime);
        }

        void BatchCollision() {
            foreach (var block in level1.blocks) {
                if (SphereAABBCollision(ref level1.ball, block)) {
                    level1.blocks.Remove(block);
                    break;
                }
            }
        }
        
        bool SphereAABBCollision(ref Ball ball, Block block) {

            float bax = ball.pos.X;
            float bay = ball.pos.Y;

            float blx = block.pos.X;
            float bly = block.pos.Y;

            float blw = block.tex.Width * block.scale.X;
            float blh = block.tex.Height * block.scale.Y;


            Vector2 ballmidpoint = new Vector2(ball.pos.X + ball.tex.Width * ball.scale.X / 2, ball.pos.Y + ball.tex.Height * ball.scale.Y / 2);
            ballmidpoint -= ball.dir * Helper.gametime;
            Vector2 clampPoint = new Vector2(Math.Clamp(ballmidpoint.X, blx, blx + blw), Math.Clamp(ballmidpoint.Y, bly, bly + blh)); // den närmaste punkten till bollens mittpunkt som ligger på blockets kanter          
            Vector2 diffVec = clampPoint - ballmidpoint; // vektorn mellan bollens mittpunkt och clamppunkten


            if (diffVec.Length() <= ball.tex.Width * ball.scale.X / 2) {


                ball.pos -= ball.dir * 1.5f * Helper.gametime;
                diffVec.Normalize();
                double diffRad = Math.Atan2(diffVec.Y, diffVec.X); // omvandla diffvektorn till radianer på enhetscirkeln

                Vector2 dirVec = Vector2.Normalize(ball.dir);
                double dirRad = Math.Atan2(dirVec.Y, dirVec.X);

                double angle = Math.Abs(diffRad - dirRad);
                dirRad += (dirRad > diffRad) ? -angle * 2 : angle * 2;

                Vector2 newDir = new Vector2((float)Math.Cos(dirRad), (float)Math.Sin(dirRad));
                newDir *= -1;
                ball.dir = newDir * ball.dir.Length();

                return true;
            }
            else {
                return false;
            }


        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(39, 42, 53));

            level1.Draw(blockshader);
            //startButton.Draw();

            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, $"{level1.ball.dir}", new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
