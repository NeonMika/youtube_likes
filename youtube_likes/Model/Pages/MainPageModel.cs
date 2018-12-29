using System.ComponentModel;
using System.Runtime.CompilerServices;
using YoutubeLikes.Annotations;

namespace YoutubeLikes.Model.Pages
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private YoutubeLikeModel youtubeLikeModel;
        public MainPageModel(YoutubeLikeModel youtTubeLikeModel) => YoutubeLikeModel = youtTubeLikeModel;

        public YoutubeLikeModel YoutubeLikeModel
        {
            get => youtubeLikeModel;
            set
            {
                youtubeLikeModel = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}