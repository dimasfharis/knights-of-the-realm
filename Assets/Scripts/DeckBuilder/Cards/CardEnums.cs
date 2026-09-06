
namespace DeckBuilder.Cards
{
    public enum CardType
    {
        Attack,
        Skill,
        Power,
        Summon
    }

    public enum TargetType
    {
        None,
        SingleEnemy,
        AllEnemies,
        Self,
        SingleAlly,
        AllAllies
    }
}