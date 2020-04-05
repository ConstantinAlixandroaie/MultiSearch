using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using MultiSearch.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MultiSearch.Controller
{
    class MainController
    {

        public List<string> GetSearchStrings(string filePath)
        {
            string str;
            int num = 0;
            List<string> searchStrings = new List<string>();
            StreamReader reader = new StreamReader(filePath);
            while ((str = reader.ReadLine()) != null)
            {
                searchStrings.Add(str);
                num++;
            }
            reader.Close();
            return searchStrings;
        }
        public List<SearchResult> SearchPdfFiles(string path, List<string> searchStrings, List<string> searchFilters)
        {
            List<SearchResult> searchResults = new List<SearchResult>();
            if (searchFilters.Contains("*.pdf"))
            {
                foreach (string file in Directory.GetFiles(path, "*.pdf"))
                {
                    PdfDocument pdfDoc = new PdfDocument(new PdfReader(file));
                    int numberOfPages = pdfDoc.GetNumberOfPages();
                    foreach (string searchString in searchStrings)
                    {
                        if (!string.IsNullOrEmpty(searchString))
                        {
                            for (int i = 1; i <= numberOfPages; i++)
                            {
                                PdfPage page = pdfDoc.GetPage(i);
                                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                                var pageText = PdfTextExtractor.GetTextFromPage(page, strategy);
                                if (pageText.Contains(searchString))
                                { 
                                    string pageNumber = pdfDoc.GetPageNumber(page).ToString();
                                    var searchResult = new SearchResult()
                                    {
                                        FilePath = file,
                                        PageNumber = Int16.Parse(pageNumber),
                                        Keyword = searchString
                                    };
                                    searchResults.Add(searchResult);
                                }
                            }
                        }
                    }
                }
            }
            return searchResults;
        }

        public List<SearchResult> SearchTextFiles(string path, List<string> searchStrings, List<string> searchFilters)
        {
            List<string> searchResults = new List<string>();
            List<SearchResult> myResults = new List<SearchResult>();
            string[] textArray1 = new string[] { "*.pdf", "*.jpg", "*.doc", "*.docx" };
            string[] strArray = textArray1;
            foreach (string str in searchFilters)
            {
                if (!Enumerable.Contains<string>(strArray, str))
                {
                    foreach (string str2 in Directory.GetFiles(path, str))
                    {
                        searchResults.Add(str2);
                    }
                }
            }
            foreach (string searchString in searchStrings)
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    foreach (string str4 in searchResults)
                    {
                        int num2 = 0;
                        using (IEnumerator<string> enumerator3 = File.ReadLines(str4).GetEnumerator())
                        {
                            while (enumerator3.MoveNext())
                            {
                                num2++;
                                if (enumerator3.Current.Contains(searchString))
                                {
                                    SearchResult item = new SearchResult();
                                    item.FilePath = str4;
                                    item.RowNumber = new int?(num2);
                                    item.Keyword = searchString;
                                    myResults.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            return myResults;
        }

        public void WriteListToCSV(List<SearchResult> searchResults,string path)
        {
            FileStream myFile = File.Create(path + @"\Results.csv");
            myFile.Close();
            using (StreamWriter writer = File.AppendText(myFile.Name))
            {
                writer.WriteLine("Path,Keyword,PageNumber,RowNumber");
            }
            foreach (SearchResult result in searchResults)
            {
                string[] textArray1 = new string[8];
                textArray1[0] = (string)result.FilePath;
                textArray1[1] = ",";
                textArray1[2] = (string)result.Keyword;
                textArray1[3] = ",";
                textArray1[4] = (string)result.PageNumber.ToString();
                textArray1[5] = ",";
                int? rowNumber = result.RowNumber;
                textArray1[6] = (string)rowNumber.ToString();
                textArray1[7] = "\r\n";
                File.AppendAllText(myFile.Name, string.Concat((string[])textArray1));
            }
        }


    }
}
