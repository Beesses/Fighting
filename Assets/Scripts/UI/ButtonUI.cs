using UnityEngine;
using UnityEngine.UI;

public class ButtonUI
{
    private GameObject prefab;
    private int width;
    private int height;
    private Text text;
    private Image image;
    public Button button;
    public ButtonUI(ButtonSO data, Transform parent, Vector2 pos)
    {
        prefab = GameObject.Instantiate(data.prefab, parent);
        prefab.transform.localPosition = pos;
        height = data.height;
        width = data.width;
        image = prefab.GetComponent<Image>();
        image.type = Image.Type.Sliced;
        image.sprite = data.sprite;
        image.rectTransform.sizeDelta = new Vector2(width, height);
        text = prefab.transform.GetChild(0).GetComponent<Text>();
        text.text = data.btnText;
        text.fontSize = data.textSize;
        button = prefab.GetComponent<Button>();
    }
}
