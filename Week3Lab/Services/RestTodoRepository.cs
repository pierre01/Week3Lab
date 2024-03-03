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

        }

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

                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteTodoItemAsync(string id)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, id));

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public Todo Add(Todo todo)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Todo> GetAllTodos()
        {
            throw new NotImplementedException();
        }

        public Todo? GetTodoById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}
