using Week3Lab.Views;

namespace Week3Lab
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EditTodoPage), typeof(EditTodoPage));

        }
    }
}
