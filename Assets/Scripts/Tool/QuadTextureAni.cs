using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(QuadTextureNgui))]
public class QuadTextureAni : MonoBehaviour
{
	//[HideInInspector][SerializeField] protected int mFPS = 30;
	//[HideInInspector][SerializeField] protected string mPrefix = "";
	//[HideInInspector][SerializeField] protected bool mLoop = true;
	protected List<string> mSpriteNames = new List<string>();
	public int frames { get { return mSpriteNames.Count; } }
	public float mFPS = 10;
	public string namePrefix;
	public bool flip ;
	public bool reverse;
	public bool loop;
	[HideInInspector][SerializeField]public bool mirror = false;
	protected QuadTextureNgui mSprite;
	protected float mDelta = 0f;
	protected int mIndex = 0;
	public bool mActive = true;
	public bool isPlaying { get { return mActive; } }
	public delegate  void CallBack();
	public  event CallBack OnNormalAniFinished;
	//public Chara target;
	
	protected virtual void Start () {
		RebuildSpriteList(true); 
		if (reverse) {
			mIndex = mSpriteNames.Count -1;
		}
		
	}
	static int SortByName(string n1, string n2)
	{
		if (n1.IndexOf ('_') == -1)
			return -1;
		else if (n2.IndexOf ('_') == -1)
			return 1;
		else if (int.Parse(n1.Substring (n1.LastIndexOf('_')+1)) < int.Parse(n2.Substring(n2.LastIndexOf('_')+1)))
			return -1;
		else if (int.Parse(n1.Substring (n1.LastIndexOf('_')+1)) == int.Parse(n2.Substring(n2.LastIndexOf('_')+1)))
			return 0;
		else
			return 1;
	}
	public void RebuildSpriteList (bool first = false)
	{
		if (mSprite == null) mSprite = GetComponent<QuadTextureNgui>();
		mSpriteNames.Clear();
		if(first == false)
			OnNormalAniFinished = null;
		
		if (mSprite != null && mSprite.Atlas != null )
		{
			List<UISpriteData> sprites = mSprite.Atlas.spriteList;
			
			for (int i = 0, imax = sprites.Count; i < imax; ++i)
			{
				UISpriteData sprite = sprites[i];
				
				if (string.IsNullOrEmpty(namePrefix) || sprite.name.StartsWith(namePrefix))
				{
					mSpriteNames.Add(sprite.name);
				}
			}
			if(mSpriteNames.Count != 0 &&  mSpriteNames[0].Contains("_"))
				mSpriteNames.Sort(SortByName);
		}
	}
	
	public void Reset()
	{
		mActive = true;
		mIndex = 0;
		
		if (mSprite != null && mSpriteNames.Count > 0)
		{
			mSprite.spriteName = mSpriteNames[mIndex];
			//if (mSnap) mSprite.MakePixelPerfect();//now we default is pixel perfect, so we did not needed!!
		}
	}
	private bool needReverse = false;
	// Update is called once per frame
	void FixedUpdate ()
	{

		if (mActive && mSpriteNames.Count > 1 && Application.isPlaying && mFPS > 0f)
		{
			mDelta += RealTime.deltaTime;
			float rate = 1f / mFPS;
			
			if (rate < mDelta)
			{
				
				mDelta = (rate > 0f) ? mDelta - rate : 0f;
				if(flip)
				{
					if(needReverse)
					{
						mIndex--;
						mActive = loop;
					}
					else
					{
						mIndex++;
						mActive = loop;
					}
					if (mIndex+1 >= mSpriteNames.Count)
					{
						needReverse = true;
					}
					else if(mIndex-1 < 0)
					{
						needReverse = false;
					}
					
				}
				else if(reverse)
				{
					if(--mIndex <= 0)
					{
						mIndex = mSpriteNames.Count - 1;
						mActive = loop;
					}
				}
				else
				{
					if (++mIndex >= mSpriteNames.Count)
					{
						if(OnNormalAniFinished != null)
							OnNormalAniFinished();
						mIndex = 0;
						mActive = loop;
					}
				}
				
				
				if (mActive)
				{

					//Debug.LogError(mIndex);
					if(mIndex >= 1 && mIndex < mSpriteNames.Count)
					mSprite.spriteName = mSpriteNames[mIndex];
					mSprite.mirrorX = mirror;
					mSprite.InitFace();
					//if (mSnap) mSprite.MakePixelPerfect();
				}
			}
		}
	}
}

