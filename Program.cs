namespace Gravity_Simulation
{
    public class SpaceObject(Vector pos, double mass, double radius)
    {
        public Vector Pos { get; set; } = pos;
        public double Mass { get; } = mass;
        public double Radius { get; } = radius;

        public void Draw(Graphics g, float scale, Vector cameraPosition)
        {
            var screenSize = Screen.PrimaryScreen.Bounds.Size;
            float x = (float)((Pos.X - cameraPosition.X) * scale) + (screenSize.Width / 2);
            float y = (float)((Pos.Y - cameraPosition.Y) * scale) + (screenSize.Height / 2);
            float diameter = (float)(Radius * scale);

            if (diameter > 0)
            {
                g.FillEllipse(Brushes.Blue, x - diameter / 2, y - diameter / 2, diameter, diameter);
            }
        }
    }

    public class Vector(double x, double y)
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
    }

    public class MainForm : Form
    {
        private SpaceObject earth;
        private SpaceObject moon;
        private float scale = 1e-6f; // Масштаб
        private Vector cameraPosition = new(0, 0);

        public MainForm()
        {
            earth = new SpaceObject(new Vector(0, 0), 5.972e24, 6371e3);
            moon = new SpaceObject(new Vector(384400e3, 0), 7.347673e22, 1737.4e3);

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
                case Keys.W or Keys.Up:
                    cameraPosition.Y -= moveAmount; 
                    break;
                case Keys.S or Keys.Down:
                    cameraPosition.Y += moveAmount;
                    break;
                case Keys.A or Keys.Left:
                    cameraPosition.X -= moveAmount;
                    break;
                case Keys.D or Keys.Right:
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