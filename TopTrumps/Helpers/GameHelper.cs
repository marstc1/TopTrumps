
namespace TopTrumps.Helpers
{
    using System.Web;
    using TopTrumps.Models.Domain;

    public class GameHelper
    {
        private HttpContextBase httpContext;
        
        public GameHelper(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }
        
        /// <summary>
        /// Saves the game details.
        /// </summary>
        /// <param name="gameDetails">The game details.</param>
        public void SaveGameDetails(GameDetails gameDetails)
        {
            if (httpContext.Session != null)
            {
                httpContext.Session["gameDetails"] = gameDetails;
            }
        }

        /// <summary>
        /// Gets the game details.
        /// </summary>
        /// <returns>All previously stored Game Details</returns>
        public GameDetails GetGameDetails()
        {
            if (httpContext.Session != null && httpContext.Session["gameDetails"] != null)
            {
                return (GameDetails)httpContext.Session["gameDetails"];
            }

            return new GameDetails();
        }
    }
}