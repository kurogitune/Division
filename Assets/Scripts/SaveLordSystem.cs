using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveMapData//セーブするデータ  色々追加できるZE!!
{
    public int MapNo = 1;//マップ番号(初期データは1)
}

namespace DataSaveLoadSy
{
    public class SaveLordSystem : MonoBehaviour
    {
        static string DataFileName = "/Map_Data.json";//保存するファイル名
        static string SaveFile = "/Data";//保存先ファイルパス
        public static void Save(SaveMapData data)//マップデータセーブ
        {
            try
            {
                StreamWriter strw;
                string jos = JsonUtility.ToJson(data);
                strw = new StreamWriter(new FileStream(Application.dataPath + SaveFile+DataFileName, FileMode.Create));//ファイルがある場合上書き
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
                strw = new StreamWriter(new FileStream(Application.dataPath  +SaveFile + DataFileName, FileMode.OpenOrCreate));
                strw.Write(jos);
                strw.Flush();
                strw.Close();
            }
        }

        public static SaveMapData Lord()//マップデータロード
        {
            try
            {
                StreamReader str;
                str = new StreamReader(new FileStream(Application.dataPath + SaveFile + DataFileName, FileMode.Open));//ファイル開く
                string json = str.ReadToEnd();
                str.Close();
                return JsonUtility.FromJson<SaveMapData>(json);
            }
            catch
            {
                SaveMapData data = new SaveMapData();
                Save(data);

                StreamReader str;
                str = new StreamReader(new FileStream(Application.dataPath + SaveFile + DataFileName, FileMode.Open));
                string json = str.ReadToEnd();
                str.Close();
                return JsonUtility.FromJson<SaveMapData>(json);
            }
        }
    }
}

