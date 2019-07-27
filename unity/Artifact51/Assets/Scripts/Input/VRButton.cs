using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRButton : Button
{
    const float DELAY_TIME = 1;
    CoroutineHandler delayActivationCo;
    Image _image;

    public bool IsActived
    {
        get
        {
            return !DelayActivationCo.IsRunning;
        }
    }

    public CoroutineHandler DelayActivationCo
    {
        get
        {
            if (delayActivationCo == null)
                delayActivationCo = new CoroutineHandler(this);
            return delayActivationCo;
        }
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        SetCollider();
 }
    public virtual BoxCollider SetCollider()
    {
        BoxCollider col = GetComponent<BoxCollider>();

        if (col == null)
            col = gameObject.AddComponent<BoxCollider>();

        RectTransform rect = GetComponent<RectTransform>();

        col.size = rect.sizeDelta;// * 0.9f;
        col.size += new Vector3(0, 0, 0.001f);

        Vector2 pivotDelta = Vector2.one * 0.5f;
        pivotDelta -= rect.pivot;

        col.center = new Vector3(pivotDelta.x * col.size.x, pivotDelta.y * col.size.y, -col.size.z / 2);
        return col;
    }
    IEnumerator DelayCo()
    {
        yield return new WaitForSeconds(DELAY_TIME);
        DelayActivationCo.StopCoroutine();
    }
    protected override void Start()
    {
        base.Start();
        _image = targetGraphic as Image;
        Dehighlight();
    }
    public virtual void Highlight()
    {
        _image.color = colors.highlightedColor;
    }

    public virtual void Dehighlight()
    {
        _image.color = colors.normalColor;
    }

    public virtual void OnTouchHeld(Vector3 pos)
    {

    }

    public virtual void OnTouchUp()
    {
        if (IsActived)
        {
            Dehighlight();
            onClick?.Invoke();
        }
    }
}
