import cv2
import PIL.Image, PIL.ImageTk
from tkinter import *

from socket import *
import sys
import numpy as np
import time
from hpe import hpe
import cv2



class App:
    '''
        기본적인 출력하는 UI 설정부분 
    '''
    def __init__(self, window):
        self.width, self.height = 640,480
        # self.width, self.height = 1280,480
        self.window = window
        # self.window.geometry("640x480")
        self.window.geometry("1280x480")

        self.window.title("Application Programming Team02 HPE python server")
        self.cap = cv2.VideoCapture(0)
        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH, self.width)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, self.height)
        self.canvas = Canvas(window, width = self.width, height = self.height)
        self.canvas.pack(side = "right")

        # self.canvas.bind("<Button-1>", self.mouse_down)
        # self.canvas.bind("<B1-Motion>", self.mouse_move)
        # self.canvas.bind("<ButtonRelease-1>", self.mouse_up)
        self.canvas_on_down = False
        self.delay = 33
        serverSocket = socket(AF_INET, SOCK_STREAM)
        serverPort = 5006
        serverSocket.bind(('localhost', serverPort))
        serverSocket.listen(1)
        print('server listening')
        clientSocket, addr = serverSocket.accept()
        print('Connection from ', addr[0])

        self.client()
        self.window.mainloop()
    def client(self):
        #     # client로부터 데이터 받아옴
        try:
            msg = self.clientSocket.recv(16384)
            ctime = time.time()
            read = b''
            if(msg):
                while True:
                    read += msg
                    msg = self.clientSocket.recv(16384)
                    if len(msg) < 16384:
                        read += msg
                        break
                
                # 읽어온 byte array -> np 매트릭스에 올림 (np array만 cv2 imread 가능)
                read_np = np.fromstring(read, dtype = np.uint8)
                # HPE 결과 img, landmark 표시된 json
                img, landmark_json = hpe(read_np)
                print('Message received!')
                self.canvas.create_image(0, 0, image = self.photo, anchor = NW)

                if landmark_json is not None:
                    self.clientSocket.sendall(landmark_json.encode())
                    print('Landmark is sent!')
                    print('finish: ', time.time()-ctime)

        except IOError:
            # client 연결 없을 시  소켓 닫음
            clientSocket.close()
            print('Disconnected!')
            # client 연결 대기
            clientSocket, addr = self.serverSocket.accept()
            print('Connection from ', addr[0])
            
        self.window.after(self.delay, self.client)
                


# serverSocket = socket(AF_INET, SOCK_STREAM)
# serverPort = 5006
# serverSocket.bind(('localhost', serverPort))
# serverSocket.listen(1)
# print('server listening')
# clientSocket, addr = serverSocket.accept()
# print('Connection from ', addr[0])
App(Tk())