using Microsoft.Office.Interop.Word;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
namespace DocGenerator.Shared.Models
{
    public class DocumentWord
    {
        /// <summary>
        /// Id to directory
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// File in byte[]
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// List of string to alter in file
        /// </summary>
        public List<DocumentInfo> ListNewInfoFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useAgent"></param>
        /// <param name="streamFile">File in .docx to template</param>
        public DocumentWord(string useAgent, MemoryStream streamFile)
        {
           // this.dicChangedTextFile = dicChangedTextFile;
            this.File = streamFile.ToArray();
            Id = Guid.NewGuid();
        }

        public DocumentWord()
        {

        }


    }
}
