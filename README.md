# Zad�n� [Czech]

* Projd�te tento k�d a popi�te p��padn� probl�my a jak byste je vy�e�ili.
* P�eve�te tento k�d na webovou slu�bu (ASP.NET Core Web API) a upravte k�d tak aby bylo mo�n�:
   * konvertovat mezi form�ty XML a JSON
   * snadno p�idat nov� form�t (nap�. Protobuf)
   * odeslat zdrojov� a st�hnout v�sledn� soubor z API
   * na��st a ulo�it data z/do libovoln� cesty na disku (p��padn� cloud-storage)
   * na��st data z HTTP URL (nelze ukl�dat)
   * odeslat v�sledn� soubor e-mailem (sta�� pouze n�st�el)
* Napi�te testy
* Refactorujte k�d tak, jak by jste si p�edstavovali produk�n� aplikaci, kter� je vyv�jena a provozov�na va��m t�mem.

# Assignment [English]

* Investigate the following source code and decribe potential issues with your proposed solution
* Transform this source code to WEB API service (ASP.NET Core Web API) and change source code to met this following criteria:
   * conversion between exchange formats XML and JSON
   * easy extension to support Protobuf as well
   * sending source file and downloading destination file from API
   * loading / saving file to any path (cloud can be used as well)
   * loading data from HTTP URL (without saving functionality)
   * sending file via e-mail (Proof-Of-Concept will be sufficient)

* Write tests
* Refactor source code, to be ready on production environment.

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Notino.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

            try
            {
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);

            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);


        }
    }
}



C:\Users\duchp\Downloads\test.json


Example online XML file
https://www.w3schools.com/xml/note.xml


Example of getting online data
http://aukro.cz
https://www.linkedin.com/feed/
https://www.blesk.cz/
https://www.vsb.cz/cs/
https://www.w3schools.com/xml/note.xml
https://www.csob.cz/portal/firmy/prehled-on-line-kanalu-a-aplikaci/csob-ceb
