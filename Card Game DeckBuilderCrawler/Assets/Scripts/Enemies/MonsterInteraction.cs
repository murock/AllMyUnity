using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    //Name of monster
    [SerializeField]
    private Text nameTxt;

    //Description of monster
    [SerializeField]
    private Text descriptionTxt;

    //Health of monster
    [SerializeField]
    private Text healthTxt;

    [SerializeField]
    private Text attackTxt;

    [SerializeField]
    private int health = 15;

    [SerializeField]
    private int attack = 4;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float travelTime = 2f;

    [SerializeField]
    private Transform monsterSpawnLoc;

    private Vector3 startPos;
    private float t;
    private Monster currentMonster;

    private bool isAlive = false;
    private bool takingDamage = false;

    // Tooltip stuff
    private bool timerRunning = false;
    private const float toolTipWaitTime = 1.5f;
    private static float timer;

    public bool IsAlive
    {
        get
        {
            return this.isAlive;
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            ShowToolTip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        timerRunning = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.HideTooltip();
        timerRunning = false;
    }

    public void Spawn(Monster monster)
    {
        currentMonster = monster;
        if (currentMonster.MonsterMech != null)
        {
            // Trigger any monster mechanics that need to happen as soon as they spawn e.g blocking card draw
            currentMonster.MonsterMech.OnSpawn();
        }

        this.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(monster.ArtLocation);
        this.health = monster.Health;
        this.attack = monster.Attack;
        this.healthTxt.text = string.Format(health.ToString());
        this.attackTxt.text = string.Format(attack.ToString());
        this.nameTxt.text = monster.Title;
        this.startPos = transform.position;
        this.isAlive = true;
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        this.transform.position = this.monsterSpawnLoc.position;
        t = 0;
        StartCoroutine(MoveMonster(this.startPos));
    }

    public void Defeat()
    {
        if (currentMonster.MonsterMech != null)
        {
            currentMonster.MonsterMech.OnDeath();
        }
        this.isAlive = false;
        this.health = 0;
        this.nameTxt.text = "DEFEATED!!!";
        this.healthTxt.text = string.Format(this.health.ToString());

        this.isAlive = false;
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        if (IsLastStanding())
        {
            //If monster is the last one standing then spawn another
            TurnManager.Instance.SpawnCount = -1;
        }
        //Instead of opening shop, could open centre panel with some 'Loot' to pick up??
        //GameManager.Instance.centrePanel.SetActive(true);
        //ShopManager.Instance.StartShop();
    }

    private bool IsLastStanding()
    {
        bool lastAlive = true;
        foreach (MonsterInteraction monster in TurnManager.Instance.monsterSpawners)
        {
            if (monster != null && monster.IsAlive)
            {
                //Another monster is Alive
                lastAlive = false;
            }           
        }
        return lastAlive;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.Defeat();
        }
        else
        {
            this.healthTxt.text = string.Format(this.health.ToString());
        }
    }

    public void DoDamage()
    {
        t = 0;
        this.takingDamage = true;
        StartCoroutine(MoveMonster(GameManager.Instance.player.transform.position));
    }

    private IEnumerator MoveMonster(Vector3 endPos)
    {
        Vector3 fromPoint = this.transform.position;
        while (this.transform.position != endPos)
        {
            this.t += Time.deltaTime / this.travelTime;
            this.transform.position = Vector3.Lerp(fromPoint, endPos, this.t);
            yield return new WaitForEndOfFrame();
        }
        this.transform.position = this.startPos;
        if (this.takingDamage)
        {
            this.takingDamage = false;
            PlayerInteraction.Instance.TakeDamage(this.attack);

            //This should be moved to the turn manager doesn't make sense  for it to be in MoveMonster()
            if (GameManager.Instance.Money > 0)
            {
                GameManager.Instance.centrePanel.SetActive(true);
                ShopManager.Instance.StartShop();
            }
        }

    }

    private void AttachTooltip()
    {
        GameManager.Instance.toolTip.text = "";
        if (currentMonster != null && currentMonster.MonsterMech != null)
        {
            GameManager.Instance.toolTipCanvasGroup.alpha = 1;
            GameManager.Instance.toolTip.text += currentMonster.MonsterMech.GetToolTip();
        }
    }

    private void ShowToolTip()
    {
        timer += Time.deltaTime;
        if (timer > toolTipWaitTime && !CardActions.isDragging)
        {
            this.AttachTooltip();
            timer = 0f;
            timerRunning = false;
        }
    }

    private void HideTooltip()
    {
        GameManager.Instance.toolTipCanvasGroup.alpha = 0;
    }


}
