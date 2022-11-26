using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocksInSpace.Systems.Debug
{
    public class FPSCounter
    {
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;

        private Queue<float> _sampleBuffer = new Queue<float>();

        public void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
        }

        public void Draw()
        {
            string fpsString = string.Format("FPS:\n    Average: {0}\n    Current: {1}", AverageFramesPerSecond, CurrentFramesPerSecond);

            GameManager.SpriteBatch.Begin();
            GameManager.SpriteBatch.DrawString(RockSpaceGame.Assets.spriteFont, fpsString, new Vector2(5, 5), Color.White);
            GameManager.SpriteBatch.End();
        }
    }
}
