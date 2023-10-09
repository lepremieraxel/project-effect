using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject invaderPrefab;
    [SerializeField] string levelFile = "Assets/Level.csv";
    private Text levelTitle;
    private GameObject spawnPoint;
    private GameObject invadersParent;

    void Awake()
    {
        levelTitle = GameObject.Find("LevelTitle").GetComponent<Text>();
        spawnPoint = GameObject.Find("SpawnPoint");
        invadersParent = GameObject.Find("Invaders");
    }
    public void ChoosenLevel(int currentLevel)
    // lis le fichier csv et récupère la ligne placé en paramètres
    {
        string[] lines = File.ReadAllLines(levelFile);
        string[] level = lines[currentLevel].Split(';');
        StartLevel(level);
    }

    void StartLevel(string[] level)
    {
        levelTitle.text = level[1];
        string[] waves = level[2].Split("|");
        int currentWave = 0;

        GameObject invader = Instantiate(invaderPrefab, spawnPoint.transform.position, Quaternion.identity, invadersParent.transform);
    }
}
