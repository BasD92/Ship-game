using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shipGame
{
    class Battleship
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Speed;

        public Rectangle sourceRectangle;

        public float Scale;
        public float Rotation;
        public Vector2 Origin;

        //Fictional square around ship
        public Rectangle BoundingBox()
        {
            return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                sourceRectangle.Width,
                sourceRectangle.Height
                );
        }

        //Constructor
        public Battleship(Vector2 _position, Vector2 _speed)
        {
            Position = _position;
            Speed = _speed;

            //Draw part of the whole texture
            sourceRectangle = new Rectangle(0, 0, 161, 161);

            Scale = 1;
            Rotation = 0;
            Origin = Vector2.Zero;
        }

        public void Move()
        {
            Position += Speed;
        }

        //Change direction when battleship collide top and bottom of the screen
        public void checkBounds(Viewport viewport)
        {
            if (Position.Y < 0)
            {
                Speed.Y *= -1;
                sourceRectangle = new Rectangle(0, 0, 161, 161);
            }

            if (Position.Y + sourceRectangle.Height > viewport.Bounds.Bottom)
            {
                Speed.Y *= -1;
                sourceRectangle = new Rectangle(0, 483, 161, 161);
            }
        }

        //Draw battleship to screen
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(    Texture, 
                        Position, 
                        sourceRectangle, 
                        Color.White, 
                        Rotation, 
                        Origin, 
                        Scale, 
                        SpriteEffects.None, 
                        0f);
        }
    }
}
