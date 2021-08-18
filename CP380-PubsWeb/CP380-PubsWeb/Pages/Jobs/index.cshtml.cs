using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CP380_PubsLab.Models;

namespace CP380_PubsWeb.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly PubsDbContext _db;

        public IndexModel(PubsDbContext db)
        {
            _db = db;
        }

        public List<CP380_PubsLab.Models.Jobs> Jobs { get; set; }
        public void OnGet()
        {
            this.Jobs = _db.Jobs.ToList();
        }
    }
}