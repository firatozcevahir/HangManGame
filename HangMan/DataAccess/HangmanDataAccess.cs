using HangMan.EntityModels;
using HangMan.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace HangMan.DataAccess
{
    public class HangmanDataAccess : BaseDataAccess, IHangmanWordService
    {
        public HangmanDataAccess(HangManDbContext context) : base(context) { }

        public async Task<List<HangmanWord>> GetWordsAsync(int categoryId)
        {
            return await _context.HangmanWord
                .Where(w => w.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<HangmanWord> GetRandomWordAsync(int categoryId)
        {
            var result = await _context.HangmanWord.Where(w => w.CategoryId == categoryId).ToListAsync();
            var rnd = new Random();
            var randomIndex= rnd.Next(0, result.Count);
            return result[randomIndex];
        }

        public async Task<List<Category>> GetCategoriesAsync(int languageId)
        {
            return await _context.Category
                .Where(c => c.LanguageId == languageId)
                .ToListAsync();
        }

        public async Task<List<Language>> GetLanguagesAsync()
        {
            return await _context.Language
                .ToListAsync();
        }

    }
}
