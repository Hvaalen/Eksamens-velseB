import socket
import json
import requests

# 1. Konfiguration
UDP_PORT = 44556 
API_URL = "https://localhost:5125/chairs" 

# 2. Opsæt UDP Socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind(('', UDP_PORT))

print(f"Lytter på port {UDP_PORT} og videresender til API...")

while True:
    try:
        # 3. Modtag data fra Broadcaster
        data, addr = sock.recvfrom(1024)
        message = data.decode()
        print(f"Modtog fra UDP: {message}")
        chair_object = json.loads(message)

        response = requests.post(API_URL, json=chair_object, verify=False)

        print(f"API Svar: {response.status_code}")

    except Exception as e:
        print(f"Fejl: {e}")