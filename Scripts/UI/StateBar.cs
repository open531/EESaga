using EESaga.Scripts.Entities.BattleParties;
using Godot;
using System;
using System.Collections.Generic;

public partial class StateBar : Node2D
{
    private ColorRect _backgroundRect;
    private TextureRect _playerHeadRect;
    private TextureProgressBar _healthBar;
    private TextureProgressBar _energyBar;

    private List<string> _livePartiesTexturePath = new List<string>
    {
        "res://Assets/Textures/player_head.png",
        "res://Assets/Textures/wizard_head.png",
        "res://Assets/Textures/knight_head.png",
        "res://Assets/Textures/priest_head.png",
    };

    public static StateBar Instance() => GD.Load<PackedScene>("res://Scenes/UI/state_bar.tscn").Instantiate<StateBar>();

    public override void _Ready()
    {
        _backgroundRect = GetNode<ColorRect>("%BackgroundRect");
        _playerHeadRect = GetNode<TextureRect>("%PlayerHeadRect");
        _healthBar = GetNode<TextureProgressBar>("%HealthBar");
        _energyBar = GetNode<TextureProgressBar>("%EnergyBar");

    }

    public void Initialize(BattleParty party,int rank)
    {
        if (party == null)
        {
            this.Visible = false;
            return;
        }
        this.Visible = true;
        Texture2D texture = GD.Load<Texture2D>(_livePartiesTexturePath[rank]);
        _playerHeadRect.Texture = texture;
        _healthBar.MaxValue = 100;
        _healthBar.Value = GetValue(party.HealthMax, party.Health);
        _energyBar.MaxValue = 100;
        _energyBar.Value = GetValue(party.EnergyMax, party.Energy);
    }

    public void UpdateStateBar(BattleParty party)
    {
        if (party == null)
        {
            this.Visible = false;
            return;
        }
        _healthBar.Value = GetValue(party.HealthMax, party.Health);
        _energyBar.Value = GetValue(party.EnergyMax, party.Energy);
    }

    public void Death(BattleParty party, int rank)
    {
        if (party == null)
        {
            this.Visible = false;
            return;
        }
        _healthBar.Value = GetValue(party.HealthMax, 0);
    }

    private float GetValue(int max, int value)
    {
        var floatValue = (float)value;
        return floatValue / max * 80 + 20;
    }
}
