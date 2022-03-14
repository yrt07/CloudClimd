using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//LoadSceneを使うために必要！！

public class ClearDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("playtime")); 
        Debug.Log(PlayerPrefs.GetFloat("besttime"));
    }

    // Update is called once per frame
    void Update()
    {
        /*GameDirector director = new GameDirector();
        director.gametimer*/

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
