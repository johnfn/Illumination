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
using Illumination.Components.Panels;

namespace Illumination {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Illumination : Microsoft.Xna.Framework.Game, ActionListener, MouseListener, KeyListener {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyController keyController;

        InformationPanel informationPanel;
        MenuBar menuBar;

        public Illumination() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 600;
            this.graphics.PreferredBackBufferHeight = 700;

            this.IsMouseVisible = true;
        }

        /* FOR TESTING ANIMATION EVENT */
        public class TrickyHelloWorld : FrameEvent
        {
            public override void DoEvent(Animation animation)
            {
                Console.WriteLine("Hello World");
            }

            public override bool IsTriggered()
            {
                return isTriggered;
            }

            public override void MarkTriggered()
            {
                isTriggered = true;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            Display.InitializeDisplay(10, 10, new Rectangle(50, 25, 500, 500));
            World.InitalizeWorld(10, 10);

            base.Initialize();

            MouseController.Initialize();
            MouseController.AddMouseListener(this);

            keyController = new KeyController();
            keyController.AddKeyListener(this);

            informationPanel = new InformationPanel(new Rectangle(0, 525, 600, 175));
            menuBar = new MenuBar(new Rectangle(0, 0, 600, 25));


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

            //Animation a2 = Display.CreateAnimation(MediaRepository.Textures["Worker"], new Point(p2.BoundingBox.X, p2.BoundingBox.Y), new Dimension(p2.BoundingBox.Width, p2.BoundingBox.Height), 2);
            //a2.AddTranslationFrame(new Point(p2.BoundingBox.X + Display.TileWidth, p2.BoundingBox.Y), 2);
            
            /* Frame by frame manipulation demo 
            Animation a1 = Display.CreateAnimation(MediaRepository.Textures["Blank"], new Point(25, 25), new Dimension(50, 50), 5.5);
            a1.SetRelativeOrigin(new Vector2(25, 25));

            a1.AddTranslationFrame(new Point(25, 175), 0, Animation.EaseType.Out);
            a1.AddTranslationFrame(new Point(425, 175), 2);
            a1.AddTranslationFrame(new Point(125, 325), 3, Animation.EaseType.In);
            
            a1.AddExtensionFrame(new Dimension(50, 50), 2);
            a1.AddExtensionFrame(new Dimension(150, 150), 3);
            
            a1.AddColorFrame(Color.White, 2);
            a1.AddColorFrame(new Color(0, 0, 255, 0), 5); // Time order does not matter
            a1.AddColorFrame(new Color(0, 0, 255, 255), 3);
            
            a1.AddRotationFrame((float)(Math.PI * 5), 3);
            a1.AddRotationFrame((float)(Math.PI * 0), 5);

            a1.AddEventFrame(new TrickyHelloWorld(), 2);

            // Ease in and out demo + Sprite sheet demo 
            Animation a3 = Display.CreateAnimation(MediaRepository.Sheets["Glow"], new Point(0, 0), new Dimension(100, 100), 12, 0.1);
            a3.AddTranslationFrame(new Point(0, 0), 0, Animation.EaseType.InAndOut);
            a3.AddTranslationFrame(new Point(400, 0), 3, Animation.EaseType.InAndOut);
            a3.AddTranslationFrame(new Point(0, 0), 6, Animation.EaseType.InAndOut);
            a3.AddTranslationFrame(new Point(400, 0), 9, Animation.EaseType.InAndOut);

            a3.AddColorFrame(Color.White, 9);
            a3.AddColorFrame(Color.TransparentWhite, 12);
            */
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
            MouseController.Update();
            keyController.Update();

            World.NextTimestep(gameTime);

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
            //menuButton.Draw(spriteBatch);
            
            //spriteBatch.DrawString(MediaRepository.Fonts["DefaultFont"], "Press 'D' for Day ... Press 'N' for Night", new Vector2(100, 510), Color.White);

            informationPanel.Draw(spriteBatch);
            menuBar.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ActionPerformed(ActionEvent evt) {
        }

        public void MouseClicked(MouseEvent evt) {
            Point gridLocation = Display.ViewportToGridLocation(evt.CurrentLocation);
            if (!World.InBound(gridLocation)) {
                return;
            }

            World.RemoveAllHighlight();

            HashSet <Entity> entities = World.GetEntities(gridLocation.X, gridLocation.Y);
            if (entities.Count > 0) {
                Entity entity = entities.First();
                if (entity is Person && entity.Selectable) {
                    World.SelectedEntity = entity;
                    Person thisPerson = (Person) entity;
                    Dictionary <Point, Person.SearchNode> range = thisPerson.ComputeMovementRange();
                    World.AddHighlight(range.Keys);
                } else if (entity is Tree && entity.Selectable) {
                    World.SelectedEntity = entity;
                    World.AddHighlight(gridLocation.X, gridLocation.Y);
                } else {
                    World.SelectedEntity = null;
                    World.RemoveAllHighlight();
                }
            } else {
                World.SelectedEntity = null;
                World.RemoveAllHighlight();
            }
        }
        public void MousePressed(MouseEvent evt) { /* Ignore */ }

        public void MouseReleased(MouseEvent evt) {
            if (evt.Button == MouseEvent.MouseButton.Right) {
                Point gridLocation = Display.ViewportToGridLocation(evt.CurrentLocation);
                if (World.SelectedEntity is Person && World.IsClear(gridLocation.X, gridLocation.Y)) {
                    Dictionary <Point, Person.SearchNode> range = ((Person) World.SelectedEntity).ComputeMovementRange();
                    if (range.ContainsKey(gridLocation)) {
                        World.MovePerson((Person) World.SelectedEntity, gridLocation);
                        PersonAnimation.CreateMovementAnimation((Person) World.SelectedEntity, range[gridLocation]);
                        World.SelectedEntity = null;
                        World.RemoveAllHighlight();
                    }
                }
            }
        }

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
