using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Illumination.Logic;
using Illumination.Graphics;
using Illumination.Graphics.AnimationHandler;
using Illumination.Utility;

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

        public const int EDUCATION_MAX = 3;

        #region Properties

        private static Dictionary<ProfessionType, Light.LightType> lightColorMapping;
        private static Dictionary<ProfessionType, Texture2D> texturesMap;

        static Person() {
            lightColorMapping = new Dictionary<ProfessionType, Light.LightType>();

            lightColorMapping[ProfessionType.Worker] = Light.LightType.Gray;
            lightColorMapping[ProfessionType.Educator] = Light.LightType.Yellow;
            lightColorMapping[ProfessionType.Doctor] = Light.LightType.Red;
            lightColorMapping[ProfessionType.Scientist] = Light.LightType.Blue;
            lightColorMapping[ProfessionType.Environmentalist] = Light.LightType.Green;

            texturesMap = new Dictionary<ProfessionType, Texture2D>();
            texturesMap[ProfessionType.Worker] = MediaRepository.Textures["Worker"];
            texturesMap[ProfessionType.Educator] = MediaRepository.Textures["Educator"];
            texturesMap[ProfessionType.Doctor] = MediaRepository.Textures["Doctor"];
            texturesMap[ProfessionType.Scientist] = MediaRepository.Textures["Scientist"];
            texturesMap[ProfessionType.Environmentalist] = MediaRepository.Textures["Environmentalist"];
        }

        public static Texture2D GetTexture(ProfessionType profession)
        {
            return texturesMap[profession];
        }

        DirectionType direction;
        int age;
        ProfessionType profession;
        GenderType gender;
        int health;
        int education;
        int movementRange;
        int remainingMovement;

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

        public bool IsEducated {
            get { return education >= EDUCATION_MAX; }
        }

        public Light.LightType ReflectedLightColor {
            get { return lightColorMapping[profession]; }
        }

        public int MovementRange {
            get { return movementRange; }
            set { movementRange = value; }
        }

        public Texture2D Texture {
            get { return texturesMap[profession]; }
        }

        public int RemainingMovement {
            get { return remainingMovement; }
            set { remainingMovement = value; }
        }
    
        #endregion

        public Person(int x, int y) : base(x, y, 1, 1, texturesMap[ProfessionType.Worker]) {
            age = 0;
            education = 0;
            gender = GenderType.Male;
            health = 100;
            profession = ProfessionType.Worker;
            Random random = new Random();
            direction = (DirectionType)(random.Next() % (int)DirectionType.SIZE);
            remainingMovement = movementRange = 36;
            BlocksMovement = false;

            Name = "Person";
        }

        public void Move(int newX, int newY) {
            GridLocation = new Rectangle(newX, newY, 1, 1);
            BoundingBox = Display.GetTextureBoundingBox(texturesMap[ProfessionType.Worker], GridLocation, 0);
        }

        public override void Draw(SpriteBatchRelative spriteBatch) {
            Rectangle arrowBox = Display.GridLocationToViewport(GridLocation);
            Color arrowColor = new Color(255, 255, 255, 200);
            switch (direction)
            {
                case DirectionType.North:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_N"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
                case DirectionType.East:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_E"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
                case DirectionType.South:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_S"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
                case DirectionType.West:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_W"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
            }

            spriteBatch.DrawRelative(Texture, BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));

            if (IsEducated && (profession == ProfessionType.Worker))
            {
                Rectangle noticeBox = new Rectangle(BoundingBox.X, BoundingBox.Y, BoundingBox.Width / 2, BoundingBox.Height / 2);
                spriteBatch.DrawRelative(MediaRepository.Textures["Notice"], noticeBox, Color.Yellow, Layer.Depth["Notice"]);
            }
        }

        public void Educate(int increment)
        {
            if (education < EDUCATION_MAX)
            {
                education = Math.Min(education + increment, EDUCATION_MAX);
            }
            Rectangle boundingBox = Display.GridLocationToViewport(GridLocation);
            Animation educateEffect = Display.CreateAnimation(MediaRepository.Sheets["Glow"], boundingBox, 2, 0.1);
            educateEffect.AddColorFrame(Color.TransparentWhite, 0);
            educateEffect.AddColorFrame(Color.Yellow, 0.5);
            educateEffect.AddColorFrame(Color.TransparentWhite, 2);
        }

        public class SearchNode {
            public Point point;
            public int cost;
            public SearchNode nextNode;

            public SearchNode(Point point, int cost, SearchNode nextNode) {
                this.point = point;
                this.cost = cost;
                this.nextNode = nextNode;
            }

            public override bool Equals(object obj) {
                if (this == obj) {
                    return true;
                } else if (!(obj is SearchNode)) {
                    return false;
                }

                return point.Equals(((SearchNode) obj).point);
            }

            public override int GetHashCode() {
                return point.GetHashCode();
            }

            // Reversing a linked list!
            public static SearchNode GetForwardPath(SearchNode endNode) {
                if (endNode == null) {
                    return null;
                }

                SearchNode previousNode = null;
                SearchNode currentNode = endNode;
                SearchNode nextNode = endNode.nextNode;

                while (nextNode != null) {
                    currentNode.nextNode = previousNode;

                    previousNode = currentNode;
                    currentNode = nextNode;
                    nextNode = nextNode.nextNode;
                }

                currentNode.nextNode = previousNode;
                return currentNode;
            }
        };

        public override Texture2D GetTexture() {
            return texturesMap[profession];
        }

        public Dictionary<Point, SearchNode> ComputeMovementRange() {
            Dictionary<Point, SearchNode> possibleLocations = new Dictionary<Point, SearchNode>();
            
            Queue <SearchNode> queue = new Queue<SearchNode>();
            queue.Enqueue(new SearchNode(new Point(GridLocation.X, GridLocation.Y), 0, null));

            while (queue.Count > 0) {
                SearchNode node = queue.Dequeue();
                Point currentPosition = node.point;
                if (World.InBound(currentPosition) && node.cost <= remainingMovement) {
                    possibleLocations[node.point] = node;
                    for (int i = 0; i < directionX.Length; i++) {
                        Point nextPoint = new Point(currentPosition.X + directionX[i], currentPosition.Y + directionY[i]);
                        int cost = World.GetMovementCost(nextPoint);
                        if (cost == -1 || (possibleLocations.ContainsKey(nextPoint) 
                            && node.cost + cost >= possibleLocations[nextPoint].cost)) {
                            continue;
                        }
                        queue.Enqueue(new SearchNode(nextPoint, node.cost + cost, node));
                    }
                }
            }

            return possibleLocations;
        }

        public override IEnumerable<Point> GetRange() {
            return ComputeMovementRange().Keys;
        }
    }
}