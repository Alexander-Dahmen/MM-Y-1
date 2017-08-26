using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAssertEquals<T> : CustomYieldInstruction {

    private readonly Func<T> function;
    private readonly T assert;

    public WaitAssertEquals(Func<T> function, T assert) {
        this.function = function;
        this.assert = assert;
    }

    public override bool keepWaiting {
        get {
            return !(function().Equals(assert));
        }
    }
}
