using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RocksInSpace.Systems;
using System;

namespace RocksInSpace
{
    public class PlayerStats
    {

        SpriteBatch spriteBatch = GameManager.SpriteBatch;

        public int Score { get; private set; } = 0;

        public event EventHandler OnScoreEarned;
        public void AddScore()
        {
            Score += 1;

            OnScoreEarned?.Invoke(this, EventArgs.Empty);
        }

        public int GetScore() => this.Score;

        
        public void Init()
        {

        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            spriteBatch.Begin();

            string scoreText = string.Format("Score: {0}", this.Score);
            spriteBatch.DrawString(RockSpaceGame.Assets.spriteFont, scoreText, new Vector2(20, 20), Color.White);

            spriteBatch.End();
        }
    }
}
