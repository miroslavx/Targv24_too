namespace targv24_too
{
    public class Theme
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }
        public Color CardBackColor { get; set; }
        public Color TextColor { get; set; }
        public Color AccentColor { get; set; }
        public string FontFamily { get; set; }

        public Theme(string name, Color bgColor, Color cardColor, Color textColor, Color accentColor)
        {
            Name = name;
            BackgroundColor = bgColor;
            CardBackColor = cardColor;
            TextColor = textColor;
            AccentColor = accentColor;
            FontFamily = "ST-Kooperativ";
        }
        public void Apply(ContentPage page)
        {
            page.BackgroundColor = BackgroundColor;
        }
    }
}