using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameMamager;
    // Start is called before the first frame update
    void Awake() 
    {
        if (GameController.instance == null)
        {
            Instantiate (gameMamager);
        }
    }
}
