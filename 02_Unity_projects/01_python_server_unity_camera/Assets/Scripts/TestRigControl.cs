using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRigControl : MonoBehaviour
{
    public GameObject Remy;
    private Animator anim;
    private Transform lua, lla, rua, rla;
    private Quaternion lua_init, lla_init,rua_init, rla_init;
    public Vector3 lua_v, lla_v, rua_v, rla_v;
    void Start()
    {
        anim = Remy.GetComponent<Animator>();

        // Bones
        lua = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        lla = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        rua = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
        rla = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);

        // Init Quaternion
        lua_init = lua.rotation;
        lla_init = lla.rotation;
        rua_init = rua.rotation;
        rla_init = rla.rotation;
    }

    void Update()
    {
        lua.rotation = lua_init * Quaternion.Euler(lua_v);
        lla.rotation = lla_init * Quaternion.Euler(lla_v);
        rua.rotation = rua_init * Quaternion.Euler(rua_v);
        rla.rotation = rla_init * Quaternion.Euler(rla_v);
    }
}
