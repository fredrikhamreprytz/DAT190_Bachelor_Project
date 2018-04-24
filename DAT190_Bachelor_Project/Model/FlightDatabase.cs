using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public class FlightDatabase
    {
        readonly SQLiteAsyncConnection database;

        public FlightDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Flight>().Wait();
        }

        public Task<List<Flight>> GetFlightsAsync() 
        {
            return database.Table<Flight>().ToListAsync();
        }

        public Task<Flight> GetFlightAsync(int id)
        {
            return database.Table<Flight>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveFlightAsync(Flight flight)
        {
            if (flight.Id == 0)
            {
                return database.InsertAsync(flight);
            }
            else
            {
                return database.UpdateAsync(flight);
            }
        }

        public Task<int> DeleteFlightAsync(Flight flight)
        {
            return database.DeleteAsync(flight);
        }
    }
}
