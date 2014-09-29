using UnityEngine;
using System.Collections;

public class BlockDestructionController : MonoBehaviour {

    public enum BlockType { Destructible, Solid, SeeThrough } ;

    public BlockType blockType = BlockType.Destructible; 

    public void OnTriggerEnter(Collider other)
    {
        BulletModel bulletModel = other.transform.GetComponent<BulletModel>();

        if (bulletModel == null)
            return;

        switch (blockType)
        {
            case BlockType.Destructible:
                Destroy(other.gameObject);
                Destroy(transform.gameObject);
                break;
            case BlockType.Solid:
                Destroy(other.gameObject);
                break;
            case BlockType.SeeThrough:
                //Do nothing for the minute
                break;
        }

    }
}
