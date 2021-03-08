using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSaveLoadSy;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip BGM;
    [Header("シーン切り替え時間")]
    public float SeenChangeTime;
    public AudioSource BGMPlayer;//BGM再生機
    public AudioSource SEPLayer;//SE再生機
    public GameObject PlayerObj;//プレイヤーオブジェクト
    public FadeSystem FadeSy;//フェードシステム
    [Header("プレイヤーが落ちる速度")]
    public float PlayerDownSpeed;
    bool Seenchange,Fade;
    // Start is called before the first frame update
    void Start()
    {
        FadeSy = GameObject.Find("FadeSystem").GetComponent<FadeSystem>();
        BGMPlayer.clip = BGM;
        BGMPlayer.Play();
        FadeSy.FadeOUT();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown&!Seenchange)
        {
            Seenchange = true;
            PlayerObj.GetComponent<Rigidbody2D>().simulated = true;
            Invoke(nameof(SeenChang), SeenChangeTime);
        }

        if (Seenchange& PlayerObj.transform.position.y<-8&! Fade)
        {
            Fade = true;
            FadeSy.FadeIN();
        }

    }

    void SeenChang()
    {
        SaveMapData MapD = SaveLordSystem.Lord();//データが無い場合は(1)が読み込まれる 　　データがある場合は保存されている番号が読み込まれる
        SaveLordSystem.Save(MapD);//次マップの番号保存
        SceneManager.LoadScene(string.Format("{0}", MapD.MapNo));//{0}の前に共用の名前を代入
    }

    public void SEPlay(AudioClip Se)//指定効果音再生
    {
        SEPLayer.PlayOneShot(Se);
    }
}
