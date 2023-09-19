using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "BuffSO")]
public class BuffSO : ScriptableObject
{
    public string buffName;
    public int duration;
    public GameObject prefab;
}
