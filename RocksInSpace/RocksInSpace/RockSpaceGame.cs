using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RocksInSpace.DataTypes;
using RocksInSpace.Systems;
using RocksInSpace.Systems.Debug;
using System;
using System.Collections.Generic;

namespace RocksInSpace
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RockSpaceGame : Game
    {
        // Core

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Assets Assets { get; private set; }

        // System

        const int MaxFPS = 144;

        // Gameplay

        public Player player;

        public List<Rock> rocks;

        // Shaders

        private RenderTarget2D shaderLayerOne;
        private RenderTarget2D shaderLayerTwo;

        // DEBUG

#if DEBUG
        FPSCounter FPSCounter;
#endif

        public RockSpaceGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                PreferredDepthStencilFormat = DepthFormat.Depth16,
                IsFullScreen = false
            };
            Window.AllowUserResizing = false;
            Window.IsBorderless = false;
            this.IsMouseVisible = true;

            graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / MaxFPS);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = "TNAAR | Totally Not An Asteroids Remake";

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            GameManager.MainGame = this;
            GameManager.SpriteBatch = spriteBatch;
            GameManager.Content = this.Content;
            GameManager.GraphicsManager = this.graphics;
            GameManager.GraphicsDevice = this.GraphicsDevice;
            GameManager.ScreenResolution = new Vector2Int(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            GameManager.Init();

            Assets = new Assets();

#if DEBUG
            FPSCounter = new FPSCounter();
#endif


            // TODO: Add your gameplay initialization logic here

            // Wrote a whole new version of Vector2 for sole ints, to avoid having to cast.
            // why did i do this to my self.
            shaderLayerOne = new RenderTarget2D(GraphicsDevice, GameManager.ScreenResolution.X, GameManager.ScreenResolution.Y);
            shaderLayerTwo = new RenderTarget2D(GraphicsDevice, GameManager.ScreenResolution.X, GameManager.ScreenResolution.Y);

            Vector2 screenCenter = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            // Spawn Player and call Init method.
            player = new Player(screenCenter, 2.5f, 0f, Vector2.One * 0.2f);
            player.Init();

            rocks = new List<Rock>();

            // Create 15 rock objects [Limited to 15 for the assignment for performance]
            int rocksToSpawn = 15;
            for (int i = 0; i < rocksToSpawn; i++)
            {
                Vector2 loc = new Vector2((float)GameManager.Random.NextDouble() * graphics.PreferredBackBufferWidth, (float)GameManager.Random.NextDouble() * graphics.PreferredBackBufferHeight);

                rocks.Add(new Rock(loc, 0f, 100f));
                rocks[i].Init();
            }

            base.Initialize();
        }

        #region LOAD/UNLOAD CONTENT

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        #endregion

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GameManager.Update(gameTime);

#if DEBUG
            FPSCounter.Update(GameManager.deltaTime);
#endif

            // Using custom input script.
            if (Input.GetGamepadButtonDown(Buttons.Back) || Input.GetKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your gameplay update logic here

            for (int i = 0; i < rocks.Count; i++)
            {
                rocks[i].Update();
            }

            player.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(shaderLayerOne);
            GraphicsDevice.Clear(Color.Black);
            

            // TODO: Add your gameplay drawing code here

            foreach(Rock rock in rocks)
            {
                rock.Draw();
            }
            
            player.Draw();

#if DEBUG
            FPSCounter.Draw();
#endif


            GraphicsDevice.SetRenderTarget(null);
            //GraphicsDevice.SetRenderTarget(shaderLayerTwo);


            spriteBatch.Begin(effect: Assets.distortShader);
            spriteBatch.Draw(shaderLayerOne, Vector2.Zero, shaderLayerOne.Bounds, Color.White);
            spriteBatch.End();

            GameManager.Draw();

            //GraphicsDevice.SetRenderTarget(null);

            //spriteBatch.Begin(effect: testShader);
            //spriteBatch.Draw(shaderLayerTwo, Vector2.Zero, shaderLayerTwo.Bounds, Color.White);
            //spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
