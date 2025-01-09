using IdleArcade.Services;
using UnityEngine;

namespace IdleArcade.Core.AssetManagement
{
  public interface IAssetProvider: IService
  {
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path);
  }
}