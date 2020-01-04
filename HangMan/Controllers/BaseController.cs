using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangMan.DataAccess;
using HangMan.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangMan.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        internal HangmanDataAccess _hangmanDataAccess;
        public BaseController(HangManDbContext context)
        {
            _hangmanDataAccess = new HangmanDataAccess(context);
        }
    }
}