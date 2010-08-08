using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Components;

namespace Illumination.Logic.ActionHandler {
    public class ActionEvent {
        private Component invokingComponent;

        public ActionEvent(Component invokingComponent) {
            this.invokingComponent = invokingComponent;
        }

        public Component InvokingComponent {
            get { return invokingComponent; }
        }
    }
}
