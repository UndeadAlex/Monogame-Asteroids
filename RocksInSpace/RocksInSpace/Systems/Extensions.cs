using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace RocksInSpace.Systems
{
    static class SpriteBatchExtensions
    {
        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D tex, Rectangle pixel, Vector2 begin, Vector2 end, Color color, float thick = 1)
        {
            Vector2 delta = end - begin;
            float rot = (float)Math.Atan2(delta.Y, delta.X);
            if (pixel.Width > 0) { pixel.Width = 1; pixel.Height = 1; }
            spriteBatch.Draw(tex, begin, pixel, color, rot, new Vector2(0, 0.5f), new Vector2(delta.Length(), thick), SpriteEffects.None, 0);
        }


        public static void DrawRectLines(this SpriteBatch spriteBatch, Texture2D tex, Rectangle pixel, Rectangle r, Color color, int thick = 1)
        {
            spriteBatch.Draw(tex, new Rectangle(r.X, r.Y, r.Width, thick), pixel, color);
            spriteBatch.Draw(tex, new Rectangle(r.X + r.Width, r.Y, thick, r.Height), pixel, color);
            spriteBatch.Draw(tex, new Rectangle(r.X, r.Y + r.Height, r.Width, thick), pixel, color);
            spriteBatch.Draw(tex, new Rectangle(r.X, r.Y, thick, r.Height), pixel, color);
        }

        public static void DrawSphere(this SpriteBatch spriteBatch, Vector2 location, float rotation, float radius, int points, Color color, float thickness = 1)
        {
            Texture2D tex = TextureUtilities.CreateSquareTexture(2, 2, Color.White);
            Rectangle texRect = new Rectangle(0, 0, tex.Width, tex.Height);

            Vector2 pVert = Vector2.Zero;
            for(int p = 0; p <= points; p++)
            {
                //float randX = MathHelper.Clamp((float)GameManager.Random.NextDouble(), 0.5f, 1);
                //float randY = MathHelper.Clamp((float)GameManager.Random.NextDouble(), 0.5f, 1);

                float angle = MathUtilites.Map(p, 0, points, 0, (float)(Math.PI*2)) + rotation;
                float x = (float) (radius * Math.Cos(angle));
                float y = (float) (radius * Math.Sin(angle));

                Vector2 cVert = location + new Vector2(x, y);
                if(pVert != Vector2.Zero)
                    spriteBatch.DrawLine(tex, texRect, pVert,  cVert, color, thickness);
                pVert = cVert;
            }
        }

        public static void DrawPolygon(this SpriteBatch spriteBatch, Vector2 location, float rotation, Vector2[] points, Color color, float thickness = 1)
        {
            Texture2D tex = RockSpaceGame.Assets.whiteTexture;
            Rectangle texRect = new Rectangle(0, 0, tex.Width, tex.Height);

            for(int p = 1; p < points.Length; p++)
            {
                spriteBatch.DrawLine(tex, texRect, location + points[p - 1], location + points[p], color, thickness);
            }
            spriteBatch.DrawLine(tex, texRect, location + points[points.Length - 1], location + points[0], color, thickness);
        }
    }

    public static class VectorExtentions
    {
        public static float GetMagnitude(this Vector2 vec)
        {
            return (vec.X * vec.X + vec.Y * vec.Y);
        }
        public static float GetMagnitudeSqrt(this Vector2 vec)
        {
            return (float)Math.Sqrt((vec.X * vec.X + vec.Y * vec.Y));
        }
    }

    public static class MathUtilites
    {
        public static float Map(float value, float a1, float a2, float b1, float b2)
        {
            return b1 + ((value - a1) * (b2 - b1)) / (a2 - a1);
        }

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(MathHelper.ToRadians(degree));
        }
    }
}
