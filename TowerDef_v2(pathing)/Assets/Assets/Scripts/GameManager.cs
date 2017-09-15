using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {


    public TowerButton ClickedBtn  { get;  set; }

    [SerializeField]
    private int currency;

    private int wave = 0;

    [SerializeField]
    private int lives;

    public bool gameOver = false;

    [SerializeField]
    private Text livesTxt;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyTxt;

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject gameOverMenu;

    private Tower selectedTower;    //current selected tower


    //keeps a list of active monsters so we know when the wave is finished when there are none
    private List<Monster> activeMonsters = new List<Monster>();

    public ObjectPool Pool { get; set; }

    public bool WaveActive  //are there monsters left?
    {
        get {
            return activeMonsters.Count > 0;
        }
    }

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {

            currency = value;
            this.currencyTxt.text = value.ToString() + " <color=yellow>G</color>";  //sets the currency with a yellow "G" after it
        }
    }

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            this.lives = value;
            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }
            livesTxt.text = lives.ToString();

        }
    }
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Use this for initialization
    void Start () {
        Currency = currency;
        Lives = lives;
	}
	
	// Update is called once per frame
	void Update () {
        HandleEscape();
	}

    // called from Onclick event in unity in the Canvas -> towerPanel ->Btn
    public void PickTower(TowerButton towerBtn)
    {
        if (Currency >= towerBtn.Price && !WaveActive) //checks you have enough money to buy tower  && in "buy" phase ie no active monsters
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }

    }

    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price) //checks you have enough money when you place the tower
        {
            Currency -= ClickedBtn.Price;   //take tower price from currency
        }
        Hover.Instance.Deavtivate();    //get tower out of hand
    }

    public void SelectTower(Tower tower)
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select(); //show/hide sprite rendener ie range

    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = null;
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deavtivate();
        }
    }

    public void StartWave()
    {
        wave++;

        waveTxt.text = string.Format("Wave: <color=orange>{0}</color>", wave);

        StartCoroutine(SpawnWave());    // coroutine so they spawn gradually

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();   

        for (int i = 0; i < wave; i++)  //spawn as many monsters as wave number 
        {
            int monsterIndex = Random.Range(0, 5); // ADD MORE MONSTERS WHEN HAVE ANIMATIONS

            string type = string.Empty;

            switch (monsterIndex)   //spawn a monster based on the random number
            {
                case 0:
                    type = "Seedo";
                    break;
                case 1:
                    type = "Shroom";
                    break;
                case 2:
                    type = "Snail";
                    break;
                case 3:
                    type = "Tree";
                    break;
                case 4:
                    type = "Girl";
                    break;
                default:
                    break;
            }
            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn();
            activeMonsters.Add(monster);
            yield return new WaitForSeconds(2.5f);

        }


    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive && !gameOver)
        {
            waveBtn.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1; //if timescale is 0 then everything stops moving this returns the timescale to normal speed

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //can later be used to reset other levels
    }

    public void QuitGame()
    {
        Application.Quit(); //closes the game
    }
}
