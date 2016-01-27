using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//check if billboard exist
//Used to generate the enemies in the scene
public class QuadTextureNgui : MonoBehaviour, IMessageObject {

	public UIAtlas Atlas;
	public string mSpriteName;
	//the size of the enemy
	public float ScaleFactor = 1;
	public bool mirrorX = false;
	public bool mirrorY = false;
	public bool mirrorXY = false;
	[System.NonSerialized]
	protected UISpriteData mSprite;
	bool mSpriteSet = false;
	private Mesh mesh;

	// Use this for initialization
	void Start () {
		//if the size of the enemy is 0, too small, set it to 1
		if (ScaleFactor == 0) {
			ScaleFactor = 1;
		}
		InitAtlas ();
		InitFace ();
	}
	
	void InitAtlas(){
		if (null != Atlas) {
			mSpriteSet = false;
			mSprite = null;
			if(string.IsNullOrEmpty(mSpriteName)){
				if(Atlas != null && Atlas.spriteList.Count>0){
					SetAtlasSprite(Atlas.spriteList[6]);
					mSpriteName = mSprite.name;
				}
			}
			if(!string.IsNullOrEmpty(mSpriteName)){
				string sprite = mSpriteName;
				mSpriteName = "";
				spriteName = sprite;
			}
		}
	}

	public string spriteName{
		get{
			return mSpriteName;
		}
		set{
			if(string.IsNullOrEmpty(value)){
				if(string.IsNullOrEmpty(mSpriteName)){
					return;
				}
				mSpriteName = "";
				mSprite = null;
				mSpriteSet = false;
			} else if(mSpriteName != value){
				mSpriteName = value;
				mSprite = null;
				mSpriteSet = false;
			}
		}
	}

	protected void SetAtlasSprite(UISpriteData sprite){
		mSpriteSet = true;
		if (sprite != null) {
			mSprite = sprite;
			mSpriteName = mSprite.name;
		} else {
			mSpriteName = (mSprite!=null) ? mSprite.name : "";
			mSprite = sprite;
		}
	}

	public UISpriteData GetAltasSprite(){
		if (!mSpriteSet) {
			mSprite = null;
		}
		if (mSprite == null && Atlas != null) {
			if(!string.IsNullOrEmpty(mSpriteName)){
				UISpriteData sp = Atlas.GetSprite(mSpriteName);
				if(sp == null) return null;
				SetAtlasSprite(sp);
			}
			if(mSprite == null && Atlas.spriteList.Count > 0){
				UISpriteData sp = Atlas.spriteList[0];
				if(sp == null) return null;
				SetAtlasSprite(sp);
				if(mSprite == null){
					this.PRINT(Atlas.name+"seems to have a null sprite");
					return null;
				}
				mSpriteName = mSprite.name;
			}
		}
		return mSprite;
	}

	public void InitFace(bool needSnap = true){
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter> ();
		if (meshFilter == null) {
			return;
		}
		mesh = meshFilter.mesh;
		UISpriteData mSprite1 = Atlas.GetSprite (spriteName);
		if (mSprite1 == null) {
			return;
		}
		Texture tex = meshFilter.GetComponent<Renderer>().material.mainTexture;
		//0,0,1,1
		Rect outer = new Rect (mSprite1.x, mSprite1.y, mSprite1.width, mSprite1.height);
		//change the right face to the left face
		if (!mirrorX) {
			mesh.uv = new Vector2[]{
				//left top point
				new Vector2(outer.xMin/tex.width, 1.0f-outer.yMax/tex.height), //0,1,1,0
				//right bottom point
				new Vector2(outer.xMax/tex.width, 1.0f-outer.yMin/tex.height),
				//right top point
				new Vector2(outer.xMax/tex.width, 1.0f-outer.yMax/tex.height),
				//left bottom point
				new Vector2(outer.xMin/tex.width, 1.0f-outer.yMin/tex.height) };
		} else {
			mesh.uv = new Vector2[]{
				new Vector2(outer.xMax/tex.width, 1.0f-outer.yMax/tex.height),	//1,0,0,1
				new Vector2(outer.xMin/tex.width, 1.0f-outer.yMin/tex.height),
				new Vector2(outer.xMin/tex.width, 1.0f-outer.yMax/tex.height),
				new Vector2(outer.xMax/tex.width, 1.0f-outer.yMin/tex.height) };

			}
		//decrease the size
		//float scale = (float)(Screen.height/2.0f)/5;
		//transform.localScale = new Vector3((float)mSprite1.width/scale,(float)mSprite1.height/scale,1.0f)*ScaleFactor;
		}

}
