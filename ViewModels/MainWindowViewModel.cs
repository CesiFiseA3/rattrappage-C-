using Microsoft.Win32;
using Rattrapage_Programmation_Système.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_Programmation_Système.ViewModels
{
    internal class MainWindowViewModel
    {
        public List<MessageDataModel> messages { get; } = new List<MessageDataModel>();
        public string currentMessageContent { get; set; }
        public MainWindowViewModel() {}
        private void SendMessage()
        {
            // Logique pour envoyer un message
            var newMessage = new MessageDataModel
            {
                id = messages.Count,
                content = currentMessageContent
            };

            messages.Add(newMessage);
            currentMessageContent = string.Empty; // Effacer le champ de texte après l'envoi du message
        }
        private void AttachFile()
        {
            // Logique pour joindre un fichier
            var openFileDialog = new OpenFileDialog
            {
                Title = "Choisir un fichier",
                Filter = "Tous les fichiers (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Lire le contenu du fichier en tant qu'array d'octets
                byte[] fileContent = File.ReadAllBytes(openFileDialog.FileName);

                // Créer un objet FileDataModel pour représenter le fichier joint
                var attachedFile = new FileDataModel
                {
                    FileName = Path.GetFileName(openFileDialog.FileName),
                    Content = fileContent
                };

                // Ajouter le fichier joint à la liste des fichiers joints du message actuel
                var currentMessage = messages.LastOrDefault();
                if (currentMessage != null)
                {
                    currentMessage.attachedFiles.Add(attachedFile);
                }
            }
        }
    }
}
