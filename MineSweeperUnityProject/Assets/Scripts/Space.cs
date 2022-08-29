using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Space : ItemTarget
{
    [SerializeField]
    public SpaceData data;

    public GameObject text;
    public GameObject model;

    public List<Space> siblings;

    [SerializeField] GameObjectEvent _gameEvent;

    void Awake()
    {
        data = new SpaceData();
        data.type = 0;
    }
    void Start()
    {
        UpdateDisplay();
    }

    public override void LeftClicked()
    {
        if (data.Click())
        {
            Debug.Log("bomb hit end game");
        }
        else
        {
            Debug.Log("hit number: " + data.type + " at " + data.location);
            if (data.type == 0)
            {
                FlipOpenArea();
            }
        }
        UpdateDisplay();
        _gameEvent.Raise(this.gameObject);
    }

    public override void RightClicked()
    {
        var didFlag = data.Flag();
        Debug.Log("flagging space: " + data.location);
        UpdateDisplay();

        _gameEvent.Raise(this.gameObject);
    }

    public void UpdateDisplay()
    {
        var t = text.GetComponent<TextMeshProUGUI>();
        t.color = Color.white;
        if (!data.isClicked)
        {
            t.SetText("");
            if (data.isFlagged)
            {
                t.SetText("F");
                t.color = Color.green;
            }
            return;
        }

        if (data.IsBomb())
        {
            t.SetText("B");
            t.color = Color.red;
        }
        else
        {
            t.SetText(data.type > 0 ? data.type + "" : "");
        }
        model.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    public void FlipOpenArea()
    {
        foreach (var s in siblings)
        {
            if (!s.data.isFlagged && !s.data.isClicked)
            {
                s.data.Click();
                s.UpdateDisplay();
                s.FlipOpenArea();
            }
        }
    }
}
