using SQLite;
using Week3Lab.Models;

namespace Week3Lab.Services
{
    public class DbTodoRepository : ITodoRepository
    {
        // https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0#configure-app-constants

        SQLiteAsyncConnection Database;

        public DbTodoRepository()
        {

        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Todo>();
        }

        public async Task<List<Todo>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Todo>().ToListAsync();
        }

        public async Task<List<Todo>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<Todo>().Where(t => t.IsDone).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<Todo> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Todo>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateItemAsync(Todo item)
        {
            await Init();
            if (item.Id != 0)
            {
                return await Database.UpdateAsync(item);
            }

            return 0;
        }

        public async Task<int> SaveItemAsync(Todo item)
        {
            await Init();
            if (item.Id == 0)
            {
                return await Database.InsertAsync(item);
            }

            return 0;
        }

        public async Task<int> DeleteItemAsync(Todo item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        public async Task<Todo?> Add(Todo todo)
        {
            var i = await SaveItemAsync(todo);
            return i == 0 ? null : todo;
        }

        public async Task<bool> Delete(int id)
        {
            var todo = await GetItemAsync(id);
            var i = await DeleteItemAsync(todo);
            return i > 0;
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            var res = await GetItemsAsync();
            return res;
        }

        public Task<Todo> GetTodoById(int id)
        {
            return GetItemAsync(id);
        }

        public async Task<bool> Update(Todo todo)
        {
            var i = await UpdateItemAsync(todo);
            return i > 0;
        }
    }
}
