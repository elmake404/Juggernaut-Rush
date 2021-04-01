using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Mouse(0) state in previos Update().
    bool MousePrev = false;

    // Object to play animations.
    public GameObject GunObject;
    Animation GunAnimation;
    // Fire sound emitter.
    AudioSource SoundEmmiter;


    void Start()
    {
        // Get those components only once.
        GunAnimation = GunObject.GetComponent<Animation>();
        SoundEmmiter = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool Mouse = Input.GetMouseButton(0);

        // if Mouse(0) was pressed first frame:
        if (Mouse && !MousePrev)
        {
            // Replay animation.
            GunAnimation.Stop();
            GunAnimation.Play("Fire");
            // Play sound.
            SoundEmmiter.Play();

            // Create Ray from camera.
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit))
            {
                // Check if we hit ShatterableGlass object:
                if (Hit.transform.gameObject.tag == "ShatterableGlass")
                {
                    // Create new ShatterInfo object and write hit location and force dirrection.
                    ShatterableGlassInfo Inf = new ShatterableGlassInfo(Hit.point, gameObject.transform.forward);
                    // Send the info.
                    Hit.transform.gameObject.SendMessage("Shatter3D", Inf);
                }
            }
        }
        // Save state.
        MousePrev = Mouse;
    }
}