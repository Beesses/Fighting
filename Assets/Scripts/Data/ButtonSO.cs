using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ButtonSO")]
public class ButtonSO : ScriptableObject
{
    public GameObject prefab;
    public int width;
    public int height;
    public string btnText;
    public int textSize;
    public Sprite sprite;
}
