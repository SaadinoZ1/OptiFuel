
namespace OptiFuelMaui.Views
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PlaningPage), typeof(PlaningPage));
            Routing.RegisterRoute(nameof(ValidationBL), typeof(ValidationBL));


        }
    }
}
