using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.Client.UI.UIObjects
{
    public class UIWindow : UIObject
    {
        private Rectangle displayArea;
        private Button ScrollBarUp;
        private UIObject ScrollBar;
        private Button ScrollBarDown;
        private Button CloseButton;
        private UIObject DragHandle;
        private UIObject ResizeHandle;
        public UIWindow(string objectName) : base(objectName)
        {

        }
    }
}
