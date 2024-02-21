using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isColliding;
    public TextMeshProUGUI interactText;
    public string npcName;
    private NPCScript npc;
   // public GameObject playerGrabbyArm;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Objective Item"))
        {
            npc = GameObject.Find(npcName).GetComponent<NPCScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.CompareTag("Door"))
            {
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("Objective Item") && npc.questBestowed)
            {
                gameObject.SetActive(false);
                npc.questComplete = true;
            }
          /*  else if (gameObject.name == "Grabby Arm" && npc.questBestowed)
            {
                playerGrabbyArm.gameObject.SetActive(true);
                gameObject.SetActive(false);
                npc.questComplete = true;
            } */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Door") || (gameObject.CompareTag("Objective Item") && npc.questBestowed) || gameObject.CompareTag("Non-Interactable"))
            {
                isColliding = true;
                interactText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            interactText.gameObject.SetActive(false);
        }
    }
}
