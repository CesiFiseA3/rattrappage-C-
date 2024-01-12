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
        public readonly MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseFileButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AttachFile();
            AttachedFilesLabel.Content = "Fichier(s) sélectionné(s) : " + viewModel.currentAttachedFile.FileName;
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SendMessage();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }
    }
}
