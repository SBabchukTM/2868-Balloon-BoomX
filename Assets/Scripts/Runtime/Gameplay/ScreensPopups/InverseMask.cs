using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups
{
    [RequireComponent(typeof(Image))]
    public class InverseMask : MonoBehaviour, IMaterialModifier
    {
        [SerializeField] private Material inverseMaskMaterial;

        public Material GetModifiedMaterial(Material baseMaterial)
        {
            if (inverseMaskMaterial == null)
            {
                Shader shader = Shader.Find("UI/Default");
                inverseMaskMaterial = new Material(shader);
                inverseMaskMaterial.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.NotEqual);
            }

            return inverseMaskMaterial;
        }
    }
}
