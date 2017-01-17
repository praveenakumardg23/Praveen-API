using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CRUDApi.Controllers
{
    public class LoginController : ApiController
    {
        private angular2Entities4 db = new angular2Entities4();

        // GET: api/login
        [HttpGet]
        [ResponseType(typeof(login))]
        public IHttpActionResult Authenticate(string userid, string password)
        {
            foreach (var login in db.logins)
            {
                if (login.userid == userid && login.password == password)
                {
                    return Ok(login);
                }
            }
            return NotFound();
        }

        // POST: api/Login
        [ResponseType(typeof(login))]
        public IHttpActionResult PostLogin(login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.logins.Add(login);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = login.userid }, login);
        }
    }
}
