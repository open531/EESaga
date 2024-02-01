namespace EESaga.Scripts.Managers;

using Godot;

public partial class RoomManager : Node
{
    [ExportGroup("Rooms", "Room")]
    [Export] public int RoomNull { get; set; } = 0;
    [Export] public int RoomBattle { get; set; } = 5;
    [Export] public int RoomChest { get; set; } = 1;
    [Export] public int RoomShop { get; set; } = 1;
    [Export] public int RoomRest { get; set; } = 1;
    [Export] public int RoomBoss { get; set; } = 1;
    public int RoomCount => RoomNull + RoomBattle + RoomChest + RoomShop + RoomRest + RoomBoss;
    private static readonly PackedScene _roomScene = GD.Load<PackedScene>("res://Scenes/Maps/room.tscn");
}
