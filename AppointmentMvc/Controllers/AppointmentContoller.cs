using AppointmentMvc.Data;
using AppointmentMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentMvc.Controllers
{
    [Authorize]
    public class AppointmentContoller : Controller
    {
        private readonly ApplicationDbContext _context;
        public AppointmentContoller(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var appointments = _context.Appointments.ToList();
            return View(appointments);
        }
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Find(id);
            return View(appointment);
        }
        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
