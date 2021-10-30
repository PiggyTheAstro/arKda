using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Color> playerColors;
    [SerializeField] List<Color> enemyColors;
    [SerializeField] List<Color> bulletColors;
    [SerializeField] List<Color> wallColors;
    List<Color>[] colorListsList;
    [SerializeField] Material player;
    [SerializeField] Material enemy;
    [SerializeField] Material bullet;
    [SerializeField] Material wall;
    Material[] matList;
    void Start()
    {
        List<Color>[] colorListsList = { playerColors, enemyColors, bulletColors, wallColors};
        Material[] matList = { player, enemy, bullet, wall };
        for (int i = 0; i < matList.Length; i++)
        {
            matList[i].color = colorListsList[i][(int)Random.Range(0, colorListsList[i].Count)];
            ClearColorLists(matList[i].color);
        }
    }
    void ClearColorLists(Color col)
    {
        playerColors.Remove(col);
        enemyColors.Remove(col);
        bulletColors.Remove(col);
        wallColors.Remove(col);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
