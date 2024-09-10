using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PlayerArchetype : MonoBehaviour
    {
        #region Publics

        public enum Archetype
        {
            NONE,
            WARRIOR,
            ROGUE,
            MAGE
        }

        public Archetype m_archetype
        {
            get => _archetype;
            set
            {
                _archetype = value;
                _playerBlackboard.SetValue<Archetype>("Archetype", _archetype);
            }
        }

        #endregion

        #region Unity

        private void Awake()
        {
            _camera = Camera.main;
            _mouseCurrentPosition = Mouse.current.position;

            _distanceInteract = 2f;
            _interactableLayer = LayerMask.GetMask("Archetype");
        }

        private void Start() => m_archetype = Archetype.NONE;

        private void Update()
        {
            if (IsArchetypeNone() && Input.GetMouseButtonDown(0)) OnChangeArchetype();
        }

        #endregion

        #region Methods

        public void SetArchetypeNone() => m_archetype = Archetype.NONE;
        public void SetArchetypeWarrior() => m_archetype = Archetype.WARRIOR;
        public void SetArchetypeRogue() => m_archetype = Archetype.ROGUE;
        public void SetArchetypeMage() => m_archetype = Archetype.MAGE;

        #endregion

        #region Utils

        private void OnChangeArchetype()
        {
            Ray ray = _camera.ScreenPointToRay(_mouseCurrentPosition.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, _distanceInteract, _interactableLayer))
            {
                hit.collider.gameObject.TryGetComponent<Button>(out Button archetypeButton);
                archetypeButton.onClick.Invoke();
            }
        }

        private bool IsArchetypeNone() => _archetype == Archetype.NONE;

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Privates")]
        private Archetype _archetype;
        private Camera _camera;
        private LayerMask _interactableLayer;
        private Vector2Control _mouseCurrentPosition;

        private float _distanceInteract;

        #endregion
    }
}
