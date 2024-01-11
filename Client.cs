using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_Programmation_Système
{
    public class Client
    {
        private Socket clientSocket;
        private string clientName;

        public Client(string name)
        {
            clientName = name;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 49153);
            clientSocket = socket;
        }

        public void EnvoyerMessage(string message)
        {
            clientSocket.Send(Encoding.ASCII.GetBytes($"{clientName}: {message}"));
        }

        public void Deconnecter()
        {
            Deconnecter(clientSocket);
        }

        public string RecevoirMessage()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = clientSocket.Receive(buffer);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }

        private static void Deconnecter(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
