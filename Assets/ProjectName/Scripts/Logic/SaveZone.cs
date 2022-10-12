using ProjectName.Scripts.Infrastructure.Services;
using ProjectName.Scripts.Infrastructure.Services.SaveLoad;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveZone : MonoBehaviour
    {
        private BoxCollider _collider;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            ExceptionValidator.ThrowIfNull(_saveLoadService, _collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (_collider == null)
                return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}