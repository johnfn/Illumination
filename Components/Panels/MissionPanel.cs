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

namespace Illumination.Components.Panels {
    public class MissionPanel : Panel {
        TextBox[] conditions;
        ImageBox[] iconBorders;
        ImageBox[] statusIcons;

        const int LineHeight = 25;
        const int IconIndentation = 10;
        const int IconSize = 15;
        const int TextIndentation = 5;

        public MissionPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            conditions = new TextBox[World.CurrentMission.GetNumConditions()];
            iconBorders = new ImageBox[World.CurrentMission.GetNumConditions()];
            statusIcons = new ImageBox[World.CurrentMission.GetNumConditions()];

            int index = 0;
            foreach (Objective objective in World.CurrentMission.PrimaryObjectives) {

                Rectangle iconRect = new Rectangle(IconIndentation, (index + 1) * LineHeight - IconSize, IconSize, IconSize);
                Rectangle textRect = Geometry.Translate(iconRect, IconSize + TextIndentation, 0);
                
                statusIcons[index] = new ImageBox(iconRect, Color.TransparentWhite);
                iconBorders[index] = new ImageBox(MediaRepository.Textures["Border"], iconRect, Color.Black);
                conditions[index] = new TextBox(textRect, objective.Description, Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Left);

                AddComponent(statusIcons[index]);
                AddComponent(iconBorders[index]);
                AddComponent(conditions[index]);

                index++;
            }

            consumesMouseEvent = false;
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            int index = 0;
            foreach (Objective objective in World.CurrentMission.PrimaryObjectives) {
                Objective.StatusType status = objective.GetStatus();
                if (status == Objective.StatusType.Failure) {
                    statusIcons[index].Color = new Color(255, 0, 0, 100);
                } else if (status == Objective.StatusType.Success) {
                    statusIcons[index].Color = new Color(0, 0, 255, 100);
                } else {
                    statusIcons[index].Color = Color.TransparentWhite;
                }

                index++;
            }

            foreach (ImageBox icon in statusIcons) {
                icon.Draw(spriteBatch, false);
            }

            foreach (ImageBox border in iconBorders) {
                border.Draw(spriteBatch, false);
            }

            foreach (TextBox text in conditions) {
                text.Draw(spriteBatch, false);
            }
        }
    }
}
