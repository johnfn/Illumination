﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Illumination.Graphics;

namespace Illumination.Logic {
    public class ShopItem {
        private Item item;
        public Item BaseItem {
            get { return item; }
        }

        public Dimension ItemDimension {
            get { return item.ItemDimension; }
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
