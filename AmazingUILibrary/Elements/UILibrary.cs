using UILibrary.Backgrounds;
using UILibrary.Containers;
using SharpDX;

namespace UILibrary.Elements
{
    public class UILibrary : UISingleElementContainer
    {
        private IBackground _pressedBackground;
        private IBackground _releasedBackground;

        public IBackground PressedBackground { get => _pressedBackground; set => _pressedBackground = value; }
        public IBackground ReleasedBackground 
        {
            get => _releasedBackground; 
            set
            {
                _releasedBackground = value;
                Background = value;
            }
        }

        public UILibrary(Vector2 position, Vector2 size, UIElement element) 
            : base(position, size, element)
        {
            IsClickable = true;
            OnPressed += () => Background = _pressedBackground;
            OnReleazed += () => Background = _releasedBackground;
        }

        public UILibrary(Vector2 size, UIElement element)
            : this(Vector2.Zero, size, element)
        {
        }

        public UILibrary(UIElement element)
            : this(Vector2.Zero, element.Size, element)
        {
        }
    }
}
