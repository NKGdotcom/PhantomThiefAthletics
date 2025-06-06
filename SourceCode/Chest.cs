using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using System;
using Cinemachine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject topChest;
    [SerializeField] private float openAngle = 70f;
    [SerializeField] private float openSpeed = 1.0f;

    [SerializeField] private float gotoGameClearWaitTime = 0.5f;
    [SerializeField] private float cameraToCameraTime = 1.2f;

    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [SerializeField] private CinemachineVirtualCamera gamesceneCamera;
    [SerializeField] private int priorityValue = 20;
    [SerializeField] private int defaultValue = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetTrigger()
    {
        ChestOpen().Forget();
    }
    public async UniTaskVoid ChestOpen()
    {
        float _angle = 0f;
        SoundManager.Instance.PlaySE(SESource.openChest);
        while (_angle < openAngle)
        {
            _angle += openSpeed * Time.deltaTime;
            topChest.transform.localRotation = Quaternion.Euler(_angle, transform.rotation.y, transform.rotation.z);
            await UniTask.Yield();
        }
        await UniTask.Delay(TimeSpan.FromSeconds(gotoGameClearWaitTime));

        gamesceneCamera.Priority = defaultValue;
        resultCamera.Priority = priorityValue;

        await UniTask.Delay(TimeSpan.FromSeconds(cameraToCameraTime));
        ResultManager.Instance.GameClear();
    }
}
