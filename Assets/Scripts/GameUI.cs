using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject winScreen; 
    public GameObject loseScreen;
    public static bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    { 
        if(GuardController.hasBeenCaught) {
            showLoseScreen();
        }
        else if(PlayerController.hasWon) {
            showWinScreen();
        }

        if(isGameOver && PlayerController.hasWon) {
            int buildPosition = SceneManager.GetActiveScene().buildIndex;
            if(Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadSceneAsync(buildPosition+1);
                
            }
        }
        else if(isGameOver) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                int buildPosition = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadSceneAsync(buildPosition);
            }
        }
        
    }

    void showWinScreen() {
        winScreen.SetActive(true);
        isGameOver = true;
        
    }

    void showLoseScreen() {
        loseScreen.SetActive(true);
        isGameOver = true;
        
    }
}
