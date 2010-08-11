using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using System;
using System.Collections.Generic;

namespace Illumination.WorldObjects {
    public class Person : Entity {
        public enum ProfessionType {
            Worker,
            Educator,
            Doctor,
            Scientist,
            Environmentalist,
            SIZE
        }

        public enum GenderType {
            Male,
            Female
        }

        #region Properties

        private static Dictionary <ProfessionType, Light.LightType> lightColorMapping;

        static Person() {
            lightColorMapping = new Dictionary<ProfessionType, Light.LightType>();

            lightColorMapping[ProfessionType.Worker] = Light.LightType.Gray;
            lightColorMapping[ProfessionType.Educator] = Light.LightType.Yellow;
            lightColorMapping[ProfessionType.Doctor] = Light.LightType.Red;
            lightColorMapping[ProfessionType.Scientist] = Light.LightType.Blue;
            lightColorMapping[ProfessionType.Environmentalist] = Light.LightType.Green;
        }

        int age;
        ProfessionType profession;
        GenderType gender;
        int health;
        int education;

        public int Age {
            get { return age; }
            set { age = value; }
        }

        public ProfessionType Profession {
            get { return profession; }
            set { profession = value; }
        }

        public GenderType Gender {
            get { return gender; }
            set { gender = value; }
        }

        public int Health {
            get { return health; }
            set { health = value; }
        }

        public int Education {
            get { return education; }
            set { education = value; }
        }

        public Light.LightType ReflectedLightColor {
            get { return lightColorMapping[profession]; }
        }

        #endregion

        public Person(int x, int y) : base(x, y, 1, 1) {
            age = 0;
            education = 0;
            gender = GenderType.Male;
            health = 100;
            profession = ProfessionType.Worker;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            switch (profession) {
                case ProfessionType.Doctor:
                    spriteBatch.Draw(MediaRepository.Textures["Doctor"], base.BoundingBox, Color.White);
                    break;
                case ProfessionType.Educator:
                    spriteBatch.Draw(MediaRepository.Textures["Educator"], base.BoundingBox, Color.White);
                    break;
                case ProfessionType.Environmentalist:
                    spriteBatch.Draw(MediaRepository.Textures["Environmentalist"], base.BoundingBox, Color.White);
                    break;
                case ProfessionType.Worker:
                    spriteBatch.Draw(MediaRepository.Textures["Worker"], base.BoundingBox, Color.White);
                    break;
                case ProfessionType.Scientist:
                    spriteBatch.Draw(MediaRepository.Textures["Scientist"], base.BoundingBox, Color.White);
                    break;
            }
        }
    }
}