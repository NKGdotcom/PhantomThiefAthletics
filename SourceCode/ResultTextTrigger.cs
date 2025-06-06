using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ResultTextTrigger : MonoBehaviour
{
    public static ResultTextTrigger Instance {  get; private set; }

    [SerializeField] private GameObject emptyTextSummarize;
    [SerializeField] private GameObject nextStageTextObj;
    [SerializeField] private GameObject retryStageTextObj;
    [SerializeField] private GameObject titleTextObj;
    private string nowStageName;
    private string nowStageNumString;
    private int nowStageNum;

    private Color redColor = new Color(1, 0, 0);
    private Color whiteColor = new Color(1,1,1);


    private int nextStageNum = 1;

    [SerializeField] private float setactiveTime = 1.333f;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        nowStageName = SceneManager.GetActiveScene().name;
        nowStageNumString = nowStageName.Replace("Stage", "");
        nowStageNum = int.Parse(nowStageNumString);
        nextStageNum += nowStageNum;

        SetEventTrigger(nextStageTextObj, nextStageNum);
        SetEventTrigger(retryStageTextObj, nowStageNum);
        SetTriggerTitle(titleTextObj);

        ChangeTextColor(nextStageTextObj);
        ChangeTextColor(retryStageTextObj);
        ChangeTextColor(titleTextObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// テキストの色を変える準備
    /// </summary>
    /// <param name="_textObj"></param>
    private void ChangeTextColor(GameObject _textObj)
    {
        _textObj.AddComponent<EventTrigger>();
        TextMeshProUGUI tmp = _textObj.GetComponent<TextMeshProUGUI>();

        if (tmp == null)
        {
            Debug.LogWarning("TextMeshProUGUI が見つかりません");
            return;
        }

        EventTrigger _eventTrigger = _textObj.GetComponent<EventTrigger>();

        void AddTrigger(EventTriggerType type, Color color)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener((x) => HandlePointerColorChange(_textObj, color));
            _eventTrigger.triggers.Add(entry);
        }

        AddTrigger(EventTriggerType.PointerEnter, redColor);
        AddTrigger(EventTriggerType.PointerExit, whiteColor);
    }
    /// <summary>
    /// 色を変える
    /// </summary>
    /// <param name="_textObj"></param>
    /// <param name="targetColor"></param>
    private void HandlePointerColorChange(GameObject _textObj, Color targetColor)
    {
        TextMeshProUGUI tmp = _textObj.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            ChangeColor(tmp, targetColor).Forget();
        }
    }
    private async UniTaskVoid ChangeColor(TextMeshProUGUI text, Color targetColor)
    {
        float duration = 0.3f; // 色変化の所要時間
        float time = 0f;
        Color startColor = text.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            text.color = Color.Lerp(startColor, targetColor, time / duration);
            await UniTask.Yield();
        }
        text.color = targetColor;
    }
    /// <summary>
    /// タイトルに行く用の設定
    /// </summary>
    /// <param name="_textObj"></param>
    private void SetTriggerTitle(GameObject _textObj)
    {
        _textObj.AddComponent<EventTrigger>();
        EventTrigger _eventTrigger = _textObj.GetComponent<EventTrigger>();
        EventTrigger.Entry _entry = new EventTrigger.Entry();
        _entry.eventID = EventTriggerType.PointerClick;
        _entry.callback.AddListener((x) => PlayTitleScene()); 
        _eventTrigger.triggers.Add(_entry);
    }
    /// <summary>
    /// テキストを設定し、どのシーンに移るかを決める
    /// </summary>
    /// <param name="_textObj"></param>
    /// <param name="_nowStageNum"></param>
    private void SetEventTrigger(GameObject _textObj, float _nowStageNum)
    {
        _textObj.AddComponent<EventTrigger>();
        EventTrigger _eventTrigger = _textObj.GetComponent<EventTrigger>();
        EventTrigger.Entry _entry = new EventTrigger.Entry();
        _entry.eventID = EventTriggerType.PointerClick;
        _entry.callback.AddListener((x) => PlayScene(_nowStageNum));
        _eventTrigger.triggers.Add(_entry);
    }
    /// <summary>
    /// 新しいシーンもしくは同じシーンを再生
    /// </summary>
    /// <param name="_playeSceneNum"></param>
    private void PlayScene(float _playeSceneNum)
    {
        SceneManager.LoadScene($"Stage{_playeSceneNum}");
    }
    private void PlayTitleScene()
    {
        SceneManager.LoadScene("Opening");
    }
    public async UniTaskVoid SetActiveText()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(setactiveTime));
        emptyTextSummarize.SetActive(true);
    }
}
