using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Input;
using SM.Core.Window;

namespace SM.Keybinds
{
    [Serializable]
    public class KeybindCollection : List<Keybind>
    {
        public static KeybindCollection SystemKeybinds = CreateSystemKeybinds();

        private static KeybindCollection CreateSystemKeybinds()
        {
            return new KeybindCollection
            {
                new Keybind(a => GLWindow.Window.Close(), Key.AltLeft, Key.F4),
                new Keybind(a => GLWindow.Window.WindowState = GLWindow.Window.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen, Key.AltLeft, Key.Enter)
            };
        }

        public static List<KeybindCollection> AutoCheckKeybindCollections = new List<KeybindCollection>() {SystemKeybinds};

        public bool IsAnyPressed => this.Any(a => a.IsPressed);
        public bool IsAllPressed => this.All(a => a.IsPressed);

        public void CheckAllAndExecute() => this.ForEach(a => a.CheckAndExecute());

        public static void ExecuteAutoCheck()
        {
            Keybind.Update();
            AutoCheckKeybindCollections.ForEach(a => a.CheckAllAndExecute());
        }
    }
}