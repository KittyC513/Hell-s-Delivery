using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ScoreCount : MonoBehaviour
{

    public static ScoreCount instance;
    [SerializeField]
    public float scoreValueP1 = 0;
    [SerializeField]
    public float scoreValueP2 = 0;
    [SerializeField]
    public TextMeshProUGUI deathCountP1;
    [SerializeField]
    public TextMeshProUGUI deathCountP2;
    [SerializeField]
    public SaveData sceneInfo;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        deathCountP1.text = "Player1 Score: " + (int)scoreValueP1;
        deathCountP2.text = "Player2 Score: " + (int)scoreValueP2;
        sceneInfo.player1Score = scoreValueP1;
        sceneInfo.player2Score = scoreValueP2;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("ScoreCards");
        }

    }

    public void AddPointToP1(int value)
    {
        scoreValueP1 += value;
        deathCountP1.text = "Player1 Score: " + scoreValueP1.ToString();
    }

    public void AddPointToP1Package(int value)
    {
        scoreValueP1 += value * Time.fixedDeltaTime;
        deathCountP1.text = "Player1 Score: " + scoreValueP1.ToString();
    }

    public void AddPointToP2(int value)
    {
        scoreValueP2 += value;
        deathCountP2.text = "Player2 Score: " + scoreValueP2.ToString();
    }

    public void AddPointToP2Package(int value)
    {
        scoreValueP2 += value * Time.fixedDeltaTime;
        deathCountP2.text = "Player2 Score: " + scoreValueP2.ToString();
    }


}
