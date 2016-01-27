using UnityEngine;
using System.Collections;
//draw the grid, where you can put the towers, towers position
//the grid color
//there are two kinds of grids, the smaller one and the bigger one
public class GridOverview : MonoBehaviour {

	public GameObject plane;
	public GameObject[] obj;

	//show the big grids
	public bool showMain = true;
	//show the small grids
	public bool showSub = false;
	public bool showObj = false;
	
	public float gridSizeX;
	public float gridSizeY;
	public float gridSizeZ;

	public float smallStep;
	public float largeStep;

	public int gridObjSizeX;
	public int gridObjSizeY;
	public int gridObjSizeZ;

	public float startX;
	public float startY;
	public float startZ;

	public float startObjX;
	public float startObjY;
	public float startObjZ;

	private float offsetY = 0f;
	private float ScrollRate = 0.1f;
	private float lastScroll = 0f;

	private Material lineMatrial;

	private Color mainColor = new Color (0f, 1f, 0f, 1.0f);
	private Color subColor = new Color(0f,0.5f,0f,1.0f);
	private Color objColor = new Color(1f,0f,0f,1.0f);

	void CreateLineMaterial(){
		if (!lineMatrial) {
			lineMatrial = new Material("Shader \"Lines/Colored Blended\" {"+"subShader {Pass {"+" Blend srcAlpha OneMinusSrcalpha "
			                           +" Zwrite Off Cull Off Fog{ Mode Off} "+" BindChannels { "+" Bind\"vertex\", vertex Bind \"color\", color } " +
			                           "} } } ");
			lineMatrial.hideFlags = HideFlags.HideAndDontSave;
			lineMatrial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}


	//after effect, invoke this function every frame
	void OnPostRender(){
		CreateLineMaterial ();
		lineMatrial.SetPass (0);
		GL.Begin (GL.LINES);
		//show the sub grid, small grid
		if (showSub) {
			GL.Color(subColor);
			for(float i = 0; i <= gridSizeY; i+=smallStep){
				for(float j = 0; j <= gridSizeZ - startZ; j+=smallStep){
					GL.Vertex3(startX, i+offsetY, startZ+j);
					GL.Vertex3(gridSizeX, i+offsetY, startZ+j);
				}
				for(float j = 0; j <= gridSizeX - startX; j+=smallStep){
					GL.Vertex3(startX+j, i+offsetY, startZ);
					GL.Vertex3(startX+j, i+offsetY, gridSizeZ);
				}
			}
			for(float i = 0; i <= gridSizeZ - startZ; i+=smallStep){
				for(float k = 0; k <= gridSizeX - startX; k+=smallStep){
					GL.Vertex3(startX+k, startY+offsetY, startZ+i);
					GL.Vertex3(startX+k, gridSizeY+offsetY, startZ+i);
				}
			}
		}
		//show the main grid, large grid
		if (showMain) {
			GL.Color(subColor);
			for(float i = 0; i <= gridSizeY; i+=largeStep){
				for(float j = 0; j <= gridSizeZ - startZ; j+=largeStep){
					GL.Vertex3(startX, i+offsetY, startZ+j);
					GL.Vertex3(gridSizeX, i+offsetY, startZ+j);
				}
				for(float j = 0; j <= gridSizeX - startX; j+=largeStep){
					GL.Vertex3(startX+j, i+offsetY, startZ);
					GL.Vertex3(startX+j, i+offsetY, gridSizeZ);
				}
			}
			for(float i = 0; i <= gridSizeZ - startZ; i+=largeStep){
				for(float k = 0; k <= gridSizeX - startX; k+=largeStep){
					GL.Vertex3(startX+k, startY+offsetY, startZ+i);
					GL.Vertex3(startX+k, gridSizeY+offsetY, startZ+i);
				}
			}
		}
		GL.End ();
	}
}
