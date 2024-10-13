using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MTCG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeckController : ControllerBase
    {
        private static List<User> Users = UserController.Users;

        [HttpPost("create")]
        public IActionResult CreateDeck([FromBody] List<Card> cards)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault();
            
            if (token == null)
            {
                return Unauthorized("Token is missing");
            }

            // Überprüfen, ob der Token gültig ist (Benutzer eingeloggt)
            var user = Users.FirstOrDefault(u => $"{u.Username}-{u.Token}" == token);
            if (user == null)
            {
                return Unauthorized("Invalid token");
            }

            if (cards.Count != 4)
            {
                return BadRequest("A deck must contain exactly 4 cards.");
            }

            user.Deck = new Deck { SelectedCards = cards };

            //Token gültig
            return Ok("Deck created successfully!");
        }

        [HttpGet("view")]
        public IActionResult ViewDeck()
        {
            // Token-Überprüfung und Deck-Anzeige
            // Token aus dem Header extrahieren
            var token = Request.Headers["Authorization"].FirstOrDefault();
            
            if (token == null)
            {
                return Unauthorized("Token is missing");
            }

            // Überprüfen, ob der Token gültig ist (Benutzer eingeloggt)
            var user = Users.FirstOrDefault(u => $"{u.Username}-{u.Token}" == token);
            if (user == null)
            {
                return Unauthorized("Invalid token");
            }

            // Überprüfen, ob der Benutzer ein Deck hat
            if (user.Deck == null)
            {
                return NotFound("No deck found.");
            }

            // Deck zurückgeben
            return Ok(user.Deck);
        }
    }
}