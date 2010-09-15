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
    public class ItemPanel : Panel {
        private class ItemDisplay : Panel, ActionListener {
            private ShopItem item;
            private Button useButton;

            public ItemDisplay(ShopItem item, Rectangle rectangle)
                : base(rectangle) {
                this.item = item;

                useButton = new Button(item.BaseItem.GetTexture(), rectangle, Color.White);
                useButton.AddActionListener(this);

                AddComponent(useButton);

                string description = String.Format("{0} ({1})", item.BaseItem.Name, World.Inventory[item]);
                AddComponent(new TextBox(new Rectangle(rectangle.X, rectangle.Y + rectangle.Height + 5, 
                    rectangle.Width, 20), description, Color.Black, TextBox.AlignType.Center));
            }

            public void ActionPerformed(ActionEvent evt) {
                if (World.RemoveItemFromInventory(item)) {
                    World.ItemToPlace = item.BaseItem.CreateNewItem();
                }
            }

            public override string ToString() {
                return useButton.ToString();
            }
        }

        public ItemPanel(Rectangle rectangle, Color color)
            : base(rectangle, color) {
            UpdateDisplay();
        }

        public void UpdateDisplay() {
            foreach (Component c in components) {
                c.Destroy();
            }
            RemoveAllComponents();

            foreach (ShopItem item in World.Inventory.Keys) {
                AddComponent(new ItemDisplay(item, new Rectangle(0, 0, 100, 100)));
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
