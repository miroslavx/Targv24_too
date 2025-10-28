namespace targv24_too;

public partial class TextPage : ContentPage
{
    Label lblTekst;
    Editor editorTekst;
    StackLayout mainLayout;

    public TextPage()
    {
        Title = "TEKST";
        BackgroundColor = Colors.White;

        LooKonstruktivistlikLiides();
        Content = mainLayout;
    }

    private void LooKonstruktivistlikLiides()
    {
        // Pealkiri konstruktivistlikus stiilis
        var pealkirjLabel = new Label
        {
            Text = "TEKSTI REDAKTOR",
            FontFamily = "ST-Agitaciya",
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Red,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 15)
        };

        // Väljastatav tekst
        lblTekst = new Label()
        {
            Text = "SINU TEKST ILMUB SIIA",
            FontFamily = "ST-Kooperativ",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            BackgroundColor = Colors.White,
            Padding = new Thickness(15, 10),
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            HorizontalTextAlignment = TextAlignment.Center
        };

        // Teksti sisestamise ala
        editorTekst = new Editor
        {
            FontFamily = "ST-Kooperativ",
            FontSize = 16,
            BackgroundColor = Colors.Black,
            TextColor = Colors.White,
            Placeholder = "KIRJUTA SIIA...",
            PlaceholderColor = Color.FromRgb(200, 200, 200),
            FontAttributes = FontAttributes.Bold,
            HeightRequest = 120,
            Margin = new Thickness(0, 10, 0, 0)
        };

        editorTekst.TextChanged += EditorTekst_TextChanged;

        // Konstruktivistlik dekoratiivne element
        var dekoratiivneRiba = new BoxView
        {
            Color = Colors.Red,
            HeightRequest = 8,
            HorizontalOptions = LayoutOptions.Fill,
            Margin = new Thickness(0, 15, 0, 15)
        };

        // Instruktsioonid
        var instruktsioonLabel = new Label
        {
            Text = "KIRJUTAGE TEKST MUSTASSE KASTI",
            FontFamily = "ST-Kooperativ",
            FontSize = 12,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10, 0, 5)
        };

        mainLayout = new StackLayout
        {
            BackgroundColor = Colors.White,
            Padding = new Thickness(20),
            Spacing = 0,
            Children = {
                pealkirjLabel,
                dekoratiivneRiba,
                lblTekst,
                instruktsioonLabel,
                editorTekst
            }
        };
    }

    private void EditorTekst_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(editorTekst.Text))
        {
            lblTekst.Text = "SINU TEKST ILMUB SIIA";
            lblTekst.TextColor = Color.FromRgb(150, 150, 150);
        }
        else
        {
            lblTekst.Text = editorTekst.Text.ToUpper(); // Konstruktivistlik suur tähed
            lblTekst.TextColor = Colors.Black;
        }
    }
}