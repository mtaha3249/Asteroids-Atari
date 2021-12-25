using UnityEngine;

public class Comment : MonoBehaviour
{
    #if UNITY_EDITOR
    [SerializeField] [TextArea(4, 25)] private string _comment;
#endif
}
