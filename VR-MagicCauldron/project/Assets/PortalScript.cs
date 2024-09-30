using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] Transform exit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<AutoHandPlayer>())
        {
            if (AutoHandPlayer.Instance != null)
            {
                    AutoHandPlayer.Instance.SetPosition(exit.position);
            }
        }
    }
}
