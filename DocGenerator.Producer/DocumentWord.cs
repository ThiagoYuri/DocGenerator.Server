using Microsoft.AspNetCore.Http;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
namespace DocGenerator.Producer
{
    public class DocumentWord
    {
        private string useAgent { get; set; }

        public Guid Id { get; set; }

        public DocumentWord(string useAgent)
        {
            this.useAgent = useAgent;
            Id = Guid.NewGuid();
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
                string directoryDocx = $"{Properties.StaticProperties.pathDefaultPdf}{Id}.docx";
                CreateFile(file, directoryDocx);

                Word.Application word = new Word.Application() { Visible = false, ScreenUpdating = false };
                object OnMissing = System.Reflection.Missing.Value;

                FileInfo fileInfo = new FileInfo(directoryDocx);
                Object fileName = (Object)fileInfo.FullName;
                Word.Document doc = word.Documents.Open(ref fileName, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing);
                doc.Activate();

                #region convert docx to pdf
                string directoryPdf = fileInfo.FullName.Replace(".docx", ".pdf");
                object outputFilename = directoryPdf;
                object fileformat = WdSaveFormat.wdFormatPDF;
                doc.SaveAs2(ref outputFilename, ref fileformat, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                    ref OnMissing, ref OnMissing, ref OnMissing);
                object savechanges = WdSaveOptions.wdSaveChanges;
                #endregion

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
            try
            {
                new FileInfo(directory).Delete();
            }
            catch { throw; }
        }

        /// <summary>
        /// Create file
        /// </summary>
        /// <param name="file">File of request</param>
        /// <param name="directoryDocx">New directory with extension</param>
        private void CreateFile(IFormFile file, string directoryDocx)
        {
            try
            {
                using (var f = File.Create(directoryDocx))
                {
                    file.CopyTo(f);
                    f.Dispose();
                }
            }
            catch { throw; }
        }

    }
}
