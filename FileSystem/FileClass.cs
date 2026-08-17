using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSystem
{
    internal class FileClass
    {
        internal static void ReadandWrite()
        {
            string sourceFile = "source.txt";
            string destinationFile = "destination.txt";

            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("File not found");
                return;
            }

            try
            {
                using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0){
                        destinationStream.Write(buffer, 0, bytesRead);
                    }
                    Console.WriteLine("File Written successfully");
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine("Error occured : " + ex.Message);
            }
        }
    }
}
