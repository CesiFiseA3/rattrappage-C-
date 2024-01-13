using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppClient.Models
{
    public class MessageDataModel
    {
        public int id { get; set; }
        public string content { get; set; }
        public ObservableCollection<FileDataModel> attachedFiles { get; set; } 
    }
}
