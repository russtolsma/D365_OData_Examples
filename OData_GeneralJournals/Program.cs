using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OData_GeneralJournals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string request = null;

            ODataLedgerJournalExamples oDataLedgerJournalExamples = new ODataLedgerJournalExamples();
            ODataHttpClientLedgerJournalExamples oDataHttpClientExamples = new ODataHttpClientLedgerJournalExamples();

            if (oDataHttpClientExamples.Initialize())
            {
                DisplayOptions();

                do
                {
                    Console.Write("Enter a value: ");


                    request = Console.ReadLine();
                    Console.WriteLine("");

                    switch (request)
                    {
                        case "1":
                            oDataHttpClientExamples.GetLedgerJournalsViaODataURI();
                            request = "";
                            break;
                        case "2":
                            oDataLedgerJournalExamples.ListJournals();
                            request = "";
                            break;
                        case "3":
                            oDataLedgerJournalExamples.GetJournalLines();
                            break;
                        case "4":
                            oDataHttpClientExamples.CreateLedgerJournalViaODataURI();
                            break;
                        case "5":
                            oDataLedgerJournalExamples.DeleteJournal();
                            break;
                        case "i":
                            DisplayOptions();
                            break;
                        default:
                            break;
                    }
                } while (request.ToLower() != "x");
            }
            else
            {
                Console.WriteLine("Exiting test program.");
            }
        }

        private static void DisplayOptions()
        {
            Console.WriteLine("");
            Console.WriteLine("Select an option to test a function:");
            Console.WriteLine("1. Get Journals with OData/Http Client");
            Console.WriteLine("2. List General Journals with Dynamics Entities");
            Console.WriteLine("3. Get General Journal lines");
            Console.WriteLine("4. Create a new General Journal");
            Console.WriteLine("5. Delete an existing General Journal");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press 'x' to quit or 'i' for options.");
            Console.WriteLine("");
        }
    }
}
