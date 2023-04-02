﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hospital_project.Models;

namespace hospital_project.Controllers
{
    public class PhysicianDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PhysicianData/ListPhysicians
        [HttpGet]
        public IQueryable<Physician> ListPhysicians()
        {
            return db.Physicians;
        }

        // GET: api/PhysicianData/FindPhysician/5
        [ResponseType(typeof(Physician))]
        [HttpGet]
        public IHttpActionResult FindPhysician(int id)
        {
            Physician physician = db.Physicians.Find(id);
            if (physician == null)
            {
                return NotFound();
            }

            return Ok(physician);
        }

        // POST: api/PhysicianData/UpdatePhysician/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePhysician(int id, Physician physician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != physician.physician_id)
            {
                return BadRequest();
            }

            db.Entry(physician).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicianExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PhysicianData/AddPhysician/5
        [ResponseType(typeof(Physician))]
        [HttpPost]
        public IHttpActionResult AddPhysician(Physician physician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Physicians.Add(physician);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = physician.physician_id }, physician);
        }

        // POST: api/PhysicianData/DeletePhysician/5
        [ResponseType(typeof(Physician))]
        [HttpPost]
        public IHttpActionResult DeletePhysician(int id)
        {
            Physician physician = db.Physicians.Find(id);
            if (physician == null)
            {
                return NotFound();
            }

            db.Physicians.Remove(physician);
            db.SaveChanges();

            return Ok(physician);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhysicianExists(int id)
        {
            return db.Physicians.Count(e => e.physician_id == id) > 0;
        }
    }
}