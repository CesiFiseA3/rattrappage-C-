using Rattrapage_Programmation_Système.ViewModels;
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
            clientSocket.Send(Encoding.ASCII.GetBytes($"{message}"));
        }

        public void Deconnecter()
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }

        public string RecevoirMessage()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = clientSocket.Receive(buffer);
                return Encoding.ASCII.GetString(buffer, 0, bytesRead);
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
