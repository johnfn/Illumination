using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Illumination.Logic;

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

        DirectionType direction;
        int age;
        ProfessionType profession;
        GenderType gender;
        int health;
        int education;
        int movementRange;

        static int[] directionX = { -1, 1, 0, 0 };
        static int[] directionY = { 0, 0, -1, 1 };

        public DirectionType Direction
        {
            get { return direction; }
            set { direction = value; }
        }

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

        public int MovementRange {
            get { return movementRange; }
            set { movementRange = value; }
        }

        #endregion

        public Person(int x, int y) : base(x, y, 1, 1) {
            age = 0;
            education = 0;
            gender = GenderType.Male;
            health = 100;
            profession = ProfessionType.Worker;
            Random random = new Random();
            direction = (DirectionType)(random.Next() % (int)DirectionType.SIZE);
            movementRange = 3;
            BlocksMovement = false;
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

            switch (direction)
            {
                case DirectionType.North:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_N"], BoundingBox, Color.White);
                    break;
                case DirectionType.East:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_E"], BoundingBox, Color.White);
                    break;
                case DirectionType.South:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_S"], BoundingBox, Color.White);
                    break;
                case DirectionType.West:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_W"], BoundingBox, Color.White);
                    break;
            }
        }

        public struct SearchNode {
            public Point point;
            public int cost;

            public SearchNode(Point point, int cost) {
                this.point = point;
                this.cost = cost;
            }
        };

        public HashSet<SearchNode> ComputeMovementRange() {
            HashSet<SearchNode> possibleLocations = new HashSet<SearchNode>();
            
            Queue <SearchNode> queue = new Queue<SearchNode>();
            queue.Enqueue(new SearchNode(new Point(GridLocation.X, GridLocation.Y), 0));

            while (queue.Count > 0) {
                SearchNode node = queue.Dequeue();
                Point currentPosition = node.point;
                if (World.InBound(currentPosition) && node.cost <= movementRange) {
                    possibleLocations.Add(node);
                    for (int i = 0; i < directionX.Length; i++) {
                        Point nextPoint = new Point(currentPosition.X + directionX[i], currentPosition.Y + directionY[i]);
                        int cost = World.GetMovementCost(nextPoint);
                        if (cost == -1) {
                            continue;
                        }
                        queue.Enqueue(new SearchNode(nextPoint, node.cost + cost));
                    }
                }
            }

            return possibleLocations;
        }
    }
}