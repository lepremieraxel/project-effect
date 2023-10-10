using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject invaderPrefab;
    [SerializeField] string levelFile = "Assets/Level.csv";
    private Text levelTitleText;
    private GameObject levelTitle;
    private Transform spawnPoints;
    private GameObject invadersParent;
    private float titleTime = 3f;
    private int ennemiesCount = 0;
    private int currentWave = 0;
    

    void Awake()
    {
        levelTitleText = GameObject.Find("LevelTitle").GetComponent<Text>();
        levelTitle = GameObject.Find("LevelTitle");
        spawnPoints = GameObject.Find("SpawnPoints").transform;
        invadersParent = GameObject.Find("Invaders");
    }
    void Update()
    {
        
    }
    public void ChoosenLevel(int currentLevel)
    // lis le fichier csv et récupère la ligne placé en paramètres
    {
        string[] lines = File.ReadAllLines(levelFile);
        string[] level = lines[currentLevel].Split(';');
        StartCoroutine(StartLevel(level));
    }

    IEnumerator StartLevel(string[] level)
    {
        levelTitle.SetActive(true);
        levelTitleText.text = level[1];
        new WaitForSeconds(titleTime);
        levelTitle.SetActive(false);

        string[] waves = level[2].Split("|");
        StartCoroutine(Wave(int.Parse(waves[currentWave]), waves));
        yield return null;
    }

    IEnumerator Wave(int nbEnnemies, string[] waves)
    {
        levelTitleText.text = "Wave " + currentWave+1.ToString();
        levelTitle.SetActive(true);
        new WaitForSeconds(titleTime);
        levelTitle.SetActive(false);
        for(int i = 0; i < nbEnnemies; i++)
        {
            Instantiate(invaderPrefab, spawnPoints.GetChild(ennemiesCount).position, Quaternion.identity, invadersParent.transform);
            ennemiesCount++;
        }
        StartCoroutine(CountDown(3));
        currentWave++;
        if(currentWave < waves.Length)
        {
            StartCoroutine(Wave(int.Parse(waves[currentWave].ToString()), waves));
        }
        yield return null;
    }

    IEnumerator CountDown(int c)
    {
        levelTitle.SetActive(true);
        for (int i = c; i >= 0; i--)
        {
            levelTitleText.text = i.ToString();
            new WaitForSeconds(1);
        }
        levelTitle.SetActive(false);
        yield return null;
    }
}
