using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilUpdate : CustomYieldInstruction {

    private Action update;
    private Func<bool> predicate;

    public WaitUntilUpdate(Func<bool> predicate, Action update) {
        this.update = update;
        this.predicate = predicate;
        if (update == null)
            throw new NullReferenceException("update");
        if (predicate == null)
            throw new NullReferenceException("predicate");
    }

    public override bool keepWaiting {
        get {
            update();
            return !predicate();
        }
    }
}