namespace Hangman;

public partial class HarryPotterGame : ContentPage
{
	public HarryPotterGame()
	{
		InitializeComponent();
	}

	private async void Hechizos_Clicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new HechizosPage());
	}

    private async void Personajes_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void Pociones_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PocionesPage());
    }

    private async void MundoMagico_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MundoMagicoPage());
    }
}