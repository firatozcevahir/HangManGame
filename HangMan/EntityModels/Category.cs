using System;
using System.Collections.Generic;

namespace HangMan.EntityModels
{
    public partial class Category
    {
        public Category()
        {
            HangmanWord = new HashSet<HangmanWord>();
        }

        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Language Language { get; set; }
        public virtual ICollection<HangmanWord> HangmanWord { get; set; }
    }
}
