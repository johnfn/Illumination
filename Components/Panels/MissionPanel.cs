using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Illumination.Logic;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Logic.Missions;
using Microsoft.Xna.Framework;

namespace Illumination.Components.Panels
{
    public class MissionPanel : Panel 
    {
        TextBox[] missionBoxes;

        public MissionPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite)
        {
            missionBoxes = new TextBox[World.CurrentMission.GetNumConditions()];

            int boxHeight = boundingBox.Height / World.CurrentMission.GetNumConditions();

            int index = 0;
            foreach (Objective objective in World.CurrentMission.PrimaryObjectives)
            {
                missionBoxes[index] = new TextBox(new Rectangle(0, index * boxHeight, boundingBox.Width, boxHeight),
                    objective.Description, Color.Black);
                missionBoxes[index].Color = new Color(255, 255, 255, 100);

                AddComponent(missionBoxes[index]);

                index++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int index = 0;
            foreach (Objective objective in World.CurrentMission.PrimaryObjectives)
            {
                Objective.StatusType status = objective.GetStatus();
                if (status == Objective.StatusType.Failure)
                {
                    missionBoxes[index].Color = new Color(255, 0, 0, 100);
                }
                else if (status == Objective.StatusType.Success)
                {
                    missionBoxes[index].Color = new Color(0, 0, 255, 100);
                }
                else
                {
                    missionBoxes[index].Color = new Color(255,255,255,100);
                }

                index++;
            }

            base.Draw(spriteBatch);
        }
    }
}
