using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Wish I could pass my enum here but unity doesn't support that in the onclick inspector
    public void LoadScene(string scene)
    {
       SceneManager.LoadScene(scene);
    }
}
