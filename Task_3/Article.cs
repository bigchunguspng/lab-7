using System;

namespace Task_3
{
    public class Article
    {
        public Article(string name, string storeName, float price)
        {
            Name = name;
            StoreName = storeName;
            Price = price;
        }

        public string Name { get; }
        private string StoreName { get; }
        private float Price { get; }

        public string Info() => $"Назва: {Name}\nПродавець: {StoreName}\nЦіна: {Math.Round(Price, 2)} грн";
    }
}