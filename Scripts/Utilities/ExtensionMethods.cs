namespace EESaga.Scripts.Utilities;

using Entities.BattleParties;

public static class ExtensionMethods
{
    public static float ToGPA(this Grade grade) => grade switch
    {
        Grade.F => 0.0f,
        Grade.D => 1.3f,
        Grade.DPlus => 1.6f,
        Grade.CMinus => 2.0f,
        Grade.C => 2.3f,
        Grade.CPlus => 2.6f,
        Grade.BMinus => 3.0f,
        Grade.B => 3.3f,
        Grade.BPlus => 3.6f,
        Grade.AMinus => 4.0f,
        Grade.A => 4.0f,
        Grade.APlus => 4.0f,
        _ => 0.0f
    };
}
