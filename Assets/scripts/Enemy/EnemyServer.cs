/*using GameClient.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyServer : MonoBehaviour
{
    #region ===================Request====================
    public static void SendEnemyAI()
    {
        var data = new Dictionary<byte, object>();
        data[1] = EnemyCode.EnemyAI;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data, true);
    }
    #endregion

    #region ===================Response====================
    #endregion
}
*/