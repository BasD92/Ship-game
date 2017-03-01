using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace shipGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Ship _ship;
        private Dock _dock;

        private List<Buoy> _buoy;
        private Battleship _battleship;

        private Random _random;

        private List<Chest> _chest;

        private float _seconds; 

        private int _numberChest;
        private int _totalGold;

        private SoundEffect _crash;
        private SoundEffect _coins;

        private SpriteFont _verdana;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _ship = new Ship(new Vector2(10, 150), 4);
            _dock = new Dock(new Vector2(650, 0), Content.Load<Texture2D>("Dock"));

            _buoy = new List<Buoy>();
            _buoy.Add(new Buoy(new Vector2(300, 200), Content.Load<Texture2D>("Buoy")));
            _buoy.Add(new Buoy(new Vector2(250, 500), Content.Load<Texture2D>("Buoy")));

            _battleship = new Battleship(new Vector2(450, 200), new Vector2(0, 3));

            _random = new Random();

            _chest = new List<Chest>();

            _seconds = 0;

            _totalGold = 0;

            _verdana = Content.Load<SpriteFont>("Verdana");

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _ship.Texture = Content.Load<Texture2D>("Ship");
            _battleship.Texture = Content.Load<Texture2D>("Battleship");

            _crash = Content.Load<SoundEffect>("Ship-crash");
            _coins = Content.Load<SoundEffect>("Coins-sound");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            //Ship updates
            _ship.Input();
            _ship.checkBounds(GraphicsDevice.Viewport);

            //Collision between Ship and Dock
            if (_ship.BoundingBox().Intersects(_dock.BoundingBox()))
            {
                _dock.UnloadChests();

                _totalGold = _numberChest * 42;
            }

            //Collision tussen Ship en Buoy
            foreach (Buoy b in _buoy)
            {
                if (_ship.BoundingBox().Intersects(b.BoundingBox()))
                {
                    b.Collision();

                    //New begin position ship
                    _ship.Position = new Vector2(10, 150);

                    //Number of chests to 0
                    _numberChest = 0;

                    //Total gold to 0
                    _totalGold = 0;

                    //Crash sound
                    _crash.Play();
                }
            }

            //Battleship updates
            _battleship.Move();
            _battleship.checkBounds(GraphicsDevice.Viewport);

            //Collision between Ship and Battleship
            if (_ship.BoundingBox().Intersects(_battleship.BoundingBox()))
            {
                Console.WriteLine("Schip botst tegen gevechtsship!");

                //New begin position ship
                _ship.Position = new Vector2(10, 150);

                //Number of chests to 0
                _numberChest = 0;

                //Total gold to 0
                _totalGold = 0;

                //Crash sound
                _crash.Play();
            }

            //Collision between Ship and Chest
            for (int i = 0; i < _chest.Count; i++)
            {
                if (_ship.BoundingBox().Intersects(_chest[i].BoundingBox()))
                {
                    _chest[i].CatchChests();

                    //Remove chest
                    _chest.RemoveAt(i);

                    //Count chests
                    _numberChest += 1;

                    //Coins sound
                    _coins.Play();
                }
            }

            _seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Every 5 seconds add a chest in list (_chest)
            if (_seconds >= 5)
            {
                _seconds = 0;
                if (_chest.Count < 10)
                {
                    _chest.Add(new Chest(new Vector2(_random.Next(10, 600), _random.Next(10, 550)), Content.Load<Texture2D>("Chest")));
                }
            }

                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            _dock.Draw(spriteBatch);
            _ship.Draw(spriteBatch);

            foreach (Buoy b in _buoy)
            {
                b.Draw(spriteBatch);
            }

            _battleship.Draw(spriteBatch);

            foreach (Chest c in _chest)
            { 
                c.Draw(spriteBatch);
            }

            spriteBatch.DrawString(_verdana, "Schatkisten: " + _numberChest, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(_verdana, "Goudstukken: " + _totalGold, new Vector2(10, 40), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
