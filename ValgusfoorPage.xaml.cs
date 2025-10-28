namespace targv24_too;

public partial class ValgusfoorPage : ContentPage
{
    BoxView redLight, yellowLight, greenLight;
    Label infoLabel;
    Button startBtn, stopBtn;
    bool isOn = false;

    public ValgusfoorPage()
    {
        Title = "VALGUSFOOR";

        // Pealkiri
        infoLabel = new Label
        {
            Text = "Valgusfoor on välja lülitatud",
            FontFamily = "ST-Kooperativ",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White,
            Margin = new Thickness(0, 30, 0, 20)
        };

        // Tulud
        redLight = CreateLight(Colors.DarkGray);
        var tapRed = new TapGestureRecognizer();
        tapRed.Tapped += (s, e) => ShowMessage("SEISA", redLight, Colors.Red);
        redLight.GestureRecognizers.Add(tapRed);

        yellowLight = CreateLight(Colors.DarkGray);
        var tapYellow = new TapGestureRecognizer();
        tapYellow.Tapped += (s, e) => ShowMessage("VALMISTA", yellowLight, Colors.Yellow);
        yellowLight.GestureRecognizers.Add(tapYellow);

        greenLight = CreateLight(Colors.DarkGray);
        var tapGreen = new TapGestureRecognizer();
        tapGreen.Tapped += (s, e) => ShowMessage("SÕIDA", greenLight, Colors.Green);
        greenLight.GestureRecognizers.Add(tapGreen);

        // Nupud
        startBtn = new Button
        {
            Text = "SISSE",
            FontFamily = "ST-Kooperativ",
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            CornerRadius = 5,
            HeightRequest = 50,
            WidthRequest = 120
        };
        startBtn.Clicked += OnStartClicked;

        stopBtn = new Button
        {
            Text = "VÄLJA",
            FontFamily = "ST-Kooperativ",
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = 5,
            HeightRequest = 50,
            WidthRequest = 120
        };
        stopBtn.Clicked += OnStopClicked;

        var buttonsLayout = new HorizontalStackLayout
        {
            Spacing = 30,
            HorizontalOptions = LayoutOptions.Center,
            Children = { startBtn, stopBtn }
        };

        var lightsLayout = new VerticalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Children = { redLight, yellowLight, greenLight }
        };

        var trafficFrame = new Frame
        {
            BackgroundColor = Color.FromRgb(40, 40, 40),
            BorderColor = Colors.Black,
            CornerRadius = 15,
            Padding = new Thickness(25, 20),
            HorizontalOptions = LayoutOptions.Center,
            Content = lightsLayout
        };

        var mainLayout = new VerticalStackLayout
        {Spacing = 30,
            Children =
            {
                infoLabel,
                trafficFrame,
                buttonsLayout
            }
        };
        Content = new ScrollView
        {
            Content = mainLayout,
            BackgroundColor = Color.FromRgb(50, 50, 50),
            Padding = new Thickness(20)
        };
    }

    private BoxView CreateLight(Color color)
    {return new BoxView
        {
            Color = color,
            WidthRequest = 80,
            HeightRequest = 80,
            CornerRadius = 40,
            HorizontalOptions = LayoutOptions.Center
        };
    }

    private void OnStartClicked(object? sender, EventArgs e)
    {
        isOn = true;
        infoLabel.Text = "Valgusfoor sisse lülitatud!";
        ResetLights();
    }

    private void OnStopClicked(object? sender, EventArgs e)
    {
        isOn = false;
        infoLabel.Text = "Valgusfoor välja lülitatud!";
        ResetLights();
    }

    private void ShowMessage(string text, BoxView light, Color color)
    {
        if (!isOn)
        {
            infoLabel.Text = "Kõigepealt lülita valgusfoor sisse!";
            return;
        }

        ResetLights();
        light.Color = color;
        infoLabel.Text = text;
    }

    private void ResetLights()
    {
        redLight.Color = Colors.DarkGray;
        yellowLight.Color = Colors.DarkGray;
        greenLight.Color = Colors.DarkGray;
    }
}