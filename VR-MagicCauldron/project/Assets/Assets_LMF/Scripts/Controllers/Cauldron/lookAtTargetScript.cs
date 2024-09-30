using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtTargetScript : MonoBehaviour
{

    [SerializeField]
    Transform target;

    void Update()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(-lookPos);
    }
}
