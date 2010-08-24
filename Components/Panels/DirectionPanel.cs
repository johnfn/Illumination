using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Logic.ActionHandler;
using Illumination.Logic;
using Illumination.WorldObjects;

namespace Illumination.Components.Panels {
    public class DirectionPanel : Panel, ActionListener {
        Button northButton, southButton, eastButton, westButton;

        public DirectionPanel(Rectangle boundingBox) : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            Initialize();

            base.Deactivate();
        }

        public void Initialize() {
            northButton = new Button(MediaRepository.Textures["Arrow_N"], new Rectangle(40, 0, 40, 40), Color.Black);
            southButton = new Button(MediaRepository.Textures["Arrow_S"], new Rectangle(40, 80, 40, 40), Color.Black);
            eastButton = new Button(MediaRepository.Textures["Arrow_E"], new Rectangle(80, 40, 40, 40), Color.Black);
            westButton = new Button(MediaRepository.Textures["Arrow_W"], new Rectangle(0, 40, 40, 40), Color.Black);

            AddComponent(northButton);
            AddComponent(southButton);
            AddComponent(eastButton);
            AddComponent(westButton);
        }

        public void ActionPerformed(ActionEvent evt) {
            if (World.SelectedEntity is Person) {
                if (evt.InvokingComponent == northButton) {
                    ((Person) World.SelectedEntity).Direction = Entity.DirectionType.North;
                } else if (evt.InvokingComponent == southButton) {
                    ((Person) World.SelectedEntity).Direction = Entity.DirectionType.South;
                } else if (evt.InvokingComponent == eastButton) {
                    ((Person) World.SelectedEntity).Direction = Entity.DirectionType.East;
                } else {
                    ((Person) World.SelectedEntity).Direction = Entity.DirectionType.West;
                }
            }
            else if (World.SelectedEntity is Tree) {
                if (evt.InvokingComponent == northButton) {
                    ((Tree) World.SelectedEntity).Direction = Entity.DirectionType.North;
                } else if (evt.InvokingComponent == southButton) {
                    ((Tree) World.SelectedEntity).Direction = Entity.DirectionType.South;
                } else if (evt.InvokingComponent == eastButton) {
                    ((Tree) World.SelectedEntity).Direction = Entity.DirectionType.East;
                } else {
                    ((Tree) World.SelectedEntity).Direction = Entity.DirectionType.West;
                }
            }
        }

        public override void Activate()
        {
            if (IsActive)
            {
                return;
            }

            northButton.AddActionListener(this);
            southButton.AddActionListener(this);
            eastButton.AddActionListener(this);
            westButton.AddActionListener(this);

            base.Activate();
        }

        public override void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            northButton.RemoveActionListener(this);
            southButton.RemoveActionListener(this);
            eastButton.RemoveActionListener(this);
            westButton.RemoveActionListener(this);

            base.Deactivate();
        }
    }
}
