using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FOODMATE.Pages;
using System.Diagnostics;

namespace FOODMATE
{
    public class _LayoutUserModel : PageModel
    {
        public string userName { get; set; }

        public void OnGet()
        {
        }
    }
}