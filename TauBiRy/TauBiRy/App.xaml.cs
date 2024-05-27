using TauBiRy.Views;

namespace TauBiRy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new BookListPage());
        }
    }
}
