using HangMan.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangMan.Services
{
    public interface IHangmanWordService
    {
        Task<List<HangmanWord>> GetWordsAsync(int categoryId);
        Task<List<Category>> GetCategoriesAsync(int languageId);
        Task<List<Language>> GetLanguagesAsync();
    }
}
