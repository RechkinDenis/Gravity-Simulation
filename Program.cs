namespace Gravity_Simulation
{
    public class MainForm : Form
    {
        private readonly SpaceObject earth;
        private readonly SpaceObject moon;
        private readonly Vector cameraPosition = new(0, 0);
        private float scale = 1e-6f; // scale 1e-6f
        private readonly float scaleFactor = 1f;

        public float Scale => scale;

        private readonly System.Windows.Forms.Timer timer;
        private readonly double deltaTime = 1;

        private List<SpaceObject> objects = [];

        private ControlForm controlForm;

        public MainForm()
        {
            earth = new SpaceObject(name: "earth", pos: new Vector(0, 0), inertia: new Vector(0, 0), mass: 5.972e24, radius: 6371e3);
            moon = new SpaceObject(name: "moon", pos: new Vector(384400e3, 0), inertia: new Vector(0, 1022 * 250), mass: 7.347673e22, radius: 1737.4e3);

            objects = [earth, moon];

            Paint += new PaintEventHandler(OnPaint);
            KeyDown += new KeyEventHandler(OnKeyDown);
            var screenSize = Screen.PrimaryScreen.Bounds.Size;
            ClientSize = new Size(screenSize.Width, screenSize.Height);
            WindowState = FormWindowState.Maximized;

            timer = new System.Windows.Forms.Timer
            {
                Interval = 16
            };
            timer.Tick += OnTick; ;
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
            foreach (var obj in objects)
            {
                obj.UpdatePosition(deltaTime);
            }
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            float s = scale * scaleFactor;
            earth.Draw(g, s, cameraPosition);
            moon.Draw(g, s, cameraPosition);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            const float moveAmount = 100e3f * 100;

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