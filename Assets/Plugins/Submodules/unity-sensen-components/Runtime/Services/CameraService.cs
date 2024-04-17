using LnxArch;
using UnityEngine;

namespace Sensen.Components
{
    [LnxService]
    public class CameraService : MonoBehaviour
    {
        private Camera _camera;
        public Camera Camera => _camera == null ? _camera = Camera.main : _camera;
    }
}
