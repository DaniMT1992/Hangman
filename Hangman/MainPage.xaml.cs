using System.ComponentModel;
using static Microsoft.Maui.Controls.Internals.Profile;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        #region UI Properties
        public string Spotlight
        {
            get => spotlight; set
            {
                spotlight = value;
                OnPropertyChanged();
            }
        }

        public List<char> Letters
        {
            get => letters; set
            {
                letters = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get => message; set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        public string GameStatus
        {
            get => gameStatus; set
            {
                gameStatus = value;
                OnPropertyChanged();
            }
        }
        public string CurrentImage
        {
            get => currentImage1; set
            {
                currentImage1 = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Fields
        List<string> words = new List<string>()
        {
            "HARRY POTTER",
            "HERMIONE GRANGER",
            "RON WEASLEY",
            "ALBUS DUMBLEDORE",
            "SEVERUS SNAPE",
            "RUBEUS HAGRID",
            "DRACO MALFOY",
            "SIRIUS BLACK",
            "REMUS LUPIN",
            "MINERVA MCGONAGALL",
            "GELLERT GRINDELWALD",
            "TOM RIDDLE",
            "LORD VOLDEMORT",
            "GINNY WEASLEY",
            "NEVILLE LONGBOTTOM",
            "LUNA LOVEGOOD",
            "FRED WEASLEY",
            "GEORGE WEASLEY",
            "PERCY WEASLEY",
            "ARTHUR WEASLEY",
            "MOLLY WEASLEY",
            "ALASTOR MOODY",
            "DOLORES UMBRIDGE",
            "NYPHADORA TONKS",
            "CEDRIC DIGGORY",
            "VIKTOR KRUM",
            "CHO CHANG",
            "FLEUR DELACOUR",
            "HORACE SLUGHORN",
            "SYBILL TRELAWNEY",
            "RITA SKEETER",
            "CORNELIUS FUDGE",
            "RUFUS SCRIMGEOUR",
            "PHINEAS NIGELLUS BLACK",
            "ABERFORTH DUMBLEDORE",
            "BATHILDA BAGSHOT",
            "PANSY PARKINSON",
            "BLAISE ZABINI",
            "MILLICENT BULSTRODE",
            "VINCENT CRABBE",
            "GREGORY GOYLE",
            "LAVENDER BROWN",
            "PARVATI PATIL",
            "PADMA PATIL",
            "ANTHONY GOLDSTEIN",
            "HANNAH ABBOTT",
            "ERNIE MACMILLAN",
            "SUSAN BONES",
            "TERRY BOOT",
            "MICHAEL CORNER",
            "SEAMUS FINNIGAN",
            "DEAN THOMAS",
            "LEE JORDAN",
            "ANGELINA JOHNSON",
            "ALICIA SPINNET",
            "KATIE BELL",
            "MARCUS FLINT",
            "ROGER DAVIES",
            "ELOISE MIDGEN",
            "QUENTIN TRIMBLE",
            "CORMAC MCLAGGEN",
            "ROMILDA VANE",
            "HELENA RAVENCLAW",
            "THE BLOODY BARON",
            "THE FAT FRIAR",
            "PROFESSOR FLITWICK",
            "PROFESSOR SPROUT",
            "PROFESSOR SINISTRA",
            "PROFESSOR BINNS",
            "PROFESSOR VECTOR",
            "PROFESSOR BABBLING",
            "MADAM POMFREY",
            "MADAM PINCE",
            "FAWKES",
            "FLUFFY",
            "BUCKBEAK",
            "CROOKSHANKS",
            "SCABBERS",
            "NAGINI",
            "SLYTHERIN BASILISK",
            "GRIPHOOK",
            "OLLIVANDER",
            "WILHELMINA GRUBBLYPLANK",
            "MUNDUNGUS FLETCHER",
            "RAGNOK",
            "BORGIN AND BURKES",
            "NICOLAS FLAMEL",
            "MODESTY RABNOTT",
            "SANGUINI",
            "CELESTINA WARBECK",
            "ELDRED WORPLE",
            "GORNUK",
            "YAXLEY",
            "ANTONIN DOLOHOV",
            "AMYCUS CARROW"
            };
        string answer = "";
        private string spotlight;
        List<char> guessed= new List<char>();
        private List<char> letters = new List<char>();
        private string message;
        int mistakes = 0;
        int maxWrong = 6;
        private string gameStatus;
        private string currentImage1 = "img000.png";
        #endregion

        public MainPage()
        {
            InitializeComponent();
            Letters.AddRange("QWERTYUIOPASDFGHJKLZXCVBNM");
            BindingContext = this;
            PickWord();
            CalculateWord(answer, guessed);
        }

        #region Game Engine
        private void PickWord()
        { 
            answer = words[new Random().Next(0, words.Count)];
            Debug.WriteLine(answer);
        }

        private void CalculateWord(string answer, List<Char> guessed)
        {
            var temp = answer.Select(x =>
        x == ' ' ? ' ' : (guessed.Contains(x) ? x : '_')).ToArray();
            Spotlight = string.Join(' ', temp);
        }
        #endregion

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var letter = btn.Text;
                btn.IsEnabled=false;
                HandleGuess(letter[0]);
            }
        }

        private void HandleGuess(char letter)
        {
            if (guessed.IndexOf(letter) == -1)
            { 
                guessed.Add(letter);
            }
            if (answer.IndexOf(letter) >= 0)

            {
                CalculateWord(answer, guessed);
                CheckIfGameWon();
            }
            else if (answer.IndexOf(letter) == -1)
            {
                mistakes++;
                UpdateStatus();
                CheckIfGameLost();
                CurrentImage = $"img{mistakes}.jpg";
            }
        }

        private void CheckIfGameLost()
        {
            if (mistakes == maxWrong)
            {
                Message = "You Lose!!";
                Message = $"The word was: {answer}";
                DisableLetters();
            }
        }

        private void DisableLetters()
        {
            foreach (var children in LettersContainer.Children)
            { 
                var btn = children as Button;
                if (btn != null)
                { 
                btn.IsEnabled=false;
                }
            }
        }
        private void EnableLetters()
        {
            foreach (var children in LettersContainer.Children)
            {
                var btn = children as Button;
                if (btn != null)
                {
                    btn.IsEnabled = true;
                }
            }
        }

        private void CheckIfGameWon()
        {
            if (Spotlight.Replace(" ", "") == answer.Replace(" ", ""))
            {
                Message = "You WIN !";
                winImage.IsVisible = true;
                currentImage.IsVisible = false;
                DisableLetters ();
            }
        }

        private void UpdateStatus()
        {
            GameStatus = $"Errors: {mistakes} of {maxWrong}";
        }

        private void ResetGame_Clicked(object sender, EventArgs e)
        {
            mistakes = 0;
            guessed = new List<char>();
            CurrentImage = "img000.png";
            PickWord();
            CalculateWord(answer, guessed);
            Message = "";
            UpdateStatus();
            EnableLetters();
            winImage.IsVisible = false;
            currentImage.IsVisible = true;

        }
    }
}
