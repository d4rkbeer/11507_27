using System;

public class TaskDispatcher
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5, 5);
    private readonly ConcurrentBag<Person> _results = new ConcurrentBag<Person>();
    private readonly List<Action> _tasks = new List<Action>();

    public TaskDispatcher()
    {

        for (int i = 1; i <= 50; i++)
        {
            int taskNumber = i;
            Action action = () =>
            {
                _semaphore.Wait();
                try
                {
                    var data = new Dictionary<string, object>
                        {
                            { "Name", $"Пользователь_{taskNumber}" },
                            { "Age", 20 + (taskNumber % 30) },
                            { "City", taskNumber % 2 == 0 ? "Казань" : "Москва" },
                            { "CreatedAt", DateTime.Now }
                        };


                    object obj = TypeFactory.CreateAndFill(typeof(Person), data);
                    Person person = (Person)obj;

                    _results.Add(person);

                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Задача {taskNumber} выполнена → {person}");
                }
                finally
                {
                    _semaphore.Release();
                }
            };

            _tasks.Add(action);
        }
    }
}