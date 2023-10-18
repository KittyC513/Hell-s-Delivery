using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{

    public static ScoreCount instance;
    [SerializeField]
    public int scoreValueP1 = 0;
    [SerializeField]
    public int scoreValueP2 = 0;
    [SerializeField]
    public TextMeshProUGUI deathCountP1;
    [SerializeField]
    public TextMeshProUGUI deathCountP2;
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
        deathCountP1.text = "Death Count :" + scoreValueP1.ToString();
        deathCountP2.text = "Death Count :" + scoreValueP2.ToString();
    }

    public void AddPointToP1(int value)
    {
        scoreValueP1 += value;
        deathCountP1.text = "Death Count :" + scoreValueP1.ToString();
    }

    public void AddPointToP2(int value)
    {
        scoreValueP2 += value;
        deathCountP2.text = "Death Count :" + scoreValueP2.ToString();
    }
}
