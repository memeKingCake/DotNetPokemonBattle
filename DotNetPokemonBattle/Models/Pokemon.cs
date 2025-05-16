namespace DotNetPokemonBattle.Models
{
    public class Pokemon
    {
        public class Character
        {
            public string Name { get; set; }
            public int HP { get; set; }

            public int AttackPower { get; set; }

            public List<Move> Moves { get; set; }
        }

        public class Move
        {
            public string Name { get; set; }
            public int Damage { get; set; }
        }

        public class GameState
        {
            public Character Player { get; set; }
            public Character Enemy { get; set; }
            public bool IsPlayerTurn { get; set; }
        }
    }
}
