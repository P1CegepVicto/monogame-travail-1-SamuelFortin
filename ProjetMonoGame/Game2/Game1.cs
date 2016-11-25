using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject background;
        GameObject megaman;
        GameObject[] Ennemis = new GameObject[6];
        Random de = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ApplyChanges();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);

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
            megaman = new GameObject();
            megaman.estVivant = true;
            megaman.position.X = 900;
            megaman.position.Y = 900;
            megaman.sprite = Content.Load<Texture2D>("megaman.png");

            background = new GameObject();
            background.position.X = 0;
            background.position.Y = 0;
            background.sprite = Content.Load<Texture2D>("background.jpg");



            for (int i = 0; i < Ennemis.Length; i++)
            {
                
                Ennemis[i] = new GameObject();
                Ennemis[i].estVivant = true;
                Ennemis[i].position.X = 200;
                Ennemis[i].position.Y = de.Next(0,301);
                Ennemis[i].vitesse.X = de.Next(5,8);
                if (Ennemis[i] == Ennemis[0])
                {
                    Ennemis[0].sprite = Content.Load<Texture2D>("bombman.png");
                }
                if (Ennemis[i] == Ennemis[1])
                {
                    Ennemis[1].sprite = Content.Load<Texture2D>("cutman.png");
                }
                if (Ennemis[i] == Ennemis[2])
                {
                    Ennemis[2].sprite = Content.Load<Texture2D>("elecman.png");
                }
                if (Ennemis[i] == Ennemis[3])
                {
                    Ennemis[3].sprite = Content.Load<Texture2D>("fireman.PNG");
                }
                if (Ennemis[i] == Ennemis[4])
                {
                    Ennemis[4].sprite = Content.Load<Texture2D>("gutsman.png");
                }
                if (Ennemis[i] == Ennemis[5])
                {
                    Ennemis[5].sprite = Content.Load<Texture2D>("iceman.PNG");
                }
                
            }

            // TODO: use this.Content to load your game content here
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
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                megaman.position.X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                megaman.position.X += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                megaman.position.Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                megaman.position.Y += 5;
            }
            if (megaman.position.X < fenetre.Left)
            {
                megaman.position.X = fenetre.Left;
            }
            if (megaman.position.X > fenetre.Right - 87)
            {
                megaman.position.X = fenetre.Right - 87;
            }
            if (megaman.position.Y < fenetre.Top)
            {
                megaman.position.Y = fenetre.Top;
            }
            if (megaman.position.Y > fenetre.Bottom - 200)
            {
                megaman.position.Y = fenetre.Bottom - 200;
            }
            // TODO: Add your update logic here
            //UpdateEnnemis();

            base.Update(gameTime);
        }

        //public void UpdateEnnemis();
        
        //public void UpdateEnnemis();
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(megaman.sprite, megaman.position, Color.White);
            for (int j = 0; j < Ennemis.Length; j++)
            {
                spriteBatch.Draw(Ennemis[j].sprite, Ennemis[j].position, Color.White);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
