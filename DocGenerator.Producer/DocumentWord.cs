using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
namespace DocGenerator.Producer
{
    public static class DocumentWord
    {        
        /// <summary>
        /// Convert File .docx to .pdf
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>Document pdf</returns>
        public static Word.Document DocumentWordPdf(FileInfo fileInfo)
        {
                      
            Word.Application word = new Word.Application();
            object OnMissing = System.Reflection.Missing.Value;

            word.Visible = false;
            word.ScreenUpdating = false;

            Object fileName = (Object)fileInfo.FullName;

            Word.Document doc = word.Documents.Open(ref fileName,ref OnMissing,
                ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                ref OnMissing, ref OnMissing, ref OnMissing);
            doc.Activate();

            object outputFilename = fileInfo.FullName.Replace(".docx", ".pdf");
            object fileformat = WdSaveFormat.wdFormatPDF;

            doc.SaveAs2(ref outputFilename, ref fileformat, ref OnMissing, ref OnMissing,
                ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing, ref OnMissing,
                ref OnMissing, ref OnMissing, ref OnMissing);

            object savechanges = WdSaveOptions.wdSaveChanges;
            ((Document)doc).Close(ref savechanges, ref OnMissing, ref OnMissing);
            doc = null;

            ((Application)word).Quit(ref OnMissing);

            return doc;

        }
               

    }
}
