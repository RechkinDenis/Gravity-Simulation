namespace Gravity_Simulation
{
    public class MainForm : Form
    {
        private readonly Vector cameraPosition = new(9.98948e10, 0);
        private float scale = 1e-7f; // scale 1e-6f
        private readonly float scaleFactor = 0.01f;

        public float Scale => scale;

        private readonly System.Windows.Forms.Timer timer;
        private readonly double deltaTime = 120000;

        private List<SpaceObject> objects = [];

        private readonly ControlForm controlForm;

        public MainForm()
        {
            objects = [
                new SpaceObject(name: "sun", pos: new Vector(0, 0), inertia: new Vector(0, 0), mass: 1.989e30, radius: 6.957e8),
                new SpaceObject(name: "earth", pos: new Vector(1.496e11, 0), inertia: new Vector(0, 29783), mass: 5.972e24, radius: 6371e3),
                new SpaceObject(name: "moon", pos: new Vector(1.496e11 + 384400e3, 0), inertia: new Vector(0, 29783 + 1022), mass: 7.347673e22, radius: 1737.4e3)
            ];

            Paint += new PaintEventHandler(OnPaint);
            KeyDown += new KeyEventHandler(OnKeyDown);
            var screenSize = Screen.PrimaryScreen.Bounds.Size;
            ClientSize = new Size(screenSize.Width, screenSize.Height);
            WindowState = FormWindowState.Maximized;

            timer = new System.Windows.Forms.Timer
            {
                Interval = 10
            };
            timer.Tick += OnTick;
            timer.Start();

            controlForm = new ControlForm(this);
            controlForm.Show();

            DoubleBuffered = true;
        }

        public void UpdateScale(float newScale)
        {
            scale = newScale;
            Invalidate();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = 0; j < objects.Count; j++)
                {
                    if (i != j)
                    {
                        objects[i].ApplyGravity(objects[j], deltaTime);
                    }
                }
            }

            foreach (var obj in objects)
            {
                obj.UpdatePosition(deltaTime);
            }

            var earth = objects.FirstOrDefault(o => o.Name == "earth");
            if (earth != null)
            {
                cameraPosition.X = earth.Pos.X;
                cameraPosition.Y = earth.Pos.Y;
            }

            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            float s = scale * scaleFactor;

            foreach (var obj in objects)
            {
                obj.Draw(g, s, cameraPosition);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            const float moveAmount = 100e3f * 1000;

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