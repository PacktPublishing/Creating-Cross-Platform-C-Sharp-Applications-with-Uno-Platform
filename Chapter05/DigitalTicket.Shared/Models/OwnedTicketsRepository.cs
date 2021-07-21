using DigitalTicket.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace DigitalTicket.Models
{
    public class OwnedTicketsRepository
    {
        const string DBFileName = "ownedTickets.db";
        private static SQLiteAsyncConnection database;

        public async static Task InitializeDatabase()
        {
            if (database != null)
            {
                return;
            }
            await ApplicationData.Current.LocalFolder.CreateFileAsync(DBFileName, CreationCollisionOption.OpenIfExists);
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DBFileName);

            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<OwnedTicket>().Wait();
        }
        public static Task<int> SaveTicketAsync(OwnedTicket ticket)
        {
            if (ticket.DBId != 0)
            {
                // Update an ticket.
                return database.UpdateAsync(ticket);
            }
            else
            {
                // Save a new ticket.
                return database.InsertAsync(ticket);
            }
        }

        public static Task<List<OwnedTicket>> LoadTicketsAsync()
        {
            //Get all tickets.
            return database.Table<OwnedTicket>().ToListAsync();
        }
    }
}
