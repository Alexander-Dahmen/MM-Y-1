using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilLayersTouch : CustomYieldInstruction {

    private readonly LayerMask layers;
    private readonly Collider2D collider;

    public WaitUntilLayersTouch(Collider2D collider, LayerMask layers) {
        this.layers = layers;
        this.collider = collider;
    }

    public WaitUntilLayersTouch(Collider2D collider, params string[] layers) {
        this.collider = collider;
        this.layers = LayerMask.GetMask(layers);
    }
    
    public override bool keepWaiting {
        get {
            return !(collider.IsTouchingLayers(layers));
        }
    }
}