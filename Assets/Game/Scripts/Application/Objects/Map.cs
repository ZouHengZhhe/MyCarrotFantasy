using System.Collections.Generic;
using UnityEngine;

//用于描述一个关卡地图的状态
public class Map : MonoBehaviour
{
    #region 常量

    public const int RowCount = 8;      //行数
    public const int ColumnCount = 12;  //列数

    #endregion 常量

    #region 字段

    private float MapWidth;//地图宽
    private float MapHeight;//地图高

    private float TileWidth;//格子宽
    private float TileHeight;//格子高

    private Level m_level; //关卡数据

    private List<Tile> m_grid = new List<Tile>(); //格子集合
    private List<Tile> m_road = new List<Tile>(); //路径集合

    #endregion 字段

    #region 属性

    //背景图片
    public string BackgroundImage
    {
        set
        {
            SpriteRenderer render = transform.Find("Background").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
        }
    }

    //路径图片
    public string RoadImage
    {
        set
        {
            SpriteRenderer render = transform.Find("Road").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
        }
    }

    #endregion 属性

    #region 方法

    public void LoadLevel(Level level)
    {
        //清除当前状态
        Clear();

        //保存
        this.m_level = level;

        //加载图片
        this.BackgroundImage = "file://" + Consts.LevelDir + "/" + level.Background;
        this.RoadImage = "file://" + Consts.MapDir + "/" + level.Road;

        //寻路点
        for (int i = 0; i < level.Path.Count; i++)
        {
            Point p = level.Path[i];
            Tile t = GetTile(p.X, p.Y);
            m_road.Add(t);
        }

        //炮塔点
        for (int i = 0; i < level.Holder.Count; i++)
        {
            Point p = level.Holder[i];
            Tile t = GetTile(p.X, p.Y);
            t.CanHold = true;
        }
    }

    //清除所有信息
    public void Clear()
    {
        m_level = null;
        ClearHolder();
        ClearRoad();
    }

    //清除塔位信息
    public void ClearHolder()
    {
        foreach (Tile t in m_grid)
        {
            if (t.CanHold)
            {
                t.CanHold = false;
            }
        }
    }

    //清除路径信息
    public void ClearRoad()
    {
        m_road.Clear();
    }

    #endregion 方法

    #region Unity回调

    private void Awake()
    {
        //计算地图和格子大小
        CalculateSize();

        //创建所有的格子
        for (int i = 0; i < RowCount; i++)//行，Y值
        {
            for (int j = 0; j < ColumnCount; j++) //列，X值
            {
                m_grid.Add(new Tile(j, i));
            }
        }
    }

    #endregion Unity回调

    #region 帮助方法

    //计算地图大小，格子大小
    private void CalculateSize()
    {
        Vector3 leftDown = new Vector3(0, 0);
        Vector3 rightUp = new Vector3(1, 1);

        Vector3 p1 = Camera.main.ViewportToWorldPoint(leftDown);
        Vector3 p2 = Camera.main.ViewportToWorldPoint(rightUp);

        MapWidth = (p2.x - p1.x);
        MapHeight = (p2.y - p1.y);

        TileWidth = MapWidth / ColumnCount;
        TileHeight = MapHeight / RowCount;
    }

    //根据格子索引号获得格子
    private Tile GetTile(int tileX, int tileY)
    {
        int index = tileX + tileY * ColumnCount;

        if (index < 0 || index >= m_grid.Count)
            return null;

        return m_grid[index];
    }

    #endregion 帮助方法
}