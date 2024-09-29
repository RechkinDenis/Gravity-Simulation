namespace Gravity_Simulation
{
    public class ControlForm : Form
    {
        private readonly MainForm mainForm;
        private TrackBar scaleTrackBar;

        public ControlForm(MainForm mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponents();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Screen.AllScreens[1].WorkingArea.X, Screen.AllScreens[1].WorkingArea.Y); // Устанавливаем положение на втором экране
        }

        private void InitializeComponents()
        {
            scaleTrackBar = new TrackBar
            {
                Minimum = 1, // Минимальное значение
                Maximum = 100 // Максимальное значение
            };

            // Убедимся, что значение scale находится в диапазоне от 1 до 100
            int initialValue = Math.Clamp((int)(mainForm.Scale * 1e6), scaleTrackBar.Minimum, scaleTrackBar.Maximum);
            scaleTrackBar.Value = initialValue; // Присваиваем значение в допустимых пределах
            scaleTrackBar.Dock = DockStyle.Fill;

            scaleTrackBar.Scroll += ScaleTrackBar_Scroll;

            Controls.Add(scaleTrackBar);
        }


        private void ScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            float newScale = scaleTrackBar.Value / 1e6f;
            mainForm.UpdateScale(newScale);
        }
    }
}