﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SampleMembership.Models;

namespace SampleMembership.Controllers
{
    public class aspnet_MembershipController : Controller
    {
        private HHDBEntities db = new HHDBEntities();

        // GET: aspnet_Membership
        public ActionResult Index()
        {
            var aspnet_Membership = db.aspnet_Membership.ToList();
            return View(aspnet_Membership);
        }

        // GET: aspnet_Membership/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName");
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName");
			ViewBag.UserRoles = new SelectList(db.aspnet_Roles, "RoleId", "RoleName");
			return View();
        }

        // POST: aspnet_Membership/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Password,Email,IsLockedOut,Comment")] aspnet_Membership aspnet_Membership)
        {
            if (ModelState.IsValid)
            {
				var hubbardHouseApplication = db.aspnet_Applications.Where(a => a.ApplicationName == "Hubbard House Survey Analysis System").SingleOrDefault();
				MembershipUser user = Membership.CreateUser
				(
					aspnet_Membership.UserName,
					aspnet_Membership.Password					
				);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Membership.ApplicationId);
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Membership.ApplicationId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_Membership.UserId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_Membership.UserId);
            return View(aspnet_Membership);
        }

        // GET: aspnet_Membership/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnet_Membership aspnet_Membership = db.aspnet_Membership.Find(id);
			
			if (aspnet_Membership == null)
            {
                return HttpNotFound();
            }

			var user = db.aspnet_Users.Where(a => a.UserId == aspnet_Membership.UserId).FirstOrDefault();
			aspnet_Membership.UserName = user.UserName;

			ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Membership.ApplicationId);
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Membership.ApplicationId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_Membership.UserId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_Membership.UserId);
            return View(aspnet_Membership);
        }

        // POST: aspnet_Membership/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Password,PasswordFormat,PasswordSalt,CreateDate,ApplicationId,UserId,LastLoginDate,LoweredEmail,passwordChange,IsApproved,IsLockedOut,Comment,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart")] aspnet_Membership aspnet_Membership)
        {
            if (ModelState.IsValid)
            {

				if (!String.IsNullOrWhiteSpace(aspnet_Membership.passwordChange)) {
					var member = Membership.GetUser(aspnet_Membership.UserName.ToString());
					if (member != null)
					{
						
						var result = member.ChangePassword(member.ResetPassword(), aspnet_Membership.passwordChange);
						aspnet_Membership.LastPasswordChangedDate = DateTime.Now;
					}
					else
					{
						ModelState.AddModelError(string.Empty,"Error occured updating user password.");
					}
				}


                db.Entry(aspnet_Membership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Membership.ApplicationId);
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Membership.ApplicationId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_Membership.UserId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_Membership.UserId);
            return View(aspnet_Membership);
        }

        // GET: aspnet_Membership/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnet_Membership aspnet_Membership = db.aspnet_Membership.Find(id);
            if (aspnet_Membership == null)
            {
                return HttpNotFound();
            }
            return View(aspnet_Membership);
        }

        // POST: aspnet_Membership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            aspnet_Membership aspnet_Membership = db.aspnet_Membership.Find(id);
            db.aspnet_Membership.Remove(aspnet_Membership);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
