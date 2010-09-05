using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic.ActionHandler;
using Microsoft.Xna.Framework;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Logic;
using Illumination.WorldObjects;
using Illumination.Graphics;
using Illumination.Utility;

namespace Illumination.Components.Panels
{
    public class ProfessionPanel : Panel, ActionListener
    {
        Button[] professionButtons;

        const int ButtonWidth = 80;
        const int ButtonHeight = 52;
        const int TilePosY = ButtonHeight - (ButtonWidth / 2);

        public ProfessionPanel(Rectangle boundingBox) : base(MediaRepository.Textures["Blank"], boundingBox, new Color(255, 255, 255, 100)) {
            Initialize();
            Deactivate();
        }

        public void Initialize() {
            int buttonCount = (int)Person.ProfessionType.SIZE - 1;
            professionButtons = new Button[buttonCount];

            for (Person.ProfessionType profession = (Person.ProfessionType)1; profession < Person.ProfessionType.SIZE; profession++)
            {
                Button button = new Button(Person.GetTexture(profession), new Rectangle(ButtonWidth * ((int)profession - 1), 0, ButtonWidth, ButtonHeight), Color.White);
                professionButtons[(int)profession - 1] = button;
                AddComponent(button);
                button.AddActionListener(this);
            }
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (IsActive) {
                spriteBatch.DrawAbsolute(base.background, base.boundingBox, base.color);
                
                Rectangle tileRect = new Rectangle(boundingBox.X, boundingBox.Y + TilePosY, ButtonWidth, ButtonWidth / 2);
                for (int n = 0; n < professionButtons.Count(); n++) {
                    spriteBatch.DrawAbsolute(MediaRepository.Textures["BlankTile"],
                        tileRect, new Color(255, 255, 255, 100));
                    tileRect = Geometry.Translate(tileRect, ButtonWidth, 0);
                }

                foreach (Button button in professionButtons) {
                    button.Draw(spriteBatch, false);
                }
            }
        }

        public void ActionPerformed(ActionEvent evt) {
            if (World.SelectedEntityType == World.EntityType.Person) {
                foreach (Entity e in World.SelectedEntities) {
                    Person thisPerson = (Person) e;

                    for (Person.ProfessionType profession = (Person.ProfessionType)1; profession < Person.ProfessionType.SIZE; profession++) {
                        if (evt.InvokingComponent == professionButtons[(int)profession - 1]) {
                            thisPerson.Profession = profession;
                            break;
                        }
                    }
                }
                Deactivate();
            }
        }
    }
}
