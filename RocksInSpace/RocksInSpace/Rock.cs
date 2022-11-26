using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RocksInSpace.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RocksInSpace
{
    public class Rock : Entity
    {
        Vector2[] polygonVerts;

        public Rock(Vector2 location, float rotation, float size)
        {
            this.Location = location;
            this.Angle = rotation;
            this.Size = new Vector2(size);
        }

        public override void Init()
        {
            polygonVerts = new Vector2[16];

            for(int i = 0; i < polygonVerts.Length; i++)
            {
                float randX = MathHelper.Clamp((float)GameManager.Random.NextDouble(), 0.6f, 1);
                float randY = MathHelper.Clamp((float)GameManager.Random.NextDouble(), 0.6f, 1);

                float angle = MathUtilites.Map(i, 0, polygonVerts.Length, 0, (float)(Math.PI * 2)) + this.Angle;
                float x = (float)((this.Size.X * randX) * Math.Cos(angle));
                float y = (float)((this.Size.Y * randY) * Math.Sin(angle));

                polygonVerts[i] = new Vector2(x, y);
            }


            float dirAngle = (float)GameManager.Random.NextDouble() * 360;
            float dirX = (float)Math.Cos(dirAngle);
            float dirY = (float)Math.Sin(dirAngle);

            this.Velocity = new Vector2(dirX, dirY);

            this.CollisionRect = new Rectangle(0, 0, (int)this.Size.X, (int)this.Size.Y);
        }

        public override void Update()
        {
            this.CollisionRect = new Rectangle((int)(this.Location.X - (this.Size.X / 2)), (int)(this.Location.Y - (this.Size.Y / 2)), (int)this.Size.X, (int)this.Size.Y);

            

#if DEBUG
            if (Input.GetKeyDown(Keys.NumPad2))
                this.Split();
#endif

            this.Location += this.Velocity;
            IsOffscreen();
        }

        public override void Draw()
        {
            spriteBatch.Begin();

            base.Draw();

            spriteBatch.DrawPolygon(this.Location, this.Angle, polygonVerts, Color.White);

            spriteBatch.End();
        }

        public void Split()
        {
            float size = this.Size.X / 2;

            GameManager.stats.AddScore();

            if(size < 25f)
            {
                GameManager.MainGame.rocks.Remove(this);
                return;
            }

            Rock r1 = new Rock(this.Location, 0f, this.Size.X / 2);
            r1.Init();
            GameManager.MainGame.rocks.Add(r1);

            Rock r2 = new Rock(this.Location, 0f, this.Size.X / 2);
            r2.Init();
            GameManager.MainGame.rocks.Add(r2);

            GameManager.MainGame.rocks.Remove(this);
        }
    }
}
