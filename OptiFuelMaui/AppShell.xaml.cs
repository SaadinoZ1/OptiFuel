
namespace OptiFuelMaui.Views
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PlanningPage), typeof(PlanningPage));
            Routing.RegisterRoute(nameof(ValidationBL), typeof(ValidationBL));
            Routing.RegisterRoute(nameof(AddPlanning), typeof(AddPlanning));



        }
    }
}
