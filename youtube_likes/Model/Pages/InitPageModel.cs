using System.ComponentModel;
using System.Runtime.CompilerServices;
using YoutubeLikes.Annotations;

namespace YoutubeLikes.Model.Pages
{
    public class InitPageModel : INotifyPropertyChanged
    {
        private int currentInitValue;
        private int initLength;
        private YoutubeLikeModel youtubeLikeModel;

        public InitPageModel(YoutubeLikeModel youtubeLikeModel)
        {
            YoutubeLikeModel = youtubeLikeModel;
            CurrentInitValue = 0;
            InitLength = 0;
        }

        public YoutubeLikeModel YoutubeLikeModel
        {
            get => youtubeLikeModel;
            set
            {
                youtubeLikeModel = value;
                OnPropertyChanged();
            }
        }

        public int CurrentInitValue
        {
            get => currentInitValue;
            set
            {
                currentInitValue = value;
                OnPropertyChanged();
            }
        }

        public int InitLength
        {
            get => initLength;
            set
            {
                initLength = value;
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