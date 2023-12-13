using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel;
    public void Start()
    {
        Panel.SetActive(false);
    }

    public void Start_Game()
    {
        SceneManager.LoadScene(2);
    }

    public void Info()
    {
        Panel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
