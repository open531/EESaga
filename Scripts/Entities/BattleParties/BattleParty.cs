namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;
using Utilities;

public partial class BattleParty : Area2D, IBattleParty
{
    public string PartyName { get; set; }
    public int Level { get; set; }
    public RangedInt Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
    public RangedInt Energy { get; set; }
    public BattleCards BattleCards { get; set; }
    protected AnimatedSprite2D _sprite;
    protected CollisionShape2D _collision;
    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");
    }
    public void Initialize(Player player)
    {
        PartyName = player.PlayerName;
        Level = player.Level;
        Health = player.Health;
        Shield = player.Shield;
        Agility = player.Agility;
        Energy = player.Energy;
        BattleCards = player.BattleCards;
    }
    public void Initialize(IBattleParty party)
    {
        PartyName = party.PartyName;
        Level = party.Level;
        Health = party.Health;
        Shield = party.Shield;
        Agility = party.Agility;
        Energy = party.Energy;
        BattleCards = party.BattleCards;
    }

}
