using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSign : MonoBehaviour
{
    TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        this.textMesh = GetComponentInChildren<TextMeshPro>();
        this.textMesh.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
