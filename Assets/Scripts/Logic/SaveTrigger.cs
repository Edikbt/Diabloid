using UnityEngine;
using Zenject;

namespace Diabloid
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        public BoxCollider Collider;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }        

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 120, 30, 0.5f);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}
