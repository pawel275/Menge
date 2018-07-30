namespace XmlGenerator
{
    public static class Utils
    {
        public static string Str(double val)
        {
            return string.Format("{0:0.###}", val).Replace(',', '.');
        }

        public static (double min_x, double min_y, double max_x, double max_y) BoxPosition(double x, double y, double size, double scale = 1)
        {
            return (
                (x - size / 2) * scale,
                (y - size / 2) * scale,
                (x + size / 2) * scale,
                (y + size / 2) * scale);
        }
    }
}