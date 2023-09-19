using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fighter
{
    private float health;
    private float maxHealth;
    private float armor;
    private float maxArmor;
    private float vamp;
    private float maxVamp;
    private float baseDamage;
    private float decVamp;
    private float armorDestr;
    private SpriteRenderer sprite;
    private GameObject fighter;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    public float Vamp
    {
        get { return vamp; }
        set { vamp = value; }
    }

    public float Damage
    {
        get { return baseDamage; }
    }

    public float armorDestruction
    {
        get { return armorDestr; }
    }

    public float DecrVamp
    {
        get { return decVamp; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public SpriteRenderer FSprite
    {
        get { return sprite; }
    }

    public Dictionary<Buff, int> currentBuffs = new Dictionary<Buff, int>();
    public Fighter(FighterSO data)
    {
        fighter = GameObject.Instantiate(data.prefab);
        health = data.health;
        maxHealth = data.health;
        armor = data.armor;
        maxArmor = 100;
        maxVamp = 100;
        vamp = 0;
        baseDamage = data.damage;
        sprite = fighter.GetComponent<SpriteRenderer>();
    }

    public void GetBuff(Buff buff, int duration)
    {
        if(buff != Buff.None)
        {
            switch (buff)
            {
                case Buff.ArmorDestruction:
                    currentBuffs.Add(Buff.ArmorDestruction, duration);
                    armorDestr = 10;
                    break;
                case Buff.ArmorSelf:
                    currentBuffs.Add(Buff.ArmorSelf, duration);
                    armor += 50;
                    if(armor > 100)
                    {
                        armor = maxArmor;
                    }
                    break;
                case Buff.DoubleDamage:
                    currentBuffs.Add(Buff.DoubleDamage, duration);
                    baseDamage *= 2;
                    break;
                case Buff.VampirismDecrease:
                    currentBuffs.Add(Buff.VampirismDecrease, duration);
                    decVamp = 25;
                    break;
                case Buff.VampirismSelf:
                    currentBuffs.Add(Buff.VampirismSelf, duration);
                    vamp = 50;
                    if (vamp > 100)
                    {
                        vamp = maxVamp;
                    }
                    if (armor >= 25)
                    {
                        armor -= 25;
                    }
                    else
                    {
                        armor = 0;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void BuffEnds(Buff buff)
    {
        if (buff != Buff.None)
        {
            switch (buff)
            {
                case Buff.ArmorDestruction:
                    currentBuffs.Remove(Buff.ArmorDestruction);
                    armorDestr = 0;
                    break;
                case Buff.ArmorSelf:
                    currentBuffs.Remove(Buff.ArmorSelf);
                    armor = 0;
                    break;
                case Buff.DoubleDamage:
                    currentBuffs.Remove(Buff.DoubleDamage);
                    baseDamage /= 2;
                    break;
                case Buff.VampirismDecrease:
                    currentBuffs.Remove(Buff.VampirismDecrease);
                    decVamp = 0;
                    break;
                case Buff.VampirismSelf:
                    currentBuffs.Remove(Buff.VampirismSelf);
                    vamp = 0;
                    break;
                default:
                    break;
            }
        }
    }

    public void BuffTimer()
    {
        for(int i = 0; i < currentBuffs.Count; i++)
        {
            if(currentBuffs.ElementAt(i).Value >= 2)
            {
                currentBuffs[currentBuffs.ElementAt(i).Key] -= 1;
            }
            else
            {
                BuffEnds(currentBuffs.ElementAt(i).Key);
                i--;
            }
        }
    }
}
