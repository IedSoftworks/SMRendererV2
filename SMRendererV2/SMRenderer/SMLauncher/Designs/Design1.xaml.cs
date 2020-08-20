using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMLauncher.Designs
{
    /// <summary>
    /// Interaction logic for Design1.xaml
    /// </summary>
    public partial class Design1 : IDesign
    {
        public Image BaseImage { get; set; }
        public Button StartButton { get; set; }
        public Design1()
        {
            InitializeComponent();
            StartButton = Pressy;
            BaseImage = Image;

        }

        public void Prepare(Launcher parameter)
        {
        }
    }
}
