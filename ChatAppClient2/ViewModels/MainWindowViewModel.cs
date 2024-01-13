using Microsoft.Win32;
using ChatAppClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ChatAppClient.Ressources;

namespace ChatAppClient.ViewModels
{
    public class MainWindowViewModel
    {
        public event EventHandler<string> MessageReceived;

        private ObservableCollection<MessageDataModel> messages { get; } = new ObservableCollection<MessageDataModel>();
        public MessageDataModel[] messagesOnDisplay = new MessageDataModel[20];
        public string currentMessageContent { get; set; }
        public FileDataModel currentAttachedFile { get; set; }

        private readonly Client clientModel;

        public MainWindowViewModel(string clientName) {
            clientModel = new Client(clientName);
            Task.Run(() => EcouterReseau());
        }

        public void EnvoyerMessage()
        {
            if (currentMessageContent != string.Empty)
            {
                // Logique pour envoyer un message
                var newMessage = new MessageDataModel
                {
                    id = messages.Count,
                    content = currentMessageContent,
                    attachedFiles = new ObservableCollection<FileDataModel>()
                };

                if (currentAttachedFile != null)
                {
                    newMessage.attachedFiles.Add(currentAttachedFile);
                }
                clientModel.EnvoyerMessage(newMessage.content);
                currentMessageContent = string.Empty; // Effacer le champ de texte après l'envoi du message
                currentAttachedFile = null;
            }
        }

        public async Task EcouterReseau()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    string message = clientModel.RecevoirMessage();
                    OnMessageReceived(message);
                }
            });
        }

        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, message);
            messages.Add(new MessageDataModel
            {
                id = messages.Count,
                content = message,
            });
            messagesOnDisplay = messages.Skip(Math.Max(0, messages.Count - 20)).Take(20).ToArray();
        }

        public void AttachFile()
        {
            currentAttachedFile = null;
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
                currentAttachedFile = new FileDataModel
                {
                    FileName = Path.GetFileName(openFileDialog.FileName),
                    Content = fileContent
                };
            }
        }
        public void Deconnecter()
        {
            clientModel.Deconnecter();
        }
    }
}
