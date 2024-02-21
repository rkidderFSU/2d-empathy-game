using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isColliding;
    public TextMeshProUGUI interactText;
    public string npcName;
    private NPCScript npc;
    private SpriteRenderer playerSr;
    public Sprite grabbySprite;
    public Sprite mugTwo;
    private SpriteRenderer sr;
    public bool haveKettle;
    public Sprite burgerPlayer;

    // Start is called before the first frame update
    void Start()
    {
        haveKettle = false;
        if (gameObject.CompareTag("Objective Item"))
        {
            npc = GameObject.Find(npcName).GetComponent<NPCScript>();
            playerSr = GameObject.Find("Player").GetComponent<SpriteRenderer>();
            sr = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.CompareTag("Door"))
            {
                if (gameObject.name == "Tea Kettle")
                {
                    haveKettle = true;
                    gameObject.SetActive(false);
                }
                else if (haveKettle && gameObject.name == "Mug")
                {
                    sr.sprite = mugTwo;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else if (gameObject.CompareTag("Objective Item") && npc.questBestowed)
            {
                gameObject.SetActive(false);
                npc.questComplete = true;
            }
            if (gameObject.name == "Grabby Arm" && npc.questBestowed)
            {
                playerSr.sprite = grabbySprite;
                gameObject.SetActive(false);
                npc.questComplete = true;
            } 
            else if (gameObject.name == "Mug" && npc.questBestowed && haveKettle)
            {
                sr.sprite = mugTwo;
                npc.questComplete = true;
            }
            else if (gameObject.CompareTag("Burger"))
            {
                npc.questTwoComplete = true;
                playerSr.sprite = burgerPlayer;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Door") || (gameObject.CompareTag("Objective Item") && npc.questBestowed) || gameObject.CompareTag("Burger"))
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
