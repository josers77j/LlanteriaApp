using SQLite;
using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwipeViewDemos.Data
{
    public class ProductosDataBase
    {
        private readonly SQLiteAsyncConnection database;

        public ProductosDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ProductoModel>().Wait();
        }

        public async Task<List<ProductoModel>> GetItemsAsync()
        {
            return await database.Table<ProductoModel>().ToListAsync();
        }

        public Task<ProductoModel> GetItemAsync(int id)
        {
            return database.Table<ProductoModel>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ProductoModel item)
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

        public Task<int> DeleteItemAsync(ProductoModel item)
        {
            return database.DeleteAsync(item);
        }
    }
}
