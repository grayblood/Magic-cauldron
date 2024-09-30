using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectorScript : MonoBehaviour
{
    public Transform pos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.transform.position = pos.position;
        }
    }
}
