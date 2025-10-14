using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides.Notebook.Presenter
{
    public class NoteTitle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Button button;

        public Button Button => button;

        public void Init(string text)
        {
            title.text = text;
        }
    }
}