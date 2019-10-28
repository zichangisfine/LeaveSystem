using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 請假系統.Models;

namespace 請假系統.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBmanager dbmanager = new DBmanager();
            List<LeaveTable> leaveTables = dbmanager.GetLeaveTable();
            ViewBag.leaveTables = leaveTables;
            return View();
        }

        public ActionResult CreateLeave() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLeave(LeaveTable leave)
        {
            DBmanager dbmanager = new DBmanager();
            try
            {
                dbmanager.NewLeave(leave);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditLeave(int 序號)
        {
            DBmanager dbmanager = new DBmanager();
            LeaveTable leave = dbmanager.GetLeaveById(序號);
            return View(leave);
        }
        [HttpPost]
        public ActionResult EditLeave(LeaveTable leave)
        {
            DBmanager dbmanager = new DBmanager();
            try
            {
                dbmanager.UpdateLeave(leave);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View(leave);
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteLeave(int 序號)
        {
            DBmanager dbmanager = new DBmanager();
            dbmanager.DeleteLeaveById(序號);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SearchLeave()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SearchLeaveView(string 員編)
        {
            DBmanager dbmanager = new DBmanager();
            List<LeaveTable> leaveTables = dbmanager.SearchLeaveById(員編);
            ViewBag.leaveTables = leaveTables;
            return View(leaveTables);
        }


    }
}