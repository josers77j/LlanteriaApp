using SQLite;
using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwipeViewDemos.Data
{
    public class VentasDataBase
    {
        private readonly SQLiteAsyncConnection database;

        public VentasDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<VentaModel>().Wait();
        }

        public async Task<List<VentaModel>> GetItemsAsync()
        {
            return await database.Table<VentaModel>().ToListAsync();
        }

        public Task<VentaModel> GetItemAsync(int id)
        {
            return database.Table<VentaModel>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(VentaModel item)
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

        public Task<int> DeleteItemAsync(VentaModel item)
        {
            return database.DeleteAsync(item);
        }
    }
}
