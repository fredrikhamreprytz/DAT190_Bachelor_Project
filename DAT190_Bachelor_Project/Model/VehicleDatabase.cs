using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public class VehicleDatabase
    {
        readonly SQLiteAsyncConnection database;

        public VehicleDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Vehicle>().Wait();
        }

        public Task<List<Vehicle>> GetVehiclesAsync()
        {
            return database.Table<Vehicle>().ToListAsync();
        }

        public Task<Vehicle> GetVehicleAsync(string registrationNumber)
        {
            return database.Table<Vehicle>().Where(x => x.RegistrationNumber == registrationNumber).FirstOrDefaultAsync();
        }

        public Task<int> SaveVehicleAsync(Vehicle vehicle)
        {
            if (vehicle.RegistrationNumber == null)
            {
                return database.InsertAsync(vehicle);
            }
            else
            {
                return database.UpdateAsync(vehicle);
            }
        }

        public Task<int> DeleteVehicleAsync(Vehicle vehicle)
        {
            return database.DeleteAsync(vehicle);
        }
    }
}
