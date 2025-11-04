namespace targv24_too
{
    public class Game
    {
        public List<Card> Cards { get; set; }
        public Player Player { get; set; }
        public Theme CurrentTheme { get; set; }
        private Card? firstCard = null;
        private Card? secondCard = null;
        private bool isChecking = false;
        private int totalPairs;

        // Доступные картинки для карт
        private List<string> availableImages = new List<string>
        {
            "sunduk.png",
            "almaz.png",
            "noz.png",
            "lampa.png"
        };

        // Рубашка карты (закрытое состояние)
        public string CardBackImage { get; } = "rubashka.png";
        public Game(Player player, Theme theme, int pairs = 8)
        {
            Player = player;
            CurrentTheme = theme;
            totalPairs = pairs;
            Cards = new List<Card>();
            InitializeCards();  // перемешка карт
        }

        // выбор карт
        private void InitializeCards()
        {
            Random random = new Random();
            List<string> selectedImages = new List<string>();

            //нужное количество картинок (могут повторяться)
            for (int i = 0; i < totalPairs; i++)
            {
                string randomImage = availableImages[random.Next(availableImages.Count)];
                selectedImages.Add(randomImage);
            }

            // по 2 карты для каждой картинки (это и есть пара)
            for (int i = 0; i < selectedImages.Count; i++)
            {
                Cards.Add(new Card(selectedImages[i]));  // Первая карта
                Cards.Add(new Card(selectedImages[i]));  // Вторая карта (пара)
            }

            ShuffleCards();  
        }

        //(алгоритм Фишера-Йейтса)
        private void ShuffleCards()
        {
            Random random = new Random();
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                // Меняем местами две случайные карты
                var temp = Cards[i];
                Cards[i] = Cards[j];
                Cards[j] = temp;
            }
        }

        // Главная логика: обработка клика по карте
        public async Task<bool> OnCardTapped(Card card, Action updateUI)
        {
            if (isChecking || card.IsMatched || card.IsFlipped)
                return false;
            // Открытие карты
            card.Flip();
            updateUI();  // Обновляем картинку на экране

            // ПЕРВАЯ КАРТА: просто запоминаем и ждём вторую
            if (firstCard == null)
            {
                firstCard = card;
                return true;
            }

            // ВТОРАЯ КАРТА: проверяем совпадение
            if (secondCard == null && card != firstCard)
            {
                secondCard = card;
                Player.IncrementMoves();  
                isChecking = true;  
                await Task.Delay(1000);
                if (firstCard.ImageName == secondCard.ImageName)
                {
                    // СОВПАЛИ оставляем открытыми
                    firstCard.SetMatched();
                    secondCard.SetMatched();
                    Player.AddPair();  // +10 очков
                }
                else
                {
                    // НЕ СОВПАЛИ закрываем обе карты
                    firstCard.Flip();
                    secondCard.Flip();
                }

                updateUI();  
                firstCard = null;
                secondCard = null;
                isChecking = false;

                return true;
            }

            return false;
        }

        // Проверка: все ли пары найдены?
        public bool IsGameFinished()
        {
            return Cards.All(c => c.IsMatched);
        }
        public void Reset()
        {
            Cards.Clear();
            Player.Moves = 0;
            Player.PairsFound = 0;
            Player.Score = 0;
            firstCard = null;
            secondCard = null;
            isChecking = false;
            InitializeCards();
        }
    }
}