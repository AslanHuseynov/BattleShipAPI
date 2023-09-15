namespace BattleshipAPI.Models.Game
{
    public class BattleshipGame
    {
        //public List<Cell> GameBoard { get; set; }
        //public List<Ship> Ships { get; set; }
        public int MovesLeft { get; set; }
        public int BoardSize { get; set; }
        public DateTime StartTime { get; set; }
        public string Result { get; set; }
        public int NumberOfMoves { get; set; }
    }
}
