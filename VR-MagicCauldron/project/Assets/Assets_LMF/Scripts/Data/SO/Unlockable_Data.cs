using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RData", menuName = "ScriptableObjects/data/Unlocks", order = 2)]
public class Unlockable_Data : ScriptableObject
{
    [Serializable]
    public class Unlockable
    {
        public string r_Name;
        public Ingredient_Data I_Data;
        public List<Recipe_Data> recipes;

        public bool b_Unlocked;
    }
    [SerializeField]
    public Unlockable unlockable;
}
