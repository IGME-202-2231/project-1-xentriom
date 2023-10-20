using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Variable field
    [SerializeField] TextMesh scorePrefab;
    private TextMesh scoreText;
    private float score;

    public float Score { get { return score; } }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = Instantiate(scorePrefab, scorePrefab.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            scoreText.text = "";
        }
    }

    /// <summary>
    /// Add 100 points to the score
    /// </summary>
    public void CompletedRound()
    {
        score += 100;
    }

    /// <summary>
    /// Add 10 points to the score
    /// </summary>
    public void DefeatedMonster()
    {
        score += 10;
    }
}
