using System.Windows;
using YoutubeLikes.Model;
using YoutubeLikes.View.Pages;

namespace YoutubeLikes.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly InitPage initPage;
        private readonly MainPage mainPage;

        public MainWindow()
        {
            var model = new YoutubeLikeModel();

            initPage = new InitPage(this, model);
            mainPage = new MainPage(this, model);

            Content = mainPage;
        }

        public void ShowMainPage()
        {
            Content = mainPage;
        }

        public void ShowInitPage()
        {
            Content = initPage;
        }
    }
}