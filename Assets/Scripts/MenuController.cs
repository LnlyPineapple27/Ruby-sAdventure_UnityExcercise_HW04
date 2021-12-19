// Student name: Phan Tan Dat
// Student ID: 18127078
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject OptionsMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowOptionsMenu()
    {
        // SceneManager.LoadScene("Menu");
        Debug.Log("ShowOptionsMenu");
        OptionsMenu.SetActive(true);
    }
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit game");
    }
}
