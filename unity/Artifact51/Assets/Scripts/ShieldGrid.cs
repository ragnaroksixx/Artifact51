﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ShieldGrid : MonoBehaviour
{
    public List<Shield> shields;
    public Shield centerShield;
    ShieldGenerator source;
    float duration = 10;
    float spawnDuration = .2f;
    public bool isAnimatingUp;
    Tween animateUp;
    public Image i;
    public void Init(ShieldGenerator s)
    {
        source = s;
        foreach (Shield shield in shields)
        {
            shield.Init(this, 3);
        }
        centerShield.Init(this, 9999);
        duration += spawnDuration;
        Vector3 maxScale = transform.localScale;
        transform.localScale = Vector3.one * 0.001f;//unity doesn't like 0 here for some reason
        isAnimatingUp = true;
        animateUp = transform.DOScale(maxScale, spawnDuration);
        animateUp.OnComplete(() => { isAnimatingUp = false; });
        i.DOFillAmount(0, duration + spawnDuration);
        i.DOColor(Color.red, duration + spawnDuration);
    }
    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            DestroyGrid();
        }
    }
    private void DestroyGrid()
    {
        foreach (Shield s in shields)
            s.DestroyShield();
        if (centerShield)
            centerShield.DestroyShield();
        source.shieldsInWorld--;
        Destroy(this.gameObject);
    }
    public void RemoveShield(Shield s)
    {
        if (shields.Contains(s))
            shields.Remove(s);
    }

    public void Return(Shield shield)
    {
        if (shield == centerShield)
        {
            isAnimatingUp = false;
            animateUp.Kill();
            foreach (Shield s in shields)
                s.DisableCollision();
            centerShield.DisableCollision();
            Tween t = transform.DOScale(Vector3.zero, spawnDuration / 1.5f);
            t.OnComplete(() =>
            {
                DestroyGrid();
            });
        }else
        {
            shields.Remove(shield);
            shield.DestroyShield();
        }

    }
}
