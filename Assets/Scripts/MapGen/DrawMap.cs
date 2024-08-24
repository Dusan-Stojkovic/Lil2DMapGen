using UnityEngine;

namespace MapGen
{
   public class DrawMap : MonoBehaviour
   {
     Map _map;
     Texture2D _texture;
     [Header("Map parameters")]
     [SerializeField]
     byte seed = 0;
     [SerializeField]
     int textureWidth = 256;
     [SerializeField]
     int textureHeight = 256;
     [SerializeField]
     [Header("Scene")]
     Material dstMaterial;
     void Start()
     {
       CreateNewTexture();
       Map m = new Map(_texture, textureWidth, textureHeight);
     }
     void CreateNewTexture ()
     {
	    _texture = new Texture2D( width:textureWidth , height:textureHeight , textureFormat:TextureFormat.ARGB32 , mipCount:3 , linear:true );
	 	_texture.filterMode = FilterMode.Point;// no smooth pixels
	 	_texture.SetPixel( 0 , 0 , Color.black );
	 	_texture.SetPixel( x:_texture.width-1 , y:_texture.height-1 , Color.magenta );
	 	_texture.Apply();
	 	if( dstMaterial!=null ) dstMaterial.mainTexture = _texture;
        Debug.Log( "Texture created" );
	 }
     void Dispose () => Dispose( _texture );
	 void Dispose ( Object obj )
	 {
	 	#if UNITY_EDITOR
	 	if( Application.isPlaying ) { UnityEngine.Object.Destroy( obj ); }
	 	else { UnityEngine.Object.DestroyImmediate( obj ); }
	 	#else
	 	UnityEngine.Object.Destroy( this );
	 	#endif
	 }
   }
}
