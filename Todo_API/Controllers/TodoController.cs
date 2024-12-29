using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using Todo_API.AppData;
using Todo_API.Models;

namespace Todo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {

        private readonly TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new ApiResponse<List<TodoItem>>();
            try
            {
                var list = await _context.TodoItems.ToListAsync();
                response.Data = list;
                response.Message = "Success";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(TodoItem item)
        {
            var response = new ApiResponse<List<TodoItem>>();
            if (!ModelState.IsValid)
            {
                response.Message = "Invalid item";
                return BadRequest(ModelState);
            }

            if (item.Title == "")
            {
                response.Message = "Title cannot be empty";
                return BadRequest(response);
            }

            try {
                await _context.TodoItems.AddAsync(item);
                await _context.SaveChangesAsync();
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, TodoItem item)
        {
            var response = new ApiResponse<List<TodoItem>>();
            if (Id != item.Id)
            {
                response.Message = "Id Missmatch";
                return BadRequest(response);
            }

            if (!ModelState.IsValid)
            {
                response.Message = "Invalid item";
                return BadRequest(response);
            }

            try
            {
                _context.TodoItems.Update(item);
                await _context.SaveChangesAsync();
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = new ApiResponse<List<TodoItem>>();
            try
            {
                var item = await _context.TodoItems.FindAsync(Id);
                if (item == null)
                {
                    response.Message = "Item not found";
                    return NotFound(response);
                }

                _context.TodoItems.Remove(item);

                await _context.SaveChangesAsync();
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(500, response);
            }
        }


    }
}
