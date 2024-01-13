using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChatAppServer.Model;
using System.Threading;
using System.Runtime.CompilerServices;

namespace ChatAppServer.ViewModel
{
    class MainViewModel
    {
        public MainWindow mainWindow;
        public MainViewModel(MainWindow console)
        {
            this.mainWindow = console;
            // Appeler la méthode SeConnecter et sauvegarder le résultat dans une variable locale
            Socket serverSocket = Server.SeConnecter();

            while (true)
            {
                // Appeler la méthode AccepterConnexion pour accepter les connexions clientes
                Socket clientSocket = Server.AccepterConnexion(serverSocket);

                // Démarrer un thread pour gérer la communication avec le client
                Thread clientThread = new Thread(() => Server.EcouterReseau(clientSocket));
                clientThread.Start();
            }
        }

        public void NewMessage(string message)
        {
            this.mainWindow.Console.Text += Environment.NewLine + message ;
        }
    }
}
