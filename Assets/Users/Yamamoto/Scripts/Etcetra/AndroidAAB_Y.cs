using UnityEngine;
using Google.Play.AssetDelivery;

public class AndroidAAB_Y : MonoBehaviour
{
    private void LoadAsset1(string assetBundleName, string fileName)
    {
        var bundleRequest = PlayAssetDelivery.RetrieveAssetBundleAsync(assetBundleName);
        bundleRequest.Completed += request =>
        {
            if (request.Status == AssetDeliveryStatus.Loaded ||
            request.Status == AssetDeliveryStatus.Available)
            {
                var prefab = request.AssetBundle.LoadAsset<GameObject>(fileName);
                Instantiate(prefab);
            }
        };
    }
    // 異なる場合は別途指定
    private void LoadAsset2(string assetPackName, string assetBundlePath, string fileName)
    {
        var packRequest = PlayAssetDelivery.RetrieveAssetPackAsync(assetPackName);
        packRequest.Completed += request =>
        {
            if (request.Status == AssetDeliveryStatus.Loaded ||
            request.Status == AssetDeliveryStatus.Available)
            {
                var bundleCreateRequest = packRequest.LoadAssetBundleAsync(assetBundlePath);
                bundleCreateRequest.completed += _ =>
                {
                    var prefab = bundleCreateRequest.assetBundle.LoadAsset<GameObject>(fileName);
                    Instantiate(prefab);
                };
            };
        };
    }
}
