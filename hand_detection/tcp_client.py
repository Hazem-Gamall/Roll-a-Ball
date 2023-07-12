import socket
import time

host, port = "127.0.0.1", 25001
# data = "1,2,3"
# data2 = "4,5,6"

# SOCK_STREAM means TCP socket

def get_client():
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    sock.connect((host, port))

    def send_data(data):
        sock.sendall(data.encode("utf-8"))
        
    def close_connection():
        sock.close()
    
    return send_data, close_connection
