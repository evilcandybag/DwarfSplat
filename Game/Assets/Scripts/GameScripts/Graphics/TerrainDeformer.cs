using UnityEngine;
using System.Collections;

public class TerrainDeformer : MonoBehaviour
{
    public int terrainDeformationTextureSnow = 0;
	public int terrainDeformationTextureGrass = 0;
	public int snowTexture = 1;
	public int grassTexture = 1;
    private Terrain terr; // terrain to modify
    protected int hmWidth; // heightmap width
    protected int hmHeight; // heightmap height
    protected int alphaMapWidth;
    protected int alphaMapHeight;
    protected int numOfAlphaLayers;
    protected const float DEPTH_METER_CONVERT=0.05f;
    protected const float TEXTURE_SIZE_MULTIPLIER = 1.25f;
    private float[,] heightMapBackup;
    private float[, ,] alphaMapBackup;
	bool isGrass = false;
	
	void Start()
    {
        terr = this.GetComponent<Terrain>();
        hmWidth = terr.terrainData.heightmapWidth;
        hmHeight = terr.terrainData.heightmapHeight;
        alphaMapWidth = terr.terrainData.alphamapWidth;
        alphaMapHeight = terr.terrainData.alphamapHeight;
        numOfAlphaLayers = terr.terrainData.alphamapLayers;
        if (Debug.isDebugBuild)
        {
            heightMapBackup = terr.terrainData.GetHeights(0, 0, hmWidth, hmHeight);
            alphaMapBackup = terr.terrainData.GetAlphamaps(0, 0, alphaMapWidth, alphaMapHeight);   
        } 
    }

    //this has to be done because terrains for some reason or another terrains don't reset after you run the app
    void OnApplicationQuit()
    {
        if (Debug.isDebugBuild)
        {
            terr.terrainData.SetHeights(0, 0, heightMapBackup);
            terr.terrainData.SetAlphamaps(0, 0, alphaMapBackup);
        }
    }
	
    public void DestroyTerrain(Vector3 pos, float craterSizeInMeters)
    {
        DeformTerrain(pos,craterSizeInMeters);
        TextureDeformation(pos, craterSizeInMeters*1.5f);
    }
    
    protected void DeformTerrain(Vector3 pos, float craterSizeInMeters)
    {
        //get the heights only once keep it and reuse, precalculate as much as possible
        Vector3 terrainPos = GetRelativeTerrainPositionFromPos(pos,terr,hmWidth,hmHeight);
        int heightMapCraterWidth = (int)(craterSizeInMeters * (hmWidth / terr.terrainData.size.x));
        int heightMapCraterLength = (int)(craterSizeInMeters * (hmHeight / terr.terrainData.size.z));
        int heightMapStartPosX = (int)(terrainPos.x - (heightMapCraterWidth / 2));
        int heightMapStartPosZ = (int)(terrainPos.z - (heightMapCraterLength / 2));

        float[,] heights = terr.terrainData.GetHeights(heightMapStartPosX, heightMapStartPosZ, heightMapCraterWidth, heightMapCraterLength);
        float circlePosX;
        float circlePosY;
        float distanceFromCenter;
        float depthMultiplier;
		

        float deformationDepth = (craterSizeInMeters / 3.0f) / terr.terrainData.size.y;

        // we set each sample of the terrain in the size to the desired height
        for (int i = 0; i < heightMapCraterLength; i++) //width
        {
            for (int j = 0; j < heightMapCraterWidth; j++) //height
            {
                circlePosX = (j - (heightMapCraterWidth / 2)) / (hmWidth / terr.terrainData.size.x);
                circlePosY = (i - (heightMapCraterLength / 2)) / (hmHeight / terr.terrainData.size.z);
                distanceFromCenter = Mathf.Abs(Mathf.Sqrt(circlePosX * circlePosX + circlePosY * circlePosY));
                //convert back to values without skew
                
                if (distanceFromCenter < (craterSizeInMeters / 2.0f))
                {
                    depthMultiplier = ((craterSizeInMeters / 2.0f - distanceFromCenter) / (craterSizeInMeters / 2.0f));

                    depthMultiplier += 0.1f;
                    depthMultiplier += Random.value * .1f;

                    depthMultiplier = Mathf.Clamp(depthMultiplier, 0, 1);
                    heights[i, j] = Mathf.Clamp(heights[i, j] - deformationDepth * depthMultiplier, 0, 1);
                }

            }
        }

        // set the new height
       terr.terrainData.SetHeights(heightMapStartPosX, heightMapStartPosZ, heights);
    }
	
	public void TextureSnow(Vector3 pos){
		Vector3 alphaMapTerrainPos = GetRelativeTerrainPositionFromPos(pos, terr, alphaMapWidth, alphaMapHeight);
        int alphaMapStartPosX = (int)(alphaMapTerrainPos.x);
        int alphaMapStartPosZ = (int)(alphaMapTerrainPos.z);
		
        float[, ,] alphas = terr.terrainData.GetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, 100, 100);
		
