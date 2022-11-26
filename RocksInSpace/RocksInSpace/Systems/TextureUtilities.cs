using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocksInSpace.Systems
{
    public static class TextureUtilities
    {
        public static Texture2D CreateSquareTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(GameManager.GraphicsDevice, width, height);
            Color[] colorData = new Color[width * height];

            for(int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = color;
            }

            texture.SetData(colorData);
            return texture;
        }
    }
}
