using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origin : MonoBehaviour
{
    public static Origin instance;

    void Awake()
    {
		if (instance != null) {//APlayerManagerインスタンスが存在したら
			Destroy(this.gameObject);//今回インスタンス化したPlayerManagerを破棄
		
		} else if (instance == null){//PlayerManagerインスタンスがなかったら
			instance = this;//このPlayerManagerをインスタンスとする
		}

        //シーンを跨いでもAudioManagerインスタンスを破棄しない
		DontDestroyOnLoad (this.gameObject);
    }

    public　void DeleteInformation()
    {
        Destroy(instance.gameObject);
    }
}
