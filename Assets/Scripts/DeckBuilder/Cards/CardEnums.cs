
namespace DeckBuilder.Cards
{
    public enum CardType
    {
        Attack,
        Defend,
        Skill,
        Summon
    }

    public enum TargetType
    {
        None,
        SingleEnemy,
        AllEnemies,
        SingleAlly,
        AllAllies
    }
}