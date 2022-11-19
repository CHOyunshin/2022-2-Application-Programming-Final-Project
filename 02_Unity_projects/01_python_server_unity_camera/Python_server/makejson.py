import json

# landmark 값 dictionary로 바꿔 json 형식 생성
def makejson(result):
    if(result.pose_landmarks is None):
        return

    datas = result.pose_landmarks.landmark
    landmark_dict = {}
    landmark_list = []
    for idx, data in enumerate(datas):
        j = {
            "index":idx,
            "x":data.x,
            "y":data.y,
            "z":data.z,
            "visibility":data.visibility
        }
        landmark_list.append(j)

    landmark_dict['joint'] = landmark_list    
    landmark_json = json.dumps(landmark_dict, indent=2)

    return landmark_json

# landmark index -> name
def landmark_name(idx):
    name = ['NOSE',
     'LEFT_EYE_INNER',
     'LEFT_EYE',
     'LEFT_EYE_OUTER',
     'RIGHT_EYE_INNER',
     'RIGHT_EYE',
     'RIGHT_EYE_OUTER',
     'LEFT_EAR',
     'RIGHT_EAR',
     'MOUTH_LEFT',
     'MOUTH_RIGHT',
     'LEFT_SHOULDER',
     'RIGHT_SHOULDER',
     'LEFT_ELBOW',
     'RIGHT_ELBOW',
     'LEFT_WRIST',
     'RIGHT_WRIST',
     'LEFT_PINKY',
     'RIGHT_PINKY',
     'LEFT_INDEX',
     'RIGHGT_INDEX',
     'LEFT_THUMB',
     'RIGHT_THUMB',
     'LEFT_HIP',
     'RIGHT_HIP',
     'LEFT_KNEE',
     'RIGHT_KNEE',
     'LEFT_ANKLE',
     'RIGHT_ANKLE',
     'LEFT_HEEL',
     'RIGHT_HEEL',
     'LEFT_FOOT_INDEX',
     'RIGHT_FOOT_INDEX']
    
    return name[idx]