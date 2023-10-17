using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMesh textPrefab;
    private TextMesh text;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        text = Instantiate(textPrefab, textPrefab.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.Health)
        {
            case 3:
                text.text = "Health: 3";
                break;
            case 2:
                text.text = "Health: 2";
                break;
            case 1:
                text.text = "Health: 1";
                break;
            case 0:
                text.anchor = TextAnchor.MiddleCenter;
                text.text = "Game Over";
                text.transform.position = new Vector3(0f, 0f, 0f);
                StartCoroutine(WaitAndLoad(0.5f));
                break;
            default:
                text.text = "Health: undetermined";
                break;
        }
    }

    private IEnumerator WaitAndLoad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameOver");
    }
}
