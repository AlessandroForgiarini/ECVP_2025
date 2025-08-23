using UnityEngine;
using UnityEngine.UI;

namespace ECVP_2025_Unity_Workshop
{
    [RequireComponent(typeof(Button))]
    public class PlayClickEffect : MonoBehaviour
    {
        private void Awake()
        {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(delegate
            {
                FantasyAudioManager.Instance.PlayUIClickEffect();
            });
        }
    }
}