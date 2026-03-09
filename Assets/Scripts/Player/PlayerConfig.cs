using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs / Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field:SerializeField] public  float Speed { get; private set; } = 0.1f;
        [field:SerializeField] public  float AttackDamage { get; private set; } = 20.0f;
        [field:SerializeField] public  float AttackSpeed { get; private set; } = 20.0f;
        [field:SerializeField] public  float AttackRadius { get; private set; } = 5.0f;
    }
}
