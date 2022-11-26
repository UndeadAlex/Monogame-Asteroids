using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RocksInSpace.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace RocksInSpace
{
    public class Entity
    {
        public Vector2 Location { get; protected set; }
        public Vector2 Velocity { get; protected set; }
        public float Speed { get; protected set; }
        public float Angle { get; protected set; }
        public Vector2 Size { get; protected set; }
        public Vector2 Origin { get; protected set; }
        public Rectangle CollisionRect { get; protected set; }
        public Vector2 UpVector
        {
            get
            {
                return MathUtilites.RadianToVector2(this.Angle + MathHelper.ToRadians(-90f));
            }
        }
        public Vector2 RightVector 
        { 
            get
            {
                return MathUtilites.RadianToVector2(this.Angle);
            }
        }

        protected SpriteBatch spriteBatch = GameManager.SpriteBatch;
        protected Texture2D sprite = RockSpaceGame.Assets.missingTexture;



        public virtual void Init()
        {

        }
        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
#if DEBUG
            spriteBatch.DrawRectLines(RockSpaceGame.Assets.whiteTexture, new Rectangle(0, 0, 2, 2), this.CollisionRect, Color.White);
#endif
        }


        public virtual bool IsOffscreen()
        {
            int screenWidth = 100, screenHeight = 100;

            screenWidth = GameManager.ScreenResolution.X;
            screenHeight = GameManager.ScreenResolution.Y;

            float newX = this.Location.X, newY = this.Location.Y;

            if (this.Location.X > screenWidth)
                newX = 0;
            else if (this.Location.X < 0)
                newX = screenWidth;

            if (this.Location.Y > screenHeight)
                newY = 0;
            else if (this.Location.Y < 0)
                newY = screenHeight;

            this.Location = new Vector2(newX, newY);
            return true;
        }

        public virtual bool IsOffscreen(int screenWidth, int screenHeight)
        {
            float newX = this.Location.X, newY = this.Location.Y;

            if (this.Location.X > screenWidth)
                newX = 0;
            else if (this.Location.X < 0)
                newX = screenWidth;

            if (this.Location.Y > screenHeight)
                newY = 0;
            else if (this.Location.Y < 0)
                newY = screenHeight;

            this.Location = new Vector2(newX, newY);
            return true;
        }

        public virtual bool IsOverlapping(Entity overlapEntity)
        {
            if (this.CollisionRect.Intersects(overlapEntity.CollisionRect))
                return true;
            else
                return false;
        }
    }
}
