using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class SpritePalette : MonoBehaviour {

    #region Static Variables/Constants

    private static readonly int MAIN_TEXTURE = Shader.PropertyToID("_MainTex");
    private static readonly int PALETTE_TEXTURE = Shader.PropertyToID("_PaletteTex");
    private static readonly int PALETTE_INDEX = Shader.PropertyToID("_PaletteIndex");
    private static readonly string FLASH_TEXTURE_NAME = "EmptySprite";

    private static Texture2D flashTex = null;

    #endregion

    #region Variables

    [SerializeField] private Texture2D mainTex;
    [SerializeField] private Texture2D paletteTexture;

    private int paletteIndex;
    private bool flash;
    private SpriteRenderer rend;
    private MaterialPropertyBlock mpb;
    private float paletteTextureHeight;

    #endregion

    #region Unity Methods

    void Awake() {
        if (flashTex == null)
            flashTex = Resources.Load<Texture2D>(FLASH_TEXTURE_NAME);
        rend = GetComponent<SpriteRenderer>();
        mpb = new MaterialPropertyBlock();
        paletteTextureHeight = paletteTexture.height;
        mainTex = GetSpriteTexture();
        flash = false;
    }

    void Start() {
        ApplyPalette();
    }

    #endregion

    #region Properties

    public Texture2D MainTex {
        get { return mainTex; }
        set {
            mainTex = value;
            ApplyPalette();
        }
    }

    public int Palette {
        get { return paletteIndex; }
        set {
            if (value != paletteIndex)
                SetPalette(value);
        }
    }

    public bool Flash {
        get { return flash; }
        set {
            if (value != flash)
                SetFlash(value);
        }
    }

    #endregion

    #region Public Methods

    public void SetPalette(int index) {
        this.paletteIndex = index;
        ApplyPalette();
    }

    public void SetFlash(bool flash) {
        SetPaletteTexture(flash ? flashTex : mainTex);
    }

    public void SetPaletteTexture(Texture2D texture) {
        if (texture == null)
            return;
        if (mpb == null)
            mpb = new MaterialPropertyBlock();
        if (rend == null)
            rend = GetComponent<SpriteRenderer>();
        if (mainTex == null)
            mainTex = GetSpriteTexture();

        float indexHeight = ((float)paletteIndex) / paletteTextureHeight;

        mpb.SetTexture(MAIN_TEXTURE, mainTex);
        mpb.SetTexture(PALETTE_TEXTURE, texture);
        mpb.SetFloat(PALETTE_INDEX, indexHeight);
        rend.SetPropertyBlock(mpb);
    }

    #endregion

    #region Private Methods

    private void ApplyPalette() {
        if ((paletteIndex >= paletteTextureHeight) || (paletteIndex < 0)) {
            Debug.LogWarningFormat("[{0}] Palette index out of bounds: {1}", this, paletteIndex);
        } else {
            SetPaletteTexture(paletteTexture);
        }
    }

    private Texture2D GetSpriteTexture() {
        MaterialPropertyBlock get = new MaterialPropertyBlock();
        if (rend == null)
            rend = GetComponent<SpriteRenderer>();
        rend.GetPropertyBlock(get);
        return get.GetTexture(MAIN_TEXTURE) as Texture2D;
    }

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(SpritePalette))]
public class SpritePaletteEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        SpritePalette palette = target as SpritePalette;
        palette.Palette = EditorGUILayout.IntField("Palette Index", palette.Palette);
        palette.Flash = EditorGUILayout.Toggle("Sprite Flash", palette.Flash);
    }
}
#endif
