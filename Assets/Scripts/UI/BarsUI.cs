using UnityEngine;
using UnityEngine.UI;

public class BarsUI
{
    private GameObject prefab;
    private Image backImage;
    private int width;
    private int height;
    public bool isF1;
    public Image mainImage;
    public Text text;
    public Bars type;
    public BarsUI(BarsSO data, Transform parent, bool isFighter1)
    {
        prefab = GameObject.Instantiate(data.prefab, parent);
        height = data.height;
        width = data.width;
        backImage = prefab.GetComponent<Image>();
        backImage.rectTransform.sizeDelta = new Vector2(width, height);
        backImage.color = data.backColor;
        mainImage = prefab.transform.GetChild(0).GetComponent<Image>();
        mainImage.rectTransform.sizeDelta = new Vector2(width, height);
        mainImage.color = data.mainColor;
        text = prefab.transform.GetChild(1).GetComponent<Text>();
        text.rectTransform.sizeDelta = new Vector2(width, height);
        text.fontSize = data.textSize;
        type = data.type;
        isF1 = isFighter1;
    }

    public void UpdateBar(float value)
    {
        if(value == 0)
        {
            mainImage.rectTransform.sizeDelta = new Vector2(0, height);
        }
        else
        {
            mainImage.rectTransform.sizeDelta = new Vector2(width * (value / 100), height);
        }
    }
}
