namespace targv24_too;

public partial class FigurePage : ContentPage
{
    BoxView geometrilineKujund;
    Label instruktsioonLabel;
    Label infoLabel;
    StackLayout mainLayout;
    Random random = new Random();

    public FigurePage()
    {
        Title = "FIGUUR";
        BackgroundColor = Colors.White;

        LooKonstruktivistlikLiides();
        Content = mainLayout;
    }

    private void LooKonstruktivistlikLiides()
    {
        // Pealkiri
        var pealkirjLabel = new Label
        {
            Text = "GEOMEETRILINE VORM",
            FontFamily = "ST-Agitaciya",
            FontSize = 26,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Red,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 10)
        };

        // Alampealkiri
        var alamPealkirjLabel = new Label
        {
            Text = "KONSTRUKTIVISTLIK EKSPERIMENT",
            FontFamily = "ST-Kooperativ",
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, 15)
        };

        // Geomeetriline kujund
        geometrilineKujund = new BoxView
        {
            Color = Colors.Red,
            WidthRequest = 120,
            HeightRequest = 120,
            CornerRadius = 0, // Terav nurk
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        // Tap gesture
        TapGestureRecognizer tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += Kujund_Puudutatud;
        geometrilineKujund.GestureRecognizers.Add(tapGesture);

        // Instruktsioonid
        instruktsioonLabel = new Label
        {
            Text = "PUUDUTAGE KUJUNDIT",
            FontFamily = "ST-Kooperativ",
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 10)
        };

        // Info väljund
        infoLabel = new Label
        {
            Text = "VALMISTUGE TRANSFORMATSIOONIKS",
            FontFamily = "ST-Agitaciya",
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Red,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 10, 0, 10)
        };

        // Dekoratiivne element
        var dekoratiivneElement = LooDekoratiivneElement();

        mainLayout = new StackLayout
        {
            BackgroundColor = Colors.White,
            Padding = new Thickness(20),
            Spacing = 10,
            Children = {
                pealkirjLabel,
                alamPealkirjLabel,
                dekoratiivneElement,
                geometrilineKujund,
                instruktsioonLabel,
                infoLabel
            }
        };
    }

    private Grid LooDekoratiivneElement()
    {
        var grid = new Grid
        {
            HeightRequest = 40,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
        };

        var vasakRiba = new BoxView
        {
            Color = Colors.Black,
            HeightRequest = 8
        };

        var paremRiba = new BoxView
        {
            Color = Colors.Red,
            HeightRequest = 8
        };

        Grid.SetColumn(vasakRiba, 0);
        Grid.SetColumn(paremRiba, 2);

        grid.Add(vasakRiba);
        grid.Add(paremRiba);

        return grid;
    }

    private async void Kujund_Puudutatud(object? sender, TappedEventArgs e)
    {
        // Konstruktivistlik värvipalett
        Color[] konstruktivistlikudVarvid = {
            Colors.Red,
            Colors.Black,
            Colors.White
        };

        // Geomeetrilised suurused
        int[] suurused = { 80, 100, 120, 140, 160 };
        int[] kujundid = { 0, 20, 60 }; // 0=ruut, 20=kergelt ümmargune, 60=ümmargune

        // Muudame omadusi
        Color uusVarv = konstruktivistlikudVarvid[random.Next(0, konstruktivistlikudVarvid.Length)];
        int uusSuurus = suurused[random.Next(0, suurused.Length)];
        int uusKujund = kujundid[random.Next(0, kujundid.Length)];

        // Animatsioon
        await geometrilineKujund.ScaleTo(0.8, 100);

        geometrilineKujund.Color = uusVarv;
        geometrilineKujund.WidthRequest = uusSuurus;
        geometrilineKujund.HeightRequest = uusSuurus;
        geometrilineKujund.CornerRadius = uusKujund;

        await geometrilineKujund.ScaleTo(1.0, 150);

        // Uuenda infot
        string kujundiNimi = uusKujund == 0 ? "RUUT" : uusKujund == 20 ? "KUJUND" : "RING";
        string varviNimi = uusVarv == Colors.Red ? "PUNANE" : uusVarv == Colors.Black ? "MUST" : "VALGE";

        infoLabel.Text = $"{varviNimi} {kujundiNimi}\nSUURUS: {uusSuurus}PX";
    }
}