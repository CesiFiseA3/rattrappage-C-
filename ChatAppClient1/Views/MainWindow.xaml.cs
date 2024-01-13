using Rattrapage_Programmation_Système.Ressources;
using Rattrapage_Programmation_Système.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rattrapage_Programmation_Système.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly MainWindowViewModel viewModel;
        private readonly MessageListConverter converter = new MessageListConverter();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel = new MainWindowViewModel("name");
        }


        private void BrowseFileButton_Click(object sender, RoutedEventArgs e)
        {
            AttachedFilesLabel.Content = "Fichier(s) sélectionné(s) : ";
            viewModel.AttachFile();
            if (viewModel.currentAttachedFile != null)
            {
                AttachedFilesLabel.Content += viewModel.currentAttachedFile.FileName;
            }
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessageButton_Click(sender, e);
            }
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EnvoyerMessage();
            MessageTextBox.Text = string.Empty;
            AttachedFilesLabel.Content = "Fichier(s) sélectionné(s) : ";
            if (viewModel.messagesOnDisplay[0] != null)
            {
                Display.Text = converter.ManualConvert(viewModel.messagesOnDisplay);
            }
        }

        private void MessageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.currentMessageContent = MessageTextBox.Text;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.messagesOnDisplay[0] != null)
            {
                Display.Text = converter.ManualConvert(viewModel.messagesOnDisplay);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            viewModel.Deconnecter();
        }
    }
}
