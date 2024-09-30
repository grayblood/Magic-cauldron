using System.Collections;
using UnityEngine;

public class WandScript : MonoBehaviour
{
    [SerializeField]
    Transform t_Out;

    [SerializeField]
    Renderer laser;
    /*
    [SerializeField]
    LineRenderer laser;
   */
    [SerializeField]
    LayerMask cMinigameLayer;

    [SerializeField]
    GameEvent On_ResetMinigame;

    bool b_Magic;
    bool b_PCM;
    public bool waiting;


    private void Awake()
    {
        waiting = false;
        b_PCM = false;
        TurnOnOff(false);
    }
    private void Update()
    {
        if (b_Magic)
            Magic();
    }
    public void PlayingCauldronM(bool a)
    {
        b_PCM = a;
    }

    public void TurnOnOff(bool bs)
    {
        b_Magic = bs;
        laser.gameObject.SetActive(bs);
    }
    

    void Magic()
    {
        RaycastHit hit;
        if (Physics.Raycast(t_Out.position, t_Out.TransformDirection(Vector3.forward), out hit, 3, cMinigameLayer))
        {
            if (hit.collider.gameObject.GetComponent<PointsScriptCauldron>() && !waiting)
            {
                laser.material.color = Color.red;
                hit.collider.gameObject.GetComponent<PointsScriptCauldron>().SendData();
                StartCoroutine("WaitForNextPuzzle", 1f);
            }
            else if (hit.collider.gameObject.GetComponent<StartCauldronMinigameScript>())
            {
                laser.material.color = Color.red;
                hit.collider.gameObject.GetComponent<StartCauldronMinigameScript>().StartMinigame();
                StartCoroutine("WaitForNextPuzzle", 1f);
            }
        }
        else
        {
            if (b_PCM)
            {
                StartCoroutine("WaitForNextPuzzle", 1f);
                On_ResetMinigame.Raise();
                laser.material.color = Color.blue;
            }
            else
            {
                laser.material.color = Color.yellow;
            }
        }
    }
    IEnumerator WaitForNextPuzzle(float ftime)
    {
        waiting = true;
        yield return new WaitForSeconds(ftime);
        waiting = false;
    }
}
