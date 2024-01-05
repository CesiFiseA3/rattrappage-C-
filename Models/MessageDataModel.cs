using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_Programmation_Système.Models
{
    internal class MessageDataModel
    {
        public int id { get; set; }
        public string content { get; set; }
        public List<FileDataModel> attachedFiles { get; set; } 
    }
}
