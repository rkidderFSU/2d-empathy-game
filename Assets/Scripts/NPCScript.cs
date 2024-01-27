using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject textContainer;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI questBestowText;
    public TextMeshProUGUI questIncompleteText;
    public TextMeshProUGUI questCompletedText;
    private bool isColliding;
    public bool questBestowed;
    public bool questComplete;

    // Start is called before the first frame update
    void Start()
    {

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
                questCompletedText.gameObject.SetActive(true);
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
            questCompletedText.gameObject.SetActive(false);
        }
    }

    private IEnumerator BestowQuest()
    {
        questBestowText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        questBestowed = true;
    }
}
