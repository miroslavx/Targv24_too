using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace targv24_too;

public partial class LumememmPage : ContentPage
{
    private CancellationTokenSource? _cts;

    public LumememmPage()
    {
        InitializeComponent();
        ActionPicker.SelectedIndex = 1;
        UpdateOpacityLabel();
        UpdateSpeedLabel();
    }

    private void SetSnowmanVisible(bool visible) => Canvas.IsVisible = visible;

    private void SetSnowmanOpacity(double value)
    {
        Head.Opacity = value;
        MiddleBall.Opacity = value;
        BottomBall.Opacity = value;
        Bucket.Opacity = value;
    }

    private void UpdateOpacityLabel() => OpacityValueLabel.Text = OpacitySlider.Value.ToString("0.00");
    private void UpdateSpeedLabel() => SpeedLabel.Text = $"{(int)SpeedStepper.Value} ms";
    private int SpeedMs => (int)SpeedStepper.Value;

    private void StopAnimations()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        Head.TranslationX = MiddleBall.TranslationX = BottomBall.TranslationX = Bucket.TranslationX = 0;
        Head.Scale = MiddleBall.Scale = BottomBall.Scale = Bucket.Scale = 1.0;

        SetSnowmanOpacity(OpacitySlider.Value);
    }

    private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        SetSnowmanOpacity(e.NewValue);
        UpdateOpacityLabel();
    }

    private void OnSpeedChanged(object sender, ValueChangedEventArgs e) => UpdateSpeedLabel();

    private async void OnDoActionClicked(object sender, EventArgs e)
    {
        var action = ActionPicker.SelectedItem as string ?? "";
        InfoLabel.Text = $"Tegevus: {action}";
        StopAnimations();

        switch (action)
        {
            case "Peida": SetSnowmanVisible(false); break;
            case "Näita":
                SetSnowmanVisible(true);
                Canvas.Opacity = 0;
                await Canvas.FadeTo(1, (uint)SpeedMs);
                break;
            case "Muuda värvi": await ChangeColorAsync(); break;
            case "Sulata": await MeltAsync(); break;
            case "Tantsi": await DanceAsync(); break;
        }
    }

    private async Task ChangeColorAsync()
    {
        bool ok = await DisplayAlert("Kinnitus", "Kas muuta ämber värvi?", "Jah", "Ei");
        if (!ok) return;

        var rnd = new Random();
        Color RandomColor() => Color.FromRgb(rnd.Next(20, 236), rnd.Next(20, 236), rnd.Next(20, 236));

        Bucket.BackgroundColor = RandomColor();
        await Bucket.ScaleTo(1.1, (uint)(SpeedMs / 2));
        await Bucket.ScaleTo(1.0, (uint)(SpeedMs / 2));
    }

    private async Task MeltAsync()
    {
        SetSnowmanVisible(true);
        uint d = (uint)SpeedMs;

        await Task.WhenAll(
            Head.FadeTo(0, d), MiddleBall.FadeTo(0, d), BottomBall.FadeTo(0, d), Bucket.FadeTo(0, d),
            Head.ScaleTo(0.7, d), MiddleBall.ScaleTo(0.7, d), BottomBall.ScaleTo(0.7, d), Bucket.ScaleTo(0.7, d)
        );

        SetSnowmanVisible(false);

        Head.Opacity = MiddleBall.Opacity = BottomBall.Opacity = Bucket.Opacity = OpacitySlider.Value;
        Head.Scale = MiddleBall.Scale = BottomBall.Scale = Bucket.Scale = 1.0;
    }

    private async Task DanceAsync()
    {
        SetSnowmanVisible(true);
        _cts ??= new CancellationTokenSource();
        var token = _cts.Token;
        double dx = 30;
        uint d = (uint)SpeedMs;

        try
        {
            while (!token.IsCancellationRequested)
            {
                await Task.WhenAll(
                    Head.TranslateTo(-dx, 0, d), MiddleBall.TranslateTo(-dx, 0, d),
                    BottomBall.TranslateTo(-dx, 0, d), Bucket.TranslateTo(-dx, 0, d)
                );
                await Task.WhenAll(
                    Head.TranslateTo(dx, 0, d), MiddleBall.TranslateTo(dx, 0, d),
                    BottomBall.TranslateTo(dx, 0, d), Bucket.TranslateTo(dx, 0, d)
                );
                await Task.WhenAll(
                    Head.TranslateTo(0, 0, d), MiddleBall.TranslateTo(0, 0, d),
                    BottomBall.TranslateTo(0, 0, d), Bucket.TranslateTo(0, 0, d)
                );
            }
        }
        catch (TaskCanceledException) { }
    }
}