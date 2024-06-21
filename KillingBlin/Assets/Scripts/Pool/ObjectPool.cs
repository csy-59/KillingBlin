using System;
using System.Collections.Generic;

/// <summary>
/// 오브젝트 풀에 들어갈 수 있는 객체의 베이스
/// </summary>
public interface IPoolable
{
    /// <summary> 풀 안에 들어있는지 여부 </summary>
    public bool InPool { get; set; }
    
    /// <summary> 스스로를 복제한다. 풀로 되돌아갈 수 있는 콜백함수를 받는다. </summary>
    public abstract IPoolable Clone(Action<IPoolable> returnToPool);
    
    /// <summary>
    /// 풀에 들어가기 전에 호출되는 함수
    /// </summary>
    public abstract void Sleep();
    
    /// <summary>
    /// 풀에서 나오기 전에 호출되는 함수
    /// </summary>
    public abstract void Wakeup();
}

/// <summary>
/// 오브젝트를 생성해주고 관리해주는 풀
/// </summary>
public class ObjectPool<T> where T : IPoolable
{
    /// <summary> 모든 오브젝트의 정보가 있는 풀 </summary>
    protected List<T> objectList;
    /// <summary> 풀 안에 있는 오브젝트 리스트 </summary>
    protected List<T> objectInPool;
    /// <summary> 풀 밖에 있는 오브젝트 리스트 </summary>
    protected List<T> objectOutPool;

    protected T originalObject;

    public int Count => objectInPool.Count;
    
    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="originalObject"> 풀에서 생성할 객체 </param>
    /// <param name="capacity"> 예측되는 최대 크기 </param>
    /// <param name="initCount"> 미리 생성할 개수 </param>
    public ObjectPool(IPoolable originalObject, int capacity, int initCount = 0)
    {
        this.originalObject = (T)originalObject;
        objectList = new List<T>(capacity);
        objectInPool = new List<T>(capacity);
        objectOutPool = new List<T>(capacity);
        
        for (int i = 0; i < initCount; ++i)
        {
            CreatePoolObject();
        }
    }

    protected IPoolable CreatePoolObject()
    {
        var newObject = (T)originalObject.Clone(Push);
        newObject.Sleep();
        newObject.InPool = true;
        objectList.Add(newObject);
        objectInPool.Add(newObject);
        return newObject;
    }
    
    /// <summary>
    /// 오브젝트를 꺼낸다.
    /// </summary>
    public T Pop()
    {
        if (Count == 0)
        {
            CreatePoolObject();
        }

        var obj = objectInPool[objectInPool.Count - 1];
        objectInPool.RemoveAt(objectInPool.Count - 1);
        obj.InPool = false;

        return obj;
    }
    
    /// <summary>
    /// 오브젝트를 집어 넣는다.
    /// </summary>
    public void Push(IPoolable poolObject)
    {
        var obj = (T)poolObject;
        objectOutPool.Remove(obj);
        objectList.Add(obj);
        objectInPool.Add(obj);
        poolObject.InPool = true;
    }
}
