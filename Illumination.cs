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
using Illumination.Utility;

namespace Illumination {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Illumination : Microsoft.Xna.Framework.Game, ActionListener, MouseListener, KeyListener {
        GraphicsDeviceManager graphics;
        SpriteBatchRelative spriteBatch;

        InformationPanel informationPanel;
        MenuBar menuBar;

        Rectangle gameWindow;

        public Illumination() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1000;
            this.graphics.PreferredBackBufferHeight = 700;

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            gameWindow = new Rectangle(0, 0, this.graphics.GraphicsDevice.Viewport.Width,
                    this.graphics.GraphicsDevice.Viewport.Height);

            Display.InitializeDisplay(10, 10, new Rectangle(0, 25, 1000, 500));
            World.InitalizeWorld(10, 10);

            base.Initialize();

            MouseController.Initialize();
            KeyController.Initialize();

            MouseController.AddMouseListener(this);
            KeyController.AddKeyListener(this);

            informationPanel = new InformationPanel(new Rectangle(25, 525, 1000, 150));
            menuBar = new MenuBar(new Rectangle(0, 0, 1000, 25));

            Tree t1 = new Tree(2, 1);
            t1.Direction = Entity.DirectionType.East;
            World.AddEntity(t1);

            Tree t2 = new Tree(4, 5);
            t2.Direction = Entity.DirectionType.East;
            World.AddEntity(t2);

            Tree t3 = new Tree(9, 9);
            t3.Direction = Entity.DirectionType.North;
            World.AddEntity(t3);

            School s = new School(6, 6);
            World.AddEntity(s);

            Random random = new Random();
            for (int n = 2; n <= 3; n++) {
                Person p = new Person(3, n);
                p.Direction = Entity.DirectionType.South;
                if (n == 2)
                    p.Direction = Entity.DirectionType.East;
                p.Profession = (Person.ProfessionType) (random.Next() % (int) Person.ProfessionType.SIZE);
                if (p.Profession != Person.ProfessionType.Worker) {
                    p.Education = 3;
                }
                World.AddEntity(p);
            }
            Person p1 = new Person(4, 6);
            Person p2 = new Person(6, 7);
            Person p3 = new Person(4, 7);
            Person p4 = new Person(6, 1);
            p3.Profession = Person.ProfessionType.Educator;
            p3.Education = 3;
            p3.Direction = Entity.DirectionType.West;
            p2.Direction = Entity.DirectionType.West;
            World.AddEntity(p1);
            World.AddEntity(p2);
            World.AddEntity(p3);
            World.AddEntity(p4);

            Mirror m1 = new Mirror(2, 4);
            Mirror m2 = new Mirror(5, 4);
            m1.Reflection = Mirror.ReflectionType.SouthWest;
            m2.Reflection = Mirror.ReflectionType.NorthEast;
            World.AddEntity(m1);
            World.AddEntity(m2);

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
            spriteBatch = new SpriteBatchRelative(GraphicsDevice);

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
            KeyController.Update();

            if (gameWindow.Contains(new Point(MouseController.CurrentState.X, MouseController.CurrentState.Y))) {
                if (MouseController.CurrentState.X > 0.95 * gameWindow.Width) {
                    Display.TranslateViewport(3, 0);
                } else if (MouseController.CurrentState.X < 0.05 * gameWindow.Width) {
                    Display.TranslateViewport(-3, 0);
                }

                if (MouseController.CurrentState.Y > 0.95 * gameWindow.Height) {
                    Display.TranslateViewport(0, 3);
                } else if (MouseController.CurrentState.Y < 0.05 * gameWindow.Height) {
                    Display.TranslateViewport(0, -3);
                }
            }

            World.NextTimestep(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            spriteBatch.GraphicsDevice.Clear(World.IsNight ? Color.SlateBlue : Color.SkyBlue);

            spriteBatch.Begin();

            Display.DrawWorld(spriteBatch, gameTime);

            informationPanel.Draw(spriteBatch);
            menuBar.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ActionPerformed(ActionEvent evt) { }

        public void MouseClicked(MouseEvent evt) {
            Point gridLocation = Display.RelativeViewportToGridLocation(evt.CurrentLocation);

            HashSet <Entity> entities = World.GetEntities(gridLocation.X, gridLocation.Y);
            if (entities.Count > 0) {
                Entity entity = entities.First();
                if (entity.Selectable) {
                    if (!evt.IsShiftDown) {
                        World.ClearSelection();
                    }

                    if (evt.IsShiftDown && World.SelectedEntities.Contains(entity)) {
                        World.DeselectEntity(entity);
                    } else {
                        World.SelectEntity(entity);
                    }
                } else {
                    World.ClearSelection();
                }
            } else {
                World.ClearSelection();
            }

            informationPanel.UpdateDetailPanel();
        }
        public void MousePressed(MouseEvent evt) { /* Ignore */ }

        public void MouseReleased(MouseEvent evt) {
            Point gridLocation = Display.RelativeViewportToGridLocation(evt.CurrentLocation);

            if (evt.Button == MouseEvent.MouseButton.Right) {
                if (World.SelectedEntities.Count == 0) {
                    return;
                }
                if (World.SelectedEntities.Count == 1 && World.SelectedEntityType == World.EntityType.Person) {
                    if (World.IsClear(gridLocation.X, gridLocation.Y)) {
                        Person person = (Person) World.SelectedEntities.First();
                        Dictionary <Point, Person.SearchNode> range = person.ComputeMovementRange();
                        if (range.ContainsKey(gridLocation)) {
                            World.MovePerson(person, gridLocation);
                            PersonAnimation.CreateMovementAnimation(person, range[gridLocation]);

                            World.ClearSelection();
                            informationPanel.UpdateDetailPanel();
                        }
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
