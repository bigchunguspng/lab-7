using System.ComponentModel;
using System.Runtime.CompilerServices;
using Task_3.Annotations;

namespace Task_3
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;
        private int _searchIndex;
        private string _selectedArticleInfo;

        public ViewModel()
        {
            //приклад товарів
            Store = new Store()
            {
                Articles =
                {
                    new Article("Hubba Bubba", "SomeSweet", 59),
                    new Article("2 гривні", "Михайло Джексонович", 2),
                    new Article("PS5", "Sony", 16799),
                    new Article("Штани", "Все по 40", 40),
                    new Article("PS5", "Adobe", 11706)
                }
            };
        }

        private Store Store { get; }

        #region search

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                Search();
            }
        }

        public int SearchIndex
        {
            get => _searchIndex;
            set
            {
                _searchIndex = value;
                OnPropertyChanged(nameof(SearchIndex));
                Search(true);
            }
        }

        private void Search(bool searchByIndex = false)
        {
            var result = "";
            if (searchByIndex) result = Store.Articles[SearchIndex].Info();
            else
                foreach (var article in Store.Articles)
                    if (article.Name.ToLower().Trim().Contains(SearchQuery.ToLower().Trim()))
                    {
                        result = article.Info();
                        break;
                    }

            SelectedArticleInfo = result != "" ? result : "Товар не знайдено";
        }

        public string SelectedArticleInfo
        {
            get => _selectedArticleInfo;
            set
            {
                if (value == _selectedArticleInfo) return;
                _selectedArticleInfo = value;
                OnPropertyChanged(nameof(SelectedArticleInfo));
            }
        }

        #endregion

        #region new artice addition
        
        private string _newArticleName;
        private string _newArticleStore;
        private float _newArticlePrice;
        private RelayCommand _newArticle;

        public string NewArticleName
        {
            get => _newArticleName;
            set
            {
                _newArticleName = value;
                OnPropertyChanged(nameof(NewArticleName));
            }
        }

        public string NewArticleStore
        {
            get => _newArticleStore;
            set
            {
                _newArticleStore = value;
                OnPropertyChanged(nameof(NewArticleStore));
            }
        }

        public float NewArticlePrice
        {
            get => _newArticlePrice;
            set
            {
                _newArticlePrice = value;
                OnPropertyChanged(nameof(NewArticlePrice));
            }
        }

        public RelayCommand NewArticle => _newArticle ?? (_newArticle = new RelayCommand(o =>
        {
            Store.Articles.Add(new Article(NewArticleName, NewArticleStore, NewArticlePrice));
            OnPropertyChanged(nameof(StoreInfo));
        }));

        #endregion
        
        public string StoreInfo => "Асортимент товарів: " + Store.Articles.Count + " од.";

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator] private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}