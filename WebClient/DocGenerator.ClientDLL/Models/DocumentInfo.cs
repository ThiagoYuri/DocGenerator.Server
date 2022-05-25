using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  DocGenerator.ClientDLL.Models
{
    public class DocumentInfo
    {
        /// <summary>
        /// key in file .docx to alter value (default key "[key]")
        /// </summary>
        public string keyInFile { get; set; }
        /// <summary>
        /// value
        /// </summary>
        public string value { get; set; }
    }
}
