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
        StatusBar movementBar;
        StatusBar healthBar;
        Person person;
        ImageBox professionIcon;

        Dictionary<Person.ProfessionType, string> professionToString;
        Dictionary<Person.ProfessionType, Color> professionToColor;

        public PersonPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite)
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
            movementBar = new StatusBar(new Rectangle(125, 40, 150, 10), Color.Green, Color.White);
            educationBar = new StatusBar(new Rectangle(125, 65, 150, 10), new Color(200, 200, 0), Color.White);
            healthBar = new StatusBar(new Rectangle(125, 90, 150, 10), Color.Red, Color.White);

            professionIcon = new ImageBox(new Rectangle(25, 15, 10, 10), Color.White);

            AddComponent(title);
            AddComponent(new TextBox(new Rectangle(25, 40, 100, 10), "Movement:", Color.Black, TextBox.AlignType.Left));
            AddComponent(movementBar);
            AddComponent(new TextBox(new Rectangle(25, 65, 100, 10), "Education:", Color.Black, TextBox.AlignType.Left));
            AddComponent(educationBar);
            AddComponent(new TextBox(new Rectangle(25, 90, 100, 10), "Health:", Color.Black, TextBox.AlignType.Left));
            AddComponent(healthBar);
            AddComponent(professionIcon);
            this.consumesMouseEvent = false;
            Deactivate();
        }

        public override void ActivatePanel(bool isRecursive) {
            base.ActivatePanel(isRecursive);
        
            /* It is gauranteed that the selected entity is a single person. */
            person = (Person)World.SelectedEntities.First();
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (IsActive) {
                title.Text = professionToString[person.Profession];
                educationBar.Fraction = person.Education / (double)Person.EDUCATION_MAX;
                movementBar.Fraction = person.RemainingMovement / (double) person.MovementRange;
                healthBar.Fraction = person.Health / (double)Person.HEALTH_MAX;
                professionIcon.Color = professionToColor[person.Profession];
                base.Draw(spriteBatch, isRelative);
            }
        }
    }
}
