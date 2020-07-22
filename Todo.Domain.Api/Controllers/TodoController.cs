using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodoController : ControllerBase
    {

        [Route("")]
        [HttpPost]
        public GenericCommandResult Create(
            [FromBody] CreateTodoCommand command,
            [FromServices] TodoHandler handler
            )
        {
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("")]
        [HttpPut]
        public GenericCommandResult Update(
            [FromBody] UpdateTodoCommand command,
            [FromServices] TodoHandler handler
            )
        {
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-done")]
        [HttpPut]
        public GenericCommandResult MarkAsDone([FromServices] TodoHandler handler, [FromBody] MarkTodoAsDoneCommand command)
        {
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-unddone")]
        [HttpPut]
        public GenericCommandResult MarkAsUndone([FromServices] TodoHandler handler, [FromBody] MarkTodoAsUndoneCommand command)
        {
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAll([FromServices] ITodoRepository repository)
        {
            return repository.GetAll("fchiarotti");
        }

        [Route("done/{user}")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllDone([FromServices] ITodoRepository repository, string user)
        {
            return repository.GetAllDone("fchiarotti");
        }

        [Route("undone/{user}")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllUndone([FromServices] ITodoRepository repository, string user)
        {
            return repository.GetAllUndone("fchiarotti");
        }

        [Route("done/{date}/{user}")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllDoneByPeriod([FromServices] ITodoRepository repository, bool done, DateTime date, string user)
        {
            return repository.GetByPeriod(
                user,
                date,
                true
                );
        }

        [Route("undone/{date}/{user}")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllUndoneByPeriod([FromServices] ITodoRepository repository, bool done, DateTime date, string user)
        {
            return repository.GetByPeriod(
                user,
                date,
                false
                );
        }
    }
}