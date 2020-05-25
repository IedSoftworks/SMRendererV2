using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OpenTK.Input;
using SMRenderer.Core.Window;

namespace SMRenderer.Base.Keybinds
{
    public class KeybindCollection : List<Keybind>
    {
        public static KeybindCollection SystemKeybinds = CreateSystemKeybinds();

        private static KeybindCollection CreateSystemKeybinds()
        {
            return new KeybindCollection
            {
                new Keybind(a => GLWindow.window.Close(), Key.AltLeft, Key.F4)
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