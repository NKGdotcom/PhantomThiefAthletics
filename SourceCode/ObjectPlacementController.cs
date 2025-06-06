using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacementController : MonoBehaviour
{
    public GameObject objectPrefab,objectPrefab2,key,mono1,mono2;
    public Button placeButton,placeButton2,reset,start,retry;
    public int maxObjectCount1 ; // �ő�̃I�u�W�F�N�g��
    public int maxObjectCount2 ; // �ő�̃I�u�W�F�N�g��
    private int initialMaxObjectCount1;
    private int initialMaxObjectCount2;
    private int currentObjectCount1 = 0; // ���݂̃I�u�W�F�N�g��
    private int currentObjectCount2 = 0; // ���݂̃I�u�W�F�N�g��
    [SerializeField]
    private GameObject GameOverUI;
    public bool isFollowing=true;// �����Ǐ]�����ǂ����̃t���O

    [SerializeField] GameObject player;
    public AudioClip sound1, sound2;
    private bool isPlacingObject1 = false;
    private bool isPlacingObject2 = false;
    private Vector3 playerInitialPosition; // �����ʒu��ۑ�����ϐ�
    private Coroutine countdownCoroutine;

    public Text timeTexts;
    
    float totalTime =60;
    int retime;




    void Start()
    {totalTime =
        initialMaxObjectCount1 = maxObjectCount1;
        initialMaxObjectCount2 = maxObjectCount2;
        placeButton.gameObject.SetActive(true);
        placeButton2.gameObject.SetActive(true);
        reset.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
        timeTexts.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        // Object Placement �𖳌��ɂ���
        key.gameObject.SetActive(false);
       
        isPlacingObject1 = false;
        placeButton.onClick.AddListener(ToggleObjectPlacement);
        placeButton2.onClick.AddListener(ToggleObjectPlacement2);
        reset.onClick.AddListener(Reset);
        start.onClick.AddListener(FirstStageStart);
        retry.onClick.AddListener(Retry);
        player = GameObject.Find("Player");
        playerInitialPosition = player.transform.position;
         placeButton.GetComponentInChildren<Text>().text = maxObjectCount1.ToString();
        placeButton2.GetComponentInChildren<Text>().text = maxObjectCount2.ToString();
    }

    void Update()
    {
        
        if (isPlacingObject1)
        {
            if (Input.GetMouseButtonDown(0))
            {
               
                
                if (currentObjectCount1 < maxObjectCount1)
                {
                    
                    maxObjectCount1--;
                   
                    // UI�{�^���ȊO�̏ꏊ���N���b�N������I�u�W�F�N�g��ݒu
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("Ray hit object: " + hit.collider.gameObject.name);
                        Vector3 spawnPosition = new Vector3(hit.point.x, hit.point.y, 0f);
                        Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
                       
                        placeButton.GetComponentInChildren<Text>().text = maxObjectCount1.ToString();
                    }
                    
                }
                

                return;
            }
        }
        else if (isPlacingObject2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                

                if (currentObjectCount2 < maxObjectCount2)
                {
                    maxObjectCount2--;
                   
                    // UI�{�^���ȊO�̏ꏊ���N���b�N������I�u�W�F�N�g��ݒu
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 spawnPosition = new Vector3(hit.point.x, hit.point.y, 0f);
                        Instantiate(objectPrefab2, spawnPosition, Quaternion.Euler(0f,0f,90f));

                        placeButton2.GetComponentInChildren<Text>().text = maxObjectCount2.ToString();
                    }

                }


                return;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "kill")
        {
            
            this.gameObject.SetActive(false);
            
            retry.gameObject.SetActive(true);
        }
        if ( collision.gameObject.tag == "key")
        {
        AudioSource.PlayClipAtPoint(sound2, transform.position);
        collision.gameObject.SetActive(false);
            isFollowing = false; // key�ɓ���������isFollowing��false�ɂ���
        key.SetActive(false);        
        }
    }
    void ToggleObjectPlacement()
    {
    // UI�{�^���������ƃI�u�W�F�N�g�̐ݒu��Ԃ�؂�ւ���
    isPlacingObject1 = !isPlacingObject1;
    isPlacingObject2 = false;
    Debug.Log("Object Placement: " + (isPlacingObject1 ? "Enabled" : "Disabled"));   
    }
    void ToggleObjectPlacement2()
    {
        // UI�{�^���������ƃI�u�W�F�N�g�̐ݒu��Ԃ�؂�ւ���
        isPlacingObject2 = !isPlacingObject2;
        isPlacingObject1 = false;
        Debug.Log("Object Placement: " + (isPlacingObject2 ? "Enabled" : "Disabled"));
    }
    void Reset()
    {
        // �V�[���ɑ��݂���I�u�W�F�N�g��S�č폜
        GameObject[] objectsInScene = GameObject.FindGameObjectsWithTag("Set");
        foreach (GameObject obj in objectsInScene)
        {
            Destroy(obj);
        }

        // ���݂̃I�u�W�F�N�g�������Z�b�g
        maxObjectCount1 = initialMaxObjectCount1;
        maxObjectCount2= initialMaxObjectCount2;
        this.gameObject.SetActive(true);
        placeButton.gameObject.SetActive(true);
        placeButton2.gameObject.SetActive(true);
        reset.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
        timeTexts.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        // Object Placement �𖳌��ɂ���
        player.transform.position = playerInitialPosition;
        mono1.gameObject.SetActive(true);
        mono2.gameObject.SetActive(true);
        isPlacingObject1 = false;
        isPlacingObject2 = false;
        placeButton.GetComponentInChildren<Text>().text = maxObjectCount1.ToString();
        placeButton2.GetComponentInChildren<Text>().text = maxObjectCount2.ToString();
        GameOverUI.SetActive(false);
       

    }
    void FirstStageStart()
    {

        placeButton.gameObject.SetActive(false);
        placeButton2.gameObject.SetActive(false);
        reset.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        timeTexts.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
        isPlacingObject1 = false;
        isPlacingObject2 = false;
        mono1.gameObject.SetActive(false);
        mono2.gameObject.SetActive(false);
        countdownCoroutine = StartCoroutine(Countdown());
    }
    void Retry()
    {
        Reset();
    }
    IEnumerator Countdown()
    {
        while (totalTime > 0&& !GameOverUI.activeSelf)
        {
            totalTime -= Time.deltaTime;
            retime = (int)totalTime;
            timeTexts.text = retime.ToString();
            yield return new WaitForSeconds(1); // 1�b�҂�
        }
        AudioSource.PlayClipAtPoint(sound1, transform.position);
        // �J�E���g�_�E�����I�������Ƃ��̏����������ɒǉ�
        timeTexts.text ="�I��";
        GameOverUI.SetActive(true);
    }
}

