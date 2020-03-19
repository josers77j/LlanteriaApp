using SQLite;
using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwipeViewDemos.Data
{
    public class LlanteriaDataBase
    {
        private readonly SQLiteAsyncConnection database;

        public LlanteriaDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<CategoriaModel>().Wait();
        }

        public async Task<List<CategoriaModel>> GetItemsAsync()
        {
            return await database.Table<CategoriaModel>().ToListAsync();
        }

        public Task<CategoriaModel> GetItemAsync(int id)
        {
            return database.Table<CategoriaModel>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(CategoriaModel item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(CategoriaModel item)
        {
            return database.DeleteAsync(item);
        }
    }
}
