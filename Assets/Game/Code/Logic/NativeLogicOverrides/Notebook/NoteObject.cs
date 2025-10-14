using HFPS.Systems;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.Notebook
{
    public class NoteObject : MonoBehaviour
    {
        [SerializeField] private string noteTitle;
        [SerializeField] private Sprite contentSprite;
        
        private InteractiveItem _interactiveItem;

        private void Awake()
        {
            _interactiveItem = GetComponent<InteractiveItem>();
        }

        public void UseObject()
        {
            Presenter.Notebook.Instance.AddNote(noteTitle, contentSprite);
        }
    }
}
