namespace Gravity_Simulation
{
    public class Vector(double x, double y)
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public double LengthSquared()
        {
            return X * X + Y * Y;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public void Normalize()
        {
            double length = Math.Sqrt(X * X + Y * Y);
            if (length > 0)
            {
                X /= length;
                Y /= length;
            }
        }
    }
}