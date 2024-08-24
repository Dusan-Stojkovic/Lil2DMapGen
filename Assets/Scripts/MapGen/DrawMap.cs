using UnityEngine;

namespace MapGen
{
   public class DrawMap : MonoBehaviour
   {
     Map _map;
     Texture2D _texture;
     [SerializeField]
     int _textureWidth = 256;
     [SerializeField]
     int _textureHeight = 256;
     [SerializeField]
     Material _dstMaterial;
     void Start()
     {
       CreateNewTexture();
       Map m = new Map(_texture, 600, 600);
     }
     void CreateNewTexture ()
     {
	    _texture = new Texture2D( width:_textureWidth , height:_textureHeight , textureFormat:TextureFormat.ARGB32 , mipCount:3 , linear:true );
	 	_texture.filterMode = FilterMode.Point;// no smooth pixels
	 	_texture.SetPixel( 0 , 0 , Color.black );
	 	_texture.SetPixel( x:_texture.width-1 , y:_texture.height-1 , Color.magenta );
	 	_texture.Apply();
	 	if( _dstMaterial!=null ) _dstMaterial.mainTexture = _texture;
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
