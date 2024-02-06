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
    public GameObject questRewardItem;

    // Start is called before the first frame update
    void Start()
    {
        npcName = gameObject.name;
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
                else
                {
                    questCompletedText.gameObject.SetActive(true);
                    if (npcName == "Shady Man")
                    {
                        questRewardItem.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            interactText.gameObject.SetActive(false);
            questBestowText.gameObject.SetActive(false);
            questIncompleteText.gameObject.SetActive(false);
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
}
