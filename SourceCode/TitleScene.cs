using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject startText;

    private Color redColor = new Color(1, 0, 0);
    private Color whiteColor = new Color(1, 1, 1);

    private void Start()
    {
        SetTriggerGameScene(startText);
        ChangeTextColor(startText);
    }
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
    private void SetTriggerGameScene(GameObject _textObj)
    {
        _textObj.AddComponent<EventTrigger>();
        EventTrigger _eventTrigger = _textObj.GetComponent<EventTrigger>();
        EventTrigger.Entry _entry = new EventTrigger.Entry();
        _entry.eventID = EventTriggerType.PointerClick;
        _entry.callback.AddListener((x) => PlayGameScene());
        _eventTrigger.triggers.Add(_entry);
    }


    private void PlayGameScene()
    {
        SceneManager.LoadScene("Stage1");
    }
}
