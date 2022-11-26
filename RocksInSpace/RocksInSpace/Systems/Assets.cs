using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocksInSpace.Systems
{
    public class Assets
    {
        private ContentManager Content = GameManager.Content;

        #region Audio

        #endregion

        #region Textures

        public Texture2D missingTexture;

        public Texture2D whiteTexture;

        #endregion

        #region Font

        public SpriteFont spriteFont;

        #endregion

        #region Shaders

        public Effect distortShader;
        float currentStrength = 0.2f;
        float currentOffset = 0.03f;

        public Effect playerShader;

        #endregion

        public Assets()
        {
            Load();
        }

        public void Load()
        {
            spriteFont = Content.Load<SpriteFont>("Font");
            missingTexture = Content.Load<Texture2D>("missing");
            whiteTexture = TextureUtilities.CreateSquareTexture(2, 2, Color.White);

            distortShader = Content.Load<Effect>("lens_distort");
            distortShader.Parameters["strength"].SetValue(currentStrength);
            distortShader.Parameters["offset"].SetValue(currentOffset);

            playerShader = Content.Load<Effect>("player_ex");
        }
    }
}
