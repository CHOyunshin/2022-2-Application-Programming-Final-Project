using UnityEngine;

public class RigControl : MonoBehaviour
{
    public GameObject Remy;
    private Animator anim;
    private Transform lua, lla, rua, rla;
    private Quaternion lua_init, lla_init, rua_init, rla_init;
    private Joint[] joints;

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
        joints = SocketClient.instance.jointList.joint;
        if(joints.Length > 0)
        {
            Vector3[] lualla = CalcArms(joints, "left");
            lualla = RigArm(lualla[0], lualla[1], "left");
            Vector3[] ruarla = CalcArms(joints, "right");
            ruarla = RigArm(ruarla[0], ruarla[1], "right");
            lualla[0] = ObjRad2Deg(lualla[0]);
            lualla[1] = ObjRad2Deg(lualla[1]);
            ruarla[0] = ObjRad2Deg(ruarla[0]);
            ruarla[1] = ObjRad2Deg(ruarla[1]);

            lua.rotation = lua_init * Quaternion.Euler(lualla[0]);
            lla.rotation = lla_init * Quaternion.Euler(lualla[1]);
            rua.rotation = rua_init * Quaternion.Euler(ruarla[0]);
            rla.rotation = rla_init * Quaternion.Euler(ruarla[1]);            
        }

    }

    private float NormalizeRadians(float rad)
    {
        if(rad >= Mathf.PI/2)
        { 
            rad -= 2 * Mathf.PI;
        }
        if(rad <= -Mathf.PI/2)
        {
            rad += 2 * Mathf.PI;
            rad = Mathf.PI - rad;
        }
        return rad/Mathf.PI;
    }

    private float Find2DAngle(float cx, float cy, float ex, float ey)
    {
        float dy = ey - cy;
        float dx = ex - cx;
        float theta = Mathf.Atan2(dy, dx);
        return theta;
    }

    private Vector3 FindRotation(Joint j1, Joint j2)
    {
        float x = NormalizeRadians(Find2DAngle(j1.z, j1.x, j2.z, j2.x));
        float y = NormalizeRadians(Find2DAngle(j1.z, j1.y, j2.z, j2.y));
        float z = NormalizeRadians(Find2DAngle(j1.x, j1.y, j2.x, j2.y));
        return new Vector3(x, y, z);
    }

    private float AngleBetween3DCoords(Joint j1, Joint j2, Joint j3)
    {
        Vector3 j_vec1 = new Vector3(j1.x, j1.y, j1.z);
        Vector3 j_vec2 = new Vector3(j2.x, j2.y, j2.z);
        Vector3 j_vec3 = new Vector3(j3.x, j3.y, j3.z);

        Vector3 v1 = j_vec1 - j_vec2;
        Vector3 v2 = j_vec3 - j_vec2;

        v1 = Vector3.Normalize(v1);
        v2 = Vector3.Normalize(v2);

        float dotProduct = Vector3.Dot(v1, v2);
        float angle = Mathf.Acos(dotProduct);
        return NormalizeRadians(angle);
    }

    private float Clamp(float val, float mini, float maxi)
    {
        return Mathf.Max(Mathf.Min(val, maxi), mini);
    }

    private Vector3[] RigArm(Vector3 ua, Vector3 la, string side)
    {
        float invert = (side == "left") ? 1f : -1f;

        ua.z *= 2.3f * invert;
        ua.y *= Mathf.PI * invert;
        ua.y -= la.x;
        ua.y -= -invert * Mathf.Max(la.z, 0);
        ua.x -= 0.2f * invert;

        la.z *= -1.5f * -2.14f * invert;
        la.y *= 2f * 2.14f * invert;
        la.x *= 1f * 2.14f * invert;

        ua.x = Clamp(ua.x, -0.5f, 1f);
        la.x = Clamp(la.x, -0.3f, 0.3f);

        Vector3[] uala = new Vector3[2];
        uala[0] = ua;
        uala[1] = la;

        return uala;
    }

    private Vector3[] CalcArms(Joint[] lm, string side)
    {
        if(side == "left")
        {
            Vector3 lua = FindRotation(lm[11], lm[13]);
            lua.y = AngleBetween3DCoords(lm[12], lm[11], lm[13]);
            
            Vector3 lla = FindRotation(lm[13], lm[15]);
            lla.y = AngleBetween3DCoords(lm[11], lm[13], lm[15]);
            // lla.z = Clamp(lla.z, -2.14f, 0);

            Vector3[] lualla = new Vector3[2];
            lualla[0] = lua;
            lualla[1] = lla;

            return lualla;
        }
        else
        {
            Vector3 rua = FindRotation(lm[12], lm[14]);
            rua.y = AngleBetween3DCoords(lm[13], lm[12], lm[14]);
            
            Vector3 rla = FindRotation(lm[14], lm[16]);
            rla.y = AngleBetween3DCoords(lm[12], lm[14], lm[16]);
            // rla.z = Clamp(rla.z, -2.14f, 0);

            Vector3[] ruarla = new Vector3[2];
            ruarla[0] = rua;
            ruarla[1] = rla;

            return ruarla;
        }
    }

    private float Rad2Deg(float rad)
    { return rad * 180 / Mathf.PI; }

    private Vector3 ObjRad2Deg(Vector3 obj)
    {
        obj.x = Rad2Deg(obj.x);
        obj.y = Rad2Deg(obj.y);
        obj.z = Rad2Deg(obj.z);
        return obj;
    }
}
