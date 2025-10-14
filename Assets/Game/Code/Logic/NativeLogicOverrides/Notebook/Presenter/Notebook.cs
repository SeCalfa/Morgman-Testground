using System.Collections.Generic;
using System.Linq;
using HFPS.Systems;
using ThunderWire.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides.Notebook.Presenter
{
    public class Notebook : MonoBehaviour
    {
        [SerializeField] private Transform titlesContent;
        [SerializeField] private Image contentImage;
        [Space]
        [SerializeField] private GameObject titlePrefab;

        private CanvasGroup _canvasGroup;
        private InputAction _input;
        private bool _isActive;
        private readonly List<Note> _notes = new();

        public static Notebook Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }

            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            
            _input = InputHandler.Instance.inputActionAsset.FindAction("Notebook", true);
        }

        private void Update()
        {
            if (!_input.WasPressedThisFrame() || HFPS_GameManager.Instance.isInventoryShown || HFPS_GameManager.Instance.isPaused) return;
            
            if (_isActive)
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
                HFPS_GameManager.Instance.LockPlayerControls(true, true, false, 3, false);
            }
            else
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.blocksRaycasts = true;
                HFPS_GameManager.Instance.LockPlayerControls(false, false, true, 3, true);
            }
                
            _isActive = !_isActive;
        }

        public void AddNote(string title, Sprite content)
        {
            var note = new Note
            {
                Title = title,
                Sprite = content
            };
            
            _notes.Add(note);
            
            Render();
        }

        private void Render()
        {
            var allTitleChildren = titlesContent.GetComponentsInChildren<Transform>(includeInactive: true)
                .Where(t => t != titlesContent.transform)
                .ToArray();
            
            foreach (var title in allTitleChildren)
            {
                Destroy(title.gameObject);
            }
            
            foreach (var note in _notes)
            {
                var newTitle = Instantiate(titlePrefab, titlesContent, true).GetComponent<NoteTitle>();
                newTitle.Init(note.Title);

                newTitle.Button.onClick.AddListener(() =>
                {
                    contentImage.enabled = true;
                    contentImage.sprite = note.Sprite;
                });
            }
        }
    }
}
