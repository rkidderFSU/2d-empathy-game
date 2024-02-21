using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI questBestowText;
    public TextMeshProUGUI questIncompleteText;
    public TextMeshProUGUI questCompletedText;
    private bool isColliding;
    public bool questBestowed;
    public bool questComplete;
    private string npcName;
    // public GameObject questRewardItem;
    private GameManager m;
    private NPCScript rewardUnlock;
    public TextMeshProUGUI questTwoBestowText;
    public TextMeshProUGUI questTwoCompleteText;
    public bool questTwoBestowed;
    public bool questTwoComplete;
    private SpriteRenderer playerSpr;
    public Sprite defaultPlayer;

    // Start is called before the first frame update
    void Start()
    {
        npcName = gameObject.name;
        m = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rewardUnlock = GameObject.Find("Elevator").GetComponent<NPCScript>();
        playerSpr = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E))
        {
            interactText.gameObject.SetActive(false);

            if (!questBestowed && !questComplete)
            {
                StartCoroutine(BestowQuest());
            }
            else if (questBestowed && !questComplete)
            {
                questBestowText.gameObject.SetActive(false);
                questIncompleteText.gameObject.SetActive(true);
            }
            else if (questComplete)
            {
                if (gameObject.CompareTag("Door"))
                {
                    gameObject.SetActive(false);
                }
                else if (!questTwoComplete)
                {
                    StartCoroutine(BestowQuestTwo());
                }
            }
            if (questTwoComplete)
            {
                playerSpr.sprite = defaultPlayer;
                rewardUnlock.questComplete = true;
                StartCoroutine(QuestTwoEnd());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            // if (m.sceneName == "House") // Only displays interact text in the first scene
            {
                interactText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopAllCoroutines();
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            interactText.gameObject.SetActive(false);
            questBestowText.gameObject.SetActive(false);
            questIncompleteText.gameObject.SetActive(false);
            questTwoBestowText.gameObject.SetActive(false);
            questTwoCompleteText.gameObject.SetActive(false);
            if (!gameObject.CompareTag("Door")) // Doors do not have Quest Completed Text
            {
                questCompletedText.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator BestowQuest()
    {
        questBestowText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        questBestowed = true;
    }
    private IEnumerator BestowQuestTwo()
    {
        questTwoBestowText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        questTwoBestowed = true;
    }
    private IEnumerator QuestTwoEnd()
    {
        questTwoCompleteText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        questTwoCompleteText.gameObject.SetActive(false);
        questCompletedText.gameObject.SetActive(true);
    }
}
