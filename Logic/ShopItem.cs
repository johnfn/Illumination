using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;

namespace Illumination.Logic {
    public class ShopItem {
        private Item item;
        public Item ItemCopy {
            get { return item; }
        }

        public ShopItem(Item item) {
            this.item = item;
        }

        public override bool Equals(object obj) {
            if (this == obj) {
                return true;
            } 
            
            if (!(obj is ShopItem)) {
                return false;
            }

            ShopItem otherItem = (ShopItem) obj;
            return this.item.Name.Equals(otherItem.item.Name);
        }

        public override int GetHashCode() {
            return item.Name.GetHashCode();
        }
    }
}
