using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace Breakout {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public static GraphicsDevice gd;
        

        SpriteFont font;
        Ball ball;
        Effect blockshader;
        Level level1;

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
           
            ball = new Ball(new Vector2(10, 300), new Vector2(200, 150));
            
            blockshader = Content.Load<Effect>("blockshader");
            level1 = new Level(1);
            
        }


        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Helper.gametime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Helper.totalgametime += Helper.gametime;

            ball.Update();

            foreach (var block in level1.blocks) {
                if(SphereAABBCollision(ref ball, block)) {
                    level1.blocks.Remove(block);
                    break;
                }
            }

            base.Update(gameTime);
        }

        bool SphereAABBCollision(ref Ball ball, Block block) {

            float bax = ball.pos.X;
            float bay = ball.pos.Y;

            float blx = block.pos.X;
            float bly = block.pos.Y;

            float blw = Block.tex.Width * block.scale.X;
            float blh = Block.tex.Height * block.scale.Y;


            Vector2 clampPoint = new Vector2(Math.Clamp(bax+Ball.tex.Width/2, blx, blx+blw), Math.Clamp(bay+Ball.tex.Height/2, bly, bly+blh)); // den närmaste punkten till bollens mittpunkt som ligger på blockets kanter

            Vector2 ballmidpoint = new Vector2(ball.pos.X+Ball.tex.Width/2, ball.pos.Y+Ball.tex.Height/2);

            Vector2 diffVec = ballmidpoint - clampPoint; // vektorn mellan bollens mittpunkt och clamppunkten
           

            if (diffVec.Length() <= Ball.tex.Width/2) {
                ball.pos -= ball.dir*Helper.gametime;
                diffVec.Normalize();
                double diffRad = Math.Atan2(diffVec.Y, diffVec.X); // omvandla diffvektorn till radianer på enhetscirkeln

                Vector2 dirVec = Vector2.Normalize(ball.dir);
                double dirRad = Math.Atan2(dirVec.Y, dirVec.X);

                double angle = Math.Abs(diffRad - dirRad);
                dirRad += (dirRad > diffRad) ? -angle * 2 : angle * 2;

                Vector2 newDir = new Vector2((float)Math.Cos(dirRad), (float)Math.Sin(dirRad));
                newDir *= -1;

                ball.dir = newDir *ball.dir.Length();
                return true;
            }
            else {
                return false;
            }
            
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(39, 42, 53));


            ball.Draw(_spriteBatch);
            level1.Draw(_spriteBatch, blockshader);
           
            base.Draw(gameTime);
        }
    }
}
