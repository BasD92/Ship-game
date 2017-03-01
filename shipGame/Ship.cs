using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shipGame
{
    class Ship
    {
        //properties
        public Texture2D Texture;
        public Vector2 Position;
        public int Speed;

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
        public Ship(Vector2 _position, int _speed)
        {
            Position = _position;
            Speed = _speed;
        }

        //Input method Ship
        public void Input()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Position = new Vector2(Position.X, Position.Y - 1 * Speed);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Position = new Vector2(Position.X, Position.Y + 1 * Speed);
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + 1 * Speed, Position.Y);
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X - 1 * Speed, Position.Y);
            }
        }

        //Check bounds screen
        public void checkBounds(Viewport viewport)
        {
            if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, 0);
            }

            if (Position.Y + Texture.Height > viewport.Bounds.Bottom)
            {
                Position = new Vector2(Position.X, viewport.Bounds.Bottom - Texture.Height);
            }

            if (Position.X + Texture.Height < viewport.Bounds.Left)
            {
                Position = new Vector2(0, Position.Y);
            }

            if (Position.X + Texture.Height > viewport.Bounds.Right)
            {
                Position = new Vector2(viewport.Bounds.Right - Texture.Width, Position.Y);
            }
        }

        //Draw ship to screen
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(    Texture,
                        Position,
                        Color.White);
        }
    }
}
