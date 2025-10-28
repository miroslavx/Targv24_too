namespace targv24_too
{
    public class Player
    {
        public string Name { get; set; }
        public int Moves { get; set; }        // Количество ходов
        public int PairsFound { get; set; }   // Найдено пар
        public int Score { get; set; }        // Очки

        public Player(string name)
        {
            Name = name;
            Moves = 0;
            PairsFound = 0;
            Score = 0;
        }

        public void IncrementMoves()
        {
            Moves++;
        }

        public void AddPair()
        {
            PairsFound++;
            Score += 10; // За каждую пару 10 очков
        }

        public void CalculateScore()
        {
            // Чем меньше ходов, тем больше бонус
            int bonus = Math.Max(0, 100 - Moves);
            Score += bonus;
        }
    }
}