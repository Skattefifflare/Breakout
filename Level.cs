using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;


namespace Breakout {
    internal class Level {
        public int levelindex;

        public List<Block> blocks;
        public Platform platform;
        public Ball ball;


        public Level(int Alevelindex) {
            levelindex = Alevelindex;

            blocks = new List<Block>();
            platform = new Platform(new Vector2(500, 400), new Vector2(4, 0.4f), 10);
            ball = new Ball(new Vector2(180, 400), new Vector2(300, 400), 0.7f); // tot speed måste vara < 800
        }

        public void CreateBlocks() {
            Vector2 scale = new Vector2(2f, 1f);

            for (int i = 0; i < Helper.screenwidth / 40 * scale.X; i++) {
                for (int j = 0; j < 6; j++) {

                    blocks.Add(new Block(new Vector2(i, j), scale));
                    // Thread.Sleep(50);
                }
            }
        }
        public void CreateBlock(float Ax, float Ay, Vector2 Ascale) {
            blocks.Add(new Block(new Vector2(Ax, Ay), Ascale));
        }
        public void Draw(Effect shader) {
            shader.Parameters["time"].SetValue(Helper.totalgametime);

            foreach (Block block in blocks) {
                shader.Parameters["isMagic"].SetValue(block.isMagic);
                shader.Parameters["R"].SetValue(block.RGB.Item1 / 255);
                shader.Parameters["G"].SetValue(block.RGB.Item2 / 255);
                shader.Parameters["B"].SetValue(block.RGB.Item3 / 255);

                block.Draw(shader);

            }
        }
        
        void LevelReader() {
            string path = $"levels/level{levelindex}.txt";

            //Vector2 scale = new Vector2(2f, 1f);

            using (StreamReader sr = new StreamReader(path)) {

                var t = Helper.screenwidth / sr.ReadLine().Count();


                sr.BaseStream.Seek(0, SeekOrigin.Begin);

                Vector2 scale = new Vector2(40/50, 0.5f);

                for (int r = 0; r < File.ReadAllLines(path).Count(); r++) {
                    var row = sr.ReadLine();
                    foreach(char c in row) {
                        if (c == '1') {
                            blocks.Add(new Block(new Vector2(c, r), scale));
                        }
                    }
                }
               
            }
        }
    }
}
