using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class Package
    {
        public List<Card> PackageCards { get; set; }
        public Package()
        {
            PackageCards = new List<Card>();
        }
    }
}
