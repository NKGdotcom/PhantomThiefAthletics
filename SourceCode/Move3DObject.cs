using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3DObject : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnMouseDrag()
    {
        if(GameState.Instance.NowState == GameState.State.setObject)
        {
            Vector3 _objectPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _objectPos.z);
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}
