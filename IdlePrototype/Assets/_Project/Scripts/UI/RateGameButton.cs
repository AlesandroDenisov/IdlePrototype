using UnityEngine;

public class RateGameButton : MonoBehaviour
{
    public void OnRateButtonClick()
    {
#if UNITY_IPHONE

        //Application.OpenURL("https://apps.apple.com/us/app/go-only-up-parkour-3d/id6451052536");

#elif UNITY_ANDROID
#if RUSTORE
          Application.OpenURL("https://apps.rustore.ru/app/com.jcg.RussianRacer");
#else
          Application.OpenURL("https://play.google.com/store/apps/details?id=com.JC.RacerRussia");
#endif

#elif UNITY_WEBGL
        Application.OpenURL("https://yandex.ru/games/app/279052");
#endif
    }
}