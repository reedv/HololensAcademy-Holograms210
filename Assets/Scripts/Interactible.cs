using UnityEngine;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at
/// and when gaze is broken from the Interactible.
/// </summary>
public class Interactible : MonoBehaviour
{
    [Tooltip("Audio clip to play when interacting with this hologram.")]
    public AudioClip TargetFeedbackSound;
    private AudioSource audioSource;

    private Material[] defaultMaterials;

    void Start()
    {
        // A renderer is what makes an object appear on the screen. 
        // Renderer class is used to access the renderer of any object, mesh or particle system
        defaultMaterials = GetComponent<Renderer>().materials;  // get all the instantiated materials of this object.

        // Add a BoxCollider (simple boxshaped collider) if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        EnableAudioHapticFeedback();
    }

    private void EnableAudioHapticFeedback()
    {
        // If this hologram has an audio clip, add an AudioSource with the clip.
        if (TargetFeedbackSound != null)
        {
            // get the AudioSource for this hologram
            // TODO: I don't know how this line works with calling GetComponent on an instance (implicit 'this'?)
            // see commnets in this post http://answers.unity3d.com/answers/971494/view.html
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // add the AudioSource of this gameobject
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = TargetFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
    }

    /* TODO: DEVELOPER CODING EXERCISE 2.d */

    void GazeEntered()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to highlight the material when gaze enters.
            // Set the defaultMaterials named (_Highlight) float value.
            defaultMaterials[i].SetFloat("_Highlight", .25f);
        }
    }

    void GazeExited()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to remove highlight on material when gaze exits.
            defaultMaterials[i].SetFloat("_Highlight", 0f);
        }
    }

    void OnSelect()
    {
        // increase the _Highlight of a gazed hologram when selected
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .5f);
        }

        // Play the audioSource (if exists) feedback _when we gaze and select_ a hologram.
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        /* TODO: DEVELOPER CODING EXERCISE 6.a */
        // 6.a: Handle the OnSelect by sending a PerformTagAlong message.
        // Recall SendMessage calls the method named methodName on every MonoBehaviour in _this_ game object.
        SendMessage("PerformTagAlong");
    }
}