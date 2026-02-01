using TMPro;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    [SerializeField] CollectionManager m_manager;
    [SerializeField] TMP_Text m_text;
    void Update()
    {
        m_text.text = $"{m_manager.Points}kg";
    }
}
