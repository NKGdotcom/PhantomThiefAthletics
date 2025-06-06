using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float rotateAngle = -90;
    [SerializeField] private float rotateSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// �h�A���J���鏀��
    /// </summary>
    /// <param name="_hasKey"></param>
    /// <param name="_haveKeyNum"></param>
    public void SetTrigger(bool _hasKey, int _haveKeyNum)
    {
        if (_hasKey && _haveKeyNum > 0)
        {
            OpenDoor().Forget();
        }
    }
    /// <summary>
    /// �h�A���J����
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid OpenDoor()
    {
        float _angle = 0;
        SoundManager.Instance.PlaySE(SESource.openDoor);
        while (_angle > rotateAngle)
        {
            _angle -= rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, _angle, 0);
            await UniTask.Yield();
        }
    }
}
