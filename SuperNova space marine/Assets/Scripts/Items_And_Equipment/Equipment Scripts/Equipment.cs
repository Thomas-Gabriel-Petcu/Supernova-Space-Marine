using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        if (equipSlot == EquipmentSlot.Weapon)//If this is a weapon
        {
            EquipmentManager.instance.Equip(this);
            EquipmentManager.instance.InstantiateWeapon(this);
            RemoveFromInventory();
        }
        if (equipSlot != EquipmentSlot.Weapon && equipSlot != EquipmentSlot.Ability)//if this is not a weapon nor an ability
        {
            EquipmentManager.instance.Equip(this);
            RemoveFromInventory();
        }
        if (equipSlot == EquipmentSlot.Ability)//if this is an ability
        {
            EquipmentManager.instance.InstantiateAbility(this);
        }
    }
}

public enum EquipmentSlot {Default, Head, Chest, Legs, Weapon, Consumable, Ability }



