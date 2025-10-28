namespace targv24_too
{
    public class TripsTrapsTrullTheme
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }
        public Color CellColor { get; set; }
        public Color TextColor { get; set; }
        public Color ButtonColor { get; set; }
        public Color ButtonTextColor { get; set; }
        public Color BorderColor { get; set; }
        public Color AccentColor { get; set; }

        public TripsTrapsTrullTheme(string name, Color bg, Color cell, Color text,
                                     Color button, Color buttonText, Color border, Color accent)
        {
            Name = name;
            BackgroundColor = bg;
            CellColor = cell;
            TextColor = text;
            ButtonColor = button;
            ButtonTextColor = buttonText;
            BorderColor = border;
            AccentColor = accent;
        }
    }
}