using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShadowSpwaner : MonoBehaviour
{
    public static ShadowSpwaner Instance { get; private set; }

    private Dictionary<GameObject, List<SpriteRenderer>> shadowPool = new Dictionary<GameObject, List<SpriteRenderer>>();

    private void Awake()
    {
        Instance = GetComponent<ShadowSpwaner>();
    }

    public void ShowShadow(GameObject obj, int num, float interval) 
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        if (!shadowPool.ContainsKey(obj))
        {
            shadowPool.Add(obj, new List<SpriteRenderer>());
            for (int i = 0; i < num; i++)
            {
                GameObject shadowObj = new GameObject(obj.name + "_Shadow_" + i);
                shadowObj.transform.parent = transform;
                shadowPool[obj].Add(shadowObj.AddComponent<SpriteRenderer>());
            }
        }
        else if (shadowPool[obj].Count < num) 
        {
            int count = shadowPool[obj].Count;
            for (int i = count; i < num; i++)
            {
                GameObject shadowObj = new GameObject(obj.name + "_Shadow_" + i);
                shadowObj.transform.parent = transform;
                shadowPool[obj].Add(shadowObj.AddComponent<SpriteRenderer>());
            }
        }

        StartCoroutine(ShowShadowOneByOne(sr, num, interval));
    }

    IEnumerator ShowShadowOneByOne(SpriteRenderer sr, int num, float interval) 
    {
        for (int i = 0; i < num; i++) 
        {
            shadowPool[sr.gameObject][i].transform.position = sr.transform.position;
            shadowPool[sr.gameObject][i].transform.localEulerAngles = sr.transform.localEulerAngles;
            shadowPool[sr.gameObject][i].gameObject.SetActive(true);
            shadowPool[sr.gameObject][i].sprite = sr.sprite;
            shadowPool[sr.gameObject][i].color = Color.white;
            shadowPool[sr.gameObject][i].DOFade(0, interval / num * 3);
            yield return new WaitForSeconds(interval / num);
        }

        for (int i = 0; i < num; i++)
        {
            shadowPool[sr.gameObject][i].gameObject.SetActive(false);
        }
    }
}
