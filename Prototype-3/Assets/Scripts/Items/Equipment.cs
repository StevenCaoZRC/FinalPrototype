using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot m_equipSlot;
    public int m_damageModifier;
    public int m_armourModifier;
 

}
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Arms}
