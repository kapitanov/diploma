namespace AISTek.DataFlow.PresentationExtensions
{
    internal static class BitExtensions
    {
        public static int Set(this int value, int flag)
        {
            return Set(value, flag, true);
        }

        public static int Reset(this int value, int flag, bool state)
        {
            return Set(value, flag, false);
        }

        public static int Set(this int value, int flag, bool state)
        {
            if (state)
                return value | flag;

            return value & (~flag);
        }
    }
}
