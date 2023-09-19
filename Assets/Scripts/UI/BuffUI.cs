using UnityEngine;
using UnityEngine.UI;

public class BuffUI
{
    public int timer;
    private string buffName;
    private GameObject prefab;
    private Text text;
    public BuffUI(BuffSO data, Transform parent)
    {
        timer = data.duration;
        buffName = data.name;
        prefab = GameObject.Instantiate(data.prefab, parent);
        text = prefab.GetComponent<Text>();
        text.text = buffName + " " + timer;
    }

    public void nextRound()
    {
        if (timer >= 2)
        {
            timer -= 1;
            text.text = buffName + " " + timer;
        }
        else
        {
            timer = 0;
            Object.Destroy(prefab);
        }
    }
}
