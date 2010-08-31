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
using Illumination.Graphics;

namespace Illumination.Components.Panels
{
    public class MenuBar : Panel, ActionListener
    {
        Button menuButton;
        Button dayNightButton;
        StatusBar timeBar;
        TextBox dayBox;
        TextBox moneyBox;
        TextBox moneyNumBox;

        public MenuBar(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.Blue)
        {
            menuButton = new Button(MediaRepository.Textures["Blank"], new Rectangle(25, 0, 100, 25), Color.DarkBlue, 
                "Menu", MediaRepository.Fonts["DefaultFont"], Color.White);

            string dayNightButtonText = World.IsNight ? "Begin Day" : "Begin Night";
            dayNightButton = new Button(MediaRepository.Textures["Blank"], new Rectangle(150, 0, 200, 25), Color.DarkRed,
                dayNightButtonText, MediaRepository.Fonts["DefaultFont"], Color.White);

            timeBar = new StatusBar(new Rectangle(375, 0, 200, 25), new Color(200, 200, 0, 255), new Color(255, 255, 220, 255));
            timeBar.Fraction = 1;

            dayBox = new TextBox(new Rectangle(900, 0, 100, 25), "Day " + World.DayCount.ToString(), Color.White);

            moneyBox = new TextBox(new Rectangle(700, 0, 50, 25), "Money: ", Color.White);

            moneyNumBox = new TextBox(new Rectangle(750, 0, 100, 25), "$" + World.Money.ToString(), Color.White);

            AddComponent(menuButton);
            AddComponent(dayNightButton);
            AddComponent(timeBar);
            AddComponent(dayBox);
            AddComponent(moneyBox);
            AddComponent(moneyNumBox);

            menuButton.AddActionListener(this);
            dayNightButton.AddActionListener(this);
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative)
        {
            dayNightButton.Text = World.IsNight ? "Begin Day" : "Begin Night";
            timeBar.Fraction = World.TimeLeft / World.DAY_TIME_LIMIT;
            dayBox.Text = "Day " + World.DayCount.ToString();
            moneyNumBox.Text = "$" + World.Money.ToString();

            base.Draw(spriteBatch, isRelative);
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
