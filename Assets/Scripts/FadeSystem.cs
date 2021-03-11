using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class FadeSystem : MonoBehaviour
 {
    [Header("表示に使うオブジェクト")]
    public GameObject SetObj;
    [Header("切り替えに表示する画像S")]
    public Sprite[] Sprits;

    [Header("表示する座標数値")]
    public int Max_X;
    public int Min_X;
    public int Max_Y;
    public int Min_Y;

    [Header("表示する回数")]
    public int LoopCount;
    [Header("最終画面隠し")]
    public GameObject FameEndSprit;

    List<GameObject> INSpritList = new List<GameObject>(0);//表示したオブジェクトリスト
    bool FadeStart;//フェード開始か
    bool FadeType = true;
    int MaxCount,Cunt=0;
       // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        FameEndSprit.SetActive(false);
    }

    [System.Obsolete]
    void Update()
     {
        if (!FadeStart) return;
        Cunt++;
        if (Cunt > MaxCount)
        {
            FadeStart = false; 
            Cunt = 0;
            if(FadeType) FameEndSprit.SetActive(true);
            return;
        }
        if (FadeType) INSpritList[Cunt - 1].SetActive(!INSpritList[Cunt - 1].active);
        else Destroy(INSpritList[Cunt - 1]);
    }

    public void FadeIN()//フェードイン
    {
        if (FadeStart) return;
        INSpritList = new List<GameObject>(0);//リスト初期化
        for (int i = 0; i < LoopCount; i++)
        {
            float X = Random.Range(Min_X, Max_X + 1);//座標X
            float Y = Random.Range(Min_Y, Max_Y + 1);//座標Y
            int ImagNo = Random.Range(0, Sprits.Length);//画像番号
            GameObject Obj = Instantiate(SetObj);
            Obj.GetComponent<SpriteRenderer>().sprite = Sprits[ImagNo];
            Obj.transform.position = new Vector3(X, Y, 0);
            Obj.transform.parent = transform;
            Obj.SetActive(false);
            INSpritList.Add(Obj);
        }
        FadeStart = true;
        FadeType = true;
        MaxCount = INSpritList.Count;
    }

    public void FadeOUT()//フェードアウト
    {
        if (FadeStart || INSpritList.Count == 0) return;
        FameEndSprit.SetActive(false);
        FadeStart = true;
        FadeType = false;
        MaxCount = INSpritList.Count;
    }
 }

