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

namespace CG_3._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDrawing go = new MyDrawing();
        int Clicks = 0;

        public MainWindow()
        {
            InitializeComponent();
            Background = new ImageBrush(new BitmapImage(new Uri("IMGs\\background.png", UriKind.Relative)));
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Clicks++;
            switch (Clicks)
            {
                case 1: go.DrawPath(pathCnvs);
                    break;
                case 2: go.DrawPower(pathCnvs);
                    break;
                case 3: go.DrawBullet(pathCnvs);
                    Clicks = 0;
                    break;
            }
            
        }

    }
}
