namespace targv24_too
{
    // Класс с логикой игры (OOP)
    public class TripsTrapsTrullGame
    {
        public string CurrentPlayer { get; private set; } = "X";
        public int XWins { get; private set; }
        public int OWins { get; private set; }
        public int Draws { get; private set; }
        public int TotalGames { get; private set; }

        private string[,] board = new string[3, 3];

        public TripsTrapsTrullGame()
        {
            LoadStatistics();
            ResetBoard();
        }

        // Очистить поле
        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = "";

            CurrentPlayer = "X";
        }

        // Сделать ход
        public bool MakeMove(int row, int col)
        {
            if (string.IsNullOrEmpty(board[row, col]))
            {
                board[row, col] = CurrentPlayer;
                return true;
            }
            return false;
        }

        // Сменить игрока
        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
        }

        // Проверка победы
        public bool CheckWin()
        {
            // Строки и столбцы
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == CurrentPlayer && board[i, 1] == CurrentPlayer && board[i, 2] == CurrentPlayer)
                    return true;

                if (board[0, i] == CurrentPlayer && board[1, i] == CurrentPlayer && board[2, i] == CurrentPlayer)
                    return true;
            }

            // Диагонали
            if (board[0, 0] == CurrentPlayer && board[1, 1] == CurrentPlayer && board[2, 2] == CurrentPlayer)
                return true;

            if (board[0, 2] == CurrentPlayer && board[1, 1] == CurrentPlayer && board[2, 0] == CurrentPlayer)
                return true;

            return false;
        }

        // Проверка ничьи
        public bool CheckDraw()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (string.IsNullOrEmpty(board[i, j]))
                        return false;

            return true;
        }

        // Победа X
        public void RegisterXWin()
        {
            XWins++;
            TotalGames++;
            SaveStatistics();
        }

        // Победа O
        public void RegisterOWin()
        {
            OWins++;
            TotalGames++;
            SaveStatistics();
        }

        // Ничья
        public void RegisterDraw()
        {
            Draws++;
            TotalGames++;
            SaveStatistics();
        }

        // Сохранить статистику
        private void SaveStatistics()
        {
            Preferences.Set("TripsXWins", XWins);
            Preferences.Set("TripsOWins", OWins);
            Preferences.Set("TripsDraws", Draws);
            Preferences.Set("TripsTotalGames", TotalGames);
        }

        // Загрузить статистику
        private void LoadStatistics()
        {
            XWins = Preferences.Get("TripsXWins", 0);
            OWins = Preferences.Get("TripsOWins", 0);
            Draws = Preferences.Get("TripsDraws", 0);
            TotalGames = Preferences.Get("TripsTotalGames", 0);
        }

        // Очистить статистику
        public void ClearStatistics()
        {
            XWins = OWins = Draws = TotalGames = 0;
            SaveStatistics();
        }
    }
}