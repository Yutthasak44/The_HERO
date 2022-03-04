using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string NameLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisiongameObject = collision.gameObject;
        if (collisiongameObject.tag == "Player")
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(NameLevel);
    }

}
