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
        "res://Assets/Textures/PlayerHead.png",
        "res://Assets/Textures/PlayerHead2.png",
        "res://Assets/Textures/PlayerHead3.png",
        "res://Assets/Textures/PlayerHead4.png",
    };

    private List<string> _deadPartiesTexturePath = new List<string>
    {
        "res://Assets/Textures/PlayerHead.png",
        "res://Assets/Textures/PlayerHeadDead2.png",
        "res://Assets/Textures/PlayerHeadDead3.png",
        "res://Assets/Textures/PlayerHeadDead4.png",
    };

    public static StateBar Instance() => GD.Load<PackedScene>("res://Scenes/UI/state_bar.tscn").Instantiate<StateBar>();

    public override void _Ready()
    {
        _backgroundRect = GetNode<ColorRect>("%BackgroundRect");
        _playerHeadRect = GetNode<TextureRect>("%PlayerHeadRect");
        _healthBar = GetNode<TextureProgressBar>("%HealthBar");
        _energyBar = GetNode<TextureProgressBar>("%EnergyBar");

    }

    public void Initialize(BattleParty party, int rank)
    {
        if (party == null)
        {
            this.Visible = false;
            return;
        }
        this.Visible = true;
        _playerHeadRect.Texture.ResourcePath = _livePartiesTexturePath[rank];
        _healthBar.MaxValue = party.HealthMax;
        _healthBar.Value = party.HealthMax;
        _energyBar.MaxValue = party.EnergyMax;
        _energyBar.Value = party.EnergyMax;
    }

    public void Update(BattleParty party)
    {
        if(party == null)
        {
            this.Visible = false;
            return;
        }
        _healthBar.Value = party.Health;
        _energyBar.Value = party.Energy;
    }

    public void Death(BattleParty party, int rank)
    {
        if (party == null)
        {
            this.Visible = false;
            return;
        }
        _playerHeadRect.Texture.ResourcePath = _deadPartiesTexturePath[rank];
        _healthBar.Value = 0;
    }
}
