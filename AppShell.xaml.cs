using FinanceCo.Views;

namespace FinanceCo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainOverseePage), typeof(MainOverseePage));
            Routing.RegisterRoute(nameof(AddInfoPage), typeof(AddInfoPage));
            Routing.RegisterRoute(nameof(EditInfoPage), typeof(EditInfoPage));
            Routing.RegisterRoute(nameof(ShowStatsPage), typeof(ShowStatsPage));
        }
    }
}
