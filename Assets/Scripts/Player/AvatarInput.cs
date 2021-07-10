// GENERATED AUTOMATICALLY FROM 'Assets/Resources/AvatarInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Runtime.Player {
    public class @AvatarInput : IInputActionCollection, IDisposable {
        public InputActionAsset asset { get; }
        public @AvatarInput() {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""AvatarInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""91e40513-bef8-41b6-a30a-b9b88e23cbbe"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1546b459-b18f-4710-be8b-e24d4ee36717"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d2895bb4-d719-4860-8d2f-286c07c730dd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3c4affe5-b44a-41f7-9aa9-3d289e936f9d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sonar"",
                    ""type"": ""Button"",
                    ""id"": ""3736fa0b-6288-4586-a08e-a1e6363cae6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""a43aab79-05e1-485a-aa62-edf3a7c5983f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""424a111d-04be-43e4-97c2-6a03f5279259"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""591b5f0a-2f50-4c08-bb0a-1eb5e922b9f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b60ab0f7-a6d5-47ff-8310-8879dc84f356"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4326afba-ed6f-4daa-87ec-1be95fcbcdfc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb89e800-3d3f-4490-a953-8fc70ef94fa8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sonar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f2dd2c3c-1181-40ae-b589-b6d52e38787b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8fe5e344-86e3-49b9-b198-2845c8c461fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0d6fcbbf-1832-4890-9baa-e84ff9875b6f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d81bc17c-757f-4e1c-9204-c13eab771a0d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c69a1c84-526b-4eba-b47a-b4ebb332b992"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fec13e22-3f13-4762-84f1-ed1b09d3a2d7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""047da930-590b-48d1-a49e-678f2d60fdbe"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4717a9cb-b3f1-4684-b95e-4d8e0e5f70a5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
            m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
            m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
            m_Player_Sonar = m_Player.FindAction("Sonar", throwIfNotFound: true);
            m_Player_Menu = m_Player.FindAction("Menu", throwIfNotFound: true);
            m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
            m_Player_Special = m_Player.FindAction("Special", throwIfNotFound: true);
        }

        public void Dispose() {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action) {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator() {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Enable() {
            asset.Enable();
        }

        public void Disable() {
            asset.Disable();
        }

        // Player
        readonly InputActionMap m_Player;
        IPlayerActions m_PlayerActionsCallbackInterface;
        readonly InputAction m_Player_Movement;
        readonly InputAction m_Player_Look;
        readonly InputAction m_Player_Jump;
        readonly InputAction m_Player_Sonar;
        readonly InputAction m_Player_Menu;
        readonly InputAction m_Player_Sprint;
        readonly InputAction m_Player_Special;
        public struct PlayerActions {
            @AvatarInput m_Wrapper;
            public PlayerActions(@AvatarInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Player_Movement;
            public InputAction @Look => m_Wrapper.m_Player_Look;
            public InputAction @Jump => m_Wrapper.m_Player_Jump;
            public InputAction @Sonar => m_Wrapper.m_Player_Sonar;
            public InputAction @Menu => m_Wrapper.m_Player_Menu;
            public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
            public InputAction @Special => m_Wrapper.m_Player_Special;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance) {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null) {
                    @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Sonar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSonar;
                    @Sonar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSonar;
                    @Sonar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSonar;
                    @Menu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                    @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                    @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                    @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                    @Special.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                    @Special.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                    @Special.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null) {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Sonar.started += instance.OnSonar;
                    @Sonar.performed += instance.OnSonar;
                    @Sonar.canceled += instance.OnSonar;
                    @Menu.started += instance.OnMenu;
                    @Menu.performed += instance.OnMenu;
                    @Menu.canceled += instance.OnMenu;
                    @Sprint.started += instance.OnSprint;
                    @Sprint.performed += instance.OnSprint;
                    @Sprint.canceled += instance.OnSprint;
                    @Special.started += instance.OnSpecial;
                    @Special.performed += instance.OnSpecial;
                    @Special.canceled += instance.OnSpecial;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions {
            void OnMovement(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnSonar(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
            void OnSprint(InputAction.CallbackContext context);
            void OnSpecial(InputAction.CallbackContext context);
        }
    }
}
