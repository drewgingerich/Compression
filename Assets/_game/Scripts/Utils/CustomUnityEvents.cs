using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolEvent : UnityEvent<bool> {}

[Serializable]
public class IntEvent : UnityEvent<int> {}

[Serializable]
public class FloatEvent : UnityEvent<float> {}

[Serializable]
public class Vector2Event : UnityEvent<Vector2> {}

[Serializable]
public class ImpactInfoEvent : UnityEvent<ImpactInfo> {}

[Serializable]
public class PlayerColorEvent : UnityEvent<PlayerColorScheme> {};

[Serializable]
public class InputTypeEvent : UnityEvent<InputType> {};

[Serializable]
public class InputSchemeEvent : UnityEvent<InputScheme> {};