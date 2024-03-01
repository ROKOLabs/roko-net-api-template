namespace Roko.Template
{
    using System.Threading.Tasks;

    public static class Program
    {
        public static async Task Main(string[] args) => await ApplicationLauncher.RunAsync<Startup>(args);
    }
}
