using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FighterSO")]
public class FighterSO : ScriptableObject
{
    public float health;
    public float damage;
    public float armor;
    public GameObject prefab;
}
