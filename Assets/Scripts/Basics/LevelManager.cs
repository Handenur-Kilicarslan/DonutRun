using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Level System")]
    private int whichlevel = 0;
    public Text levelText;
    public GameObject follower;
    public bool isWin;

    //[SerializeField] public List<Scriptable> levels = new List<Scriptable>();
    [SerializeField] public List<GameObject> level = new List<GameObject>();
    //public GameObject MainPlayer;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //Elephant level started
        whichlevel = PlayerPrefs.GetInt("whichlevel");

        if (PlayerPrefs.GetInt("randomlevel") > 0)
        {
            whichlevel = Random.Range(0, level.Count);
        }
        level[whichlevel].SetActive(true); //Level Set Active?

        //Instantiate(levels[whichlevel].LevelPrefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 180, 0))); //Level Instantiate
        //MainPlayer = Instantiate(levels[whichlevel].Player, Vector3.zero, Quaternion.identity); //Player Instantiate

        //levelText.text = "Level " + (whichlevel + 1);
    }

    public void NextLevel()
    {
        whichlevel++;
        PlayerPrefs.SetInt("whichlevel", whichlevel);

        if (whichlevel >= level.Count)
        {
            whichlevel--;
            PlayerPrefs.SetInt("randomlevel", 1);
        }

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public int Whichlevel()
    {
        return whichlevel;
    }
}