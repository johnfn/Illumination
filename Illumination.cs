using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Illumination.Logic;
using Illumination.Data;
using Illumination.Logic.MouseHandler;
using Illumination.Components;
using Illumination.Logic.ActionHandler;
using Illumination.WorldObjects;
using Illumination.Graphics;
using Illumination.Logic.KeyHandler;
using Illumination.Graphics.AnimationHandler;
using SpriteSheetRuntime;

namespace Illumination {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Illumination : Microsoft.Xna.Framework.Game, ActionListener, MouseListener, KeyListener {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseController mouseController;
        KeyController keyController;
        Button menuButton;

        public Illumination() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 500;
            this.graphics.PreferredBackBufferHeight = 550;

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            Display.InitializeDisplay(10, 10, 500, 500);
            World.InitalizeWorld(10, 10);

            base.Initialize();

            mouseController = new MouseController();
            mouseController.AddMouseListener(this);

            keyController = new KeyController();
            keyController.AddKeyListener(this);

            menuButton = new Button("Menu", MediaRepository.Fonts["DefaultFont"], new Vector2(20, 510), mouseController);
            menuButton.AddActionListener(this);

            World.CreateTree(5, 5);
            Tree t1 = World.CreateTree(2, 1);
            t1.Direction = Entity.DirectionType.East;
            Tree t2 = World.CreateTree(9, 9);
            t2.Direction = Entity.DirectionType.North;
            World.CreateBuilding(6, 6, "Illumination.WorldObjects.School");

            Random random = new Random();
            for (int n = 2; n <= 9; n++)
            {
                Person p = World.CreatePerson(2, n);
                p.Direction = Entity.DirectionType.South;
                if (n == 3) p.Direction = Entity.DirectionType.North;
                p.Profession = (Person.ProfessionType)(random.Next() % (int)Person.ProfessionType.SIZE);
            }
            Person p2 = World.CreatePerson(6, 4);
            p2.Direction = Entity.DirectionType.West;

            Animation a1 = Display.CreateAnimation(MediaRepository.Textures["Blank"], new Rectangle(0,0,50,50), 3);
            a1.AddTranslation(new Rectangle(400, 400, 50, 50));
            a1.AddFading(255, 100);

            Animation a2 = Display.CreateAnimation(MediaRepository.Textures["TreeOfLight"], new Rectangle(100, 200, 50, 50), 3);
            a2.AddRotation(0, 6.28f);

            Animation a3 = Display.CreateAnimation(MediaRepository.Sheets["Glow"], new Rectangle(200, 200, 100, 100), 4, 0.1);
            a3.AddTranslation(new Rectangle(400, 200, 10, 10));
            a3.AddFading(255, 0);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MediaRepository.LoadAll(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            mouseController.Update();
            keyController.Update();

            KeyboardState keyboardState = Keyboard.GetState();

            // TODO: Add your update logic here
            World.NextTimestep();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            spriteBatch.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            

            Display.DrawWorld(spriteBatch, gameTime);
            spriteBatch.Draw(MediaRepository.Textures["Blank"], new Rectangle(0, 500, 500, 50), Color.DarkGreen);
            menuButton.Draw(spriteBatch);
            spriteBatch.DrawString(MediaRepository.Fonts["DefaultFont"], "Press 'D' for Day ... Press 'N' for Night", new Vector2(100, 510), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ActionPerformed(ActionEvent evt) {
            if (evt.InvokingComponent == menuButton) {
                Console.WriteLine("Menu triggered");
            }
        }

        public void MouseClicked(MouseEvent evt) {
            Point gridLocation = Display.ViewportToGridLocation(evt.CurrentLocation);

            //World.CreateBuilding(gridLocation.X, gridLocation.Y, "Illumination.WorldObjects.School");
            HashSet <Entity> entities = World.GetEntities(gridLocation.X, gridLocation.Y);
            if (entities.Count > 0) {
                Entity entity = entities.First();
                if (entity is Person) {
                    Person thisPerson = (Person) entity;
                    HashSet <Person.SearchNode> range = thisPerson.ComputeMovementRange();
                    foreach (Person.SearchNode node in range) {
                        World.Grid[node.point.X, node.point.Y].Highlighted = true;
                    }
                }
                //        if (thisPerson.Profession < Person.ProfessionType.SIZE - 1) {
                //            thisPerson.Profession++;
                //        } else {
                //            World.RemovePerson(thisPerson);
                //        }

                //        thisPerson.Direction++;
                //        if (thisPerson.Direction >= Entity.DirectionType.SIZE)
                //        {
                //            thisPerson.Direction = Entity.DirectionType.North;
                //        }
                //    }
                //} else {
                //    World.CreatePerson(gridLocation.X, gridLocation.Y);
                //}

            }
        }
        public void MousePressed(MouseEvent evt) { /* Ignore */ }
        public void MouseReleased(MouseEvent evt) { /* Ignore */ }

        #region KeyListener Members

        public void KeysPressed(KeyEvent evt) {
            if (evt.ChangedKeys.Contains(Keys.Escape)) {
                this.Exit();
            }
            if (evt.ChangedKeys.Contains(Keys.D)) {
                if (World.IsNight)
                    World.IsNight = false;
            }

            if (evt.ChangedKeys.Contains(Keys.N)) {
                if (!World.IsNight)
                    World.IsNight = true;
            }
        }

        public void KeysReleased(KeyEvent evt) { /* Ignore */ }

        #endregion
    }
}
