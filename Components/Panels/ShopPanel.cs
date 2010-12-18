using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.WorldObjects;
using Illumination.Data;
using Illumination.Logic.ActionHandler;
using Illumination.Logic;

namespace Illumination.Components.Panels {
    public class ShopPanel : Panel {
        public ShopPanel(Rectangle rectangle, Color color) : base(rectangle, color) {
            Initialize();
        }

        public void Initialize() {
            AddItemPanel(new Mirror(), new Point(5, 5), 100);
            AddItemPanel(new Road(), new Point(110, 5), 100);
            AddItemPanel(new Airport(), new Point(215, 5), 100);
            AddItemPanel(new Inspiration(), new Point(320, 5), 100);
            AddItemPanel(new School(), new Point(425, 5), 100);
            AddItemPanel(new Factory(), new Point(530, 5), 100);
            AddItemPanel(new Hospital(), new Point(635, 5), 100);
            AddItemPanel(new ResearchCenter(), new Point(740, 5), 100);
        }

        private void AddItemPanel(Item item, Point p, int dimension) {
            string description = String.Format("{0}: ${1}", item.Name, item.Cost);

            AddComponent(new ItemDisplay(new ShopItem(item), p, dimension, description,
                delegate(ShopItem shopItem) {
                    if (World.Money >= shopItem.BaseItem.Cost && World.ItemToPlace == null) {
                        World.Money -= shopItem.BaseItem.Cost;
                        World.AddItemToInventory(shopItem);
                    }
                })
            );
        }
    }
}
