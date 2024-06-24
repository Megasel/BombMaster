using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeCounterText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.cubesCounterText = gameObject.GetComponent<Text>() ;
    }

    
}
