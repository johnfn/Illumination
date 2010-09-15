using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.WorldObjects;
using Illumination.Data;
using Illumination.Logic.ActionHandler;
using Illumination.Logic;

namespace Illumination.Components.Panels {
    public class ItemDisplay : Panel, ActionListener {
        private const int BORDER_WIDTH = 2;
        private const int TEXT_PADDING = 5;
        private const int TEXT_HEIGHT = 25;

        public delegate void ItemClicked(ShopItem item); // Handler for when item is clicked

        private ShopItem item;
        private ItemClicked responseHandler;

        public ItemDisplay(ShopItem item, Point origin, int dimension, string description, ItemClicked responseHandler)
            : base(new Rectangle(origin.X, origin.Y, dimension + 2 * BORDER_WIDTH, dimension + TEXT_HEIGHT + 2 * BORDER_WIDTH), Color.TransparentWhite) {
            this.item = item;
            this.responseHandler = responseHandler;

            AddComponent(new ImageBox(MediaRepository.Textures["Pixel"],
                new Rectangle(0, 0, dimension + 2 * BORDER_WIDTH, dimension + 2 * BORDER_WIDTH),
                Color.Black));
            AddComponent(new ImageBox(MediaRepository.Textures["Pixel"],
                new Rectangle(BORDER_WIDTH, BORDER_WIDTH, dimension, dimension),
                Color.White));

            Button buyButton = new Button(item.BaseItem.GetTexture(),
                new Rectangle(BORDER_WIDTH, BORDER_WIDTH, dimension, dimension),
                Color.White);
            buyButton.AddActionListener(this);
            AddComponent(buyButton);

            AddComponent(new TextBox(new Rectangle(0, dimension + TEXT_PADDING,
                dimension, TEXT_HEIGHT), description, Color.Black, TextBox.AlignType.Center));
        }

        public void ActionPerformed(ActionEvent evt) {
            responseHandler(item);
        }
    }
}
