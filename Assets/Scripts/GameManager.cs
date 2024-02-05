using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gravityMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(0, -9.81f) * gravityMultiplier;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
