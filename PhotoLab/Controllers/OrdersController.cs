using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoLab.Data;
using PhotoLab.Interfaces;
using PhotoLab.Models;

namespace PhotoLab.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return _repository != null ?
                          View(await _repository.GetAll()) :
                          Problem("Entity set 'PhotoLabContext.Orders'  is null.");
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _repository == null)
            {
                return NotFound();
            }

            var order = await _repository.GetById(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Service> services = await _repository.GetServicesList();
            ViewData["ServiceId"] = new SelectList(services, "ServiceId", "ServiceName");
            ViewData["ServicePrice"] = new SelectList(services, "ServiceId", "Price");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Client_Name,Client_Surname,Status,Date,Worker,ServiceId,Count")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Worker = User.Identity.Name;
                order.Service = await _repository.GetSelectedService(order.ServiceId);
                order.Total = order.Service.Price * order.Count;
                await _repository.Add(order);
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<Service> services = await _repository.GetServicesList();
            ViewData["ServiceId"] = new SelectList(services, "ServiceId", "ServiceName", order.ServiceId);
            ViewData["ServicePrice"] = new SelectList(services, "ServiceId", "Price", order.ServiceId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _repository == null)
            {
                return NotFound();
            }

            var order = await _repository.GetById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            IEnumerable<Service> services = await _repository.GetServicesList();
            ViewData["ServiceId"] = new SelectList(services, "ServiceId", "ServiceName", order.ServiceId);
            ViewData["ServicePrice"] = new SelectList(services, "ServiceId", "Price", order.ServiceId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Client_Name,Client_Surname,Status,Date,Worker,ServiceId,Count")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.Worker = User.Identity.Name;
                    order.Service = await _repository.GetSelectedService(order.ServiceId);
                    order.Total = order.Service.Price * order.Count;
                    await _repository.Update(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<Service> services = await _repository.GetServicesList();
            ViewData["ServiceId"] = new SelectList(services, "ServiceId", "ServiceName", order.ServiceId);
            ViewData["ServicePrice"] = new SelectList(services, "ServiceId", "Price", order.ServiceId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _repository == null)
            {
                return NotFound();
            }

            var order = await _repository.GetById(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_repository == null)
            {
                return Problem("Entity set 'PhotoLabContext.Orders'  is null.");
            }
            var order = await _repository.GetById(id);
            if (order != null)
            {
                await _repository.Delete(order);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_repository.GetAll().Result.Any(e => e.OrderId == id));
        }
    }
}
