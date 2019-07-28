using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelManager : MonoBehaviour
{
    public bool isGameStarted = false;
    public bool isGameOver = false;
    public static LevelManager Instance;
    public AlienUFO enemyspawner;
    float darkDensity = 1;
    float lightDensity = .15f;
    private void Awake()
    {
        Instance = this;
#if UNITY_ANDROID && !UNITY_EDITOR
        darkDensity /= 2;
        lightDensity /= 2;
#endif

        RenderSettings.fogColor = Color.black;
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = darkDensity;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void StartGame()
    {
        if (Instance.isGameStarted) return;
        Instance.StartGameImp();
    }
    public static void GameOver()
    {
        if (Instance.isGameOver) return;
        Instance.isGameOver = true;
        Tweener t = DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, Instance.darkDensity, 1);
        t.OnComplete(() => { SceneLoader.ReloadScene(); });
    }
    void StartGameImp()
    {
        isGameStarted = true;

        Tweener t = DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, lightDensity, 1);
        t.OnComplete(() => { OnEffectEnd(); });
    }

    void OnEffectEnd()
    {
        enemyspawner.gameObject.SetActive(true);
    }
}
