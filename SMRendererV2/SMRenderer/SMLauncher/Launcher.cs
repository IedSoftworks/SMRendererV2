using System;
using System.Windows;
using SM.Core.Window;
using SMLauncher.Designs;

namespace SMLauncher
{
    public class Launcher
    {
        private LauncherWindow _window;

        public string WindowName = "Default Launcher Window";

        public GLWindow Window;
        public IDesign Design;
        public ColorPalette ColorPalette;

        public Launcher(GLWindow window, IDesign design) : this(window, design, SMLauncher.ColorPalette.Dark) {}
        public Launcher(GLWindow window, IDesign design, ColorPalette colorPalette)
        {
            Window = window;
            Design = design;
            ColorPalette = colorPalette;
        }

        public void Run()
        {
            _window = new LauncherWindow(this)
            {
                Title = WindowName
            };
            _window.ShowDialog();
        }
    }
}