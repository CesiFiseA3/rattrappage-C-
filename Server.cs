using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_Programmation_Système
{
    internal class SocketModel
    {
        private static Dictionary<Socket, string> clients = new Dictionary<Socket, string>();
        private Socket serverSocket;

        // Constructeur
        public SocketModel()
        {
            // Initialise le serveur lors de la création de l'objet
            serverSocket = SeConnecter();

            // Lance une tâche pour accepter les connexions en arrière-plan
            Task.Run(() => AccepterConnexions());
        }

        public void AccepterConnexions()
        {
            while (true)
            {
                // Accepter les connexions en continu
                Socket clientSocket = AccepterConnexion(serverSocket);

                // Lance une tâche pour écouter le réseau pour chaque client
                Task.Run(() => EcouterReseau(clientSocket));
            }
        }


        public static Socket SeConnecter()
        {
            // Implémentez la logique de création du socket serveur ici
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49153); // Adresse IP et port à utiliser
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10); // Mettre le serveur à l'écoute des connexions, avec une limite de 10 connexions en attente

            return serverSocket;
        }

        public static Socket AccepterConnexion(Socket serverSocket)
        {
            // Implémentez la logique d'acceptation des connexions clientes ici
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("Nouvelle connexion cliente acceptée.");

            // Demander au client de s'identifier avec un nom
            EnvoyerAuClient(clientSocket, "Veuillez vous identifier avec un nom:");
            string clientName = RecevoirDuClient(clientSocket);

            // Ajouter le client à la liste avec son nom
            clients.Add(clientSocket, clientName);

            // Envoyer un message de bienvenue au client
            EnvoyerAuClient(clientSocket, $"Bienvenue, {clientName}!");

            // Informer les autres clients de la nouvelle connexion
            EnvoyerATousLesClients(clientSocket, $"{clientName} vient de se connecter.");

            return clientSocket;
        }

        public static void EcouterReseau(Socket clientSocket)
        {
            // Implémentez la logique d'échange de données entre le serveur et le client ici
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while (true)
                {
                    // Lire les données du client
                    bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string messageFromClient = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        // Remplacer "client:" par le nom du client
                        Console.WriteLine($"{clients[clientSocket]}: {messageFromClient.Replace($"{clients[clientSocket]}:", "")}");

                        // Envoyer le message à tous les clients, sauf à l'expéditeur
                        EnvoyerATousLesClients(clientSocket, $"{clients[clientSocket]}: {messageFromClient}");
                    }
                }
            }
            catch (SocketException)
            {
                // Gérer la déconnexion du client
                Console.WriteLine($"{clients[clientSocket]} s'est déconnecté.");
                EnvoyerATousLesClients(clientSocket, $"{clients[clientSocket]} s'est déconnecté.");
                clients.Remove(clientSocket);
            }
        }

        public static void EnvoyerATousLesClients(Socket senderSocket, string message)
        {
            // Envoyer le message à tous les clients, sauf à l'expéditeur
            foreach (Socket client in clients.Keys)
            {
                if (client != senderSocket)
                {
                    EnvoyerAuClient(client, message);
                }
            }
        }

        private static void EnvoyerAuClient(Socket clientSocket, string message)
        {
            // Envoyer un message spécifique à un client
            clientSocket.Send(Encoding.ASCII.GetBytes($"{message}"));
        }

        public static string RecevoirDuClient(Socket clientSocket)
        {
            // Recevoir des données du client
            byte[] buffer = new byte[1024];
            int bytesRead = clientSocket.Receive(buffer);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }


    }
}
