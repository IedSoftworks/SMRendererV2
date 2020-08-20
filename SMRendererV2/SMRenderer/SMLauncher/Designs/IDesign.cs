using System.Windows.Controls;

namespace SMLauncher.Designs
{
    public interface IDesign
    {
        Image BaseImage { get; set; }
        Button StartButton { get; set; }

        void Prepare(Launcher parameter);
    }
}