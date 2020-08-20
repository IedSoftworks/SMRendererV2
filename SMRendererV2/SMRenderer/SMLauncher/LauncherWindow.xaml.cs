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
using System.Windows.Shapes;
using SMLauncher.Designs;

namespace SMLauncher
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public Launcher Parameters;
        public LauncherWindow(Launcher parameters)
        {
            Parameters = parameters;
            InitializeComponent();

            IDesign design = parameters.Design;
            design.Prepare(parameters);
            Content = design;

            design.StartButton.Click += StartWindow;
        }

        private void StartWindow(object sender, RoutedEventArgs e)
        {
            Parameters.Window.Run();
        }
    }
}
