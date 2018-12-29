using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Google.Apis.YouTube.v3.Data;
using YoutubeLikes.Annotations;

namespace YoutubeLikes.Model
{
    public class YoutubeLikeModel : INotifyPropertyChanged
    {
        private ObservableCollection<Video> likedVideos;

        public ObservableCollection<Video> LikedVideos
        {
            get => likedVideos;
            set
            {
                likedVideos = value;
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