using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppServer.Models
{
    public class FileDataModel
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
