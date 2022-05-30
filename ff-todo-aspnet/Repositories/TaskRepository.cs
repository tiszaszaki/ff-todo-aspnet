﻿using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.ResponseObjects;
using static ff_todo_aspnet.Configurations.TodoDbContext;

namespace ff_todo_aspnet.Repositories
{
    public class TaskRepository
    {
        private readonly TodoDbContext context;
        public TaskRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskResponse> FetchTasks()
        {
            return context.Tasks.Select<Entities.Task, TaskResponse>(task => task);
        }
        public IEnumerable<TaskResponse> FetchAllTasksFromTodo(long todoId)
        {
            return context.Tasks
                .Where(task => task.todoId == todoId)
                .Select<Entities.Task, TaskResponse>(task => task);
        }
        public TaskResponse FetchTask(long id)
        {
            return context.Tasks.Single(task => task.id == id);
        }
        public TaskResponse FetchTaskByName(string name)
        {
            return context.Tasks.Single(task => task.name == name);
        }
        public Entities.Task AddTask(Entities.Task task)
        {
            task.name = context.ReplaceNameToUnused(TodoDbEntityType.FFTODO_TASK, task.name, false);
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }
        public void RemoveTask(long id)
        {
            var task = context.Tasks.Single(task => task.id == id);
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
        public void RemoveAllTasks()
        {
            context.Tasks.RemoveRange(context.Tasks);
            context.SaveChanges();
        }
        public void RemoveAllTasksFromTodo(long todoId)
        {
            foreach (var task in context.Tasks.Where(task => task.todoId == todoId))
                context.Tasks.Remove(task);
            context.SaveChanges();
        }
        public TaskResponse UpdateTask(long id, Entities.Task patchedTask)
        {
            var task = context.Tasks.Single(task => task.id == id);
            task.name = patchedTask.name;
            task.done = patchedTask.done;
            task.deadline = patchedTask.deadline;
            context.SaveChanges();
            return task;
        }
    }
}
