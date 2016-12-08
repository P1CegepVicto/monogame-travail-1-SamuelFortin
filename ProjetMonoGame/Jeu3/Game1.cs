using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Jeu3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject hero;
        GameObject[] ennemis = new GameObject[3];
        GameObject ammo;
        GameObject backg1;
        GameObject backg2;
        GameObject gameover;
        GameObject start;
        SoundEffect son;
        SoundEffectInstance pew;
        Random de = new Random();
        SpriteFont font;
        int Vie = 10;
        int finalscore;
        int Points = 0;
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

            Song song = Content.Load<Song>("Sounds\\megamantitle");
            MediaPlayer.Play(song);
            
            font = Content.Load<SpriteFont>("Font");

            start = new GameObject();
            start.estVivant = true;

            son = Content.Load<SoundEffect>("Sounds\\fire");
            pew = son.CreateInstance();

            hero = new GameObject();
            
            hero.sprite = Content.Load<Texture2D>("megaman.png");

            backg1 = new GameObject();
            
            backg1.sprite = Content.Load<Texture2D>("backg.jpg");

            backg2 = new GameObject();
            
            backg2.sprite = Content.Load<Texture2D>("backg.jpg");

            gameover = new GameObject();
            gameover.position.X = 0;
            gameover.position.Y = 0;
            gameover.sprite = Content.Load<Texture2D>("gameover.jpg");

            ammo = new GameObject();
            
            ammo.sprite = Content.Load<Texture2D>("fireball.png");

            for (int i = 0; i < ennemis.Length; i++)
            {
                ennemis[i] = new GameObject();
                
            }
            reboot();
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
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                hero.position.Y -= 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                hero.position.Y += 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R) && Vie <= 0)
            {
                reboot();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                start.estVivant = false;
            }
            if (hero.position.Y < fenetre.Top)
            {
                hero.position.Y = fenetre.Top;
            }
            if (hero.position.Y > fenetre.Bottom - 156)
            {
                hero.position.Y = fenetre.Bottom - 156;
            }

            // TODO: Add your update logic here
            if (start.estVivant == false)
            {
                Updatebackg();
                Updateammo();
                Updateennemis();
                Updatehero();
            }
                
                
            
            

            base.Update(gameTime);
        }

        public void Updatebackg()
        {
            backg1.position.X -= 3;
            backg1.position.X += backg1.vitesse.X;

            if (backg1.position.X < 0)
            {
                backg2.position.X = backg1.position.X + backg1.sprite.Width;
            }
            if (backg1.position.X >= 0)
            {
                backg2.position.X = backg1.position.X - backg1.sprite.Width;
            }

            if (backg2.position.X < 0)
            {
                backg1.position.X = backg2.position.X + backg2.sprite.Width;
            }
            if (backg2.position.X >= 0)
            {
                backg1.position.X = backg2.position.X - backg2.sprite.Width;
            }
        }
        public void Updatehero()
        {
            if (Vie <= 0)
            {
                hero.estVivant = false;
            }
        }

        public void Updateammo()
        {
            if (hero.estVivant == true)
            {
                if (ammo.position.X > fenetre.Right)
                {
                        ammo.position.Y = hero.position.Y;
                        ammo.position.X = hero.position.X;
                        pew.Play();
                }
            
                for (int i = 0; i < ennemis.Length; i++)
                {
                    if (ammo.GetRect().Intersects(ennemis[i].GetRect()))
                    {
                        ammo.position.Y = hero.position.Y;
                        ammo.position.X = hero.position.X;
                        ennemis[i].position.X = 1900;
                        ennemis[i].position.Y = de.Next(0, 884);
                        if (hero.estVivant == true)
                        {
                            Points++;
                        }
                        
                        pew.Play();
                    }
                }
                ammo.position += ammo.vitesse;
            }
        }

        public void Updateennemis()
        {
            for (int i = 0; i < ennemis.Length; i++)
            {
                if (ennemis[i].position.X < fenetre.Left)
                {
                    ennemis[i].position.X = 1900;
                    ennemis[i].position.Y = de.Next(0, 884);
                }
                if (ennemis[i].GetRect().Intersects(hero.GetRect()))
                {
                    ennemis[i].position.X = 1900;
                    ennemis[i].position.Y = de.Next(0, 884);
                    Vie--;
                }
                ennemis[i].position += ennemis[i].vitesse;
            }
        }

        public void reboot()
        {
            hero.position.X = 120;
            hero.position.Y = 540;
            hero.estVivant = true;

            backg1.position.X = 0;
            backg1.position.Y = 0;
            backg1.vitesse.X = 0;

            backg2.position.X = 0;
            backg2.position.Y = 0;
            backg2.vitesse.X = 0;

            ammo.position.X = hero.position.X;
            ammo.position.Y = hero.position.Y;
            ammo.vitesse.X = 30;
            ammo.estVivant = true;

            for (int i = 0; i < ennemis.Length; i++)
            {
                
                ennemis[i].position.X = 1900;
                ennemis[i].position.Y = de.Next(0, 884);
                ennemis[i].vitesse.X = de.Next(-20, -12);
                ennemis[i].estVivant = true;
                if (ennemis[i] == ennemis[0])
                {
                    ennemis[0].sprite = Content.Load<Texture2D>("bombman.png");
                }
                if (ennemis[i] == ennemis[1])
                {
                    ennemis[1].sprite = Content.Load<Texture2D>("elecman.png");
                }
                if (ennemis[i] == ennemis[2])
                {
                    ennemis[2].sprite = Content.Load<Texture2D>("gutsman.png");
                }
            }
            Vie = 10;
            Points -= finalscore;
        }
       
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backg1.sprite, backg1.position, Color.White);
            spriteBatch.Draw(backg2.sprite, backg2.position, effects: SpriteEffects.FlipHorizontally);
            spriteBatch.Draw(hero.sprite, hero.position, Color.White);
            spriteBatch.Draw(ammo.sprite, ammo.position, Color.White);
            for (int i = 0; i < ennemis.Length; i++)
            {
                spriteBatch.Draw(ennemis[i].sprite, ennemis[i].position, Color.White);
            }
            spriteBatch.DrawString(font, "Vie : " + Vie.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(font, "Points : " + Points.ToString(), new Vector2(100, 140), Color.White);
            if (start.estVivant == true)
            {
                spriteBatch.DrawString(font, "Appuyer sur Enter pour commencer", new Vector2(760, 540), Color.White);
            }
            if (Vie <= 0)
            {
                finalscore = Points;
                spriteBatch.Draw(gameover.sprite, gameover.position, Color.White);
                spriteBatch.DrawString(font, "Score Finale: " + finalscore.ToString(), new Vector2(960, 100), Color.White);
                spriteBatch.DrawString(font, "Retry : touche R", new Vector2(960, 150), Color.White);

            }
            spriteBatch.End();
        
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
