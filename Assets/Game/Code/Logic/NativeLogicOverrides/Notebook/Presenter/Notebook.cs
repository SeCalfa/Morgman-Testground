using System.Collections.Generic;
using System.Linq;
using Game.Code.Logic.SO;
using HFPS.Systems;
using Newtonsoft.Json.Linq;
using ThunderWire.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides.Notebook.Presenter
{
    public class Notebook : MonoBehaviour, ISaveable
    {
        [SerializeField] private NotebookData notebookData;
        [Space]
        [SerializeField] private Transform titlesContent;
        [SerializeField] private Image contentImage;
        [Space]
        [SerializeField] private GameObject titlePrefab;

        private CanvasGroup _canvasGroup;
        private InputAction _input;
        private bool _isActive;
        private readonly List<Note> _notes = new();

        private List<Papers> _papersList = new();
        private Papers[] _papers;

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

        public void OnLoad(JToken token)
        {
            _papers = token["allNotes"].ToObject<Papers[]>();
            _papersList = _papers.ToList();
            
            LoadNotes();
        }

        public Dictionary<string, object> OnSave()
        {
            _papers = _papersList.ToArray();
            
            return new Dictionary<string, object>
            {
                { "allNotes", _papers },
            };
        }

        public void AddNote(Papers paper)
        {
            var newNote = notebookData.GetNote(paper);
            _notes.Add(newNote);
            
            _papers = null;

            Render();
        }

        public void AddPaperList(Papers paper)
        {
            _papersList.Add(paper);
        }

        private void LoadNotes()
        {
            var papers = _papers;
            
            foreach (var paper in papers)
            {
                AddNote(paper);
            }
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
