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
    public class ItemsPanel : Panel {
        public ItemsPanel(Rectangle rectangle, Color color)
            : base(rectangle, color) {
            UpdateDisplay();
        }

        public void UpdateDisplay() {
            foreach (Component c in components) {
                c.Destroy();
            }
            RemoveAllComponents();

            if (World.Inventory.Count != 0) {
                foreach (ShopItem item in World.Inventory.Keys) {
                    string description = String.Format("{0} ({1})", item.BaseItem.Name, World.Inventory[item]);
                    AddComponent(new ItemDisplay(item, new Point(5, 5), 100, description,
                        delegate(ShopItem shopItem) {
                            if (World.RemoveItemFromInventory(item)) {
                                World.ItemToPlace = item.BaseItem.CreateNewItem();
                            }
                        })
                    );
                }
            } else {
                AddComponent(new TextBox(new Rectangle(0, 0, boundingBox.Width, boundingBox.Height), "No Items", Color.Black, TextBox.AlignType.Center));
            }
        }

        public override void ActivatePanel(bool isRecursive) {
            UpdateDisplay();
            base.ActivatePanel(isRecursive);
        }

        public override void Activate() {
            UpdateDisplay();
            base.Activate();
        }
    }
}
