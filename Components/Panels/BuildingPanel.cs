using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Logic;
using Illumination.WorldObjects;

namespace Illumination.Components.Panels
{
    public class BuildingPanel : Panel
    {
        TextBox title = new TextBox(new Rectangle(25, 10, 50, 25), "Building", Color.White, TextBox.AlignType.Left);

        public BuildingPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, new Color(0, 0, 0, 50))
        {
            AddComponent(title);

            Deactivate();
        }

        public override void Activate()
        {
            base.Activate();

            /* It is gauranteed that selected entity is a single building. */
            Building building = (Building)World.SelectedEntities.First();
            title.Text = building.Name;
        }
    }
}
