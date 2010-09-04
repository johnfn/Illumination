using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Illumination.Logic;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Logic.Missions;
using Microsoft.Xna.Framework;
using Illumination.Graphics;
using Illumination.Utility;
using Illumination.WorldObjects;

namespace Illumination.Components.Panels {
    public class WorldStatsPanel : Panel {
        StatusBar overallBar;
        StatusBar environmentBar;
        StatusBar healthBar;
        StatusBar educationBar;

        TextBox overallText;
        TextBox environmentText;
        TextBox healthText;
        TextBox educationText;

        const int BarHeight = 20;
        const int BarInterval = 5;

        public WorldStatsPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            Rectangle firstBarRect = new Rectangle(0, BarInterval, boundingBox.Width, BarHeight);
            Rectangle firstTextRect = new Rectangle(-10, BarInterval, 5, BarHeight);

            overallBar = new StatusBar(firstBarRect, new Color(255, 255, 255, 200), new Color(255, 255, 255, 100));
            educationBar = new StatusBar(Geometry.Translate(firstBarRect, 0, BarHeight + BarInterval), new Color(255, 255, 0, 200), new Color(255, 255, 255, 100));
            healthBar = new StatusBar(Geometry.Translate(firstBarRect, 0, 2 * (BarHeight + BarInterval)), new Color(255, 0, 0, 200), new Color(255, 255, 255, 100));
            environmentBar = new StatusBar(Geometry.Translate(firstBarRect, 0, 3 * (BarHeight + BarInterval)), new Color(0, 255, 0, 200), new Color(255, 255, 255, 100));

            overallText = new TextBox(firstTextRect, "Overall", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Right);
            educationText = new TextBox(Geometry.Translate(firstTextRect, 0, BarHeight + BarInterval), "Education", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Right);
            healthText = new TextBox(Geometry.Translate(firstTextRect, 0, 2 * (BarHeight + BarInterval)), "Health", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Right);
            environmentText = new TextBox(Geometry.Translate(firstTextRect, 0, 3 * (BarHeight + BarInterval)), "Environment", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Right);

            /* Temporary Values */
            healthBar.Fraction = 0.7;
            environmentBar.Fraction = 0.8;

            AddComponent(overallBar);
            AddComponent(educationBar);
            AddComponent(healthBar);
            AddComponent(environmentBar);

            AddComponent(overallText);
            AddComponent(educationText);
            AddComponent(healthText);
            AddComponent(environmentText);
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            int educatedCount = 0; 
            foreach (Person person in World.PersonSet) {
                if (person.IsEducated) {
                    educatedCount++;
                }
            }

            educationBar.Fraction = educatedCount / (double)World.PersonSet.Count;
            overallBar.Fraction = (educationBar.Fraction + healthBar.Fraction + environmentBar.Fraction) / 3;

            base.Draw(spriteBatch, isRelative);
        }
 
    }
}
