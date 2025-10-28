namespace targv24_too;

public partial class TripsTrapsTrullPage : ContentPage
{
    private TripsTrapsTrullGame game;  // Логика игры в отдельном классе
    private List<TripsTrapsTrullTheme> themes;
    private int currentThemeIndex = 0;
    private Button[,] buttons;

    public TripsTrapsTrullPage()
    {
        InitializeComponent();

        game = new TripsTrapsTrullGame();

        buttons = new Button[3, 3]
        {
            { btn00, btn01, btn02 },
            { btn10, btn11, btn12 },
            { btn20, btn21, btn22 }
        };

        InitializeThemes();
        themePicker.SelectedIndex = 0;
        UpdateStatistics();
    }

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

    private void OnThemeChanged(object sender, EventArgs e)
    {
        currentThemeIndex = themePicker.SelectedIndex;
        ApplyTheme();
    }

    private void ApplyTheme()
    {
        var theme = themes[currentThemeIndex];

        page.BackgroundColor = theme.BackgroundColor;

        themeFrame.BackgroundColor = theme.CellColor;
        themeFrame.BorderColor = theme.BorderColor;
        themeLabelTitle.TextColor = theme.TextColor;
        themePicker.TextColor = theme.TextColor;
        themePicker.BackgroundColor = theme.BackgroundColor;

        statsFrame.BackgroundColor = theme.CellColor;
        statsFrame.BorderColor = theme.BorderColor;
        xWinsTitle.TextColor = oWinsTitle.TextColor = drawsTitle.TextColor = totalTitle.TextColor = theme.TextColor;
        xWinsLabel.TextColor = oWinsLabel.TextColor = drawsLabel.TextColor = totalLabel.TextColor = theme.AccentColor;

        turnLabel.TextColor = theme.AccentColor;

        gameFrame.BackgroundColor = theme.BorderColor;
        gameFrame.BorderColor = theme.BorderColor;
        gameGrid.BackgroundColor = theme.BorderColor;

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                ApplyButtonTheme(buttons[i, j], theme);

        newGameButton.BackgroundColor = rulesButton.BackgroundColor = theme.ButtonColor;
        newGameButton.TextColor = rulesButton.TextColor = theme.ButtonTextColor;
        clearStatsButton.BackgroundColor = backButton.BackgroundColor = theme.ButtonColor;
        clearStatsButton.TextColor = backButton.TextColor = theme.ButtonTextColor;
    }

    private void ApplyButtonTheme(Button btn, TripsTrapsTrullTheme theme)
    {
        btn.BackgroundColor = theme.CellColor;
        btn.TextColor = theme.TextColor;
    }

    private void OnCellClicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);

            if (game.MakeMove(row, col))
            {
                btn.Text = game.CurrentPlayer;

                if (game.CheckWin())
                {
                    if (game.CurrentPlayer == "X")
                        game.RegisterXWin();
                    else
                        game.RegisterOWin();

                    UpdateStatistics();
                    DisplayAlert("VÕIT!", $"{game.CurrentPlayer} võitis!", "OK");
                    ResetGame();
                    return;
                }

                if (game.CheckDraw())
                {
                    game.RegisterDraw();
                    UpdateStatistics();
                    DisplayAlert("VIIK!", "Vabad lahtrid puuduvad.", "OK");
                    ResetGame();
                    return;
                }

                game.SwitchPlayer();
                turnLabel.Text = $"KÄIK: {game.CurrentPlayer}";
            }
        }
    }

    private void OnNewGameClicked(object sender, EventArgs e)
    {
        ResetGame();
    }

    private async void OnRulesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TripsTrapsTrullRulesPage());
    }

    private async void OnClearStatsClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Kinnitus", "Kas kustutada kogu statistika?", "Jah", "Ei");
        if (confirm)
        {
            game.ClearStatistics();
            UpdateStatistics();
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void ResetGame()
    {
        game.ResetBoard();

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                buttons[i, j].Text = "";

        turnLabel.Text = $"KÄIK: {game.CurrentPlayer}";
        ApplyTheme();
    }

    private void UpdateStatistics()
    {
        xWinsLabel.Text = game.XWins.ToString();
        oWinsLabel.Text = game.OWins.ToString();
        drawsLabel.Text = game.Draws.ToString();
        totalLabel.Text = game.TotalGames.ToString();
    }
}