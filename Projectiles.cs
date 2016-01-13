using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project
{
    class Projectiles
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;

        public Rectangle boundingRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
            set { }
        }

        public bool isVisible;

        public Projectiles(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
