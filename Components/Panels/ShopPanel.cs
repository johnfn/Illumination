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
            Item mirror = new Mirror();
            string description = String.Format("{0}: ${1}", mirror.Name, mirror.Cost);

            AddComponent(new ItemDisplay(new ShopItem(new Mirror()), new Point(5, 5), 100, description,
                delegate(ShopItem item) {
                    if (World.Money >= item.BaseItem.Cost && World.ItemToPlace == null) {
                        World.Money -= item.BaseItem.Cost;
                        World.AddItemToInventory(item);
                    }
                })
            );
        }
    }
}
