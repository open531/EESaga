namespace EESaga.Scripts.Entities.BattleEnemies;

using EESaga.Scripts.Entities.BattleParties;
using Godot;
using System.Collections.Generic;

public partial class SiliconTurtle : BattleEnemy
{
    [Signal] public delegate void SiliconTurtleActionEventHandler(string actionInfo, string actionEffect, bool refreshLastLabel);
    public static new SiliconTurtle Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleEnemies/silicon_turtle.tscn").Instantiate<SiliconTurtle>();

    public override void _Ready()
    {
        base._Ready();
        PieceName = "E_MONOCRYSTURTLE";
        HealthMax = 60;
        Health = HealthMax;
        Agility = 1;
        MoveRange = 3;
        AttackTimes = 1;
        AttackDamage = 40;
        Shield = 0;
    }

    public override void Attack(BattleParty battleParty)
    {
        var damageList = new List<List<int>>();
        var damageInfo = new string("");
        var actionInfo = new string($"{Tr("T_USE")} {Tr("T_ATTACK")}");
        var deathInfo = new bool();
        for (var i = 0; i < AttackTimes; i++)
        {
            damageList.Add(battleParty.BeAttacked(AttackDamage));
        }
        deathInfo = battleParty.CheckDeath();
        foreach (var damage in damageList)
        {
            damageInfo += damage[0] == 0 ? "" : $"{battleParty.PieceName} {Tr("PIECE_SHIELD")} {Tr("T_LOST")} {damage[0]}\n";
            damageInfo += damage[1] == 0 ? "" : $"{Tr(battleParty.PieceName)} {Tr("PIECE_HEALTH")} {Tr("T_REDUCE")} {damage[1]}\n";
            damageInfo += damage[1] == 0 && damage[0] == 0 ? $"{Tr("T_NO_EFFECT")}\n" : $"{battleParty.PieceName} {Tr("PIECE_HEALTH")} : {damage[2]}\n";
        }
        if (deathInfo)
        {
            damageInfo += $"{battleParty.PieceName} {Tr("T_DECEASED")}\n";
        }
        GD.Print($"{PieceName}攻击了{battleParty.PieceName}");
        EmitSignal(SignalName.SiliconTurtleAction, actionInfo, damageInfo, true);
    }

    public override void Defend(BattleEnemy battleEnemy)
    {
        battleEnemy.Shield += 3;
    }
}
