namespace Roko.Template.Blocks.Common.Kernel
{
    public static class SystemClock
    {
        private static DateTimeOffset? _presentDateTime;

        public static DateTimeOffset UtcNow
        {
            get
            {
                return _presentDateTime?.UtcDateTime ?? DateTimeOffset.UtcNow;
            }
        }

        public static DateTimeOffset Now
        {
            get
            {
                return _presentDateTime ?? DateTimeOffset.Now;
            }
        }

        public static void Set(DateTimeOffset presentDateTime) => _presentDateTime = presentDateTime;

        public static void Reset() => _presentDateTime = null;
    }
}