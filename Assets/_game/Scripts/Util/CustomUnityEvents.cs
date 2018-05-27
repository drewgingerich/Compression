using System;
using UnityEngine;
using UnityEngine.Events;

#region custom types

[Serializable]
public class PlayerColorEvent : UnityEvent<PlayerColorScheme> {};

[Serializable]
public class PlayerInfoEvent: UnityEvent<PlayerInfo> {}

[Serializable]
public class InputTypeEvent : UnityEvent<InputType> {}

[Serializable]
public class InputSchemeEvent : UnityEvent<InputScheme> {}

#endregion

#region builtin types

[Serializable]
public class BoolEvent : UnityEvent<bool> { }

[Serializable]
public class IntEvent : UnityEvent<int> { }

[Serializable]
public class FloatEvent : UnityEvent<float> { }

[Serializable]
public class Vector2Event : UnityEvent<Vector2> { }

#endregion