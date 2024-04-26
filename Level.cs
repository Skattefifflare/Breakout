using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace Breakout {
    internal class Level {
        protected SpriteBatch sb = new SpriteBatch(Game1.gd);

        public int levelindex;
      
        public List<Block> blocks;
        public Platform platform;
        public Ball ball;

        public int lives;
        public Texture2D heart;

        bool doCollision;


        public Level(int Alevelindex) {
            levelindex = Alevelindex;

            // for scaling based on row length in the level
            string path = $"../../../levels/level{levelindex}.txt";
            var lines = File.ReadAllLines(path);
            float neededwidth = Helper.screenwidth / lines[0].Count();
            float scaleX = neededwidth / 40;
            Vector2 scale = new Vector2(scaleX, scaleX / 2);


            blocks = new List<Block>();
            platform = new Platform(new Vector2(400 - 20 * 4, 540), new Vector2(4 * scaleX, 0.3f * scaleX));//4, 0.4f
            ball = new Ball(new Vector2(380, 480), new Vector2(200, -340), 0.6f * scaleX); // tot speed måste vara < 800


            lives = 3;
            heart = Texture2D.FromFile(Game1.gd, "../../../imgs/heart.png");

            doCollision = true;

            // adding the blocks
            using (StreamReader sr = new StreamReader(path)) {
                for (int r = 0; r < lines.Count(); r++) {
                    if (!sr.EndOfStream) {
                        var row = sr.ReadLine();

                        int col = 0;
                        foreach (char c in row) {
                            if (c == '1') {
                                blocks.Add(new Block(new Vector2(col, r), scale));
                            }
                            col++;
                        }
                    }
                }
            }
        }

        public void Draw() {
            Helper.shader.Parameters["time"].SetValue(Helper.totalgametime);
            foreach (Block block in blocks) {              
                block.Draw();
            }

            ball.Draw();
            platform.Draw();

            sb.Begin();
            for (int i = 0; i < lives; i++) {
                sb.Draw(heart, new Vector2(40*i, 570), null, Color.White, -(float)(Math.PI/4), Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
            }
            sb.End();
        }

        public void Update(KeyboardState Akstate) {
            ball.Update();
            platform.Update(Akstate);

            DoCollisions();

            if (ball.WallCollision()) {
                ball.Reset();
                platform.Reset();

                lives--;
                doCollision = true;
                if (lives == 0) {
                    //game over
                }
            }
        }

        public bool CheckWin() {
            if (blocks.Count == 0) {
                return true;
            }
            else {
                return false;
            }
        }

        private void DoCollisions() {
            CirclePlatformCollision();

            foreach (Block b in blocks) {
                if (CircleAABBCollision(ref ball, b)) {
                    if (b.isMagic) {
                        int randomPower = Helper.random.Next(0, 3);
                        switch (randomPower) {
                            case 0:
                                lives++; // powerup extra life
                                break;
                            case 1:
                                blocks.RemoveAll(block => block.pos.Y == b.pos.Y ); // powerup sweep
                                break;
                            case 2:
                                ball.scale *= 2f; // powerup large ball
                                break;
                        }

                    }
                    blocks.Remove(b);
                    break;
                }
            }
        }

        private void CirclePlatformCollision() {
            if (doCollision) {
                if (ball.pos.Y + ball.tex.Height * ball.scale.Y >= platform.pos.Y) {
                    if (ball.pos.X + ball.tex.Width * ball.scale.X >= platform.pos.X && ball.pos.X <= platform.pos.X + platform.tex.Width * platform.scale.X) {
                        
                        ball.dir.Y *= -1;

                        float ballmidX = ball.pos.X + ball.tex.Width * ball.scale.X / 2;
                        float platformmidX = platform.pos.X + platform.tex.Width*platform.scale.X / 2;
                        float distance = Math.Abs(platformmidX - ballmidX);

                    }
                    else {
                        doCollision = false;
                    }
                }
            }
        }

        private bool CircleAABBCollision(ref Ball ball, Block block) {

            // for easier reading:
            float bax = ball.pos.X;
            float bay = ball.pos.Y;

            float blx = block.pos.X;
            float bly = block.pos.Y;

            float blw = block.tex.Width * block.scale.X;
            float blh = block.tex.Height * block.scale.Y;


            // standard Circle vs AABB collision checking
            Vector2 ballmidpoint = new Vector2(bax + ball.tex.Width * ball.scale.X / 2, bay + ball.tex.Height * ball.scale.Y / 2);
            ballmidpoint -= ball.dir * Helper.gametime;
            Vector2 clampPoint = new Vector2(Math.Clamp(ballmidpoint.X, blx, blx + blw), Math.Clamp(ballmidpoint.Y, bly, bly + blh)); // den närmaste punkten till bollens mittpunkt som ligger på blockets kanter          
            Vector2 diffVec = clampPoint - ballmidpoint;


            if (diffVec.Length() <= ball.tex.Width * ball.scale.X / 2) {

                // converting the direction vector to a radian and mirroring then reversing it along the vector between clamp and midpoint
                ball.pos -= ball.dir * 1.8f * Helper.gametime;
                diffVec.Normalize();
                double diffRad = Math.Atan2(diffVec.Y, diffVec.X);

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
            // returns if a collision has happened

        }
    }
}
