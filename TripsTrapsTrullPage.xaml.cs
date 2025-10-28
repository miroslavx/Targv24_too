namespace targv24_too;

public partial class TripsTrapsTrullPage : ContentPage
{
    private string current = "X";  // Чей ход
    private List<TripsTrapsTrullTheme> themes;
    private int currentThemeIndex = 0;

    public TripsTrapsTrullPage()
    {
        InitializeComponent();
        InitializeThemes();
        themePicker.SelectedIndex = 0;
    }

    // Инициализация тем
    private void InitializeThemes()
    {
        themes = new List<TripsTrapsTrullTheme>
        {
            new TripsTrapsTrullTheme("Macintosh 1984", Colors.White, Colors.White, Colors.Black, Colors.Black, Colors.White, Colors.Black, Colors.Black),
            new TripsTrapsTrullTheme("Windows 3.1", Color.FromRgb(192, 192, 192), Color.FromRgb(192, 192, 192), Colors.Black, Color.FromRgb(0, 0, 128), Colors.White, Color.FromRgb(128, 128, 128), Color.FromRgb(0, 0, 128)),
            new TripsTrapsTrullTheme("Windows 95", Color.FromRgb(0, 128, 128), Color.FromRgb(192, 192, 192), Colors.Black, Color.FromRgb(192, 192, 192), Colors.Black, Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 128)),
            new TripsTrapsTrullTheme("Commodore 64", Color.FromRgb(64, 50, 133), Color.FromRgb(111, 79, 255), Color.FromRgb(120, 207, 255), Color.FromRgb(64, 50, 133), Color.FromRgb(120, 207, 255), Color.FromRgb(120, 207, 255), Color.FromRgb(120, 207, 255))
        };
    }

    // Смена темы
    private void OnThemeChanged(object sender, EventArgs e)
    {
        currentThemeIndex = themePicker.SelectedIndex;
        ApplyTheme();
    }

    // Применить тему
    private void ApplyTheme()
    {
        var theme = themes[currentThemeIndex];

        page.BackgroundColor = theme.BackgroundColor;

        themeFrame.BackgroundColor = theme.CellColor;
        themeFrame.BorderColor = theme.BorderColor;
        themeLabelTitle.TextColor = theme.TextColor;
        themePicker.TextColor = theme.TextColor;
        themePicker.BackgroundColor = theme.BackgroundColor;

        turnLabel.TextColor = theme.AccentColor;

        gameFrame.BackgroundColor = theme.BorderColor;
        gameFrame.BorderColor = theme.BorderColor;
        gameGrid.BackgroundColor = theme.BorderColor;

        ApplyButtonTheme(btn00, theme);
        ApplyButtonTheme(btn01, theme);
        ApplyButtonTheme(btn02, theme);
        ApplyButtonTheme(btn10, theme);
        ApplyButtonTheme(btn11, theme);
        ApplyButtonTheme(btn12, theme);
        ApplyButtonTheme(btn20, theme);
        ApplyButtonTheme(btn21, theme);
        ApplyButtonTheme(btn22, theme);

        newGameButton.BackgroundColor = theme.ButtonColor;
        newGameButton.TextColor = theme.ButtonTextColor;
        backButton.BackgroundColor = theme.ButtonColor;
        backButton.TextColor = theme.ButtonTextColor;
    }

    private void ApplyButtonTheme(Button btn, TripsTrapsTrullTheme theme)
    {
        btn.BackgroundColor = theme.CellColor;
        btn.TextColor = theme.TextColor;
    }

    // Клик по клетке
    private void OnCellClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && string.IsNullOrEmpty(btn.Text))
        {
            btn.Text = current;

            if (CheckWin(current))
            {
                DisplayAlert("VÕIT!", $"{current} võitis!", "OK");
                ResetGame();
                return;
            }

            if (IsDraw())
            {
                DisplayAlert("VIIK!", "Vabad lahtrid puuduvad.", "OK");
                ResetGame();
                return;
            }

            current = current == "X" ? "O" : "X";
            turnLabel.Text = $"KÄIK: {current}";
        }
    }

    // Новая игра
    private void OnNewGameClicked(object sender, EventArgs e)
    {
        ResetGame();
    }

    // Назад
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // Очистить поле
    private void ResetGame()
    {
        btn00.Text = btn01.Text = btn02.Text = "";
        btn10.Text = btn11.Text = btn12.Text = "";
        btn20.Text = btn21.Text = btn22.Text = "";

        current = "X";
        turnLabel.Text = "KÄIK: X";

        ApplyTheme();
    }

    // Проверка победы
    private bool CheckWin(string player)
    {
        string?[,] board = new string?[3, 3]
        {
            { btn00.Text, btn01.Text, btn02.Text },
            { btn10.Text, btn11.Text, btn12.Text },
            { btn20.Text, btn21.Text, btn22.Text }
        };

        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                return true;

            if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                return true;
        }

        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            return true;

        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            return true;

        return false;
    }

    // Проверка ничьи
    private bool IsDraw()
    {
        return !string.IsNullOrEmpty(btn00.Text) &&
               !string.IsNullOrEmpty(btn01.Text) &&
               !string.IsNullOrEmpty(btn02.Text) &&
               !string.IsNullOrEmpty(btn10.Text) &&
               !string.IsNullOrEmpty(btn11.Text) &&
               !string.IsNullOrEmpty(btn12.Text) &&
               !string.IsNullOrEmpty(btn20.Text) &&
               !string.IsNullOrEmpty(btn21.Text) &&
               !string.IsNullOrEmpty(btn22.Text);
    }
}