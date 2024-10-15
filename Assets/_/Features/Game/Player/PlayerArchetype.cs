using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PlayerArchetype : MonoBehaviour
    {
        #region Publics

        public enum Archetype
        {
            NONE,
            BARBARIAN,
            ROGUE,
            MAGE,
            RANGER
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

        [Header("-- STATISTICS --")]
        public int m_strength;
        public int m_agility;

        #endregion

        #region Unity

        private void Awake()
        {
            _camera = Camera.main;
            _mouseCurrentPosition = Mouse.current.position;

            _distanceInteract = 2f;
            _interactableLayer = LayerMask.GetMask("Archetype");
            _uiLayer = LayerMask.GetMask("InteractUI");
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                _playerBlackboard.SetValue("Archetype", Archetype.NONE);
                _playerBlackboard.SetValue("Strength", 0);
                _playerBlackboard.SetValue("Agility", 0);
            }
        }

        private void Start() 
        { 
            m_archetype = _playerBlackboard.GetValue<PlayerArchetype.Archetype>("Archetype");
            m_strength = _playerBlackboard.GetValue<int>("Strength");
            m_agility = _playerBlackboard.GetValue<int>("Agility");
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.E)) OnChangeArchetype();
        }

        #endregion

        #region Methods

        public void SetArchetypeNone() => m_archetype = Archetype.NONE;
        public void SetArchetypeWarrior() => m_archetype = Archetype.BARBARIAN;
        public void SetArchetypeRogue() => m_archetype = Archetype.ROGUE;
        public void SetArchetypeMage() => m_archetype = Archetype.MAGE;
        public void SetArchetypeRanger() => m_archetype = Archetype.RANGER;

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
            else if (Physics.Raycast(ray, out RaycastHit hit2, _distanceInteract, _uiLayer))
            {
                hit2.collider.gameObject.TryGetComponent<Button>(out Button statButton);
                statButton.onClick.Invoke();
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
        private LayerMask _uiLayer;
        private Vector2Control _mouseCurrentPosition;

        private float _distanceInteract;

        #endregion
    }
}
