from http import server
from time import time
import cv2
from cv2 import FONT_HERSHEY_COMPLEX
from cv2 import FONT_HERSHEY_SIMPLEX
import mediapipe as mp
import numpy as np
import socket
import matplotlib.pyplot as plt
import timeit


mp_hands = mp.solutions.hands
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
ip = "localhost"
port = 8081
sock.connect((ip,port))
land_mark = timeit.default_timer()



def distance_count(a, b):
    c = np.linalg.norm(np.array(a) - np.array(b))
    return int(c)
def get_angel_with_x(a, b):
    if a[0]==b[0]:
        c = np.pi/2
    else:
        c = np.arctan(np.abs(a[1]-b[1])/np.abs(a[0]-b[0]))
    if a[0]>b[0]:
      c = np.pi-c
    if a[1]>b[1]:
      c = 0   
    c = c*180/np.pi
    return int(c)

def get_angel(a,b,c):
  radians = np.arctan2(c[1]-b[1],c[0]-b[0])-np.arctan2(a[1]-b[1],a[0]-b[0])
  angel = np.abs(radians*180.0/np.pi)
  if angel > 180.0:
    angel = 360-angel
  return int(angel)

def is_finger_open(wrist,pip,tip):
  if get_angel(wrist, pip,tip)>90:
    return 1
  else:
    return 0
  

motor_start = 0

# For webcam input:
cap = cv2.VideoCapture(0)

with mp_hands.Hands(
    model_complexity=0,
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as hands:
  while cap.isOpened():
    success, image = cap.read()
    if not success:
      print("Ignoring empty camera frame.")
      # If loading a video, use 'break' instead of 'continue'.
      continue

    # To improve performance, optionally mark the image as not writeable to
    # pass by reference.
    image = cv2.flip(image,1)
    image.flags.writeable = False
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = hands.process(image)

    # Draw the hand annotations on the image.
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    if results.multi_hand_landmarks: 
      for hand_landmarks in results.multi_hand_landmarks:
        wrist = [int(hand_landmarks.landmark[0].x*np.shape(image)[1]), int(hand_landmarks.landmark[0].y*np.shape(image)[0])]
        index_finger_tip = [int(hand_landmarks.landmark[8].x*np.shape(image)[1]), int(hand_landmarks.landmark[8].y*np.shape(image)[0])]
        index_finger_pip = [int(hand_landmarks.landmark[6].x*np.shape(image)[1]), int(hand_landmarks.landmark[6].y*np.shape(image)[0])]
        middle_finger_tip = [int(hand_landmarks.landmark[12].x*np.shape(image)[1]), int(hand_landmarks.landmark[12].y*np.shape(image)[0])]
        middle_finger_pip = [int(hand_landmarks.landmark[10].x*np.shape(image)[1]), int(hand_landmarks.landmark[10].y*np.shape(image)[0])]
        ring_finger_tip = [int(hand_landmarks.landmark[16].x*np.shape(image)[1]), int(hand_landmarks.landmark[16].y*np.shape(image)[0])]
        ring_finger_pip = [int(hand_landmarks.landmark[14].x*np.shape(image)[1]), int(hand_landmarks.landmark[14].y*np.shape(image)[0])]
        pinky_tip = [int(hand_landmarks.landmark[20].x*np.shape(image)[1]), int(hand_landmarks.landmark[20].y*np.shape(image)[0])]
        pinky_pip = [int(hand_landmarks.landmark[18].x*np.shape(image)[1]), int(hand_landmarks.landmark[18].y*np.shape(image)[0])]
        thump_tip = [int(hand_landmarks.landmark[4].x*np.shape(image)[1]), int(hand_landmarks.landmark[4].y*np.shape(image)[0])]
        thump_mcp = [int(hand_landmarks.landmark[2].x*np.shape(image)[1]), int(hand_landmarks.landmark[2].y*np.shape(image)[0])]
      if (is_finger_open(wrist,thump_mcp,thump_tip) 
        & ~is_finger_open(wrist,pinky_pip,pinky_tip)
        & ~is_finger_open(wrist,ring_finger_pip,ring_finger_tip)
        & ~is_finger_open(wrist,middle_finger_pip,middle_finger_tip)
        & ~is_finger_open(wrist,index_finger_pip, index_finger_tip)):
        cv2.putText(image, 'Start',[10,50],FONT_HERSHEY_SIMPLEX,2, (255, 255, 50),3,cv2.LINE_AA) 
        if (motor_start==0):
          sock.send(b'Start\n')
          print("Start_motor")
          motor_start = 1
              
        

      if (is_finger_open(wrist,thump_mcp,thump_tip) 
        & is_finger_open(wrist,pinky_pip,pinky_tip)
        & is_finger_open(wrist,ring_finger_pip,ring_finger_tip)
        & is_finger_open(wrist,middle_finger_pip,middle_finger_tip)
        & is_finger_open(wrist,index_finger_pip, index_finger_tip)):
        cv2.putText(image, 'Stop',[10,50],FONT_HERSHEY_SIMPLEX,2, (255, 255, 50),3,cv2.LINE_AA)
        sock.send(b'Stop\n')
        print("Stop motor")
        motor_start = 0

      if motor_start:

        angel = get_angel_with_x(thump_tip, wrist)
        motor_RPM = int(angel*750/180)
        axis_length = distance_count(thump_tip, wrist)
        axis_pointy = (wrist[0], wrist[1]-axis_length)
        axis_pointx = (wrist[0]-axis_length, wrist[1])
        cv2.circle(image, thump_tip, 10, (1,100,1),3)
        cv2.circle(image, wrist, 10, (150,150,150),3)
        cv2.circle(image, axis_pointx, 10, (100,1,1),3)
        cv2.line(image, thump_tip, wrist, (100, 100, 100),3)
        cv2.line(image, wrist, axis_pointx, (30, 30, 100),3)
        Text_loca = (int((thump_tip[0] + axis_pointx[0])/2-30),int((thump_tip[1] + axis_pointx[1])/2)+30)
        cv2.ellipse(image, wrist,(axis_length,axis_length),0,-180,-180+angel ,(150,100,200),3)
        cv2.putText(image, str(angel),Text_loca,FONT_HERSHEY_SIMPLEX,3, (255, 255, 50),3,cv2.LINE_AA)
        ##print('motor_power',motor_RPM)  
        chuoi = str(motor_RPM) + '\n'
        chuoi = bytes(chuoi, 'utf-8')
                
        if timeit.default_timer() - land_mark >= 0.025:
          sock.send(chuoi)
          land_mark = timeit.default_timer()
          



    cv2.imshow('MediaPipe Hands', image)
      
    if cv2.waitKey(5) & 0xFF == 27:
      break
cap.release()