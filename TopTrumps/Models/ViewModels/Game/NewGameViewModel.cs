namespace TopTrumps.Models.ViewModels.Game
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The New Game View Model
    /// </summary>
    public class NewGameViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string PlayerName { get; set; }

        public int PackId { get; set; }
    }
}