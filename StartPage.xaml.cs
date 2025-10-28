using System.Threading.Tasks;

namespace targv24_too;

public partial class StartPage : ContentPage
{
    public List<ContentPage> lehed = new List<ContentPage>()
    {
        new TextPage(),
        new FigurePage(),
        new TimerPage(),
        new ValgusfoorPage(),
        new LumememmPage(),
        new MemoPage(),
        new TripsTrapsTrullPage()  // ДОБАВЛЕНО
    };

    public List<string> tekstid = new List<string>()
    {
        "TEKST",
        "FIGUUR",
        "TAIMER",
        "VALGUSFOOR",
        "LUMEMEMM",
        "MEMO",
        "TRIPS-TRAPS-TRULL"  // ДОБАВЛЕНО
    };

    ScrollView sv;
    Grid mainGrid;

    public StartPage()
    {
        Title = "KONSTRUKTIVIST";
        BackgroundColor = Colors.White;

        LooKonstruktivistlikLiides();
        Content = sv;
    }

    private void LooKonstruktivistlikLiides()
    {
        var pealkirjLabel = new Label
        {
            Text = "MOBIILRAKENDUS",
            FontFamily = "ST-Agitaciya",
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Red,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 15, 0, 5)
        };

        var alamPealkirjLabel = new Label
        {
            Text = "TARGV24",
            FontFamily = "ST-Kooperativ",
            FontSize = 16,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, 15)
        };

        mainGrid = new Grid
        {
            BackgroundColor = Colors.White,
            Padding = new Thickness(10),
            RowSpacing = 8,
            ColumnSpacing = 8,
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = new GridLength(120) },
                new RowDefinition { Height = new GridLength(120) },
                new RowDefinition { Height = new GridLength(120) },
                new RowDefinition { Height = new GridLength(120) },
                new RowDefinition { Height = new GridLength(120) }  // ДОБАВЛЕНО для TRIPS
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
        };

        Grid.SetColumnSpan(pealkirjLabel, 2);
        Grid.SetColumnSpan(alamPealkirjLabel, 2);
        mainGrid.Add(pealkirjLabel, 0, 0);
        mainGrid.Add(alamPealkirjLabel, 0, 1);

        LooWindowsPhonePlitked();

        sv = new ScrollView
        {
            Content = mainGrid,
            BackgroundColor = Colors.White
        };
    }

    private void LooWindowsPhonePlitked()
    {
        var paigutused = new[]
        {
            new { Row = 2, Column = 0, ColumnSpan = 2 }, // TEKST
			new { Row = 3, Column = 0, ColumnSpan = 1 }, // FIGUUR
			new { Row = 3, Column = 1, ColumnSpan = 1 }, // TAIMER
			new { Row = 4, Column = 0, ColumnSpan = 1 }, // VALGUSFOOR
			new { Row = 4, Column = 1, ColumnSpan = 1 }, // LUMEMEMM
            new { Row = 5, Column = 0, ColumnSpan = 2 }, // MEMO
            new { Row = 6, Column = 0, ColumnSpan = 2 }  // TRIPS
		};

        for (int i = 0; i < lehed.Count; i++)
        {
            var nupp = LooWindowsPhonePlitk(i);
            var paigutus = paigutused[i];

            Grid.SetRow(nupp, paigutus.Row);
            Grid.SetColumn(nupp, paigutus.Column);
            Grid.SetColumnSpan(nupp, paigutus.ColumnSpan);

            mainGrid.Add(nupp);
        }
    }

    private Button LooWindowsPhonePlitk(int index)
    {
        Color[] taustavarvid = {
            Colors.Red,
            Colors.Black,
            Colors.Red,
            Colors.Black,
            Colors.Red,
            Colors.Black,
            Colors.Red  // TRIPS
		};

        int[] fondiSuurused = {
            22,
            18,
            18,
            16,
            18,
            22,
            20  // TRIPS
		};

        var nupp = new Button
        {
            Text = tekstid[index],
            FontFamily = index % 2 == 0 ? "ST-Agitaciya" : "ST-Kooperativ",
            FontSize = fondiSuurused[index],
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = taustavarvid[index],
            TextColor = Colors.White,
            CornerRadius = 0,
            BorderWidth = 0,
            ZIndex = index,
            Margin = new Thickness(0),
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill
        };

        nupp.Clicked += Nupp_Clicked;
        return nupp;
    }

    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        Button nupp = (Button)sender;
        await nupp.ScaleTo(0.95, 50);
        await nupp.ScaleTo(1.0, 50);
        await Navigation.PushAsync(lehed[nupp.ZIndex]);
    }
}