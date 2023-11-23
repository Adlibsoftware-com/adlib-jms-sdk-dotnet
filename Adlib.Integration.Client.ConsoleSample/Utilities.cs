using Adlib.Integration.Client.Library.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Adlib.Integration.Client.ConsoleSample
{
    public static class Utilities
    {
        public static void GetPassword(ApplicationSettings applicationSettings)
        {
            if (string.IsNullOrEmpty(applicationSettings.ConsolePassword))
            {
                Console.WriteLine($"No Registration password was found for {applicationSettings.ConsoleUsername}." +
                    $"\r\nPlease enter the password now or hit enter to skip if already registered:");
                applicationSettings.ConsolePassword = Utilities.GetConsolePasswordInput();
            }
        }

        public static string GetConsolePasswordInput()
        {
            var password = new StringBuilder();
            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey(true);
           
                // Ignore any key out of range.
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    for (var i = 0; i < password.Length; i++)
                    {
                        Console.Write("\b \b");
                    }
                    password.Clear();
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) {
                    // Append the character to the password.
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }   
                
                // Exit if Enter key is pressed.
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }

        public static string SerializeToXmlUtf8<T>(T serializationTarget) where T : class
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };

            var serializer = new XmlSerializer(serializationTarget.GetType());

            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
                {
                    serializer.Serialize(xmlWriter, serializationTarget);
                }
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public static T Deserialize<T>(this string serializedTarget, string rootElementNameOverride = "", bool defaultAdlibNamespace = false) where T : new()
        {
            var serializer = string.IsNullOrWhiteSpace(rootElementNameOverride) ?
                new XmlSerializer(typeof(T)) :
                new XmlSerializer(typeof(T), defaultAdlibNamespace ?
                    new XmlRootAttribute(rootElementNameOverride)
                    {
                        Namespace = "http://www.adlibsoftware.com"
                    } :
                    new XmlRootAttribute(rootElementNameOverride));

            using (var xmlReader = new XmlTextReader(new StringReader(serializedTarget)))
            {
                var result = (T)serializer.Deserialize(xmlReader);
                return result;
            }
        }

        public static string CreateTempFileShareFolder(string fileShareTempRoot)
        {
            //var fileShareTempRoot = Path.Combine(Settings.Default.AdlibFileShareRoot, fileShareTempSubFolder);
            if (Directory.Exists(fileShareTempRoot))
            {
                try
                {
                    Console.WriteLine($"Deleting file share temp from last run ({fileShareTempRoot})...");
                    Directory.Delete(fileShareTempRoot, true);
                }
                catch
                {
                    Console.WriteLine($"Could not delete temp folder on Share {fileShareTempRoot}");
                    throw;
                }
            }
            Directory.CreateDirectory(fileShareTempRoot);
            return fileShareTempRoot;
        }
    }
}
