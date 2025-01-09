using UnityEngine;

namespace IdleArcade.Services.Input
{
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
}