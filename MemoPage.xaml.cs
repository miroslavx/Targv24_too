namespace targv24_too;

public partial class MemoPage : ContentPage
{
    private Game game;  
    private List<Theme> themes;  
    private int currentThemeIndex = 0; 
    private int currentDifficulty = 1;  // (0=легко, 1=средне, 2=сложно)

    // Размеры игрового поля
    private int rows = 4;
    private int cols = 4;
    private int totalPairs = 8;

    // Связь между картами в игре и картинками на экране
    private Dictionary<Card, Image> cardImageMap;

    public MemoPage()
    {
        InitializeComponent();
        InitializeThemes();
        cardImageMap = new Dictionary<Card, Image>();
        difficultyPicker.SelectedIndex = 1;  // Средний уровень по умолчанию
        StartNewGame();
    }

    //  темы: светлая, тёмная и зелёная
    private void InitializeThemes()
    {
        themes = new List<Theme>
        {
            new Theme("Hele",
                Color.FromRgb(240, 240, 240),
                Colors.White,
                Colors.Black,
                Color.FromRgb(70, 130, 180)),  

            new Theme("Tume",
                Color.FromRgb(30, 30, 30),
                Color.FromRgb(50, 50, 50),
                Colors.White,
                Color.FromRgb(255, 100, 100)), 

            new Theme("Roheline",
                Color.FromRgb(200, 230, 200),
                Color.FromRgb(144, 238, 144),
                Color.FromRgb(0, 100, 0),
                Color.FromRgb(34, 139, 34))
        };
    }

    // Обработка выбора уровня сложности
    private void DifficultyPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        currentDifficulty = difficultyPicker.SelectedIndex;
        switch (currentDifficulty)
        {
            case 0: // KERGE 3x4 = 12 карт (6 пар)
                rows = 3;
                cols = 4;
                totalPairs = 6;
                break;
            case 1: // KESKMINE 4x4 = 16 карт (8 пар)
                rows = 4;
                cols = 4;
                totalPairs = 8;
                break;
            case 2: // RASKE 4x5 = 20 карт (10 пар)
                rows = 4;
                cols = 5;
                totalPairs = 10;
                break;
        }

