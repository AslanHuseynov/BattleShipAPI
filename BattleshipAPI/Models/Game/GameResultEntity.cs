namespace BattleshipAPI.Models.Game
{
    public class GameResultEntity
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string Result { get; set; }
        public int NumberOfMoves { get; set; }
    }
}
