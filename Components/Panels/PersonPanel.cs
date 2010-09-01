using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Illumination.WorldObjects;
using Illumination.Logic;
using Illumination.Graphics;

namespace Illumination.Components.Panels
{
    public class PersonPanel : Panel
    {
        TextBox title;
        StatusBar educationBar;
        Person person;
        ImageBox professionIcon;

        Dictionary<Person.ProfessionType, string> professionToString;
        Dictionary<Person.ProfessionType, Color> professionToColor;

        public PersonPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, new Color(0, 0, 0, 50))
        {
            professionToString = new Dictionary<Person.ProfessionType, string>();
            professionToString[Person.ProfessionType.Doctor] = "Doctor";
            professionToString[Person.ProfessionType.Educator] = "Educator";
            professionToString[Person.ProfessionType.Environmentalist] = "Environmentalist";
            professionToString[Person.ProfessionType.Scientist] = "Scientist";
            professionToString[Person.ProfessionType.Worker] = "Worker";

            professionToColor = new Dictionary<Person.ProfessionType, Color>();
            professionToColor[Person.ProfessionType.Doctor] = Color.Red;
            professionToColor[Person.ProfessionType.Educator] = Color.Yellow;
            professionToColor[Person.ProfessionType.Environmentalist] = Color.Green;
            professionToColor[Person.ProfessionType.Scientist] = Color.Blue;
            professionToColor[Person.ProfessionType.Worker] = Color.Gray;



            title = new TextBox(new Rectangle(45, 10, 50, 25), "Person", Color.White, TextBox.AlignType.Left);
            educationBar = new StatusBar(new Rectangle(25, 40, 150, 10), new Color(200, 200, 0), Color.White);
            professionIcon = new ImageBox(new Rectangle(25, 15, 10, 10), Color.White);

            AddComponent(title);
            AddComponent(educationBar);
            AddComponent(professionIcon);

            this.consumesMouseEvent = false;
            Deactivate();
        }

        public override void Activate() {
            base.Activate();
        
            /* It is gauranteed that the selected entity is a single person. */
            person = (Person)World.SelectedEntities.First();
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (IsActive) {
                title.Text = professionToString[person.Profession];
                educationBar.Fraction = person.Education / (double)Person.EDUCATION_MAX;
                professionIcon.Color = professionToColor[person.Profession];
                base.Draw(spriteBatch, isRelative);
            }
        }
    }
}
