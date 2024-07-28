using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoListMvcApp.Models;

namespace TodoListMvcApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly HttpClient _httpClient;

        public TodoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Todo> tasks;
            try
            {
                tasks = await _httpClient.GetFromJsonAsync<List<Todo>>("http://127.0.0.1:5001/tasks");
            }
            catch (HttpRequestException e)
            {
                tasks = new List<Todo>();
                ModelState.AddModelError(string.Empty, "Error fetching tasks. Please try again later.");
            }
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return View(todo);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://127.0.0.1:5001/tasks", todo);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                ModelState.AddModelError(string.Empty, "Error creating task. Please try again later.");
                return View(todo);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://127.0.0.1:5001/tasks/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                ModelState.AddModelError(string.Empty, "Error deleting task. Please try again later.");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
