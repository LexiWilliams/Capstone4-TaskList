using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone4_TaskList.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone4_TaskList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskListDbContext _context;


        public TaskController(TaskListDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListTasks()
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            List<Tasks> taskList = _context.Tasks.Where(u => u.UserId == thisUser.Id).ToList();
            return View(taskList);
        }
        public IActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTask(Tasks newTask)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();

            if (ModelState.IsValid)
            {
                newTask.Completed = "False";
                newTask.UserId = thisUser.Id;
                newTask.UserEmail = thisUser.UserName;

                _context.Tasks.Add(newTask);
                _context.SaveChanges();
                return RedirectToAction("ListTasks");
            }
            return View("AddTask");
        }
        public IActionResult DeleteTask(Tasks task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("ListTasks");
        }
        public IActionResult CompleteTask(Tasks task)
        {
            task.Completed = "True";
            _context.Tasks.Update(task);
            _context.SaveChanges();
            return RedirectToAction("ListTasks");
        }
        public IActionResult FilterTask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FilterTask(List<Tasks> list)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            List<Tasks> taskList = _context.Tasks.Where(u => u.UserId == thisUser.Id).ToList();
            return View(); //redirecttoaction??
        }
        public IActionResult FilterKeyword(string searchString)
        {
            List<Tasks> filteredList = new List<Tasks>();
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            List<Tasks> taskList = _context.Tasks.Where(u => u.UserId == thisUser.Id).ToList();
            foreach(Tasks task in taskList)
            {
                if (task.Description.Contains(searchString))
                {
                    filteredList.Add(task);
                }
            }
            return View("ListTasks",filteredList);
        }
        public IActionResult FilterCompleted(string searchString)
        {
            List<Tasks> filteredList = new List<Tasks>();
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            List<Tasks> taskList = _context.Tasks.Where(u => u.UserId == thisUser.Id).ToList();
            foreach (Tasks task in taskList)
            {
                if (task.Completed == searchString)
                {
                    filteredList.Add(task);
                }
            }
            return View("ListTasks", filteredList);
        }
        public IActionResult FilterDate(DateTime searchDate)
        {
            List<Tasks> filteredList = new List<Tasks>();
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            List<Tasks> taskList = _context.Tasks.Where(u => u.UserId == thisUser.Id).ToList();
            foreach (Tasks task in taskList)
            {
                if (task.DueDate <= searchDate)
                {
                    filteredList.Add(task);
                }
            }
            return View("ListTasks", filteredList);
        }

    }
}