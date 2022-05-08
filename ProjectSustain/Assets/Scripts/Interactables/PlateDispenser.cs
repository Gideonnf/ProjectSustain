using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateDispenser : InteractBase
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Interact(GameObject playerObject)
    {
        base.Interact(playerObject);
        Debug.Log("test");
    }

    /// <summary>
    /// Override the base cause it'll have a unique end to interaction
    /// compared to other interactables
    /// </summary>
    public override void InteractEnd()
    {
        base.InteractEnd();
    }
}
