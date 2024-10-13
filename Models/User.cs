using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        List<Card> Stack { get; set; } = new List<Card>();

        List<Card> Deck { get; set; } = new List<Card>();

        public int Coins { get; set; } = 20;

        public int ELO { get; set; } = 100;

        public int GamesPlayed { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
