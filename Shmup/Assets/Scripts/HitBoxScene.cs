using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
    }
}
