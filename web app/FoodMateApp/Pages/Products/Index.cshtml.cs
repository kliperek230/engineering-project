using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodMateApp.Models;

namespace FoodMateApp
{
    public class IndexModel : PageModel
    {
        private readonly FoodMateApp.Models.FOODMATEContext _context;

        public IndexModel(FoodMateApp.Models.FOODMATEContext context)
        {
            _context = context;
        }

        public IList<Products> Products { get;set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }
    }
}
