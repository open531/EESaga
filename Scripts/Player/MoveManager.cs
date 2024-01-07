namespace EESaga.Scripts.Player;

using Godot;

public class MoveManager
{
    public Vector2 MoveFromInput()
    {
        var velocity = Vector2.Zero;
        #region Keyboard Input

        velocity.Y -= Input.IsPhysicalKeyPressed(Key.W) ? 1 : 0;
        velocity.Y += Input.IsPhysicalKeyPressed(Key.S) ? 1 : 0;
        velocity.X -= Input.IsPhysicalKeyPressed(Key.A) ? 1 : 0;
        velocity.X += Input.IsPhysicalKeyPressed(Key.D) ? 1 : 0;
        velocity.Y -= Input.IsPhysicalKeyPressed(Key.Up) ? 1 : 0;
        velocity.Y += Input.IsPhysicalKeyPressed(Key.Down) ? 1 : 0;
        velocity.X -= Input.IsPhysicalKeyPressed(Key.Left) ? 1 : 0;
        velocity.X += Input.IsPhysicalKeyPressed(Key.Right) ? 1 : 0;

        #endregion
        #region Gamepad Input

        var joyAxisLeftX = Input.GetJoyAxis(0, JoyAxis.LeftX);
        var joyAxisLeftY = Input.GetJoyAxis(0, JoyAxis.LeftY);

        velocity.X += double.Abs(joyAxisLeftX) > 0.1f ? joyAxisLeftX : 0;
        velocity.Y += double.Abs(joyAxisLeftY) > 0.1f ? joyAxisLeftY : 0;

        #endregion

        if (velocity.Length() > 1)
        {
            velocity = velocity.Normalized();
        }

        return velocity;
    }
}
