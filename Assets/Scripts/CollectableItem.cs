using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private NPCScript npcScript;
    public string npcName;
    // Start is called before the first frame update
    void Start()
    {
        npcScript = GameObject.Find(npcName).GetComponent<NPCScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && npcScript.questBestowed)
        {
            Destroy(gameObject);
            npcScript.questComplete = true;
        }
    }
}
