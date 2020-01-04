using System.Collections.Generic;
using System.Threading.Tasks;
using HangMan.EntityModels;
using HangMan.Helpers;
using HangMan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HangMan.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(HangManDbContext context): base(context)
        {

        }
        [Route("api/[controller]/GetLanguages")]
        [HttpGet]
        public async Task<List<LanguageViewModel>> GetLanguagesAsync() => ObjectMapper.MapLanguageToVM(await _hangmanDataAccess.GetLanguagesAsync());

        [Route("api/[controller]/GetCategories")]
        [HttpGet]
        public async Task<List<CategoryViewModel>> GetCategoriesAsync(int id) => ObjectMapper.MapCategoryToVM(await _hangmanDataAccess.GetCategoriesAsync(id));
    }
}