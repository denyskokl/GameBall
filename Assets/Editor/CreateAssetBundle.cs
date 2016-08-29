namespace UnityEditor
{
	public class CreateAssetBundle
	{
		[MenuItem("AssetBundle/Create new")]

		public static void CreateNewAssetBundle()
		{
			string outpushPath = "AssetBundles";
			BuildPipeline.BuildAssetBundles(outpushPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);
		}
	}
}