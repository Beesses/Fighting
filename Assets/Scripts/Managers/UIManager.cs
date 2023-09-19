using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Buffs1Parent;
    [SerializeField]
    private GameObject Buffs2Parent;
    [SerializeField]
    private GameObject Bars1Parent;
    [SerializeField]
    private GameObject Bars2Parent;
    [SerializeField]
    private GameObject Buttons1Parent;
    [SerializeField]
    private GameObject Buttons2Parent;
    [SerializeField]
    private GameObject RestartParent;
    [SerializeField]
    private GameObject TextParent;
    [SerializeField]
    private Vector2[] BtnPos;

    private int roundCount = 1;
    private List<BuffUI> buffs = new List<BuffUI>();
    private List<BarsUI> barsList = new List<BarsUI>();

    public ButtonUI[] AttackButtons = new ButtonUI[2];
    public ButtonUI[] BuffButtons = new ButtonUI[2];
    public ButtonUI Restart;
    public Text RoundText;

    public void SetButtons()
    {
        AttackButtons[0] = new ButtonUI(Resources.Load("Data/UIButtons/AttackButton") as ButtonSO, Buttons1Parent.transform, BtnPos[0]);
        AttackButtons[1] = new ButtonUI(Resources.Load("Data/UIButtons/AttackButton") as ButtonSO, Buttons2Parent.transform, BtnPos[1]);
        BuffButtons[0] = new ButtonUI(Resources.Load("Data/UIButtons/BuffButton") as ButtonSO, Buttons1Parent.transform, BtnPos[2]);
        BuffButtons[1] = new ButtonUI(Resources.Load("Data/UIButtons/BuffButton") as ButtonSO, Buttons2Parent.transform, BtnPos[3]);
        Restart = new ButtonUI(Resources.Load("Data/UIButtons/RestartButton") as ButtonSO, RestartParent.transform, BtnPos[4]);
        GameObject text = Instantiate(Resources.Load("Prefabs/Text") as GameObject, TextParent.transform);
        RoundText = text.GetComponent<Text>();
        RoundText.text = "Round: " + roundCount;
    }

    public void SetBars(bool IsFighter1)
    {
        barsList.Add(new BarsUI(Resources.Load("Data/UIBars/VampBar") as BarsSO, Bars1Parent.transform, IsFighter1));
        barsList.Add(new BarsUI(Resources.Load("Data/UIBars/ArmorBar") as BarsSO, Bars1Parent.transform, IsFighter1));
        barsList.Add(new BarsUI(Resources.Load("Data/UIBars/HealthBar") as BarsSO, Bars1Parent.transform, IsFighter1));
        barsList.Add(new BarsUI(Resources.Load("Data/UIBars/VampBar") as BarsSO, Bars2Parent.transform, !IsFighter1));
        barsList.Add(new BarsUI(Resources.Load("Data/UIBars/ArmorBar") as BarsSO, Bars2Parent.transform, !IsFighter1));
        barsList.Add(new BarsUI(Resources.Load("Data/UIBars/HealthBar") as BarsSO, Bars2Parent.transform, !IsFighter1));
    }

    public void SetActiveButtons(bool isF1)
    {
        AttackButtons[1].button.interactable = !isF1;
        BuffButtons[1].button.interactable = !isF1;
        AttackButtons[0].button.interactable = isF1;
        BuffButtons[0].button.interactable = isF1;
    }

    public void UpdateBars(float health, float armor, float vampirism, bool IsFighter1)
    {
        for(int i = 0; i < barsList.Count; i++)
        {
            if (IsFighter1 == barsList[i].isF1)
            {
                switch (barsList[i].type)
                {
                    case Bars.Health:
                        barsList[i].text.text = health.ToString();
                        barsList[i].UpdateBar(health);
                        break;
                    case Bars.Armor:
                        barsList[i].text.text = armor.ToString();
                        barsList[i].UpdateBar(armor);
                        break;
                    case Bars.Vampirism:
                        barsList[i].text.text = vampirism.ToString();
                        barsList[i].UpdateBar(vampirism);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void CreateBuffText(BuffSO data, bool isFighter1)
    {
        if (isFighter1)
        {
            buffs.Add(new BuffUI(data, Buffs1Parent.transform));
        }
        else
        {
            buffs.Add(new BuffUI(data, Buffs2Parent.transform));
        }
    }

    public void NextRound()
    {
        roundCount++;
        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].nextRound();
            if(buffs[i].timer == 0)
            {
                buffs.RemoveAt(i);
                i--;
            }
        }
        RoundText.text = "Round: " + roundCount;
    }
}
