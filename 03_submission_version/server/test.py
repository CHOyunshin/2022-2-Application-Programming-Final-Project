import cv2
import mediapipe as mp
import numpy as np
import math

class Object:
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

def getLandmarks(image):
    mp_pose = mp.solutions.pose

    with mp_pose.Pose(
        static_image_mode=False,
        model_complexity=1,
        enable_segmentation=True,
        min_detection_confidence=0.5) as pose:

        # bytes -> 이미지 읽어옴
        image = cv2.imread('image/'+image, cv2.IMREAD_COLOR)
        image = cv2.resize(image, dsize=(640, 480), interpolation=cv2.INTER_AREA)
        # Convert the BGR image to RGB before processing.
        results = pose.process(cv2.cvtColor(image, cv2.COLOR_BGR2RGB))

        # 랜드마크
        landmark = results.pose_landmarks.landmark
    
    return landmark

def normalizeRadians(rad):
    if rad >= math.pi/2:
        rad -= 2 * math.pi
    if rad <= -math.pi/2:
        rad += 2 * math.pi
        rad = math.pi - rad
    # returns values to (-1, 1)
    return rad/math.pi

def find2DAngle(cx, cy, ex, ey):
    dy = ey - cy
    dx = ex - cx
    theta = math.atan2(dy, dx)
    return theta

def findRotation(lm1, lm2):
    # normalize version
    x = normalizeRadians(find2DAngle(lm1.z, lm1.x, lm2.z, lm2.x))
    y = normalizeRadians(find2DAngle(lm1.z, lm1.y, lm2.z, lm2.y))
    z = normalizeRadians(find2DAngle(lm1.x, lm1.y, lm2.x, lm2.y))
    return Object(x, y, z)

def angleBetween3DCoords(lm1, lm2, lm3):
    lm1 = np.array([lm1.x, lm1.y, lm1.z])
    lm2 = np.array([lm2.x, lm2.y, lm2.z])
    lm3 = np.array([lm3.x, lm3.y, lm3.z])

    v1 = lm1 - lm2
    v2 = lm3 - lm2

    v1norm = v1 / np.linalg.norm(v1)
    v2norm = v2 / np.linalg.norm(v2)

    dotProducts = np.dot(v1norm, v2norm)
    angle = math.acos(dotProducts)
    return normalizeRadians(angle)

def clamp(val, mini, maxi):
    return max(min(val, maxi), mini)

def lerp(lm1, lm2, fraction):
    lm1 = np.array([lm1.x, lm1.y, lm1.z])
    lm2 = np.array([lm2.x, lm2.y, lm2.z])
    result = np.subtract(lm2, lm1)
    result *= fraction
    result = np.add(result, lm1)
    return Object(result[0], result[1], result[2])

def rigArm(ua, la, side):
    invert = 1 if side == 'left' else -1

    ua.z *= 2.3 * invert
    ua.y *= math.pi * invert
    ua.y -= la.x
    ua.y -= -invert * max(la.z, 0)
    ua.x -= 0.2 * invert

    la.z *= -1.8 * -2.14 * invert
    la.y *= 2 * 2.14 * invert
    la.x *= 1 * 2.14 * invert

    # clamp vlaues to human limits
    ua.x = clamp(ua.x, -0.5, math.pi)
    la.x = clamp(la.x, -0.3, 0.3)

    return ua, la

def calcArms(lm, side):
    if side == 'left':
        print(lm[11])
        # Left Upper Arm
        lua = findRotation(lm[11], lm[13])
        lua.y = angleBetween3DCoords(lm[12], lm[11], lm[13])
        # Left Lower Arm
        lla = findRotation(lm[13], lm[15])
        lla.y = angleBetween3DCoords(lm[11], lm[13], lm[15])
        lla.z = clamp(lla.z, -2.14, 0)
        return lua, lla
    else:
        # Left Upper Arm
        rua = findRotation(lm[12], lm[14])
        rua.y = angleBetween3DCoords(lm[13], lm[12], lm[14])
        # Left Lower Arm
        rla = findRotation(lm[14], lm[16])
        rla.y = angleBetween3DCoords(lm[12], lm[14], lm[16])
        rla.z = clamp(rla.z, -2.14, 0)
        return rua, rla


# radian to degree angle transform
def rad2deg(rad):
    return rad * 180 / math.pi

def objRad2Deg(obj):
    obj.x = rad2deg(obj.x)
    obj.y = rad2deg(obj.y)
    obj.z = rad2deg(obj.z)
    return obj


# main
lm = getLandmarks('4.jpeg')
lua, lla = calcArms(lm, 'left')
lua, lla = rigArm(lua, lla, 'left')
rua, rla = calcArms(lm, 'right')
rua, rla = rigArm(rua, rla, 'right')
lua, lla = objRad2Deg(lua), objRad2Deg(lla)
rua, rla = objRad2Deg(rua), objRad2Deg(rla)

print('\nleft upper arm: ', [lua.x, lua.y, lua.z])
print('left lower arm: ', [lla.x, lla.y, lla.z])
print('\nright upper arm: ', [rua.x, rua.y, rua.z])
print('right lower arm: ', [rla.x, rla.y, rla.z])

