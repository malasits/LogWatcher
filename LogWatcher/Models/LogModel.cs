using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogWatcher.Models
{
    public class LogModel
    {
        public String FileName { get; set; }
        public String Path { get; set; }
        public long FileSize { get; set; }
    }
}
