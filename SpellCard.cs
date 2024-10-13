using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MTCG
{
    public class SpellCard : Card
    {
        public string SpellEffect { get; set; }
        public SpellCard(string name, ElementType element, string spellEffect) : base(name, element)
        {
            if (string.IsNullOrWhiteSpace(spellEffect))
            {
                throw new ArgumentNullException("SpellEffect cannot be null or empty!");
            }
            SpellEffect = spellEffect;
        }

        public override string ToString()
        {
            return $"{Name} - {Element} - {SpellEffect} - {AddedDate}";
        }
    }
}
