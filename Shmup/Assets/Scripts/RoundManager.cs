using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // Variable field
    [SerializeField] TextMesh textMesh;
    private TextMesh text;
    private int round;

    private Monster_Fly bat;

    // Start is called before the first frame update
    void Start()
    {
        //bat = new Monster_Fly();
        text = Instantiate(textMesh, new Vector3(0f, 0f, 0f), Quaternion.identity);
        round = 0;
        NextRound();
    }

    /// <summary>
    /// Start the next round
    /// </summary>
    public void NextRound()
    {
            round++;
            text.text = $"Round: {round}";
            StartCoroutine(ResetText(5));
            text.text = "";

            // Spawn wave
            for (int i = 0; i < round; i++)
            {
                // Spawn monster
            }
    }

    /// <summary>
    /// A coroutine that waits for a given duration in seconds
    /// </summary>
    /// <returns>A yield instruction</returns>
    IEnumerator ResetText(int duration) { yield return new WaitForSeconds(duration); }
}