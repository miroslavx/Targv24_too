using System.Timers;

namespace targv24_too;

public partial class TimerPage : ContentPage
{
    private Label ajaNaitaja;
    private Label staatusLabel;
    private Button startButton;
    private Button stopButton;
    private Button resetButton;
    private Stepper ajaStepper;
    private StackLayout mainLayout;

    private System.Timers.Timer timer;
    private int sekundid;
    private int algusSekundid;
    private bool kaiib;

    public TimerPage()
    {
        Title = "TAIMER";
        BackgroundColor = Colors.White;

        LooKonstruktivistlikLiides();
        Content = mainLayout;

        timer = new System.Timers.Timer(1000);
        timer.Elapsed += Timer_Tick;
        sekundid = 60;
        algusSekundid = 60;
    }

    private void LooKonstruktivistlikLiides()
    {
        // Pealkiri
        var pealkirjLabel = new Label
        {
            Text = "AJAMASIN",
            FontFamily = "ST-Agitaciya",
            FontSize = 32,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Red,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 5)
        };

        var alamPealkirjLabel = new Label
        {
            Text = "KONSTRUKTIVISTLIK KRONOMEETER",
            FontFamily = "ST-Kooperativ",
            FontSize = 12,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, 20)
        };

        // Ajanäitaja suur number
        ajaNaitaja = new Label
        {
            Text = "01:00",
            FontFamily = "ST-Agitaciya",
            FontSize = 72,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            BackgroundColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 20, 0, 20)
        };

        // Stepper aja seadmiseks
        var stepperLabel = new Label
        {
            Text = "SEKUNDID:",
            FontFamily = "ST-Kooperativ",
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center
        };

        ajaStepper = new Stepper
        {
            Minimum = 10,
            Maximum = 300,
            Value = 60,
            Increment = 10,
            BackgroundColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center
        };
        ajaStepper.ValueChanged += AjaStepper_ValueChanged;

        // Nupud konstruktivistlikus stiilis
        var nuppudeGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            ColumnSpacing = 5,
            Margin = new Thickness(0, 20, 0, 10)
        };

        startButton = new Button
        {
            Text = "START",
            FontFamily = "ST-Agitaciya",
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = 0,
            HeightRequest = 50
        };
        startButton.Clicked += StartButton_Clicked;

        stopButton = new Button
        {
            Text = "STOPP",
            FontFamily = "ST-Agitaciya",
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.Black,
            TextColor = Colors.White,
            CornerRadius = 0,
            HeightRequest = 50
        };
        stopButton.Clicked += StopButton_Clicked;

        resetButton = new Button
        {
            Text = "RESET",
            FontFamily = "ST-Agitaciya",
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            BorderColor = Colors.Black,
            BorderWidth = 2,
            CornerRadius = 0,
            HeightRequest = 50
        };
        resetButton.Clicked += ResetButton_Clicked;

        Grid.SetColumn(startButton, 0);
        Grid.SetColumn(stopButton, 1);
        Grid.SetColumn(resetButton, 2);

        nuppudeGrid.Add(startButton);
        nuppudeGrid.Add(stopButton);
        nuppudeGrid.Add(resetButton);

        // Staatus
        staatusLabel = new Label
        {
            Text = "VALMIS ALUSTAMISEKS",
            FontFamily = "ST-Kooperativ",
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Red,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 15, 0, 0)
        };

        // Dekoratiivne element
        var dekorRiba = new BoxView
        {
            Color = Colors.Red,
            HeightRequest = 6,
            HorizontalOptions = LayoutOptions.Fill,
            Margin = new Thickness(40, 15, 40, 15)
        };

        mainLayout = new StackLayout
        {
            BackgroundColor = Colors.White,
            Padding = new Thickness(20),
            Spacing = 5,
            Children = {
                pealkirjLabel,
                alamPealkirjLabel,
                dekorRiba,
                ajaNaitaja,
                stepperLabel,
                ajaStepper,
                nuppudeGrid,
                staatusLabel
            }
        };

        UuendaAjaNaitaja();
    }

    private void AjaStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (!kaiib)
        {
            sekundid = (int)e.NewValue;
            algusSekundid = sekundid;
            UuendaAjaNaitaja();
        }
    }

    private async void StartButton_Clicked(object sender, EventArgs e)
    {
        kaiib = true;
        timer.Start();
        staatusLabel.Text = "TAIMER TÖÖTAB";
        staatusLabel.TextColor = Colors.Red;
        ajaNaitaja.TextColor = Colors.Red;

        // Väike animatsioon
        await ajaNaitaja.ScaleTo(1.1, 100);
        await ajaNaitaja.ScaleTo(1.0, 100);
    }

    private void StopButton_Clicked(object sender, EventArgs e)
    {
        kaiib = false;
        timer.Stop();
        staatusLabel.Text = "PEATATUD";
        staatusLabel.TextColor = Colors.Black;
        ajaNaitaja.TextColor = Colors.Black;
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        kaiib = false;
        timer.Stop();
        sekundid = algusSekundid;
        UuendaAjaNaitaja();
        staatusLabel.Text = "VALMIS ALUSTAMISEKS";
        staatusLabel.TextColor = Colors.Red;
        ajaNaitaja.TextColor = Colors.Black;
    }

    private void Timer_Tick(object sender, ElapsedEventArgs e)
    {
        sekundid--;

        Dispatcher.Dispatch(() =>
        {
            UuendaAjaNaitaja();

            if (sekundid <= 0)
            {
                timer.Stop();
                kaiib = false;
                staatusLabel.Text = "AEG LÄBI!";
                staatusLabel.TextColor = Colors.Red;
                ajaNaitaja.TextColor = Colors.Red;

                // Vibratsiooni või heli võiks lisada
                DisplayAlert("AJAMASIN", "AEG ON LÄBI!", "OK");
            }
        });
    }

    private void UuendaAjaNaitaja()
    {
        int minutid = sekundid / 60;
        int sekunditJargnevad = sekundid % 60;
        ajaNaitaja.Text = $"{minutid:00}:{sekunditJargnevad:00}";
    }
}