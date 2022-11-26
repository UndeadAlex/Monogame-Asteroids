using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RocksInSpace.Systems;
using RocksInSpace;

public class ProjectileBase : Entity
{
    Entity Owner;

    Vector2 heading;

    float lifetime = 5f;
    float lifeTimer;

    public bool isDead { get; set; }

    public ProjectileBase(Entity Owner, Vector2 Location, Vector2 Velocity, float Speed, float lifetime)
    {
        this.Owner = Owner;
        this.Location = Location;
        this.heading = Velocity;
        this.Size = new Vector2(10f);
        this.Angle = 0f;
        this.Speed = Speed;
        this.sprite = RockSpaceGame.Assets.whiteTexture;
        this.lifetime = lifetime;
    }

    public override void Update()
    {
        if (isDead)
            return;
        lifeTimer += GameManager.deltaTime;
        if (lifeTimer >= lifetime)
            isDead = true;
        this.CollisionRect = new Rectangle((int)(this.Location.X - ((sprite.Width * this.Size.X) / 2)), (int)(this.Location.Y - ((sprite.Height * this.Size.Y) / 2)), (int)(sprite.Height * this.Size.X), (int)(sprite.Height * this.Size.Y));
        this.Velocity = (this.heading * Speed * (1f + GameManager.deltaTime));
        this.Location += Velocity;

        var rocks = GameManager.MainGame.rocks;
        for (int i = 0; i < rocks.Count; i++)
        {
            if (IsOverlapping(rocks[i]))
            {
                rocks[i].Split();
                isDead = true;
            }
        }
    }
    public override void Draw()
    {
        if (isDead)
            return;

        spriteBatch.Draw(sprite, this.Location, null, Color.White, this.Angle, this.Origin, this.Size, SpriteEffects.None, 1f);
    }
}