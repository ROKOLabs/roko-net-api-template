namespace Roko.Template.Blocks.Common.Kernel
{
    public static class SystemGuid
    {
        private static Guid? _presentGuid;

        public static Guid NewGuid
        {
            get
            {
                return _presentGuid ?? Guid.NewGuid();
            }
        }

        public static Guid Empty => Guid.Empty;

        public static void Set(Guid presentGuid) => _presentGuid = presentGuid;

        public static void Reset() => _presentGuid = null;
    }
}