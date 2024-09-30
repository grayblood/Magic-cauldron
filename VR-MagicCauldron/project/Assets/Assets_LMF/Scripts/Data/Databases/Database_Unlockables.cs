using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Unlockables : MonoBehaviour
{
    public List<Unlockable_Data> Unlock = new List<Unlockable_Data>();
    private void Awake()
    {
        foreach (Unlockable_Data data in Unlock)
        {
            data.unlockable.b_Unlocked = false;
        }
    }
   
}
