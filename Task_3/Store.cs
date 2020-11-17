using System.Collections.ObjectModel;

namespace Task_3
{
    public class Store
    {
        public Store()
        {
            Articles = new ObservableCollection<Article>();
        }

        public ObservableCollection<Article> Articles { get; }
    }
}