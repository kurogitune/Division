using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataSaveLoadSy;

public class GameSystem : MonoBehaviour
{
    [Header("自身のマップ番号")]
    public int MapNo;
    [Header("再生BGM")]
    public AudioClip BGM;
    [Header("シーン切り替え時間")]
    public float SeenChangeTime;
    public AudioSource BGMPlayer;
    public AudioSource SEPLayer;
    bool SeenChangeStart;//シーン切り替え開始か
    FadeSystem FadeSy;//フェードシステム
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
        if (Input.GetKeyDown(KeyCode.Space)&!SeenChangeStart)
        {
            SeenChangeStart = true;
            Invoke(nameof(SeenChang), SeenChangeTime);
        }
    }

    void SeenChang()
    {
        SaveMapData MapD = new SaveMapData
        {
            MapNo = MapNo + 1
        };
        SaveLordSystem.Save(MapD);//次マップの番号保存
        SceneManager.LoadScene(string.Format("{0}", MapD.MapNo));//{0}の前に共用の名前を代入
    }

    public void SEPlay(AudioClip Se)//指定効果音再生
    {
        SEPLayer.PlayOneShot(Se);
    }
}
