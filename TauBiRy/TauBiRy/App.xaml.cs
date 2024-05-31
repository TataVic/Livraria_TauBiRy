using TauBiRy.Views;

namespace TauBiRy
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            

            bool isUserLoggedIn = CheckUserLogado();

            if (isUserLoggedIn)
            {
                MainPage = new NavigationPage(new BookListPage());
            }
            else
            {
                // Navegue para a página de login
                MainPage = new NavigationPage(new LoginPage());
            }
        }
        private bool CheckUserLogado()
        {

            return false;
        }
    }
}
