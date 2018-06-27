namespace XmlGenerator
{
    public static class Utils
    {
        public static string Str(double val)
        {
            return string.Format("{0:0.###}", val).Replace(',', '.');
        }
    }
}