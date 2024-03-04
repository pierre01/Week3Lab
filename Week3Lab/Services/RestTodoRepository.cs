using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Week3Lab.Models;

namespace Week3Lab.Services
{
    public class RestTodoRepository : ITodoRepository
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<Todo> Items { get; private set; }

        public RestTodoRepository()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            List<Todo> todoList =
            [
                new Todo { Title ="CarWash", IsDone = false },
                new Todo { Title = "Mow Lawn", IsDone = false },
                new Todo { Title = "Trim Edges", IsDone = false },
                new Todo { Title = "Repair Fence", IsDone = false },
                new Todo { Title = "Pickup Kids", IsDone = false },
                new Todo { Title = "Cook Dinner", IsDone = false },
                new Todo { Title = "Book Ticket", IsDone = false },
                new Todo { Title = "Buy Eggs", IsDone = false },
                new Todo { Title = "Buy Orange Juice", IsDone = false },
                new Todo { Title = "Pay Housekeeper", IsDone = false },
                new Todo { CompletedOn=new DateTime(2022,1,1),Title = "Call Mom", IsDone = true },
                new Todo { CompletedOn=new DateTime(2021,6,12),Title = "Run 3 miles", IsDone = true },
                new Todo { CompletedOn=new DateTime(2023,7,18),Title = "Clean Cat litter", IsDone = true },
                new Todo { CompletedOn=new DateTime(2022,11,22),Title = "Doctor Appointment", IsDone = true },
            ];
            foreach (var todo in todoList)
            {
                Add(todo);
            }
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Get all the Todos from the Web API
        /// </summary>
        /// <returns></returns>
        public async Task<List<Todo>> RefreshDataAsync()
        {
            Items = new List<Todo>();

            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<Todo>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        /// <summary>
        /// Save a Todo item to the Web API
        /// </summary>
        /// <param name="item">Todo to Create or update</param>
        /// <param name="isNewItem">identify if we should do a Put or a Post</param>
        /// <returns></returns>
        public async Task<Todo> SaveTodoItemAsync(Todo item, bool isNewItem = false)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<Todo>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                    response = await _client.PostAsync(uri, content);
                else
                    response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    // rest returned the object with the new id
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    // deserialize the result
                    Todo retTodo = JsonSerializer.Deserialize<Todo>(jsonResult, _serializerOptions);
                    return retTodo;
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Delete a Todo item from the Web API
        /// </summary>
        /// <param name="id">id of the todo to delete</param>
        /// <returns>true if successful</returns>
        public async Task<bool> DeleteTodoItemAsync(string id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}/{id}");

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Repository Interface 
        /// Add a new Todo item to the Web API
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>the new Todo</returns>
        public async Task<Todo> Add(Todo todo)
        {
            var todoRes = await SaveTodoItemAsync(todo, true);
            return todo;
        }

        /// <summary>
        /// Repository Interface
        /// Delete a Todo item from the Web API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            return await DeleteTodoItemAsync(id.ToString());
        }

        /// <summary>
        /// Repository Interface
        /// Retrieve all the Todos from the Web API
        /// </summary>
        /// <returns></returns>
        public async Task<List<Todo>> GetAllTodos()
        {
            return await RefreshDataAsync();
        }

        /// <summary>
        /// Repository Interface
        /// Retrieve a Todo by its id from the Web API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Todo?> GetTodoById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Repository Interface
        /// Update a Todo item in the Web API
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public async Task<bool> Update(Todo todo)
        {
            var todoRes = await SaveTodoItemAsync(todo);
            return todoRes != null;
        }
    }
}
