using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Components;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Microsoft.Xna.Framework;
using Illumination.Logic.MouseHandler;
using Illumination.Logic.ActionHandler;
using Illumination.Utility;
using Illumination.Logic;

namespace Illumination.Components.Panels
{
    public class MenuBar : Panel, ActionListener
    {
        Button menuButton;
        Rectangle menuButtonRelLoc = new Rectangle(25, 0, 100, 25);

        Button dayNightButton;
        Rectangle dayNightButtonRelLoc = new Rectangle(150, 0, 200, 25);

        StatusBar timeBar;
        Rectangle timeBarRelLoc = new Rectangle(375, 0, 200, 25);

        public MenuBar(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.Blue)
        {
            Rectangle menuButtonAbsLoc = Geometry.Translate(menuButtonRelLoc, boundingBox.X, boundingBox.Y);
            menuButton = new Button(MediaRepository.Textures["Blank"], menuButtonAbsLoc, Color.DarkBlue, 
                "Menu", MediaRepository.Fonts["DefaultFont"], Color.White);

            string dayNightButtonText = World.IsNight ? "Begin Day" : "Begin Night";

            Rectangle dayNightButtonAbsLoc = Geometry.Translate(dayNightButtonRelLoc, boundingBox.X, boundingBox.Y);
            dayNightButton = new Button(MediaRepository.Textures["Blank"], dayNightButtonAbsLoc, Color.DarkRed,
                dayNightButtonText, MediaRepository.Fonts["DefaultFont"], Color.White);

            Rectangle timeBarAbsLoc = Geometry.Translate(timeBarRelLoc, boundingBox.X, boundingBox.Y);
            timeBar = new StatusBar(timeBarAbsLoc, new Color(200, 200, 0, 255), new Color(255, 255, 220, 255));
            timeBar.Fraction = 1;

            menuButton.AddActionListener(this);
            dayNightButton.AddActionListener(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            menuButton.Draw(spriteBatch);

            dayNightButton.Text = World.IsNight ? "Begin Day" : "Begin Night";
            dayNightButton.Draw(spriteBatch);

            timeBar.Fraction = World.TimeLeft / World.DAY_TIME_LIMIT;
            timeBar.Draw(spriteBatch);
        }

        public void ActionPerformed(ActionEvent evt)
        {
            if (evt.InvokingComponent == dayNightButton)
            {
                World.IsNight = !World.IsNight;
            }
            else
            {
                Console.WriteLine("Menu Triggered!");
            }
        }
    }
}
