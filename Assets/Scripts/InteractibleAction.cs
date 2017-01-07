using Academy.HoloToolkit.Unity;
using UnityEngine;

/// <summary>
/// InteractibleAction performs custom actions when you gaze at the holograms.
/// </summary>
public class InteractibleAction : MonoBehaviour
{
    [Tooltip("Drag the Tagalong prefab asset you want to display.")]
    public GameObject ObjectToTagAlong;

    // called by Assets\Scripts\Interactible.cs for OnSelected Interactible objects
    void PerformTagAlong()
    {
        if (ObjectToTagAlong == null)
        {
            return;
        }

        // FindGameObjectWithTag finds one _active_ GameObject tagged with given tag. 
        // Returns null if no GameObject was found.
        // Recommend having only one tagalong.
        // Here we check if we are already using a tagalong somewhere (?)
        GameObject existingTagAlong = GameObject.FindGameObjectWithTag("TagAlong");
        if (existingTagAlong != null)
        {
            return;
        }

        GameObject instantiatedObjectToTagAlong = GameObject.Instantiate(ObjectToTagAlong);

        instantiatedObjectToTagAlong.SetActive(true);

        /* TODO: DEVELOPER CODING EXERCISE 6.b */

        // 6.b: AddComponent Billboard to instantiatedObjectToTagAlong.
        // So this gameobject is always facing the user as they move.
        instantiatedObjectToTagAlong.AddComponent<Billboard>();

        // 6.b: AddComponent SimpleTagalong to instantiatedObjectToTagAlong.
        // So this gameobject is always following the user as they move.
        instantiatedObjectToTagAlong.AddComponent<SimpleTagalong>();  // see Assets\Utilities\Scripts\SimpleTagalong.cs

        // 6.b: Set any public properties you wish to experiment with.
    }
}