using Game.Code.Logic.NativeLogicOverrides.Notebook.Presenter;
using UnityEngine;

namespace Game.Code.Logic.SO
{
    [CreateAssetMenu(fileName = "PaperData", menuName = "SO/PaperData")]
    public class NotebookData : ScriptableObject
    {
        [Header("Paper1")]
        [SerializeField] private string title1;
        [SerializeField] private Sprite sprite1;
        
        [Header("Paper2")]
        [SerializeField] private string title2;
        [SerializeField] private Sprite sprite2;
        
        [Header("Paper3")]
        [SerializeField] private string title3;
        [SerializeField] private Sprite sprite3;

        public Note GetNote(Papers paper)
        {
            return paper switch
            {
                Papers.Paper1 => new Note { Title = title1, Sprite = sprite1 },
                Papers.Paper2 => new Note { Title = title2, Sprite = sprite2 },
                _ => new Note { Title = title3, Sprite = sprite3 }
            };
        }
    }
}
