import cv2
import mediapipe as mp
from makejson import makejson

def hpe(file):
    mp_drawing = mp.solutions.drawing_utils
    mp_drawing_styles = mp.solutions.drawing_styles
    mp_pose = mp.solutions.pose

    with mp_pose.Pose(
        static_image_mode=False,
        model_complexity=1,
        enable_segmentation=True,
        min_detection_confidence=0.5) as pose:

        # bytes -> 이미지 읽어옴
        image = cv2.imdecode(file, cv2.IMREAD_COLOR)
        image = cv2.resize(image, dsize=(640, 480), interpolation=cv2.INTER_AREA)
        # Convert the BGR image to RGB before processing.
        results = pose.process(cv2.cvtColor(image, cv2.COLOR_BGR2RGB))

        # 랜드마크 json 형식으로 생성
        landmark_json = makejson(results)

        # 랜드마크 표시된 이미지 출력
        annotated_image = image.copy()
        mp_drawing.draw_landmarks(
            annotated_image,
            results.pose_landmarks,
            mp_pose.POSE_CONNECTIONS,
            landmark_drawing_spec=mp_drawing_styles.
            get_default_pose_landmarks_style())
        
        return annotated_image, landmark_json

def hpe_test(file):
    mp_drawing = mp.solutions.drawing_utils
    mp_drawing_styles = mp.solutions.drawing_styles
    mp_pose = mp.solutions.pose

    with mp_pose.Pose(
        static_image_mode=False,
        model_complexity=1,
        enable_segmentation=True,
        min_detection_confidence=0.5) as pose:

        # bytes -> 이미지 읽어옴
        image = cv2.imread(file, cv2.IMREAD_COLOR)
        image = cv2.resize(image, dsize=(640, 480), interpolation=cv2.INTER_AREA)
        # Convert the BGR image to RGB before processing.
        results = pose.process(cv2.cvtColor(image, cv2.COLOR_BGR2RGB))

        # 랜드마크 json 형식으로 생성
        landmark_json = makejson(results)

        # 랜드마크 표시된 이미지 출력
        annotated_image = image.copy()
        mp_drawing.draw_landmarks(
            annotated_image,
            results.pose_landmarks,
            mp_pose.POSE_CONNECTIONS,
            landmark_drawing_spec=mp_drawing_styles.
            get_default_pose_landmarks_style())
        
        return annotated_image, landmark_json