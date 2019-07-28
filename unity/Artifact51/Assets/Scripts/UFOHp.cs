using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UFOHp : EnemyHP
{
    Material mat;
    Color og, og_2, hurt = Color.white;
    Sequence flashTween;
    Sequence flashTween_2;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        mat = GetComponent<MeshRenderer>().material;
        og = mat.color;
        og_2 = mat.GetColor("_EmissionColor");
    }

    public override void TakeDamage(int value = 1)
    {
        Flash();
        base.TakeDamage(value);
    }
    void Flash()
    {
        flashTween.Kill();
        flashTween_2.Kill();

        flashTween = DOTween.Sequence()
        .Append(mat.DOColor(hurt, .1f))
        .Append(mat.DOColor(og, 0.1f))
        .SetLoops(10);

        flashTween_2 = DOTween.Sequence()
        .Append(mat.DOColor(hurt, "_EmissionColor", .1f))
        .Append(mat.DOColor(og_2, "_EmissionColor", 0.1f))
        .SetLoops(10);
    }
}
