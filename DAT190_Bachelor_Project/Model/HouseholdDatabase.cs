using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public class HouseholdDatabase
    {
        readonly SQLiteAsyncConnection database;

        public HouseholdDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Household>().Wait();
        }

        public Task<List<Household>> GetHouseholdsAsync()
        {
            return database.Table<Household>().ToListAsync();
        }

        public Task<Household> GetHouseholdAsync(int id)
        {
            return database.Table<Household>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveHouseholdAsync(Household household)
        {
            if (household.Id == 0)
            {
                return database.InsertAsync(household);
            }
            else
            {
                return database.UpdateAsync(household);
            }
        }

        public Task<int> DeleteHouseholdAsync(Household household)
        {
            return database.DeleteAsync(household);
        }
    }
}
