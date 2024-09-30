using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;


    public void StartNewGame()
    {
        canvas.SetActive(false);
        GMScript.Instance.LoadScenes(false);
    }


    public void ContinueGame()
    {
        canvas.SetActive(false);
        GMScript.Instance.LoadScenes(true);
    }

  
}
