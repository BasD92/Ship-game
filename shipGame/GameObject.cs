using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shipGame
{
    public class GameObject
    {
        //Properties
        public Texture2D Texture;
        public Vector2 Position;

        //Fictional square around ship
        public Rectangle BoundingBox()
        {
            return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height
                );
        }

        //Constructor
        public GameObject(Vector2 _position, Texture2D _texture) 
        {
            Position = _position;
            Texture = _texture;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(    Texture,
                        Position,
                        Color.White);
        }
    }
}
