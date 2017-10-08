using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    #region Events

    public event System.Action<float> OnDamage;
    public event System.Action<float> OnHeal;
    public event System.Action OnDeath;

    #endregion

    #region Variables

    [SerializeField] private float hp;
    [SerializeField] private float fullHp;
    [SerializeField] private bool killOnBecameInvisible;

    #endregion

    #region Unity Methods

    protected virtual void Awake() { }
    
    protected virtual void Start() {
        FullHeal();
    }

    protected virtual void OnBecameInvisible() {
        if (killOnBecameInvisible)
			Destroy(gameObject);
    }

    #endregion

    #region Public Methods

    public virtual void Damage(float amount, Transform source = null) {
        hp -= amount;
        if (OnDamage != null)
            OnDamage(amount);
        if (!IsAlive) {
            hp = 0f;
            if (OnDeath != null)
                OnDeath();
        }
    }

    public virtual void Heal(float amount, Transform source = null) {
        hp += amount;
        if (OnHeal != null)
            OnHeal(amount);
        if (hp > fullHp) {
            hp = fullHp;
        }
    }

    public virtual void InstantKill(Transform source = null) {
        Damage(fullHp, source);
    }

    public virtual void FullHeal(Transform source = null) {
        Heal(fullHp, source);
    }

    #endregion

    #region Properties

    public virtual float HP {
		get { return hp; }
	}

    public virtual float FullHP {
		get { return fullHp; }
	}

    public virtual float HealthFraction {
		get { return (hp / fullHp); }
	}

    public virtual bool IsAlive {
		get { return (hp > 0f); }
	}
    
    #endregion
}