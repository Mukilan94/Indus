using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using Well_AI.Helpdesk.Models;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Ticket")]
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{organizationId}")]
        public IActionResult GetTicket([FromRoute]Guid organizationId)
        {
            IList<hdIssues> hdIssues = _context.hdIssues.OrderByDescending(p => p.issueID).ToList();
            IList<hdStatus> hdStatus = _context.hdStatus.ToList();

            IList<ApplicationUser> userList = _context.ApplicationUser.ToList();


            var list = (from issues in hdIssues
                        join status in hdStatus on issues.StatusID equals status.StatusID
                        select new
                        {
                            subject = issues.subject,
                            body = issues.body,
                            status = status.status,
                            issueID = issues.issueID,
                        }).ToList();


            return Json(new { data = list });
        }

        [HttpGet("Customer/{customerId}")]
        public IActionResult GetTicketCustomer([FromRoute]Guid customerId)
        {
            IList<hdIssues> hdIssues = _context.hdIssues.Where(x => x.TenantId.Equals(Convert.ToString(customerId))).OrderByDescending(p => p.issueID).ToList();
            IList<hdStatus> hdStatus = _context.hdStatus.ToList();
            
            IList<ApplicationUser> userList = _context.ApplicationUser.ToList();


            var list = (from issues in hdIssues
                        join status in hdStatus on issues.StatusID equals status.StatusID
                        select new
                        {
                            subject = issues.subject,
                            body = issues.body,
                            status = status.status,
                           issueID = issues.issueID,
                        }).ToList();


            return Json(new { data = list });
        }

        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (ticket.ticketId == Guid.Empty)
                {
                    Contact contact = _context.Contact.Where(x => x.contactId.Equals(ticket.contactId)).FirstOrDefault();
                    ticket.ticketId = Guid.NewGuid();
                    ticket.customerId = contact.customerId;
                    _context.Ticket.Add(ticket);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Add new data success." });
                }
                else
                {
                    _context.Update(ticket);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        [HttpPost("Customer")]
        public async Task<IActionResult> PostTicketCustomer([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (ticket.ticketId == Guid.Empty)
                {
                    ticket.ticketId = Guid.NewGuid();
                    _context.Ticket.Add(ticket);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Add new data success." });
                }
                else
                {
                    _context.Update(ticket);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.ticketId == id);
                if (ticket == null)
                {
                    return NotFound();
                }

                _context.Ticket.Remove(ticket);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Delete success." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        [HttpDelete("Customer/{id}")]
        public async Task<IActionResult> DeleteTicketCustomer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.ticketId == id);
                if (ticket == null)
                {
                    return NotFound();
                }

                _context.Ticket.Remove(ticket);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Delete success." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        private bool TicketExists(Guid id)
        {
            return _context.Ticket.Any(e => e.ticketId == id);
        }
    }
}