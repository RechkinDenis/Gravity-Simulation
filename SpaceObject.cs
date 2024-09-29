

namespace Gravity_Simulation
{
    public class SpaceObject(string? name, Vector pos, Vector inertia, double mass, double radius)
    {
        public string? Name { get; set; } = name;
        public Vector Pos { get; set; } = pos;
        public Vector Inertia { get; set; } = inertia;
        public double Mass { get; } = mass;
        public double Radius { get; } = radius;

        #region const
        public const double G = 6.67408e-11;
        #endregion

        #region formulas
        public double GravitationalForce(SpaceObject obj)
        {
            double distanceSq = (Pos - obj.Pos).LengthSquared();
            if (distanceSq == 0) return 0;
            return (G * Mass * obj.Mass) / distanceSq;
        }

        #endregion

        public void Draw(Graphics g, float scale, Vector cameraPosition)
        {
            var screenSize = Screen.PrimaryScreen.Bounds.Size;
            float x = (float)((Pos.X - cameraPosition.X) * scale) + (screenSize.Width / 2);
            float y = (float)((Pos.Y - cameraPosition.Y) * scale) + (screenSize.Height / 2);
            float diameter = (float)(Radius * scale);

            if (diameter > 0)
            {
                g.FillEllipse(Brushes.Blue, x - diameter / 2, y - diameter / 2, diameter, diameter);
                DrawName(g, scale, x, y);
            }

            DrawInertionArrow(g, scale, x, y);
        }

        public void ApplyGravity(SpaceObject obj, double deltaTime)
        {
            double force = GravitationalForce(obj);
            double distance = Math.Sqrt((Pos.X - obj.Pos.X) * (Pos.X - obj.Pos.X) + (Pos.Y - obj.Pos.Y) * (Pos.Y - obj.Pos.Y));

            if (distance > 0)
            {
                double acceleration = force / Mass;

                Vector direction = new(obj.Pos.X - Pos.X, obj.Pos.Y - Pos.Y);

                Inertia.X += direction.X * acceleration * deltaTime;
                Inertia.Y += direction.Y * acceleration * deltaTime;
            }
        }


        public void DrawName(Graphics g, float scale, float x, float y)
        {
            float fontSize = 12f * scale;
            if (fontSize < 6f) fontSize = 6f;
            using Font font = new("Arial", fontSize);
            using Brush brush = new SolidBrush(Color.White);
            g.DrawString(Name, font, brush, x, y - (float)(Radius * scale) / 2 - fontSize);
        }

        private void DrawInertionArrow(Graphics g, float scale, float x, float y)
        {
            if(Inertia.X == 0 && Inertia.Y == 0) return;
            float arrowLength = 50f;
            float endX = x + (float)Inertia.X * arrowLength * scale;
            float endY = y + (float)Inertia.Y * arrowLength * scale;

            g.DrawLine(Pens.Red, x, y, endX, endY);

            DrawArrowHead(g, x, y, endX, endY);
        }

        private void DrawArrowHead(Graphics g, float xStart, float yStart, float xEnd, float yEnd)
        {
            var arrowAngle = Math.Atan2(yEnd - yStart, xEnd - xStart);
            var arrowSize = 10;

            float leftX = (float)(xEnd - arrowSize * Math.Cos(arrowAngle - Math.PI / 6));
            float leftY = (float)(yEnd - arrowSize * Math.Sin(arrowAngle - Math.PI / 6));
            float rightX = (float)(xEnd - arrowSize * Math.Cos(arrowAngle + Math.PI / 6));
            float rightY = (float)(yEnd - arrowSize * Math.Sin(arrowAngle + Math.PI / 6));

            g.DrawLine(Pens.Red, xEnd, yEnd, leftX, leftY);
            g.DrawLine(Pens.Red, xEnd, yEnd, rightX, rightY);
        }

        public void UpdatePosition(double deltaTime)
        {
            Pos.X += Inertia.X * deltaTime;
            Pos.Y += Inertia.Y * deltaTime;
        }
    }
}