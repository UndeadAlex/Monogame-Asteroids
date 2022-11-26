using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RocksInSpace.DataTypes;
using System;
using System.Runtime.CompilerServices;

namespace RocksInSpace.Systems
{
    public static class GameManager
    {
        public static RockSpaceGame MainGame;

        public static SpriteBatch SpriteBatch;
        public static ContentManager Content;
        public static GraphicsDeviceManager GraphicsManager;
        public static GraphicsDevice GraphicsDevice;

        public static Random Random = new Random();

        public static PlayerStats stats;

        public static Vector2Int ScreenResolution = new Vector2Int(800, 600);

        public static float deltaTime { get; private set; }

        public static void Init()
        {
            stats = new PlayerStats();
            stats.Init();
        }

        public static void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Input.Update();
            stats.Update();
        }

        public static void Draw()
        {
            stats.Draw();
        }

    }
}
