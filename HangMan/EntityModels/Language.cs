using System;
using System.Collections.Generic;

namespace HangMan.EntityModels
{
    public partial class Language
    {
        public Language()
        {
            Category = new HashSet<Category>();
            HangmanWord = new HashSet<HangmanWord>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Alphabet { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<HangmanWord> HangmanWord { get; set; }
    }
}
