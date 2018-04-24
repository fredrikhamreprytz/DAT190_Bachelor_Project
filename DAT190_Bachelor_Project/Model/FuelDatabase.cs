using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public class FuelDatabase
    {
        readonly SQLiteAsyncConnection database;

        public FuelDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Fuel>().Wait();
        }

        public Task<List<Fuel>> GetFuelsAsync() 
        {
            return database.Table<Fuel>().ToListAsync();
        }

        public Task<Fuel> GetFuelAsync(int id)
        {
            return database.Table<Fuel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveFuelAsync(Fuel fuel)
        {
            if (fuel.Id == 0)
            {
                return database.InsertAsync(fuel);
            }
            else
            {
                return database.UpdateAsync(fuel);
            }
        }

        public Task<int> DeleteFuelAsync(Fuel fuel)
        {
            return database.DeleteAsync(fuel);
        }
    }
}
