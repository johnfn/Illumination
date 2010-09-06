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
    public class Illumination : Microsoft.Xna.Framework.Game, ActionListener, MouseListener, MouseScrollListener, KeyListener {
        GraphicsDeviceManager graphics;
        SpriteBatchRelative spriteBatch;

        InformationPanel informationPanel;
        Panel missionPanel;
        MenuBar menuBar;
        Panel worldStatsBar;

        Rectangle gameWindow;

        public Illumination() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 900;
            this.graphics.PreferredBackBufferHeight = 600;

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

            base.Initialize();

            MouseController.Initialize();
            KeyController.Initialize();

            MouseController.AddMouseListener(this);
            MouseController.AddMouseScrollListener(this);
            KeyController.AddKeyListener(this);

            Display.InitializeDisplay(new Dimension(100, 50), new Point(0, 25), new Dimension(900, 600));
            World.InitalizeWorld(12, 11);
            Layer.Initialize();

            informationPanel = new InformationPanel(new Rectangle(5, 445, 890, 150));
            missionPanel = new MissionPanel(new Rectangle(0, 25, 300, 200));
            menuBar = new MenuBar(new Rectangle(0, 0, 1000, 25));
            worldStatsBar = new WorldStatsPanel(new Rectangle(800, 25, 95, 100));

            Tree t1 = new Tree(2, 1);
            t1.Direction = Entity.DirectionType.East;
            World.AddEntity(t1);

            Tree t2 = new Tree(4, 5);
            t2.Direction = Entity.DirectionType.East;
            World.AddEntity(t2);

            Tree t3 = new Tree(9, 9);
            t3.Direction = Entity.DirectionType.North;
            World.AddEntity(t3);

            World.AddEntity(new School(6, 6));
            World.AddEntity(new Factory(8, 3));
            World.AddEntity(new ResearchCenter(1, 7));

            Random random = new Random();

            Person p0 = new Person(2, 3);
            Person p1 = new Person(4, 6);
            Person p2 = new Person(6, 7);
            Person p3 = new Person(4, 7);
            Person p4 = new Person(6, 1);
            Person p5 = new Person(6, 9);
            Person p6 = new Person(7, 9);
            p0.Direction = Entity.DirectionType.South;
            p3.Profession = Person.ProfessionType.Educator;
            p3.Education = 3;
            p3.Direction = Entity.DirectionType.West;
            p2.Direction = Entity.DirectionType.West;
            p5.Direction = Entity.DirectionType.West;
            p6.Direction = Entity.DirectionType.West;
            p5.Profession = Person.ProfessionType.Educator;
            p6.Profession = Person.ProfessionType.Scientist;
            p5.Education = 3;
            p6.Education = 3;
            World.AddEntity(p0);
            World.AddEntity(p1);
            World.AddEntity(p2);
            World.AddEntity(p3);
            World.AddEntity(p4);
            World.AddEntity(p5);
            World.AddEntity(p6);

            Mirror m1 = new Mirror(2, 4);
            Mirror m2 = new Mirror(5, 4);
            m1.Reflection = Mirror.ReflectionType.SouthWest;
            m2.Reflection = Mirror.ReflectionType.NorthEast;
            World.AddEntity(m1);
            World.AddEntity(m2);

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
                    Display.TranslateViewport(6, 0);
                } else if (MouseController.CurrentState.X < 0.05 * gameWindow.Width) {
                    Display.TranslateViewport(-6, 0);
                }

                if (MouseController.CurrentState.Y > 0.95 * gameWindow.Height) {
                    Display.TranslateViewport(0, 6);
                } else if (MouseController.CurrentState.Y < 0.05 * gameWindow.Height) {
                    Display.TranslateViewport(0, -6);
                }
            }

            if (KeyController.CurrentState.IsKeyDown(Keys.Left)) {
                Display.TranslateViewport(-6, 0);
            }
            if (KeyController.CurrentState.IsKeyDown(Keys.Right)) {
                Display.TranslateViewport(6, 0);
            }
            if (KeyController.CurrentState.IsKeyDown(Keys.Up)) {
                Display.TranslateViewport(0, -6);
            }
            if (KeyController.CurrentState.IsKeyDown(Keys.Down)) {
                Display.TranslateViewport(0, 6);
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

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, Layer.SortMode, SaveStateMode.SaveState);

            Display.DrawWorld(spriteBatch, gameTime);

            spriteBatch.End();

            spriteBatch.Begin();

            informationPanel.Draw(spriteBatch, false);
            missionPanel.Draw(spriteBatch, false);
            menuBar.Draw(spriteBatch, false);
            worldStatsBar.Draw(spriteBatch, false);

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

            informationPanel.UpdateInformationPanel();
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
                            person.RemainingMovement -= range[gridLocation].cost;
                            PersonAnimation.CreateMovementAnimation(person, range[gridLocation]);

                            World.ClearSelection();
                            informationPanel.UpdateInformationPanel();
                        }
                    }
                }
            }
        }

        public void MouseScrolled(MouseScrollEvent evt) {
            Display.ScaleView(evt.Change >= 0 ? 1.1 : 0.9);
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

            if (evt.ChangedKeys.Contains(Keys.F12)) {
                this.graphics.ToggleFullScreen();
            }
        }

        public void KeysReleased(KeyEvent evt) { /* Ignore */ }

        #endregion
    }
}
