using UnityEngine;
using Zenject;

namespace AccDot.Objects
{
    public class Dot : MonoBehaviour
    {
        [Inject]
        private readonly Config _config;

        public void OnEnable()
        {
            transform.Find("NoteArrow").gameObject.SetActive(false);
            transform.Find("NoteArrowGlow").gameObject.SetActive(false);
            transform.Find("NoteCircleGlow").gameObject.SetActive(true);
            transform.Find("NoteCircleGlow").transform.localScale = Vector3.one * _config.Scale;
            
            var colliders = transform.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                Destroy(collider);
            }

            var boxCuttables = transform.GetComponentsInChildren<BoxCuttableBySaber>();
            foreach(var boxCuttable in boxCuttables)
            {
                Destroy(boxCuttable);
            }

            var materialBlockProperties = transform.GetComponentsInChildren<MaterialPropertyBlockController>();
            foreach (var materialBlockProperty in materialBlockProperties)
            {
                materialBlockProperty.materialPropertyBlock.SetColor("_Color", _config.Color);
                materialBlockProperty.ApplyChanges();
            }
        }
    }
}