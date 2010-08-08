using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.ActionHandler {
    public interface ActionListener {
        void ActionPerformed(ActionEvent evt);
    }
}
