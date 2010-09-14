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
        private class ItemDisplay : Panel, ActionListener {
            private ShopItem item;

            public ItemDisplay(ShopItem item, Rectangle rectangle)
                : base(rectangle) {
                this.item = item;

                Button buyButton = new Button(item.ItemCopy.GetTexture(), rectangle, Color.White);
                buyButton.AddActionListener(this);
                AddComponent(buyButton);
            }

            public void ActionPerformed(ActionEvent evt) {
                if (World.Money >= item.ItemCopy.Cost && World.ItemToPlace == null) {
                    World.Money -= item.ItemCopy.Cost;
                    World.AddItemToInventory(item);
                }
            }
        }

        public ShopPanel(Rectangle rectangle, Color color) : base(rectangle, color) {
            Initialize();
        }

        public void Initialize() {
            AddComponent(new ItemDisplay(new ShopItem(new Mirror()), new Rectangle(0, 0, 100, 100)));
        }
    }
}
