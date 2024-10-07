namespace Gravity_Simulation
{
    public class MainForm : Form
    {
        private readonly Vector cameraPosition = new(9.98948e10, 0);
        private float scale = 1e-7f; // scale 1e-6f
        private readonly float scaleFactor = 0.01f;

        new public float Scale => scale;

        private readonly System.Windows.Forms.Timer timer;
        private readonly double deltaTime = 120000;

        private readonly List<SpaceObject> objects = Objects.objects;

        private readonly ControlForm controlForm;

        public MainForm()
        {
            Paint += new PaintEventHandler(OnPaint);
            KeyDown += new KeyEventHandler(OnKeyDown);
            var screenSize = Screen.PrimaryScreen?.Bounds.Size ?? new Size(800, 600);
            ClientSize = new Size(screenSize.Width, screenSize.Height);
            WindowState = FormWindowState.Maximized;

            timer = new System.Windows.Forms.Timer
            {
                Interval = 15
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

        private void OnTick(object? sender, EventArgs? e)
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

            var earth = objects.FirstOrDefault(o => o.Name == "sun");
            if (earth != null)
            {
                cameraPosition.X = earth.Pos.X;
                cameraPosition.Y = earth.Pos.Y;
            }

            Invalidate();
        }

        private void OnPaint(object? sender, PaintEventArgs? e)
        {
            if (e?.Graphics == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            float s = scale * scaleFactor;

            foreach (var obj in objects)
            {
                obj.Draw(g, s, cameraPosition);
            }
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
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