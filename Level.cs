using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;


namespace Breakout {
    internal class Level {
        public int levelindex;

        protected SpriteBatch sb = new SpriteBatch(Game1.gd);


        public List<Block> blocks;
        public Platform platform;
        public Ball ball;

        public int lives;
        public Texture2D heart;

        bool doCollision;

        public Level(int Alevelindex) {
            levelindex = Alevelindex;

            blocks = new List<Block>();

            lives = 3;
            heart = Texture2D.FromFile(Game1.gd, "imgs/heart.png");

            doCollision = true;
        }

        public void Draw(Effect shader) {
            shader.Parameters["time"].SetValue(Helper.totalgametime);
            foreach (Block block in blocks) {
                shader.Parameters["isMagic"].SetValue(block.isMagic);
                shader.Parameters["R"].SetValue(block.RGB.Item1 / 255);
                shader.Parameters["G"].SetValue(block.RGB.Item2 / 255);
                shader.Parameters["B"].SetValue(block.RGB.Item3 / 255);

                block.Draw();
            }

            ball.Draw();
            platform.Draw();
            sb.Begin();
            for (int i = 0; i < lives; i++) {
                sb.Draw(heart, new Vector2(60*i, 540), null, Color.White, -(float)(Math.PI/4), Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
            }
            sb.End();
        }

        
        void SpherePlatformCollision() {
            if (doCollision) {
                if (ball.pos.Y + ball.tex.Height * ball.scale.Y >= platform.pos.Y) {
                    if (ball.pos.X + ball.tex.Width * ball.scale.X >= platform.pos.X && ball.pos.X <= platform.pos.X + platform.tex.Width * platform.scale.X) {
                        ball.dir.Y *= -1;
                    }
                    else {
                        doCollision = false;
                    }
                }
            }
        }
        public void LevelReader() {
            string path = $"levels/level{levelindex}.txt";
            var lines = File.ReadAllLines(path);

            using (StreamReader sr = new StreamReader(path)) {

                var neededwidth = Helper.screenwidth / lines[0].Count();
                var scaleX = neededwidth / 40;
                Vector2 scale = new Vector2(scaleX, scaleX/2);

                platform = new Platform(new Vector2(400 - 20 * 4, 540), new Vector2(4*scaleX, 0.5f*scaleX), 20);//4, 0.4f
                ball = new Ball(new Vector2(380, 480), new Vector2(200, 340), 0.6f*scaleX); // tot speed måste vara < 800

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

        public void Update(KeyboardState Akstate) {
            ball.Update();
            platform.Update(Akstate);
            SpherePlatformCollision();

            if (ball.WallCollision()) {
                lives--;
                doCollision = true;
                if (lives == 0) {
                    //game over
                }
            }

        }
    }
}
