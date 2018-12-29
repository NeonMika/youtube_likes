using System.Windows;
using System.Windows.Controls;
using YoutubeLikes.Model;
using YoutubeLikes.Model.Pages;

namespace YoutubeLikes.View.Pages
{
    /// <summary>
    ///     Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly MainWindow mainWindow;
        private readonly MainPageModel model;

        public MainPage(MainWindow mainWindow, YoutubeLikeModel model)
        {
            this.mainWindow = mainWindow;
            this.model = new MainPageModel(model);

            DataContext = this.model;

            InitializeComponent();
        }

        private void InitButton_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.ShowInitPage();
        }
    }
}