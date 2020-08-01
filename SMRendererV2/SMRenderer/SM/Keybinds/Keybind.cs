using System;
using System.Linq;
using OpenTK.Input;

namespace SM.Keybinds
{
    [Serializable]
    public class Keybind
    {

        public event Action<Keybind> Pressed;

        public MouseButton[] MouseButtons;
        public Key[] Keys;

        public bool IsPressed => CheckPress();

        public bool AllowHolding = true;
        
        public Keybind(params Key[] keys) : this(new MouseButton[]{}, keys) { }
        public Keybind(Action<Keybind> action, params Key[] keys) : this(new MouseButton[] {}, keys, action) { }
        public Keybind(MouseButton[] mouseButtons, params Key[] keys) : this(mouseButtons, keys, a => {}) { }
        public Keybind(MouseButton[] mouseButtons, Key[] keys, Action<Keybind> action)
        {
            MouseButtons = mouseButtons;
            Keys = keys;
            Pressed += action;
        }

        public bool CheckPress() => MouseButtons.All(a => SMGlobals.CurrentMouseState[a]) && Keys.All(a => SMGlobals.CurrentKeyState[a]);

        public void CheckAndExecute()
        {
            if (IsPressed) Pressed?.Invoke(this);
        }

        public static implicit operator bool(Keybind kb) => kb.IsPressed;

        public static void Update()
        {
            SMGlobals.CurrentKeyState = Keyboard.GetState();
            SMGlobals.CurrentMouseState = Mouse.GetState();
        }
    }
}