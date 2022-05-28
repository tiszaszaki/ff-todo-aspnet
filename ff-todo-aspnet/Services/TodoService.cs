using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tiszaszaki_asp_webapp_2022.Repositories;

namespace tiszaszaki_asp_webapp_2022.Services
{
    public class TodoService
    {
        private readonly TodoRepository todoRepository;
        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
        public void Test()
        {
            Console.WriteLine("Testing tiszaszaki_asp_webapp_2022 service");
        }
    }
}
