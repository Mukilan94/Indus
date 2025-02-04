﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using Well_AI.Helpdesk.Models.ManageViewModels;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{organizationId}")]
        public IActionResult GetCustomer([FromRoute]Guid organizationId)
        {
            return Json(new
            {
                data = _context.CorporateProfile
                              .Select(p => new CorporateProfile()
                              {
                                  name = p.name,
                                  address1 = p.address1,
                                  address2 = p.address2,
                                  city = p.city,
                                  state = p.state,
                                  zip = p.zip,
                                  customerId = p.customerId

                              }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (customer.customerId == Guid.Empty)
                {
                    customer.customerId = Guid.NewGuid();
                    _context.Customer.Add(customer);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Add new data success." });
                }
                else
                {
                    _context.Update(customer);

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
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customer = await _context.Customer.SingleOrDefaultAsync(m => m.customerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Delete success." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customer.Any(e => e.customerId == id);
        }
    }
}