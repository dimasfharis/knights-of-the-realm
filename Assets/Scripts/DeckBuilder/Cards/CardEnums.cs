
namespace DeckBuilder.Cards
{
    public enum CardName
    {
        None = 0,
        BullPush,
        CrossCut,
        HeavySmash,
        HiltStrike,
        InnerFocus,
        IronGuard,
        ParryingShield,
        ProtectiveWall,
        QuickSlash
    }

    public enum CardType
    {
        Attack,
        Defend,
        Skill,
        Summon
    }

    public enum TargetType
    {
        None = 0,
        SingleEnemy,
        AllEnemies,
        SingleAlly,
        AllAllies
    }
}