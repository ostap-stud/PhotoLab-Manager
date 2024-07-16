using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoLab.Data;
using PhotoLab.Interfaces;
using PhotoLab.Models;
using System.Diagnostics;

namespace PhotoLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderRepository _repository;

        public HomeController(ILogger<HomeController> logger, IOrderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Order> orders = await _repository.GetAll();
            var workerOrders = orders.Count(order => order.Worker.Equals(User.Identity.Name));
            var successOrders = orders.Count(order => order.Worker.Equals(User.Identity.Name) && order.Status.Equals(Status.Success));
            ViewData["AllOrders"] = workerOrders;
            ViewData["SuccessOrders"] = successOrders;
            return orders != null ?
                        View(orders) :
                        Problem("Entity set 'PhotoLabContext.Orders'  is null.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}