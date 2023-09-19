using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameStateManager : MonoBehaviour
{

    private bool isBuffed;
    private bool isFighter1Turn;
    [SerializeField]
    private UIManager _UIManager;

    private Fighter fighter1;
    private Fighter fighter2;

    private void Awake()
    {
        fighter1 = new Fighter(Resources.Load("Data/Fighter_1") as FighterSO);
        fighter2 = new Fighter(Resources.Load("Data/Fighter_2") as FighterSO);
        isBuffed = false;
        isFighter1Turn = true;
        _UIManager.SetButtons();
        _UIManager.SetActiveButtons(isFighter1Turn);
        setButtonsEvents();
        _UIManager.SetBars(isFighter1Turn);
        _UIManager.UpdateBars(fighter1.Health, fighter1.Armor, fighter1.Vamp, isFighter1Turn);
        _UIManager.UpdateBars(fighter2.Health, fighter2.Armor, fighter2.Vamp, !isFighter1Turn);
    }

    private void nextRound()
    {
        fighter1.BuffTimer();
        fighter2.BuffTimer();
        _UIManager.NextRound();
        _UIManager.UpdateBars(fighter1.Health, fighter1.Armor, fighter1.Vamp, isFighter1Turn);
        _UIManager.UpdateBars(fighter2.Health, fighter2.Armor, fighter2.Vamp, !isFighter1Turn);
    }

    private void setButtonsEvents()
    {
        for(int i = 0; i < _UIManager.AttackButtons.Length; i++)
        {
            _UIManager.AttackButtons[i].button.onClick.AddListener(delegate { Attack(); });
        }
        for (int i = 0; i < _UIManager.BuffButtons.Length; i++)
        {
            _UIManager.BuffButtons[i].button.onClick.AddListener(delegate { BuffFighter(); });
        }
        _UIManager.Restart.button.onClick.AddListener(delegate { restart(); });
    }

    private void restart()
    {
        SceneManager.LoadScene(0);
    }

    private void setBuff(Fighter fighter)
    {
        if (isBuffed)
            return;
        if (fighter.currentBuffs.Count >= 2)
            return;
        Buff newBuff = (Buff)Random.Range(1, 5);
        while (fighter.currentBuffs.ContainsKey(newBuff))
        {
            newBuff = (Buff)Random.Range(1, 5);
        }
        BuffSO data = Resources.Load("Data/Buffs/" + newBuff.ToString()) as BuffSO;
        fighter.GetBuff(newBuff, data.duration);
        _UIManager.CreateBuffText(data, isFighter1Turn);
        isBuffed = true;
    }

    private void buffCheck(Buff buff,Fighter f1, Fighter f2)
    {
        if (buff != Buff.None)
        {
            switch (buff)
            {
                case Buff.ArmorDestruction:
                    if (f2.Armor >= f1.armorDestruction)
                    {
                        f2.Armor -= f1.armorDestruction;
                    }
                    else
                    {
                        f2.Armor = 0;
                    }
                    break;
                case Buff.VampirismDecrease:
                    if (f2.Vamp >= f1.DecrVamp)
                    {
                        f2.Vamp -= f1.DecrVamp;
                    }
                    else
                    {
                        f2.Vamp = 0;
                    }
                    break;
                case Buff.VampirismSelf:
                    f1.Health += (f1.Damage - (f1.Damage * (f2.Armor - f1.armorDestruction) / 100)) * (f1.Vamp / 100);
                    if(f1.Health > f1.MaxHealth)
                    {
                        f1.Health = f1.MaxHealth;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void setAttack(Fighter f1, Fighter f2)
    {
        float damage = f1.Damage - (f1.Damage * (f2.Armor - f1.armorDestruction) / 100);
        if (damage > 0)
        {
            f2.Health -= damage;
            StartCoroutine(ChangeColor(f2.FSprite));
        }
        if (f2.Health <= 0)
        {
            restart();
        }
        for (int i = 0; i < f1.currentBuffs.Count; i++)
        {
            buffCheck(f1.currentBuffs.ElementAt(i).Key, f1, f2);
        }
    }

    IEnumerator ChangeColor(SpriteRenderer sprite)
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(1f);
        sprite.color = Color.white;
    }

    public void Attack()
    {
        if (isFighter1Turn)
        {
            setAttack(fighter1, fighter2);
            isFighter1Turn = false;
            _UIManager.SetActiveButtons(isFighter1Turn);
        }
        else
        {
            setAttack(fighter2, fighter1);
            isFighter1Turn = true;
            _UIManager.SetActiveButtons(isFighter1Turn);

            nextRound();
        }
        _UIManager.UpdateBars(fighter1.Health, fighter1.Armor, fighter1.Vamp, true);
        _UIManager.UpdateBars(fighter2.Health, fighter2.Armor, fighter2.Vamp, false);
        isBuffed = false;
    }

    public void BuffFighter()
    {
        if (isFighter1Turn)
        {
            setBuff(fighter1);
            _UIManager.UpdateBars(fighter1.Health, fighter1.Armor, fighter1.Vamp, true);
        }
        else
        {
            setBuff(fighter2);
            _UIManager.UpdateBars(fighter2.Health, fighter2.Armor, fighter2.Vamp, false);
        }
    }
}
