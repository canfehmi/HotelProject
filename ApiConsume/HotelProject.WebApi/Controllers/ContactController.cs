﻿using HotelProject.BussinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        public IActionResult SendMessage(Contact contact)
        {
            contact.SendDate=Convert.ToDateTime(DateTime.Now.ToString());
            _contactService.TInsert(contact);
            return Ok();
        }
    }
}
