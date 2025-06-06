using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("他のクラス")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private StageKey stageKey;

    [Header("マウスを取得するカメラ")]
    [SerializeField] private Camera mainCamera;

    private Rigidbody playerRb;

    private bool isGround = false;
    private bool isClear = false;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    private void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSource.gamescene);
        GameObject[] _taggedObjectList = GameObject.FindGameObjectsWithTag("ObjectSetArea");
        if(_taggedObjectList.Length > 0)
        {
            GameState.Instance.StateSetObject();
        }
        else
        {
            GameState.Instance.StatePlay();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameState.Instance.NowState == GameState.State.play)
        {
            if (!isGround) return;
            MousePlayerAddMove();
        }
    }

    /// <summary>
    /// マウスでプレイヤーを動かす
    /// </summary>
    private void MousePlayerAddMove()
    {
        Ray _ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit _hitPos) && GameState.Instance.NowState == GameState.State.play)
        {
            SoundManager.Instance.PlaySE(SESource.jump);
            Vector3 _directionToClick = _hitPos.point - transform.position;
            Vector3 _repelDIrection = -_directionToClick.normalized;

            playerRb.AddForce(_repelDIrection * playerData.JumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject.CompareTag("Floor"))//床
        {
            isGround = true;
        }
        if (_col.gameObject.CompareTag("Door"))//ドア
        {
            var _doorScript = _col.gameObject.GetComponent<Door>();//varはGame.Gimmick.Door
            int getKeyNum = stageKey.HaveKeyCount;
            _doorScript?.SetTrigger(stageKey.HasKey, getKeyNum);
            stageKey.UseKeys();
        }
        if (_col.gameObject.CompareTag("Chest"))//宝箱(クリア)
        {
            if (!isClear)
            {
                isClear = true;
                GameState.Instance.StateResult();
                var _chestScript = _col.gameObject.GetComponent<Chest>();
                _chestScript?.SetTrigger();
            }
        }
    }
    private void OnCollisionExit(Collision _col)
    {
        if (_col.gameObject.CompareTag("Floor"))//床
        {
            isGround = false;
        }
    }
    private void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.CompareTag("Key"))//鍵
        {
            var _keyScript = _col.GetComponent<KeyFallower>();
            _keyScript?.SetTarget(this.gameObject);
            stageKey.AddListHaveKey(_col.gameObject);
        }
    }
}