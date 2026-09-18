using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Game/Stats/PlayerStats")]
public class Stats : ScriptableObject
{
    public float HP;
    public float Attack;
    public float Defense;
    public float Speed;
    public float AttackRange;
    public float AttackSpeed;
    public float Regeneration;
}
