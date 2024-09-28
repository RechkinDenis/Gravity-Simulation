using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gravity_Simulation
{
    public class SpaceObject
    {
        public Vector Pos { get; set; }
        public double Mass { get; }
        public double Radius { get; }

        public SpaceObject(Vector pos, double mass, double radius)
        {
            Pos = pos;
            Mass = mass;
            Radius = radius;
        }

        public void Draw(Graphics g, float scale, Vector cameraPosition)
        {
            // Рассчитываем экранные координаты с учетом положения камеры
            float x = (float)((Pos.X - cameraPosition.X) * scale) + (800 / 2); // Центрируем по окну
            float y = (float)((Pos.Y - cameraPosition.Y) * scale) + (600 / 2); // Центрируем по окну
            float diameter = (float)(Radius * scale);

            // Проверяем, чтобы объект был видим на экране
            if (diameter > 0)
            {
                g.FillEllipse(Brushes.Blue, x - diameter / 2, y - diameter / 2, diameter, diameter);
            }
        }
    }

    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class MainForm : Form
    {
        private SpaceObject earth;
        private SpaceObject moon;
        private float scale = 1e-6f; // Масштаб
        private Vector cameraPosition = new(0, 0); // Положение "камеры"

        public MainForm()
        {
            earth = new SpaceObject(new Vector(0, 0), 5.972e24, 6371e3); // Земля
            moon = new SpaceObject(new Vector(384400e3, 0), 7.347673e22, 1737.4e3); // Луна

            Paint += new PaintEventHandler(OnPaint);
            KeyDown += new KeyEventHandler(OnKeyDown);
            var screenSize = Screen.PrimaryScreen.Bounds.Size;
            ClientSize = new Size(screenSize.Width, screenSize.Height);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            earth.Draw(g, scale, cameraPosition);
            moon.Draw(g, scale, cameraPosition);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            const float moveAmount = 100e3f * (10 * 10);

            switch (e.KeyCode)
            {
                case Keys.W:
                    cameraPosition.Y -= moveAmount; 
                    break;
                case Keys.S:
                    cameraPosition.Y += moveAmount;
                    break;
                case Keys.A:
                    cameraPosition.X -= moveAmount;
                    break;
                case Keys.D:
                    cameraPosition.X += moveAmount;
                    break;
            }

            Invalidate();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}