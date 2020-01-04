using System;
using System.Collections.Generic;

namespace HangMan.EntityModels
{
    public partial class HangmanWord
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string Word { get; set; }
        public string Description { get; set; }

        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
    }
}
