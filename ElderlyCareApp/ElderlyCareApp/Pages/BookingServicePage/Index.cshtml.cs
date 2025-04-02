using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Extension;
using BLL.Interfaces;
using BLL.DTO.BookingDTOs;

namespace ElderlyCareApp.Pages.BookingServicePage
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public IndexModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IList<BookingDTO> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Booking = await _bookingService.GetAllBookingsAsync();
        }
    }
}
