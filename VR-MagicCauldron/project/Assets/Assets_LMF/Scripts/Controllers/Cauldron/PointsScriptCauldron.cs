using System.Collections;
using UnityEngine;

public class PointsScriptCauldron : MonoBehaviour
{
    public int pointID;

    

    Renderer ren;
    [SerializeField]
    Cauldron_Script cauldronScript;

    private void Awake()
    {
        ren = GetComponent<Renderer>();
        // cauldronScript = GetComponentInParent<Cauldron_Script>();
        //gameObject.SetActive(false);
        changeState(0);
    }


    public void SendData()
    {
        cauldronScript.receivePointData(pointID);
    }
    public void CorrectState()
    {
        StartCoroutine("correct");
    }
    IEnumerator correct()
    {
        changeState(2);
        yield return new WaitForSeconds(.5f);
        changeState(0);
    }
    public void ShineState()
    {
        StartCoroutine("shine");
    }

    IEnumerator shine()
    {
        changeState(1);
        yield return new WaitForSeconds(.5f);
        changeState(0);
    }
    public void WrongStates()
    {
        StartCoroutine("WrongSecuence");
    }

    IEnumerator WrongSecuence()
    {
        yield return new WaitForSeconds(.5f);
        changeState(3);
        yield return new WaitForSeconds(.5f);
        changeState(1);
        yield return new WaitForSeconds(.5f);
        changeState(3);
        yield return new WaitForSeconds(.5f);
        changeState(0);
    }
    public void changeState(int i)
    {
        switch (i)
        {
            case 0: ren.material.color = Color.black; break; //Off
            case 1: ren.material.color = Color.white; break; //On
            case 2: ren.material.color = Color.green; break; //True
            case 3: ren.material.color = Color.red; break; //Wrong
        }
    }
}
