using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OptionData//オプションデータ
{
    public float BGMvolume = 1;//曲音量
    public float SEvolume = 1;//効果音音量
    public float Voicevolume = 1;//声音量
}

namespace OptionSy
{
    public class OptionSystem : MonoBehaviour
    {
        static string DataFileName = "/Option_Data.json";//保存するファイル名
        static string SaveFile = "/Data";//保存先ファイルパス
        public static void Save(OptionData data)//オプションセーブ
        {
            try
            {
                StreamWriter strw;
                string jos = JsonUtility.ToJson(data);
                strw = new StreamWriter(new FileStream(Application.dataPath + SaveFile + DataFileName, FileMode.Create));//ファイルがある場合上書き
                strw.Write(jos);
                strw.Flush();
                strw.Close();
            }
            catch
            {
                if (!Directory.Exists(Application.dataPath + SaveFile))//フォルダーがある確認 ない場合フォルダー作成
                {
                    Directory.CreateDirectory(Application.dataPath + SaveFile);//フォルダー作成
                }
   
                StreamWriter strw;
                string jos = JsonUtility.ToJson(data);
                strw = new StreamWriter(new FileStream(Application.dataPath + SaveFile+ DataFileName, FileMode.OpenOrCreate));
                strw.Write(jos);
                strw.Flush();
                strw.Close();
            }
        }

        public static OptionData Lord()//オプションロード
        {
            try
            {
                StreamReader str;
                str = new StreamReader(new FileStream(Application.dataPath + SaveFile + DataFileName, FileMode.Open));//ファイル開く
                string json = str.ReadToEnd();
                str.Close();
                return JsonUtility.FromJson<OptionData>(json);
            }
            catch
            {
                OptionData data = new OptionData();
                Save(data);
                StreamReader str;
                str = new StreamReader(new FileStream(Application.dataPath + SaveFile + DataFileName, FileMode.Open));
                string json = str.ReadToEnd();
                str.Close();
                return JsonUtility.FromJson<OptionData>(json);
            }
        }
    }
}

