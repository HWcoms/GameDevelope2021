using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdongGenerator : MonoBehaviour
{
    const float CREATE_INTERVAL = 0.7f;
    float mCreatTime = 0;
    float mTotalTIme = 0;

    private float GRAVITY = 3.2f;

    private float mVelocity = 0f;



    float mNextCreateInterval = CREATE_INTERVAL;

    int mPhase = 1;

    public GameObject mDong;

    private void Update()
    {
        mTotalTIme += Time.deltaTime;
        mCreatTime += Time.deltaTime;
        if (mCreatTime > mNextCreateInterval)
        {
            mCreatTime = 0;
            mNextCreateInterval = CREATE_INTERVAL - (0.005f * mTotalTIme);
            //Debug.Log("mNextCreateInterval : " + mNextCreateInterval);
            if (mNextCreateInterval < 0.005f)
            {
                mNextCreateInterval = 0.005f;
            }

            for (int i = 0; i < mPhase; i++)
            {
                creatDdong(8f + i * 0.2f);
            }

        }

        if (mTotalTIme >= 10f)
        {
            mTotalTIme = 0;
            mPhase++;
        }
    }

    private void creatDdong(float y)
    {
        float x = Random.Range(-4f, 4f);
        createObject(mDong, new Vector3(x, y, 0), Quaternion.identity);
    }

    private GameObject createObject(GameObject original, Vector3 position, Quaternion rotation)
    {
        return (GameObject)Instantiate(original, position, rotation);
    }
}

