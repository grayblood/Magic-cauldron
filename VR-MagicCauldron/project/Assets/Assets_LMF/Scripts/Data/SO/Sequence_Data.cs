using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RData", menuName = "ScriptableObjects/data/sequence", order = 2)]
public class Sequence_Data : ScriptableObject
{
    [Serializable]
    public class SequenceNumbers
    {
        public List<int> order;

    }
    [SerializeField]
    public SequenceNumbers sequence;
}
