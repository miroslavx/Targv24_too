namespace targv24_too;

public partial class TripsTrapsTrullRulesPage : ContentPage
{
    public TripsTrapsTrullRulesPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}