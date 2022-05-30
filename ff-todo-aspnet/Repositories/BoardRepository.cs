﻿using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Repositories
{
    public class BoardRepository
    {
        private readonly TodoDbContext context;
        public BoardRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<BoardResponse> FetchBoards()
        {
            return context.Boards.Select<Board, BoardResponse>(board => board);
        }
        public IEnumerable<long> FetchBoardIds()
        {
            return context.Boards.Select(board => board.id);
        }
        public BoardResponse FetchBoard(long id)
        {
            return context.Boards.Single(board => board.id == id);
        }
        public Board AddBoard(Board board)
        {
            context.Boards.Add(board);
            context.SaveChanges();
            return board;
        }
        public void RemoveBoard(long id)
        {
            var board = context.Boards.Single(board => board.id == id);
            context.Boards.Remove(board);
            context.SaveChanges();
        }
        public BoardResponse UpdateBoard(long id, Board patchedBoard)
        {
            var board = context.Boards.Single(board => board.id == id);
            board.name = patchedBoard.name;
            board.description = patchedBoard.description;
            board.author = patchedBoard.author;
            context.SaveChanges();
            return board;
        }
        public bool FetchBoardReadonlyTodosSetting(long id)
        {
            return context.Boards.Single(board => board.id == id).readonlyTodos;
        }
        public bool UpdateBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            var board = context.Boards.Single(board => board.id == id);
            board.readonlyTodos = isReadonly;
            context.SaveChanges();
            return board.readonlyTodos;
        }
        public bool FetchBoardReadonlyTasksSetting(long id)
        {
            return context.Boards.Single(board => board.id == id).readonlyTasks;
        }
        public bool UpdateBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            var board = context.Boards.Single(board => board.id == id);
            board.readonlyTasks = isReadonly;
            context.SaveChanges();
            return board.readonlyTasks;
        }
    }
}