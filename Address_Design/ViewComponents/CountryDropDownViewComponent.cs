using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Address_Design.Models;


namespace Address_Design.ViewComponents
{
    public class CountryDropDownViewComponent : ViewComponent
    {

        public CountryDropDownViewComponent()
        {
           
        }

        public async Task<IViewComponentResult> InvokeAsync(AllForms inject)
        {
            ViewBag.cachedForms = inject;
            return View();
        }

    }
}