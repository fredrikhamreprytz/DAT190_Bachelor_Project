using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public class UserDatabase
    {
        readonly SQLiteAsyncConnection database;

        public UserDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUsersAsync() 
        {
            return database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(string email)
        {
            return database.Table<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.Email == null)
            {
                return database.InsertAsync(user);
            }
            else
            {
                return database.UpdateAsync(user);
            }
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return database.DeleteAsync(user);
        }

    }
}
