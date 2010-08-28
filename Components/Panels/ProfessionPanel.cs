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

namespace Illumination.Components.Panels
{
    public class ProfessionPanel : Panel, ActionListener
    {
        Button[] professionButtons;

        public ProfessionPanel(Rectangle boundingBox) : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            Initialize();

            base.Deactivate();
        }

        public void Initialize() {
            professionButtons = new Button[(int)Person.ProfessionType.SIZE];

            for (Person.ProfessionType profession = 0; profession < Person.ProfessionType.SIZE; profession++)
            {
                Button button = new Button(Person.GetTexture(profession), new Rectangle(80 * (int)profession, 0, 80, 50), Color.White);
                professionButtons[(int)profession] = button;
                AddComponent(button);
                button.AddActionListener(this);
            }
        }

        public void ActionPerformed(ActionEvent evt) {
            if (World.SelectedEntityType == World.EntityType.Person) {
                foreach (Entity e in World.SelectedEntities) {
                    Person thisPerson = (Person) e;

                    for (Person.ProfessionType profession = 0; profession < Person.ProfessionType.SIZE; profession++) {
                        if (evt.InvokingComponent == professionButtons[(int) profession]) {
                            thisPerson.Profession = profession;
                            break;
                        }
                    }
                }
            }
        }
    }
}
