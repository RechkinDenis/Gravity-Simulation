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
    }
}