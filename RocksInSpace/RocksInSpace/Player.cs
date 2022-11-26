using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RocksInSpace.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RocksInSpace
{
    public class Player : Entity
    {

        public List<ProjectileBase> projectiles;

        float turnSpeed = 2f;

        private bool useMouse = false;

        public Player(Vector2 location, float speed, float angle, Vector2 size, Texture2D sprite = null)
        {
            Location = location;
            Speed = speed;
            Angle = angle;
            Size = size;
            
            this.sprite = sprite ?? RockSpaceGame.Assets.missingTexture;
            this.Origin = new Vector2(this.sprite.Width /2, this.sprite.Height /2);
        }

        public override void Init()
        {
            sprite = GameManager.Content.Load<Texture2D>("player");
            this.Origin = new Vector2(this.sprite.Width / 2, this.sprite.Height / 2);
            projectiles = new List<ProjectileBase>();
            this.CollisionRect = new Rectangle(0, 0, sprite.Width * (int)this.Size.X, sprite.Height * (int)this.Size.Y);
        }

        public override void Update()
        {
            this.CollisionRect = new Rectangle((int)(this.Location.X - ((sprite.Width * this.Size.X) / 2)), (int)(this.Location.Y - ((sprite.Height * this.Size.Y) / 2)), (int)(sprite.Height * this.Size.X), (int)(sprite.Height * this.Size.Y));

            // Toggle movement mode.
            if (Input.GetKeyDown(Keys.F1))
                useMouse = !useMouse;

            Vector2 direction = (Input.mousePosition - this.Location);
            Vector2 dirNormal = direction;
            dirNormal.Normalize();

            if(useMouse)
            {
                float radAngle = (float)Math.Atan2(direction.Y, direction.X);
                this.Angle = radAngle + 1.5708f;
            }
            else
            {
                if(Input.GetKey(Keys.A) || Input.GetKey(Keys.Left))
                {
                    this.Angle -= MathHelper.ToRadians(turnSpeed);
                }
                if(Input.GetKey(Keys.D) || Input.GetKey(Keys.Right))
                {
                    this.Angle += MathHelper.ToRadians(turnSpeed);
                }    
            }

            if (useMouse ? Input.GetMouseButton(1) : (Input.GetKey(Keys.W) || Input.GetKey(Keys.Up)))
            {
                Vector2 velDirection = useMouse ? dirNormal : this.UpVector;
                this.Velocity = (velDirection * Speed * (1f + GameManager.deltaTime));
            }
            else
            {
                if (this.Velocity.GetMagnitude() < .1)
                    this.Velocity = Vector2.Zero;
                else
                    this.Velocity *= 0.99f;
            }

            if(useMouse ? Input.GetMouseButtonDown(0) : Input.GetKeyDown(Keys.Space))
            {
                FireWeapon();
            }

#if DEBUG
            if (Input.GetKeyDown(Keys.NumPad1))
                GameManager.stats.AddScore();
#endif

            this.Location += this.Velocity;
            IsOffscreen();

            for(int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].isDead)
                {
                    projectiles.Remove(projectiles[i]);
                    continue;
                }
                projectiles[i].Update();
            }
        }


        public override void Draw()
        {
            bool overlap = false;
            foreach (var rock in GameManager.MainGame.rocks)
            {
                if (IsOverlapping(rock))
                {
                    overlap = true;
                }
            }

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Rectangle drawRect = new Rectangle((int)this.Location.X, (int)this.Location.Y, (int)(this.sprite.Width * this.Size.X), (int)(this.sprite.Width * this.Size.Y));

            spriteBatch.Draw(sprite, drawRect, null, overlap ? Color.Red : Color.White, Angle, Origin, SpriteEffects.None, 0f);
#if DEBUG
            spriteBatch.DrawLine(RockSpaceGame.Assets.whiteTexture, new Rectangle(0, 0, 2, 2), Location, this.Location + (this.UpVector * 100), Color.White, 5);
            spriteBatch.DrawLine(RockSpaceGame.Assets.whiteTexture, new Rectangle(0, 0, 2, 2), Location, this.Location + (this.RightVector * 100), Color.White, 5);
            spriteBatch.DrawString(RockSpaceGame.Assets.spriteFont, projectiles.Count.ToString(), this.Location + new Vector2(25, 25), Color.White);
#endif


            foreach (ProjectileBase projectile in projectiles)
            {
                projectile.Draw();
            }

            base.Draw();
            spriteBatch.End();
        }

        void FireWeapon()
        {
            projectiles.Add(new ProjectileBase(this, this.Location + this.UpVector * 50, this.UpVector, 10f, 1f));
        }

        #region Setters

        public void SetLocation(Vector2 location) { Location = location; }
        public void SetSpeed(float speed) { Speed = speed; }
        public void SetRotation(float angle) { Angle = angle; }
        public void SetScale(Vector2 size) { Size = size; }
        public void SetSprite(Texture2D sprite)
        {
            this.sprite = sprite;
            this.Origin = sprite.Bounds.Center.ToVector2();
        }
        public void SetSprite(Texture2D sprite, Vector2 origin)
        {
            this.sprite = sprite;
            this.Origin = origin;
        }

        #endregion
    }
}
