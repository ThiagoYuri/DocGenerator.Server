using DocGenerator.Shared;
using DocGenerator.Shared.Models;
using Microsoft.Office.Interop.Word;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Word = Microsoft.Office.Interop.Word;

namespace DocGenerator.ConsumerGeneratorPDF
{
    public class Program
    {
        static void Main(string[] args)
        {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Aguardando Mesagems");
                    channel.QueueDeclare(queue: "pdfGenerato",
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
                            CreatePDF(s, $"{Shared.Utils.GeneratorPDF.pathDefaultPdf}/{doc.Id}.docx");
                            Console.WriteLine(@$"ID:{doc.Id}");
                            Console.WriteLine("---------------------------------------------------------------------");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                       
                    };
                    channel.BasicConsume(queue: "pdfGenerato",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            


        }

        private static void CreatePDF(Stream file, string directoryDocx)
        {
            try
            {
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
    }
}
