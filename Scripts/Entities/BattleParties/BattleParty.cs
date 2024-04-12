namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;

public partial class BattleParty : BattlePiece, IBattleParty
{
    public virtual string PartyName { get; set; }
    protected int _energyMax;
    public virtual int EnergyMax
    {
        get => _energyMax;
        set
        {
            if (value < 0) _energyMax = 0;
            else _energyMax = value;
            if (_energy < _energyMax) _energy = _energyMax;
        }
    }
    protected int _energy;
    public virtual int Energy
    {
        get => _energy;
        set
        {
            if (value < 0) _energy = 0;
            else if (value > _energyMax) _energy = _energyMax;
            else _energy = value;
        }
    }
    public BattleCards BattleCards { get; set; }
    public virtual int HandCardCount { get; set; }

    public static new BattleParty Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/battle_party.tscn").Instantiate<BattleParty>();

    public void Initialize(Player player)
    {
        PartyName = player.PlayerName;
        Level = player.Level;
        HealthMax = player.HealthMax;
        Health = player.Health;
        Shield = player.Shield;
        Agility = player.Agility;
        EnergyMax = player.EnergyMax;
        Energy = player.Energy;
        BattleCards = player.BattleCards;
    }

    public void Initialize(IBattleParty party)
    {
        PartyName = party.PartyName;
        Level = party.Level;
        HealthMax = party.HealthMax;
        Health = party.Health;
        Shield = party.Shield;
        Agility = party.Agility;
        EnergyMax = party.EnergyMax;
        Energy = party.Energy;
        BattleCards = party.BattleCards;
    }
}

public struct Exinke
{
    public readonly Grade 程序设计 => (Grade)(程序设计Progress / 100);
    public int 程序设计Progress;
    public readonly Grade 电子电路 => (Grade)(电子电路Progress / 100);
    public int 电子电路Progress;
    public readonly Grade 数据算法 => (Grade)(数据算法Progress / 100);
    public int 数据算法Progress;
    public readonly Grade 数字逻辑 => (Grade)(数字逻辑Progress / 100);
    public int 数字逻辑Progress;
    public readonly Grade 概率随机 => (Grade)(概率随机Progress / 100);
    public int 概率随机Progress;
    public readonly Grade 信号系统 => (Grade)(信号系统Progress / 100);
    public int 信号系统Progress;
    public readonly Grade 电磁场波 => (Grade)(电磁场波Progress / 100);
    public int 电磁场波Progress;
    public readonly Grade 通信网络 => (Grade)(通信网络Progress / 100);
    public int 通信网络Progress;
    public readonly Grade 媒体认知 => (Grade)(媒体认知Progress / 100);
    public int 媒体认知Progress;
    public readonly Grade 固体物理 => (Grade)(固体物理Progress / 100);
    public int 固体物理Progress;
}

public enum Grade
{
    F,
    D,
    DPlus,
    CMinus,
    C,
    CPlus,
    BMinus,
    B,
    BPlus,
    AMinus,
    A,
    APlus,
}
