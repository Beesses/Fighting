using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "BarsSO")]
public class BarsSO : ScriptableObject
{
    public GameObject prefab;
    public int width;
    public int height;
    public Color mainColor;
    public Color backColor;
    public Bars type;
    public int textSize;
}
