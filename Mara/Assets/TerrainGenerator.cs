using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Unity.Collections;
using System;
using System.IO.Compression;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 50; // Width of the terrain grid
    public int height = 50; // Height of the terrain grid
    public float scale = 20f; // Scale of the Perlin noise
    public int treeChance = 10;
    public Tilemap tilemap; // Reference to the isometric tilemap
    public TileBase[] tiles; // Array of tiles for different heights
    public Sprite treeSprite;
    

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                // Generate Perlin noise value based on x and y
                float heightValue = Mathf.PerlinNoise(x / scale, y / scale);

                // Map height value to tile index
                int tileIndex = Mathf.FloorToInt(heightValue * tiles.Length);

                // Get the tile to paint
                TileBase tile = tiles[tileIndex];

                // Convert grid coordinates to world coordinates
                Vector3Int cellPosition = new Vector3Int(x, y, tileIndex);

                // Place the tile on the tilemap
                tilemap.SetTile(cellPosition, tile);
                
                System.Random rnd = new System.Random();
                
                int treeNum = rnd.Next(1,100);
                if(treeNum <= treeChance){
                    // Generates a tree on the tile above the current. 

                    //Debug.Log("Tree Generated");
                   
                        GameObject tree = new GameObject("Tree");

                        Vector3 pos = tilemap.CellToWorld(cellPosition);

                        // Add SpriteRenderer component to the tree GameObject
                        SpriteRenderer spriteRenderer = tree.AddComponent<SpriteRenderer>();
                        spriteRenderer.sprite = treeSprite;
                        spriteRenderer.sortingOrder = 1; 

                       // Offset for tree position above the tile
                        Vector3 treeOffset = new Vector3(0, 2.25f, 0); // Adjust the Y value to position the tree higher or lower as needed

                        // Position the tree above the tile
                        tree.transform.position = pos + treeOffset;


                }

                // For every (x,y), make all z's below it to -10 the same block.
                for (int z = tileIndex-1; z > -10; z--){
                   Vector3Int underground = new Vector3Int(x, y,tileIndex - z);
                   tilemap.SetTile(underground, tile);
                }

            }
        }
    }
}
