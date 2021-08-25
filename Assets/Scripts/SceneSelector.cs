using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void SelectScene()
    {
        switch (this.gameObject.name)
        {
            case "Scene01Button":
                SceneManager.LoadScene("Scene01");
                break;
            case "Scene02Button":
                SceneManager.LoadScene("Scene02");
                break;
            case "Scene03Button":
                SceneManager.LoadScene("Scene03");
                break;
            case "Scene04Button":
                SceneManager.LoadScene("Scene04");
                break;
            case "Scene05Button":
                SceneManager.LoadScene("Scene05");
                break;
        }
    }
}
