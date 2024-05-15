using UnityEngine;

namespace Case_2
{
    public class FXController : MonoBehaviour
    {
        public ParticleSystem _vfxPrefab;

        public void SpawnVFX(Vector3 pos, Vector3 scale, Color color)
        {
            var vfx = Instantiate(_vfxPrefab);
            vfx.gameObject.transform.position = pos;
            vfx.gameObject.transform.localScale = scale;
            var main = vfx.main;
            main.startColor = color;
            vfx.Play();
        }
    }
}
