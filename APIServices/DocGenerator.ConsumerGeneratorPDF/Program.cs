using DocGenerator.Shared;
using DocGenerator.Shared.Models;
using Microsoft.Office.Interop.Word;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Word = Microsoft.Office.Interop.Word;

namespace DocGenerator.ConsumerGeneratorPDF
{
    public class Program
    {
        private static object OnMissing = System.Reflection.Missing.Value;

        static void Main(string[] args)
        {
                var factory = new ConnectionFactory() { Endpoint= new AmqpTcpEndpoint("http://localhost:15672"), HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Aguardando Mesagems");
                    channel.QueueDeclare(queue: "pdfGenerator",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var json = Encoding.UTF8.GetString(body);
                            DocumentWord doc = JsonSerializer.Deserialize<DocumentWord>(json);
                            Stream s = new MemoryStream(doc.File);
                            string directory = $"{Shared.Utils.GeneratorPDF.pathDefaultPdf}/{doc.Id}.docx";
                            CreateFile(s, directory);
                            CreatePDF(s, directory,doc);
                            Console.WriteLine(@$"ID:{doc.Id}");
                            Console.WriteLine("---------------------------------------------------------------------");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                       
                    };
                    channel.BasicConsume(queue: "pdfGenerator",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }      
        }



        #region Action Files
        /// <summary>
        /// Create PDF
        /// </summary>
        /// <param name="file">stream of file</param>
        /// <param name="directoryDocx">file directory</param>
        /// <param name="documentWord">List string for change</param>
        private static void CreatePDF(Stream file, string directoryDocx, DocumentWord documentWord)
        {
            try
            {               

                Word.Application word = new Word.Application() { Visible = false, ScreenUpdating = false };
                FileInfo fileInfo = new FileInfo(directoryDocx);
                Object fileName = (Object)fileInfo.FullName;
                Word.Document doc = word.Documents.Open(ref fileName);
                doc.Activate();

                ReplaceValue(word, documentWord.ListNewInfoFile);

                #region convert docx to pdf
                string directoryPdf = fileInfo.FullName.Replace(".docx", ".pdf");
                object outputFilename = directoryPdf;
                object fileformat = WdSaveFormat.wdFormatPDF;
                doc.SaveAs2(ref outputFilename, ref fileformat);
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
        /// Delete existing files
        /// </summary>
        /// <param name="directory">Directory to deleting file</param>
        private static void DeleteFile(string directory)
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
        private static void CreateFile(Stream file, string directoryDocx)
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

        /// <summary>
        ///  Replace values into word
        /// </summary>
        /// <param name="word">word</param>
        /// <param name="listDocumentInfo">List string for change</param>
        private static void ReplaceValue(Word.Application word, List<DocumentInfo> listDocumentInfo)
        { 
            #region Edit Text
            foreach (var x in listDocumentInfo)
            {
                word.Selection.Find.ClearFormatting();
                word.Selection.Find.Replacement.ClearFormatting();

                object key = $"[{x.keyInFile}]";
                object value = x.value;            

                object replace = Word.WdReplace.wdReplaceAll;

                object wrap = 1;

                var valor = word.Selection.Find.Execute(ref key, ref OnMissing,
                                                ref OnMissing, ref OnMissing, ref OnMissing,
                                                ref OnMissing, ref OnMissing,
                                                ref wrap, ref OnMissing, ref value,
                                                ref replace, ref OnMissing,
                                                ref OnMissing, ref OnMissing,
                                                ref OnMissing);
                
            }
            #endregion
        }
        #endregion
    }
}
