﻿namespace targv24_too
{
    public class Card
    {
        // Свойства карты
        public string ImageName { get; set; }  // Название изображения
        public bool IsFlipped { get; set; }    // Открыта ли карта
        public bool IsMatched { get; set; }    // Найдена ли пара

        // Конструктор
        public Card(string imageName)
        {
            ImageName = imageName;
            IsFlipped = false;
            IsMatched = false;
        }

        // Методы
        public void Flip()
        {
            IsFlipped = !IsFlipped;
        }

        public void SetMatched()
        {
            IsMatched = true;
        }
    }
}