using Microsoft.Maui.Controls;
using System.ComponentModel;
using static Microsoft.Maui.Controls.Internals.Profile;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hangman;

public partial class HechizosPage : ContentPage
{
    public HechizosPage()
    {
        InitializeComponent();
    }

    private async void GoToMainMenu(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Regresa a HarryPotterGame
    }
}