using HFPS.Systems;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.Notebook
{
    public class NoteObject : MonoBehaviour
    {
        [SerializeField] private Papers papers;
        
        private InteractiveItem _interactiveItem;

        private void Awake()
        {
            _interactiveItem = GetComponent<InteractiveItem>();
        }

        public void UseObject()
        {
            Presenter.Notebook.Instance.AddNote(papers);
            Presenter.Notebook.Instance.AddPaperList(papers);
        }
    }
}
