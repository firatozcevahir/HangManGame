using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangMan.DataAccess;
using HangMan.EntityModels;
using HangMan.Helpers;
using HangMan.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangMan.Controllers
{
    public class GameController : BaseController
    {
        public GameController(HangManDbContext context) : base(context) { }

        [Route("api/[controller]/GetWords")]
        [HttpGet]
        public async Task<List<HangmanWordViewModel>> GetWordsAsync(int categoryId)
        {
            return ObjectMapper.MapHangmanWordToVM(await _hangmanDataAccess.GetWordsAsync(categoryId));
        }

        [Route("api/[controller]/GetRandomWord")]
        [HttpGet]
        public async Task<HangmanWordViewModel> GetRandomWordAsync(int categoryId)
        {
            return ObjectMapper.MapHangmanWordToVM(await _hangmanDataAccess.GetRandomWordAsync(categoryId));
        }
    }
}