using Budget.Views;

namespace Budget
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("History//Create", typeof(CreateNewTransactionPage));
        }
    }
}