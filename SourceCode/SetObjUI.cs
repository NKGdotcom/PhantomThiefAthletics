using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetObjUI : MonoBehaviour
{
    [SerializeField] private GameObject setObj;
    [SerializeField] private SetObjKind kind;
    [SerializeField] private int setObjNum;
    [SerializeField] private TextMeshProUGUI textNum;
    [SerializeField] private GameObject enterText;

    private GameObject placingObj;

    private Vector3 mousePos;
    private Vector3 objPos;

    private bool isPlacing;
    private bool waitOneFrame;
    // Start is called before the first frame update
    void Start()
    {
        textNum.text = $"{setObjNum}";

        gameObject.AddComponent<EventTrigger>();
        var _trigger = gameObject.GetComponent<EventTrigger>();
        var _entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        _entry.callback.AddListener((x) => OnClick());
        _trigger.triggers.Add(_entry);
    }

    void Update()
    {
        if(setObjNum < 0)
        {
            enterText.SetActive(true);
        }
        if (isPlacing && placingObj != null)
        {
            mousePos = Input.mousePosition;
            objPos = Camera.main.ScreenToWorldPoint(mousePos);
            objPos = new Vector3(objPos.x, objPos.y, 0);
            placingObj.transform.position = objPos;

            if (!waitOneFrame && Input.GetMouseButtonDown(0))
            {
                isPlacing = false;
                setObjNum--;
                textNum.text = $"{setObjNum}";
                placingObj = null;
            }

            // �t���[����1�񂾂��X�L�b�v����
            if (waitOneFrame) waitOneFrame = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameState.Instance.StatePlay();
            GameObject _parent = transform.parent.gameObject;
            _parent.SetActive(false);
        }
    }

    private void OnClick()
    {
        if (setObjNum <= 0) return;

        if (placingObj != null) Destroy(placingObj); // �O�̂�����Δj��

        placingObj = Instantiate(setObj);
        isPlacing = true;
        waitOneFrame = true; // �����N���b�N�𖳎����邽��1�t���[���X�L�b�v
    }
}