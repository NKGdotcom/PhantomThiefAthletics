using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIntoAbyss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            ResultManager.Instance.GameOver();
        }
    }
}