        StartNewGame();  
    }

    private void StartNewGame()
    {
        Player player = new Player("Mängija");
        game = new Game(player, themes[currentThemeIndex], totalPairs);

        cardImageMap.Clear();  // очистка старых связей
        CreateGameBoard();     // игровое поле
        UpdateUI();            // статистика
        ApplyTheme();         
    }

    // сЕТКА
    private void CreateGameBoard()
    {
        gameGrid.Children.Clear();
        gameGrid.RowDefinitions.Clear();
        gameGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < rows; i++)
        {
            gameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
        }
        for (int i = 0; i < cols; i++)
        {
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
        }

        for (int i = 0; i < game.Cards.Count; i++)
        {
            int row = i / cols;  // Номер строки
            int col = i % cols;  // Номер столбца

            Card card = game.Cards[i];

            // Рамка карты
            Frame cardFrame = new Frame
            {
                Padding = 5,
                CornerRadius = 0,
                HasShadow = true,
                BackgroundColor = game.CurrentTheme.CardBackColor,
                BorderColor = game.CurrentTheme.AccentColor
            };

            // Картинка карты (сначала все показывают рубашку)
            Image cardImage = new Image
            {
                Source = game.CardBackImage,
                Aspect = Aspect.AspectFit
            };

            cardFrame.Content = cardImage;

            // карта картинка на экране
            cardImageMap[card] = cardImage;

            // бработчик клика
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) => await OnCardTapped(card);
            cardFrame.GestureRecognizers.Add(tapGesture);

            // Карта в нужной ячейке
            Grid.SetRow(cardFrame, row);
            Grid.SetColumn(cardFrame, col);
            gameGrid.Children.Add(cardFrame);
        }
    }


    private async Task OnCardTapped(Card card)
    {
        bool result = await game.OnCardTapped(card, UpdateAllCards);

        if (result)
        {
            UpdateUI();  

            // Если все пары найдены показываем результат
            if (game.IsGameFinished())
            {
                game.Player.CalculateScore();
                string difficultyText = currentDifficulty == 0 ? "KERGE" :
                                       currentDifficulty == 1 ? "KESKMINE" : "RASKE";
                await DisplayAlert("MÄNG LÄBI!",
                    $"Palju õnne!\n\nTase: {difficultyText}\nKäigud: {game.Player.Moves}\nPunktid: {game.Player.Score}",
                    "OK");
            }
        }
    }
    private void UpdateAllCards()
    {
        foreach (var kvp in cardImageMap)
        {
            Card card = kvp.Key;
            Image cardImage = kvp.Value;

            // Если карта открыта или найдена показываем картинку
            if (card.IsFlipped || card.IsMatched)
            {
                cardImage.Source = card.ImageName;
            }
            else  // Если закрыта показываем рубашку
            {
                cardImage.Source = game.CardBackImage;
            }
        }
    }
    private void UpdateUI()
    {
        movesLabel.Text = game.Player.Moves.ToString();
        pairsLabel.Text = $"{game.Player.PairsFound}/{totalPairs}";
        scoreLabel.Text = game.Player.Score.ToString();
    }
    private void ApplyTheme()
    {
        Theme theme = game.CurrentTheme;
        titleFrame.BackgroundColor = theme.CardBackColor;
        titleFrame.BorderColor = theme.AccentColor;
        titleLabel.TextColor = theme.TextColor;

        // Выбор сложности
        difficultyFrame.BackgroundColor = theme.CardBackColor;
        difficultyFrame.BorderColor = theme.AccentColor;
        difficultyTitleLabel.TextColor = theme.TextColor;
        difficultyPicker.TextColor = theme.TextColor;
        difficultyPicker.BackgroundColor = theme.BackgroundColor;

        // Статистика: ходы
        movesFrame.BackgroundColor = theme.CardBackColor;
        movesFrame.BorderColor = theme.AccentColor;
        movesTitle.TextColor = theme.TextColor;
        movesLabel.TextColor = theme.TextColor;

        // Статистика: пары
        pairsFrame.BackgroundColor = theme.CardBackColor;
        pairsFrame.BorderColor = theme.AccentColor;
        pairsTitle.TextColor = theme.TextColor;
        pairsLabel.TextColor = theme.TextColor;

        // Статистика: очки
        scoreFrame.BackgroundColor = theme.CardBackColor;
        scoreFrame.BorderColor = theme.AccentColor;
        scoreTitle.TextColor = theme.TextColor;
        scoreLabel.TextColor = theme.TextColor;

        // Игровое поле
        gameFrame.BackgroundColor = theme.CardBackColor;
        gameFrame.BorderColor = theme.AccentColor;

        // Кнопки
        newGameButtonFrame.BorderColor = theme.AccentColor;
        newGameButton.BackgroundColor = theme.AccentColor;
        newGameButton.TextColor = Colors.White;

        themeButtonFrame.BorderColor = theme.AccentColor;
        themeButton.BackgroundColor = theme.AccentColor;
        themeButton.TextColor = Colors.White;

        backButtonFrame.BorderColor = theme.AccentColor;
        backButton.BackgroundColor = theme.AccentColor;
        backButton.TextColor = Colors.White;
    }

    // Кнопка "UUS MÄNG" начать заново
    private void NewGame_Clicked(object sender, EventArgs e)
    {
        StartNewGame();
    }

    // Кнопка "TEEMA" сменить тему
    private void ChangeTheme_Clicked(object sender, EventArgs e)
    {
        currentThemeIndex = (currentThemeIndex + 1) % themes.Count;  // Циклично: 0→1→2→0
        game.CurrentTheme = themes[currentThemeIndex];
        ApplyTheme();
    }

    // Кнопка "TAGASI" вернуться назад
    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}