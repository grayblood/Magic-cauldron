using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveGameData
{
    public string level;
    [Serializable]
    public struct SaveMoneyRecetas
    {
        public int money;
        public Dictionary<string, bool> recetas;
    }
    public SaveMoneyRecetas playerSaveData;
}
