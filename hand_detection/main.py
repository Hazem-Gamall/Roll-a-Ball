from fileinput import close
import cv2
import mediapipe as mp
from tcp_client import get_client
import numpy as np

mp_hands = mp.solutions.hands
hands = mp_hands.Hands()
mp_drawing = mp.solutions.drawing_utils
video = cv2.VideoCapture(0)

print(ord("q"))

def calculate_distance(p1, p2):
    return ((p2[0] - p1[0])**2 + (p2[1] - p1[1])**2)**0.5

def get_orint_vector(points):
    points = np.asarray(points)
    print('points', points)
    normal_vector = np.cross(points[2] - points[0], points[1] - points[2])
    normal_vector = normal_vector / np.linalg.norm(normal_vector)
    return normal_vector


while True:
    ret, frame = video.read()
    result = hands.process(frame)
    if result.multi_hand_landmarks:
        for hand in result.multi_hand_landmarks:
            lmlist = []
            for id, lm in enumerate(hand.landmark):
                h, w, c = frame.shape
                x, y = int(lm.x * w), int(lm.y * h)
                lmlist.append([x, y])
            
            print('lmlist', len(lmlist))
            mp_drawing.draw_landmarks(frame, hand, mp_hands.HAND_CONNECTIONS)
            # cv2.circle(frame, (lmlist[12][0], lmlist[12][1]), 10, (255, 0, 0), cv2.FILLED)
            # cv2.circle(frame, (lmlist[0][0], lmlist[0][1]), 10, (255, 0, 0), cv2.FILLED)
            orint = get_orint_vector([lmlist[0],lmlist[5], lmlist[17]])
            forward_distance = calculate_distance(lmlist[0], lmlist[12])
            left_distance = calculate_distance(lmlist[4], lmlist[8])
            right_distance = calculate_distance(lmlist[4], lmlist[16])
            jump_distance = calculate_distance(lmlist[4], lmlist[12])

            send_data, close_connection = get_client()
            send_data('w' if forward_distance > 150 else 's')
            send_data('a' if left_distance < 30 else '')
            send_data('d' if right_distance < 30 else '')
            send_data(' ' if jump_distance < 30 else '')
    cv2.imshow("frame", frame)
    if cv2.waitKey(1) == ord("q"):
        break

close_connection()
video.release()
cv2.destroyAllWindows()

