﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAC_Management.DataBase;
using CAC_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAC_Management.Controllers
{
    public class TeacherController : Controller
    {
        private readonly DataContext db;
        public TeacherController(DataContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Teacher model)
        {
            var i = db.Teacher.Where(x => x.TeacherName == model.TeacherName && x.Password == model.Password).FirstOrDefault();
            if (i != null)
            {
                ViewBag.Login = "Succeecful";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Login = "User Name or Password Invalid!!!";
                return View();
            }
        }

        public IActionResult TeacherAdmission()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeacherAdmission(Teacher f)
        {
            if (ModelState.IsValid)
            {
                Teacher t = new Teacher();
                {
                    t.TeacherId = f.TeacherId;
                    t.TeacherName = f.TeacherName;
                    t.Contact = f.Contact;
                    t.Address = f.Address;
                    t.Email = f.Email;
                    t.Password = f.Password;
                    t.Salary = f.Salary;
                    t.PaidAmount = f.PaidAmount;
                    t.Payment_Status = t.Salary - t.PaidAmount;
                };
                db.Teacher.Add(t);
                db.SaveChanges();
                ViewBag.Success = "Teacher Account Created Successfully.";
                ModelState.Clear();
            }
            return View();
        }

        public IActionResult TeacherList()
        {
            var test = db.Teacher;
            var i = new List<Teacher>();
            foreach (var f in test)
            {
                Teacher t = new Teacher();
                t.TeacherId = f.TeacherId;
                t.TeacherName = f.TeacherName;
                t.Contact = f.Contact;
                t.Address = f.Address;
                t.Email = f.Email;
                t.Password = f.Password;
                t.Salary = f.Salary;
                t.PaidAmount = f.PaidAmount;
                t.Payment_Status = t.Salary - t.PaidAmount;
                i.Add(t);
            }
            return View(i);
        }

        public IActionResult Search(int Id)
        {
            var l = db.Teacher.Find(Id);
            return View(l);
        }

        public IActionResult TeacherSearch(int Id)
        {
            var l = db.Teacher.Find(Id);
            return View(l);
        }

        public IActionResult TeacherDetails(int Id)
        {
            var l = db.Teacher.Find(Id);
            return View(l);
        }
        public IActionResult EditTeacher(int Id)
        {
            var l = db.Teacher.Find(Id);
            return View(l);
        }
        [HttpPost]
        public IActionResult EditTeacher(Teacher f)
        {
            if (ModelState.IsValid)
            {
                Teacher t = new Teacher();
                t.TeacherId = f.TeacherId;
                t.TeacherName = f.TeacherName;
                t.Contact = f.Contact;
                t.Address = f.Address;
                t.Email = f.Email;
                t.Password = f.Password;
                t.Salary = f.Salary;
                t.PaidAmount = f.PaidAmount;
                t.Payment_Status = t.Salary - t.PaidAmount;
                db.Teacher.Update(t);
                db.SaveChanges();
            }
            return RedirectToAction("TeacherList");
        }

        public IActionResult DeleteTeacher(int Id)
        {
            var l = db.Teacher.Find(Id);
            db.Teacher.Remove(l);
            db.SaveChanges();
            return View();
        }
    }
}