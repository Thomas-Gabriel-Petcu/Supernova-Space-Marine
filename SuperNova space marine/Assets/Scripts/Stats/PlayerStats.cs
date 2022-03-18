using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public Stat maxEnergy;
    public int CurrentEnergy { get; private set; }
    public float energyRegenTimer;
    public int energyRegenRate;
    private int maxEnergyCache;
    public float energyRegenTick;
    public override void Awake()
    {
        base.Awake();
        CurrentEnergy = maxEnergy.GetValue();
        
    }
    void Start()
    {
        EquipmentManager.instance.onEquipmentChangedCallBack += OnEquipmentChanged;
        maxEnergyCache = maxEnergy.GetValue();
    }

    public void ReduceEnergy(int amount)
    {
        CurrentEnergy -= amount;
        StartCoroutine("RegenerateEnergy");
        if (CurrentEnergy < 0)
        {
            CurrentEnergy = 0;
        }
    }

    private IEnumerator RegenerateEnergy()
    {
        yield return new WaitForSeconds(energyRegenTimer);
        while (CurrentEnergy < maxEnergyCache)
        {
            CurrentEnergy += energyRegenRate;
            if (CurrentEnergy > maxEnergyCache)
            {
                CurrentEnergy = maxEnergyCache;
                
            }
            yield return new WaitForSeconds(energyRegenTick);
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

    }

   
}
