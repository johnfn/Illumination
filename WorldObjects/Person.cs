using Microsoft.Xna.Framework.Graphics;

namespace Illumination.WorldObjects
{
    public class Person : Entity
    {
        public enum ProfessionType
        {
            Newb,
            Educator,
            Doctor,
            Scientist,
            Environmentalist,
        }

        public enum GenderType
        {
            Male,
            Female
        }

        #region Properties

        int age;
        ProfessionType profession;
        GenderType gender;
        int health;
        int education;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public ProfessionType Profession
        {
            get { return profession; }
            set { profession = value; }
        }

        public GenderType Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Education
        {
            get { return education; }
            set { education = value; }
        }

#endregion

        public Person(int x, int y, int width, int height) : base(x, y, width, height) {}

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

    }
}