 		for (int i = 0; i < 100; i++) //width 
		{
            for (int j = 0; j < 100; j++) //height
            {
               for (int layerCount = 0; layerCount < numOfAlphaLayers; layerCount++)
                    {
                        //could add blending here in the future
                        if (layerCount == grassTexture)
                        {
                           alphas[i, j, layerCount] = 1;
								print ("SNOWW");
                        }
                       else
                       {
                         alphas[i, j, layerCount] = 0;
								print ("SNOWWelse");
                     }
                 }
            }
        } 
		terr.terrainData.SetAlphamaps(0, 0, alphas);
	}
	public bool isGrassTexture(){
       /* float[, ,] splatmapData = terr.terrainData.GetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, alphaMapCraterWidth, alphaMapCraterLength);

		float[] cellMix = new float[splatmapData.GetUpperBound(2)+1];
        for (int n=0; n<cellMix.Length; ++n)
        {
            cellMix[n] = splatmapData[0,0,n];    
        }
	//	print("cell "+cellMix);
		float grassiness = cellMix[0];
		float snowiness = cellMix[2];
		if(grassiness ==1){
			return true;	
		}
		else{
			return false;	
		}*/
		return isGrass;
	}
	
	
    protected void TextureDeformation(Vector3 pos, float craterSizeInMeters)
    {
        Vector3 alphaMapTerrainPos = GetRelativeTerrainPositionFromPos(pos, terr, alphaMapWidth, alphaMapHeight);
        int alphaMapCraterWidth = (int)(craterSizeInMeters * (alphaMapWidth / terr.terrainData.size.x));
        int alphaMapCraterLength = (int)(craterSizeInMeters * (alphaMapHeight / terr.terrainData.size.z));

        int alphaMapStartPosX = (int)(alphaMapTerrainPos.x - (alphaMapCraterWidth / 2));
        int alphaMapStartPosZ = (int)(alphaMapTerrainPos.z - (alphaMapCraterLength/2));

        float[, ,] splatmapData = terr.terrainData.GetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, alphaMapCraterWidth, alphaMapCraterLength);

        float circlePosX;
        float circlePosY;
        float distanceFromCenter;
		
	    // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
       // float[,,] splatmapData = terrainData.GetAlphamaps(mapX,mapZ,1,1);
 
        // extract the 3D array data to a 1D array:
        float[] cellMix = new float[splatmapData.GetUpperBound(2)+1];
        for (int n=0; n<cellMix.Length; ++n)
        {
            cellMix[n] = splatmapData[0,0,n];    
        }
	//	print("cell "+cellMix);
		float grassiness = cellMix[grassTexture];
		float snowiness = cellMix[snowTexture];
	//	print("grass"+grassiness);
	//	print("snow" +grassiness);

        for (int i = 0; i < alphaMapCraterLength; i++) //width
        {
            for (int j = 0; j < alphaMapCraterWidth; j++) //height
            {
                circlePosX = (j - (alphaMapCraterWidth / 2)) / (alphaMapWidth / terr.terrainData.size.x);
                circlePosY = (i - (alphaMapCraterLength / 2)) / (alphaMapHeight / terr.terrainData.size.z);

                //convert back to values without skew
                distanceFromCenter = Mathf.Abs(Mathf.Sqrt(circlePosX * circlePosX + circlePosY * circlePosY));

				if (distanceFromCenter < (craterSizeInMeters / 2.0f))
                {
                    for (int layerCount = 0; layerCount < numOfAlphaLayers; layerCount++)
                    {					
						if(snowiness == 1){
							isGrass = false;
                        //could add blending here in the future
                       	 	if (layerCount == terrainDeformationTextureSnow)
                      	  	{
                          		  splatmapData[i, j, layerCount] = 1;
                      	  	}
                       	  	else
                         	{
                   			splatmapData[i, j, layerCount] = 0;
						  	}
						}
						else if(grassiness == 1){
							isGrass = true;
                        //could add blending here in the future
                       		if (layerCount == terrainDeformationTextureGrass)
                      	  	{
                          	  	splatmapData[i, j, layerCount] = 1;
                      	  	}
	                       	else
	                        {
	                            splatmapData[i, j, layerCount] = 0;
	                        
							}
						}
                    }
                }
            }
        } 

       terr.terrainData.SetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, splatmapData);
    }
	
	//This method makes a 100 vs 100 squre with gras but it is too heavy so it will be a pause in 2 sek before the changes appear...
	public void changeGrassTerrain(){
		// read all layers of the alphamap (the splatmap) into a 3D float array:
	/*	float[,,] alphaMapData = terr.terrainData.GetAlphamaps(10, 10, 20, 20);
		 
		 
		// read all detail layers into a 3D int array:
		int numDetails = terr.terrainData.detailPrototypes.Length;
		int [,,] detailMapData = new int[terr.terrainData.detailWidth, terr.terrainData.detailHeight, numDetails];
		for (int layerNum=0; layerNum < numDetails; layerNum++) {
		    int[,] detailLayer = terr.terrainData.GetDetailLayer(10,10, 20, 20, layerNum);
		}
		 
		 
		// write all detail data to terrain data:
		for (int n = 0; n < detailMapData.Length; n++)
		{
		  //  terr.terrainData.SetDetailLayer(0, 0, n, detailMapData[n,n]);
		}
		 */
		// write alpha map data to terrain data:
	//	terr.terrainData.SetAlphamaps(0, 0, alphaMapData);
	}
	
	
    protected Vector3 GetNormalizedPositionRelativeToTerrain(Vector3 pos, Terrain terrain)
    {
        Vector3 tempCoord = (pos - terrain.gameObject.transform.position);
        Vector3 coord;
        coord.x = tempCoord.x / terr.terrainData.size.x;
        coord.y = tempCoord.y / terr.terrainData.size.y;
        coord.z = tempCoord.z / terr.terrainData.size.z;

        return coord;
    }

    protected Vector3 GetRelativeTerrainPositionFromPos(Vector3 pos,Terrain terrain, int mapWidth, int mapHeight)
    {
        Vector3 coord = GetNormalizedPositionRelativeToTerrain(pos, terrain);
        // get the position of the terrain heightmap where this game object is
        return new Vector3((coord.x * mapWidth), 0, (coord.z * mapHeight));
    }     
}
