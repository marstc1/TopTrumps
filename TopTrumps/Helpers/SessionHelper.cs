// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionHelper.cs" company="KSS">
//   1997 - 2014
// </copyright>
// <summary>
//   Defines the SessionHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TopTrumps.Helpers
{
    using System.Web;

    using TopTrumps.Models.Domain;

    /// <summary>
    /// The SessionHelper
    /// </summary>
    public static class SessionHelper
    {       
        /// <summary>
        /// Saves the game data.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="gameData">The game data.</param>
        public static void SaveGameData(HttpContextBase httpContext, Game gameData)
        {
            if (httpContext.Session != null)
            {
                httpContext.Session["gameData"] = gameData;
            }
        }

        /// <summary>
        /// Gets the game data.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>
        /// The saved game data from session.
        /// </returns>
        public static Game GetGameData(HttpContextBase httpContext)
        {
            if (httpContext.Session == null)
            {
                return new Game();
            }

            var gameData = (Game)httpContext.Session["gameData"];

            return gameData ?? new Game();
        }
    }
}