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
        public Guid Id { get; set; }

        public byte[] File { get; set; }   

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
