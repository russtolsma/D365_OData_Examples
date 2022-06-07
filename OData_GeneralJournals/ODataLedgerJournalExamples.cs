using Microsoft.Dynamics.DataEntities;
using Microsoft.OData.Client;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace OData_GeneralJournals
{
    internal class ODataLedgerJournalExamples
    {
        private string baseURI = null;
        private string authenticationHeader = null;
        private Resources context = null;

        public ODataLedgerJournalExamples()
        {
            Init();
        }

        public void ListJournals()
        {
            IEnumerable<LedgerJournalHeader> journalHeaders = context.LedgerJournalHeaders.Execute();

            foreach (var journal in journalHeaders)
            {
                Console.WriteLine("Journal Batch number: {0} - Name: {1} | Posted: {2}", journal.JournalBatchNumber, journal.JournalName, journal.IsPosted);
                Console.WriteLine("Description: {0}", journal.Description);
            }
        }

        public void ListNonPostedJournals()
        {
            IEnumerable<LedgerJournalHeader> journalHeaders = context.LedgerJournalHeaders.AddQueryOption("$filter", "IsPosted eq Microsoft.Dynamics.DataEntities.NoYes'No'").Execute();

            foreach (var journal in journalHeaders)
            {
                Console.WriteLine("Journal Batch number: {0} - Name: {1} | Posted: {2}", journal.JournalBatchNumber, journal.JournalName, journal.IsPosted);
                Console.WriteLine("Description: {0}", journal.Description);
            }
        }

        public void GetJournalLines()
        {
            Console.Write("Enter journal batch number for lines: ");
            string journalBatchNumber = Console.ReadLine();

            var journalHeader = context.LedgerJournalHeaders.Where(x => x.JournalBatchNumber == journalBatchNumber).FirstOrDefault();

            if (journalHeader.JournalBatchNumber != "")
            {
                List<LedgerJournalLine> journalLines = context.LedgerJournalLines.AddQueryOption("$filter", "JournalBatchNumber eq '" + journalHeader.JournalBatchNumber + "'").Execute().ToList();

                int lineCount = journalLines.Count();

                if (lineCount > 0)
                {
                    Console.WriteLine("Journal Lines: {0}", lineCount);
                    Console.WriteLine("");
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.WriteLine("");

                    foreach (var journalLine in journalLines)
                    {
                        Console.WriteLine("Line Number: {0} - Date: {1} - Voucher {2} | Account Type: {3} | Account: {4} | Description: {5} | Debit: {6} | Credit: {7}", journalLine.LineNumber, journalLine.DocumentDate, journalLine.Voucher, journalLine.AccountType, journalLine.AccountDisplayValue, journalLine.Text, journalLine.DebitAmount, journalLine.CreditAmount);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("No lines for General Journal: {0}", journalHeader.JournalBatchNumber);
                }
            }
            else
            {
                Console.WriteLine("General Journal number: {0} was not found.", journalBatchNumber);
            }
        }

        public void CreateJournal()
        {   
            // *** Would love to use the LedgerJournalHeader entity to do the journal creation but the entity won't allow me to send a create
            // *** call due to the JournalBatchNumber not being defined -- the only way to change this is by going into D365 and changing the property allow
            // *** for users to create their own batchnumbers on this object...not ideal depending on policy.

            ////DataServiceCollection<LedgerJournalHeader> journalHeaderCollection = new DataServiceCollection<LedgerJournalHeader>(context);
            ////DataServiceCollection<LedgerJournalLine> journalLinesCollection = new DataServiceCollection<LedgerJournalLine>(context);

            //LedgerJournalHeader journalHeader = new LedgerJournalHeader();

            //journalHeader.DataAreaId = "usmf";
            //journalHeader.JournalName = "GenJrn";
            //journalHeader.JournalBatchNumber = "";

            //context.AddToLedgerJournalHeaders(journalHeader);

            //try
            //{
            //    DataServiceResponse response = context.SaveChanges(SaveChangesOptions.BatchWithSingleChangeset);

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    Debug.WriteLine(ex.InnerException.Message);
            //}
            
        }

        public void DeleteJournal()
        {
            Console.Write("Enter journal batch number to be deleted: ");
            string journalBatchNumber = Console.ReadLine();

            LedgerJournalHeader journalHeader = context.LedgerJournalHeaders.Where(x => x.JournalBatchNumber == journalBatchNumber).FirstOrDefault();

            if (journalHeader != null)
            {
                context.DeleteObject(journalHeader);

                context.SaveChanges();
            }
        }

        public void GetJournalLines(string journalBatchNumber)
        {
            IEnumerable<LedgerJournalHeader> journalHeaders = context.LedgerJournalHeaders.AddQueryOption("$filter", "IsPosted eq Microsoft.Dynamics.DataEntities.NoYes'No'").Execute();
        }

        private void Init()
        {
            baseURI = ODataClientConfiguration.Config.ActiveDirectoryResource + "/data";
            authenticationHeader = ODataAuthenticationHelper.GetAuthenticationHeader().Result;

            context = new Resources(new Uri(baseURI, UriKind.Absolute));

            if (context != null)
            {
                context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
                {
                    e.RequestMessage.SetHeader(ODataAuthenticationHelper.OAuthHeader, authenticationHeader);
                });
            }
            else
            {
                throw new Exception("OData proxy class is null");
            }
        }
    }
}
