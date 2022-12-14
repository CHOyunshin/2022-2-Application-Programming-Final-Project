import cv2
import numpy as np
from IPython.display import clear_output
import IPython

# Set camera resolution.
cameraWidth = 640
cameraHeight = 480

face_classifier = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

# calculate the middle screen
midScreenX = int(cameraWidth / 2)
midScreenY = int(cameraHeight / 2)
midScreenWindow = 100

def face_detector(img):
# Convert image to grayscale
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

    # Clasify face from the gray image
    faces = face_classifier.detectMultiScale(gray, 1.3, 5)
    if faces is ():
        return img, []

    for (x, y, w, h) in faces:
        cv2.rectangle(img, (x, y), (x + w, y + h), (0, 255, 255), 2)
        # find the midpoint of the first face in the frame.
        xCentre = int(x + (w / 2))
        yCentre = int(y + (w / 2))
        cv2.circle(img, (xCentre, yCentre), 5, (0, 255, 255), -1)

    localization = [xCentre, yCentre]
    # print(localization)
    return img, localization


# Open Webcam
cap = cv2.VideoCapture(0) # 0 is camera device number, 0 is for internal webcam and 1 will access the first connected usb webcam

# Set camera resolution. The max resolution is webcam dependent
# so change it to a resolution that is both supported by your camera
# and compatible with your monitor
cap.set(cv2.CAP_PROP_FRAME_WIDTH, cameraWidth)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, cameraHeight)

while True:

    # Capture frame-by-frame
    ret, frame = cap.read()

    # mirror the frame
    frame = cv2.flip(frame, 1)

    image, face = face_detector(frame)

    # cv2.line(image, starting cordinates, ending cordinates, color, thickness)
    # left vertical line
    cv2.line(image, ((midScreenX - midScreenWindow), 0), ((midScreenX - midScreenWindow), cameraHeight), (255, 127, 0),2)
    # right vertical line
    cv2.line(image, ((midScreenX + midScreenWindow), 0), ((midScreenX + midScreenWindow), cameraHeight), (255, 127, 0),2)
    # up horizontal line
    cv2.line(image, (0, (midScreenY - midScreenWindow)), (cameraWidth, (midScreenY - midScreenWindow)), (255, 127, 0),2)
    # down horizontal line
    cv2.line(image, (0, (midScreenY + midScreenWindow)), (cameraWidth, (midScreenY + midScreenWindow)), (255, 127, 0),2)

    # only if the face is recoginize
    if len(face) == 2:
        midFaceX = face[0]
        midFaceY = face[1]
    # Find out if the X component of the face is to the left of the middle of the screen.
    if (midFaceX < (midScreenX - midScreenWindow)):
        cv2.putText(image, "Move Left", (250, 450), cv2.FONT_HERSHEY_COMPLEX, 1, (0, 255, 0), 2)
        # if(servoPanPosition >= 5):
    # servoPanPosition -= stepSize; #Update the pan position variable to move the servo to the left.
    if (midFaceX > (midScreenX + midScreenWindow)):
        cv2.putText(image, "Move Right", (250, 450), cv2.FONT_HERSHEY_COMPLEX, 1, (0, 255, 0), 2)
    if (midFaceY < (midScreenY - midScreenWindow)):
        cv2.putText(image, "Move Up", (250, 450), cv2.FONT_HERSHEY_COMPLEX, 1, (0, 255, 0), 2)
    if (midFaceY > (midScreenY + midScreenWindow)):
        cv2.putText(image, "Move Down", (250, 450), cv2.FONT_HERSHEY_COMPLEX, 1, (0, 255, 0), 2)

        # print(x)
        # print(y)

    # Display the resulting frame
    cv2.imshow('Video', frame)

    # Press Q on keyboard to exit
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break

# When everything is done, release the capture
# video_capture.release()
app = IPython.Application.instance()
app.kernel.do_shutdown(True) # turn off kernel
clear_output() # clear output
cv2.destroyAllWindows()