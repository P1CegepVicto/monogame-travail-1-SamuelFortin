using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projet1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject anana;
        Rectangle fenetre;
        GameObject crayon;
        GameObject pomme;
        GameObject backg;
        GameObject gameover;

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
            this.graphics.ToggleFullScreen();
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
            anana = new GameObject();
            anana.estVivant = true;
            anana.position.X = 900;
            anana.position.Y = 900;
            anana.sprite = Content.Load<Texture2D>("anana.png");

            pomme = new GameObject();
            pomme.estVivant = true;
            pomme.position.X = 960;
            pomme.position.Y = 200;
            pomme.vitesse.X = 4;
            pomme.sprite = Content.Load<Texture2D>("pomme.png");

            crayon = new GameObject();
            crayon.position.X = pomme.position.X;
            crayon.position.Y = pomme.position.Y;
            crayon.vitesse.Y = 10;
            crayon.sprite = Content.Load<Texture2D>("crayon.png");

            
            backg = new GameObject();
            backg.position.X = 0;
            backg.position.Y = 0;
            backg.sprite = Content.Load<Texture2D>("jungle.jpg");

            gameover = new GameObject();
            gameover.position.X = 0;
            gameover.position.Y = 0;
            gameover.sprite = Content.Load<Texture2D>("gameover.jpg");
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
                anana.position.X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                anana.position.X += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                anana.position.Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                anana.position.Y += 5;
            }
            if (anana.position.X < fenetre.Left)
            {
                anana.position.X = fenetre.Left;
            }
            if (anana.position.X > fenetre.Right - 87)
            {
                anana.position.X = fenetre.Right - 87;
            }
            if (anana.position.Y < fenetre.Top)
            {
                anana.position.Y = fenetre.Top;
            }
            if (anana.position.Y > fenetre.Bottom - 200)
            {
                anana.position.Y = fenetre.Bottom - 200;
            }



            // TODO: Add your update logic here
            Updateanana();
            Updatepomme();
            Updatecrayon();

            base.Update(gameTime);
        }


        public void Updatepomme()
        {

            if (pomme.position.X > fenetre.Right - 101)
            {
                pomme.vitesse.X = -4;
            }
            if (pomme.position.X < fenetre.Left)
            {
                pomme.vitesse.X = +4;
            }

            pomme.position += pomme.vitesse;
            
        }

        public void Updateanana()
        {
            if (anana.GetRect().Intersects(crayon.GetRect()))
            {
                anana.estVivant = false;
            }
            
        
        
        }
        public void Updatecrayon()
        {
            if (crayon.position.Y > fenetre.Bottom)
            {
                crayon.position.Y = pomme.position.Y;
                crayon.position.X = pomme.position.X;
            }

            crayon.position += crayon.vitesse;
        }
        //public void Updatecrayon();
        //{
        
        
        
        //}
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backg.sprite, backg.position, Color.White);
            spriteBatch.Draw(pomme.sprite, pomme.position, Color.White);

            if (anana.estVivant == true)
            {
                spriteBatch.Draw(anana.sprite, anana.position, Color.White);
                
            }
            
            spriteBatch.Draw(crayon.sprite, crayon.position, Color.White);

            if (anana.estVivant == false)
            {
                spriteBatch.Draw(gameover.sprite, gameover.position, Color.White);

            }


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
