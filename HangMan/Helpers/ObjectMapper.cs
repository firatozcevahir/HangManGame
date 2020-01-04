using HangMan.EntityModels;
using HangMan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangMan.Helpers
{
    public static class ObjectMapper
    {
        public static List<HangmanWordViewModel> MapHangmanWordToVM(List<HangmanWord> hangmanWords)
        {
            var list = new List<HangmanWordViewModel>();

            foreach (var hangmanWord in hangmanWords)
            {
                list.Add(new HangmanWordViewModel()
                {
                    Id = hangmanWord.Id,
                    Word = hangmanWord.Word
                });
            }
            return list;
        }

        public static HangmanWordViewModel MapHangmanWordToVM(HangmanWord hangmanWord)
        {
            return new HangmanWordViewModel()
            {
                Id = hangmanWord.Id,
                Word = hangmanWord.Word
            };
        }

        public static List<LanguageViewModel> MapLanguageToVM(List<Language> languages)
        {
            var list = new List<LanguageViewModel>();

            foreach (var language in languages)
            {
                list.Add(new LanguageViewModel()
                {
                    Id = language.Id,
                    Name = language.Name,
                    Code = language.Code,
                    Alphabet = language.Alphabet
                });
            }

            return list;
        }

        public static List<CategoryViewModel> MapCategoryToVM(List<Category> categories)
        {
            var list = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                list.Add(new CategoryViewModel()
                {
                    Id = category.Id,
                    Title = category.Title,
                    Description = category.Description
                });
            }
            return list;
        }
    }
}
