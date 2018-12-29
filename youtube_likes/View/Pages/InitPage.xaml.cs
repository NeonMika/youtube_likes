using System.Windows;
using System.Windows.Controls;
using YoutubeLikes.Controller;
using YoutubeLikes.Model;
using YoutubeLikes.Model.Pages;

namespace YoutubeLikes.View.Pages
{
    /// <summary>
    ///     Interaction logic for MainPage.xaml
    /// </summary>
    public partial class InitPage : Page
    {
        private readonly MainWindow mainWindow;
        private readonly InitPageModel model;

        public InitPage(MainWindow mainWindow, YoutubeLikeModel model)
        {
            this.mainWindow = mainWindow;
            this.model = new InitPageModel(model);

            DataContext = this.model;

            InitializeComponent();
        }

        private void PullButton_OnClick(object sender, RoutedEventArgs e)
        {
            YoutubeController ytController = new YoutubeController();
            ytController.Init((current, length) => (model.CurrentInitValue, model.InitLength) = (current, length));
        }
    }
}