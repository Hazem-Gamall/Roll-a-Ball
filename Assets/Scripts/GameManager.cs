using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Score{get; private set;}
    public static Action<int> ScoreChanged;
    [SerializeField]private AudioSource coin;
    [SerializeField] private UnityEngine.UI.Text score;
    [SerializeField] private GameObject win;
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    
    void Awake()
    {   
        CubeBehaviour.CubeCollided += onCubeCollided;
        coin = GetComponent<AudioSource>();
        score = GameObject.Find("score").GetComponent<UnityEngine.UI.Text>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnDisable(){
        CubeBehaviour.CubeCollided -= onCubeCollided;
    }

    void Restart(){
        int ind = SceneManager.GetActiveScene().buildIndex;
        print($"build index {ind}");
        print(SceneManager.sceneCountInBuildSettings);
        if(ind == SceneManager.sceneCountInBuildSettings-1)
            ind = -1;
        SceneManager.LoadScene(ind+1);
    }

    void onCubeCollided(){
        Score++;
        coin.Play();
        score.text = $"{Score}";
        ScoreChanged?.Invoke(Score);
        if(Score >= 8){
            win.SetActive(true);
            playerController.enabled = false;
            Invoke("Restart", 1);
        }
    }
}
