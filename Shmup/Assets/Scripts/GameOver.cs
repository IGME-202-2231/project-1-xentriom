using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private ScoreManager scoreManager;
    [SerializeField] TextMesh textPrefab;
    private TextMesh text;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        text = Instantiate(textPrefab, textPrefab.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Final score: {scoreManager.Score}";

        if (UnityEngine.InputSystem.Keyboard.current.enterKey.wasPressedThisFrame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        if (UnityEngine.InputSystem.Keyboard.current.tabKey.wasPressedThisFrame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
    }
}
