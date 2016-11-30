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
    class MyDrawing
    {

        System.Timers.Timer pathTimer;
        System.Timers.Timer powerTimer;
        System.Timers.Timer flyTimer;

        Line path;
        Line power;
        Rectangle bullet;

        const double g = 9.8;
        double a;
        double V0;
        double t;


        bool pathDirection;
        bool powerDirection;

        private double CircleX(double a)
        {
            a = a * Math.PI / 180;
            return 100 * Math.Cos(a);
        }

        private double CircleY(double a)
        {
            a = a * Math.PI / 180;
            return 100 - 100 * Math.Abs(Math.Sin(a));
        }

        private double A(double t)
        {
            double Vx = V0 * Math.Cos(a * Math.PI / 180);
            double Vy = (V0 * Math.Sin(a * Math.PI / 180) - g * t);
            if (Vy > 0)
                return Math.Atan(Vx / Vy) * 180 / Math.PI;
            else
                return Math.Atan(Vx / Vy) * 180 / Math.PI + 180;
        }
     
        private double X(double t)
        {
            return V0 * Math.Cos(a * Math.PI / 180) * t;
        }
       
        private double Y(double t)
        {
            return 390 - (V0 * Math.Sin(a * Math.PI / 180) * t - g * t * t / 2);
        }


        public void DrawPath(Canvas canvas)
        {
            if (flyTimer != null)
                flyTimer.Stop();

            pathTimer = new System.Timers.Timer(100);
            pathTimer.Elapsed += pathTimer_Elapsed;

            path = new Line();
            path.X1 = 0;
            path.Y1 = 390;
            path.X2 = 100;
            path.Y2 = 390;
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 2;
            canvas.Children.Add(path);
            a = 0;
            pathDirection = true;
            pathTimer.Start();
        }

        void pathTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                path.Dispatcher.Invoke(() =>
                {
                    if ((a == 0) || (a == 90))
                        pathDirection = !pathDirection;
                    if (pathDirection)
                        a--;
                    else
                        a++;
                    path.X2 = CircleX(a);
                    path.Y2 = 290 + CircleY(a);
                });
            }
            catch (Exception) { };
        }

        public void DrawPower(Canvas canvas)
        {
            pathTimer.Stop();

            powerTimer = new System.Timers.Timer(50);
            powerTimer.Elapsed += powerTimer_Elapsed;
            power = new Line();
            power.X1 = 0;
            power.Y1 = 10;
            power.X2 = 0;
            power.Y2 = 10;
            power.StrokeThickness = 10;
            power.Stroke = Brushes.Green;
            canvas.Children.Add(power);
            powerDirection = true;
            powerTimer.Start();
        }

        void powerTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                power.Dispatcher.Invoke(() =>
                {
                    if ((power.X2 == 0) || (power.X2 == 100))
                        powerDirection = !powerDirection;
                    if (powerDirection)
                        power.X2--;
                    else
                        power.X2++;

                    if (power.X2 > 40)
                    {
                        power.Stroke = Brushes.Yellow;
                        if (power.X2 > 80)
                            power.Stroke = Brushes.Red;
                    }
                });
            }
            catch (Exception) { };
        }

        public void DrawBullet(Canvas canvas)
        {
            powerTimer.Stop();
            canvas.Children.Clear();
            V0 = power.X2;

            bullet = new Rectangle();
            bullet.Fill = new ImageBrush(new BitmapImage(new Uri("IMGs\\bullet.png", UriKind.Relative)));
            bullet.Width = 20;
            bullet.Height = 40;
            bullet.Margin = new Thickness(-30, -30, 0, 0);
            canvas.Children.Add(bullet);

            t = 0;

            flyTimer = new System.Timers.Timer(41);
            flyTimer.Elapsed += flyTimer_Elapsed;
            flyTimer.Start();           
        }

        void flyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t += 0.1;
            try
            {
                bullet.Dispatcher.Invoke(() =>
                {
                    Draw(t);
                });
            }
            catch (Exception) { };
        }

        private void Draw(double t)
        {
            bullet.Margin = new Thickness(X(t), Y(t), 0, 0);
            bullet.RenderTransform = new RotateTransform(A(t));
        }
    }
}
