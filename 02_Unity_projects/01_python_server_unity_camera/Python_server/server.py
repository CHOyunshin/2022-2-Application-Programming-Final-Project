from socket import *
import sys
import numpy as np
import time
from hpe import hpe
import cv2

serverSocket = socket(AF_INET, SOCK_STREAM)
serverPort = 5000
serverSocket.bind(('localhost', serverPort))
serverSocket.listen(1)
print('server listening')

clientSocket, addr = serverSocket.accept()
print('Connection from ', addr[0])

while True:
    # client로부터 데이터 받아옴
    try:
        msg = clientSocket.recv(16384)
        ctime = time.time()
        read = b''
        if(msg):
            while True:
                read += msg
                msg = clientSocket.recv(16384)
                if len(msg) < 16384:
                    read += msg
                    break
            
            # 읽어온 byte array -> np 매트릭스에 올림 (np array만 cv2 imread 가능)
            read_np = np.fromstring(read, dtype = np.uint8)
            
            # HPE 결과 img, landmark 표시된 json
            img, landmark_json = hpe(read_np)
            print('Message received!')
            
            # 서버 영상 출력 테스트용
            cv2.imshow('Mediapipe Pose', cv2.flip(img, 1))
            if cv2.waitKey(10) == 13:
                cv2.destroyAllWindows()
            
            if landmark_json is not None:
                clientSocket.sendall(landmark_json.encode())
                print('Landmark is sent!')
                print('finish: ', time.time()-ctime)
                
    
    except IOError:
        # client 연결 없을 시  소켓 닫음
        clientSocket.close()
        print('Disconnected!')
        cv2.waitKey(0)
        cv2.destroyAllWindows()
        cv2.waitKey(1)
        # client 연결 대기
        clientSocket, addr = serverSocket.accept()
        print('Connection from ', addr[0])

serverSocket.close()
sys.exit()
