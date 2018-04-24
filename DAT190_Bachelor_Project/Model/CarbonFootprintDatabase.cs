using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public class CarbonFootprintDatabase
    {
        readonly SQLiteAsyncConnection database;

        public CarbonFootprintDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<CarbonFootprint>().Wait();
        }

        public Task<List<CarbonFootprint>> GetCarbonFootprintsAsync()
        {
            return database.Table<CarbonFootprint>().ToListAsync();
        }

        public Task<CarbonFootprint> GetCarbonFootprintAsync(int id)
        {
            return database.Table<CarbonFootprint>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCarbonFootprintAsync(CarbonFootprint carbonFootprint)
        {
            if (carbonFootprint.Id == 0)
            {
                return database.InsertAsync(carbonFootprint);
            }
            else 
            {
                return database.UpdateAsync(carbonFootprint);
            }
        }

        public Task<int> DeleteCarbonFootprintAsync(CarbonFootprint carbonFootprint)
        {
            return database.DeleteAsync(carbonFootprint);
        }
    }
}
