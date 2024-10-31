
namespace BlazeM4BManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BookView), typeof(BookView));
        }
    }
}
