using Microsoft.AspNetCore.Http;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
namespace DocGenerator.Producer
{
    public class DocumentWord
    {
        private string useAgent { get; set; }

        private string directoryBase { get; set; }

        public Guid Id { get; set; }

        public DocumentWord(string useAgent)
        {
            this.useAgent = useAgent;
            Id = Guid.NewGuid();
            this.directoryBase = Directory.GetCurrentDirectory()+"/doc/";
        }

        /// <summary>
        /// Convert File .docx to .pdf
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>Document stream</returns>
        public void DocumentWordConvertPdf(IFormFile file)
        {
            try
            {
                string directoryDocx = $"{directoryBase}{Id}.docx";
                using (var f = File.Create(directoryDocx))
                {
                    file.CopyTo(f);
                    f.Dispose();
                }
                Word.Application word = new Word.Application();
                object OnMissing = System.Reflection.Missing.Value;
                word.Visible = false;
                word.ScreenUpdating = false;

                FileInfo fileInfo = new FileInfo(directoryDocx);
                Object fileName = (Object)fileInfo.FullName;
                Word.Document doc = word.Documents.Open(ref fileName, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing);
                doc.Activate();
                string directoryPdf = fileInfo.FullName.Replace(".docx", ".pdf");
                object outputFilename = directoryPdf;
                object fileformat = WdSaveFormat.wdFormatPDF;
                doc.SaveAs2(ref outputFilename, ref fileformat, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing);

                object savechanges = WdSaveOptions.wdSaveChanges;
                ((Document)doc).Close(ref savechanges, ref OnMissing, ref OnMissing);
                ((Application)word).Quit(ref OnMissing);

                DeleteFile(fileInfo.FullName);
            }
            catch
            {
                throw;
            }
           
        }

        /// <summary>
        /// Delete created files 
        /// </summary>
        /// <param name="directory"></param>
        private void DeleteFile(string directory)
        {
            new FileInfo(directory).Delete();
        }
    
        
    }
}
