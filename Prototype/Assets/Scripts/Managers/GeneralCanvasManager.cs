using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralCanvasManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenInventory()
    {
        GameManager.Instance.myPlayer.inputController.CallInventory();
    }
    
    
}
