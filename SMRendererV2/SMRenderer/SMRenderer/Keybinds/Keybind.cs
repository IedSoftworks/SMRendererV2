using System;
using System.Linq;
using OpenTK.Input;

namespace SMRenderer.Keybinds
{
    public class Keybind
    {
        public static KeyboardState CurrentKeyState;
        public static MouseState CurrentMouseState;

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

        public bool CheckPress() => MouseButtons.All(a => CurrentMouseState[a]) && Keys.All(a => CurrentKeyState[a]);

        public void CheckAndExecute()
        {
            if (IsPressed) Pressed?.Invoke(this);
        }

        public static implicit operator bool(Keybind kb) => kb.IsPressed;

        public static void Update()
        {
            CurrentKeyState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }
    }
}