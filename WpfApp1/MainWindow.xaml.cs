using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {


        private bool hasKey, isOfficeUnlocked, hasCrowbar, isCabinetOpen, hasPassword, hasCode, isVaultUnlocked, isNotebookFound;

        public MainWindow()
        {
            InitializeComponent();
            LoadScene_Menu();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Application.Current.Shutdown();
        }

        private void LoadScene_Menu()
        {
            HideAllElements();
            BackgroundImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/intro.png"));
            Menu_Title.Visibility = Visibility.Visible;
            Menu_NewGame.Visibility = Visibility.Visible;
            Menu_Exit.Visibility = Visibility.Visible;
        }

        private void Menu_NewGame_Click(object sender, RoutedEventArgs e)
        {
            hasKey = isOfficeUnlocked = hasCrowbar = isCabinetOpen = hasPassword = hasCode = isVaultUnlocked = isNotebookFound = false;
            LoadScene_Street();
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void HideAllElements()
        {
            Menu_Title.Visibility = Menu_NewGame.Visibility = Menu_Exit.Visibility = Visibility.Collapsed;
            Hotspot_BarDoor.Visibility = Hotspot_Manhole.Visibility = Hotspot_Trash.Visibility = Visibility.Collapsed;
            Hotspot_PC.Visibility = Hotspot_Desk.Visibility = Hotspot_Cabinet.Visibility = Hotspot_ExitOffice.Visibility = Visibility.Collapsed;
            Hotspot_Keypad.Visibility = Hotspot_VaultDoor.Visibility = Hotspot_ExitBasement.Visibility = Visibility.Collapsed;
            DialoguePanel.Visibility = Visibility.Collapsed;
        }

        private void LoadScene_Street()
        {
            HideAllElements();
            BackgroundImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/ulice.png"));
            Hotspot_BarDoor.Visibility = Hotspot_Manhole.Visibility = Hotspot_Trash.Visibility = Visibility.Visible;
        }

        private void LoadScene_Office()
        {
            HideAllElements();
            BackgroundImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/kancelar.png"));
            Hotspot_PC.Visibility = Hotspot_Desk.Visibility = Hotspot_Cabinet.Visibility = Hotspot_ExitOffice.Visibility = Visibility.Visible;
        }

        private void LoadScene_Basement()
        {
            HideAllElements();
            BackgroundImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/sklep.png"));
            Hotspot_Keypad.Visibility = Hotspot_VaultDoor.Visibility = Hotspot_ExitBasement.Visibility = Visibility.Visible;
        }

        private void LoadScene_TrezorNotebook()
        {
            HideAllElements();
            BackgroundImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/notebook.png"));
            ShowDialogue("Sláva! Notebook se našel! Zvítězil si. (ESC pro ukončení)");
        }

        private void GoToOffice_Click(object sender, RoutedEventArgs e)
        {
            if (isOfficeUnlocked) LoadScene_Office();
            else if (hasKey) { isOfficeUnlocked = true; ShowDialogue("Odemkl jsem bar klíčem."); }
            else ShowDialogue("Je zamčeno.");
        }
        private void GoToBasement_Click(object sender, RoutedEventArgs e) => LoadScene_Basement();
        private void GoToStreet_Click(object sender, RoutedEventArgs e) => LoadScene_Street();
        private void ExamineTrash_Click(object sender, RoutedEventArgs e) { if (!hasKey) { hasKey = true; ShowDialogue("Našel jsem klíč!"); } }
        private void ExamineDesk_Click(object sender, RoutedEventArgs e) { if (!hasCrowbar) { hasCrowbar = true; ShowDialogue("Mám páčidlo."); } }
        private void SearchCabinet_Click(object sender, RoutedEventArgs e)
        {
            if (isCabinetOpen) return;
            if (hasCrowbar) { isCabinetOpen = true; hasPassword = true; ShowDialogue("Vypáčil jsem skříň a mám heslo."); }
            else ShowDialogue("Chce to páčidlo.");
        }
        private void ExaminePC_Click(object sender, RoutedEventArgs e)
        {
            if (hasPassword) { hasCode = true; ShowDialogue("Kód k trezoru je 007."); }
            else ShowDialogue("Chce to heslo.");
        }
        private void UseKeypad_Click(object sender, RoutedEventArgs e)
        {
            if (hasCode) { isVaultUnlocked = true; ShowDialogue("Kód přijat."); }
            else ShowDialogue("Neznám kód.");
        }
        private void OpenVault_Click(object sender, RoutedEventArgs e)
        {
            if (isVaultUnlocked) { isNotebookFound = true; LoadScene_TrezorNotebook(); }
            else ShowDialogue("Dveře nepustí.");
        }
        private void HideDialogue_Click(object sender, MouseButtonEventArgs e) => DialoguePanel.Visibility = Visibility.Collapsed;
        private void ShowDialogue(string text) { TxtDialogue.Text = text; DialoguePanel.Visibility = Visibility.Visible; }
    }
}
