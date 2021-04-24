using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The purpose of this class is to represent Fluid container.
/// </summary>
class FluidBottle : MonoBehaviour {
    public ParticleSystem FluidParticles;

    public UnityEvent OnBottleEmpty;
    public float SprayRatio = 0.2f;

    private float m_FluidLevel = 1f;
    private bool m_Spray = false;

    /// <summary>
    /// Current Fluid level in bottle 0-1.
    /// This property will clamp the set value.
    /// </summary>
    public float FluidLevel {
        get {
            return this.m_FluidLevel;
        }
        set {
            this.m_FluidLevel = Mathf.Clamp01(value);
        }
    }

    /// <summary>
    /// Spray state of the bottle.
    /// This property will check for fluid level in bottle.
    /// </summary>
    public bool Spray {
        get => this.m_Spray;
        set {
            if(this.FluidLevel <= 0.0f + Mathf.Epsilon) {
                this.m_Spray = false;
                return;
            }
            this.m_Spray = value;
        }
    }

    public void Update() {
        //check for spray state.
        if(this.m_Spray) {
            //emit particles and decrease fluid level by SprayRatio.
            this.m_FluidLevel -= this.SprayRatio * Time.deltaTime;
            this.FluidParticles.Emit(1);

            if(this.m_FluidLevel <= 0.0f + Mathf.Epsilon) {
                this.m_Spray = false;
                this.m_FluidLevel = 0f;
                this.OnBottleEmpty.Invoke();
            }
        }
    }

    public void ResetFluidLevel() {
        this.m_FluidLevel = 1f;
        this.m_Spray = false;
    }
}