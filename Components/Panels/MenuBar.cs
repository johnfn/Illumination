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

        public MenuBar(Rectangle boundingBox, MouseController mouseController)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.Blue)
        {
            Rectangle menuButtonAbsLoc = Geometry.Translate(menuButtonRelLoc, boundingBox.X, boundingBox.Y);
            menuButton = new Button(MediaRepository.Textures["Blank"], menuButtonAbsLoc, Color.DarkBlue, 
                "Menu", MediaRepository.Fonts["DefaultFont"], Color.White, mouseController);

            string dayNightButtonText = World.IsNight ? "Begin Day" : "Begin Night";

            Rectangle dayNightButtonAbsLoc = Geometry.Translate(dayNightButtonRelLoc, boundingBox.X, boundingBox.Y);
            dayNightButton = new Button(MediaRepository.Textures["Blank"], dayNightButtonAbsLoc, Color.DarkRed,
                dayNightButtonText, MediaRepository.Fonts["DefaultFont"], Color.White, mouseController);

            menuButton.AddActionListener(this);
            dayNightButton.AddActionListener(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            menuButton.Draw(spriteBatch);
            dayNightButton.Draw(spriteBatch);
        }

        public void ActionPerformed(ActionEvent evt)
        {
            if (evt.InvokingComponent == dayNightButton)
            {
                World.IsNight = !World.IsNight;
                dayNightButton.Text = World.IsNight ? "Begin Day" : "Begin Night";
            }
            else
            {
                Console.WriteLine("Menu Triggered!");
            }
        }
    }
}
