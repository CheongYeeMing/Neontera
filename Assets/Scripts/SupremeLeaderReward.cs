using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupremeLeaderReward : BossReward
{
    public Item keyItem;
    public override void GetReward(CharacterLevel characterLevel, CharacterWallet characterWallet)
    {
        if (isRewardGiven) return;
        isRewardGiven = true;
        characterLevel.GainExperience(expReward);
        characterWallet.AddGold(goldReward);
        Vector3 v3 = gameObject.transform.position;
        Instantiate(keyItem, new Vector3(v3.x, v3.y - 0.3f, v3.z), Quaternion.identity);
    }
}
