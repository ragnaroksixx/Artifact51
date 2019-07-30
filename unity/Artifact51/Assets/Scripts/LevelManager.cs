using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelManager : MonoBehaviour
{
    public bool isGameStarted = false;
    public bool isGameOver = false;
    public static LevelManager Instance;
    public Level[] levels;
    int levelIndex = -1;
    int numKills;

    float darkDensity = 1;
    float lightDensity = .15f;
    CoroutineHandler updateLoop;
    public GameObject alienPrefab;
    public AlienUFO ufoPrefab;
    Level CurrentLevel { get { if (levelIndex < 0) return null; return levels[levelIndex]; } }
    public Transform[] ufoPoints, groundPoints;
    float groundRange = 14;
    float intensity = 1;
    public GameObject destroyOnStart;
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
        updateLoop = new CoroutineHandler(this);

    }
    public void NextLevel()
    {
        if (CurrentLevel != null && CurrentLevel.loop)
        {
            intensity += 0.2f;
        }
        else
        {
            levelIndex++;
        }
        StartLevel();
    }

    public void StartLevel()
    {
        numKills = 0;
        updateLoop.StartCoroutine(LevelUpdateLoop());
    }

    IEnumerator LevelUpdateLoop()
    {
        float time = 0;
        int nextUFO = 0;
        float freqTrack = CurrentLevel.units.frequency;
        freqTrack /= intensity;
        while (true)
        {
            time += Time.deltaTime;
            freqTrack -= Time.deltaTime;
            if (CurrentLevel.ufos.Length > nextUFO && CurrentLevel.ufos[nextUFO].time < time)
            {
                SpawnUFO(CurrentLevel.ufos[nextUFO]);
                nextUFO++;
            }
            if (freqTrack <= 0)
            {
                freqTrack = CurrentLevel.units.frequency + Random.Range(0, 1);
                freqTrack /= intensity;
                SpawnUnit();
            }
            yield return null;
        }
    }
    public void SpawnUFO(UFOData data)
    {
        AlienUFO ufo = Instantiate(ufoPrefab);
        Vector3 p1 = Vector3.zero, p2 = Vector3.zero;
        GetRandomPoints(ref p1, ref p2);
        ufo.dropStart = p1;
        ufo.dropEnd = p2;
        ufo.dropAliens = new List<GameObject>();
        for (int i = 0; i < data.numUnits; i++)
        {
            ufo.dropAliens.Add(alienPrefab);
        }
        ufo.transform.position = ufo.dropStart + (Vector3.up * 45);
        ufo.gameObject.SetActive(true);
    }
    public void SpawnUnit()
    {
        Transform t = groundPoints[Random.Range(0, groundPoints.Length)];
        Vector3 pos = t.position;
        pos += t.right * Random.Range(-groundRange, groundRange);
        GameObject.Instantiate(alienPrefab, pos, Quaternion.identity);
    }
    public void IncrementKill()
    {
        numKills++;
        if (numKills >= Mathf.CeilToInt(CurrentLevel.NumUnits() * intensity))
        {
            NextLevel();
        }
    }

    public void GetRandomPoints(ref Vector3 p1, ref Vector3 p2)
    {
        int a = Random.Range(0, ufoPoints.Length);
        int b = a;
        if (Random.Range(0.0f, 1.0f) > 0.5f)
            b = a + 1;
        else
            b = a - 1;
        if (b < 0)
            b = ufoPoints.Length - 1;
        if (b >= ufoPoints.Length)
        {
            b = 0;
        }
        p1 = ufoPoints[a].position;
        p2 = ufoPoints[b].position;
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
        Destroy(destroyOnStart);
        Tweener t = DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, lightDensity, 1);
        t.OnComplete(() => { OnEffectEnd(); });
    }

    void OnEffectEnd()
    {
        NextLevel();
    }
}

[System.Serializable]
public class Level
{
    public UFOData[] ufos;
    public GroundUnitData units;
    public bool loop = false;
    public int NumUnits()
    {
        int result = 0;
        foreach (UFOData item in ufos)
        {
            result += item.numUnits;
        }

        result += units.numUnits;
        return result;
    }
}

[System.Serializable]
public class UFOData
{
    public int numUnits;
    public float time;
}
[System.Serializable]
public class GroundUnitData
{
    public int numUnits;
    public float frequency;
}
