import socket
import time
import json
import random

# Konfiguration
BROADCAST_IP = "255.255.255.255" # Dette er "alle adresser" (Broadcast)
PORT = 44556

# 1. Opret UDP socket DGRAM er socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# 2. Broadcast (vigtigt, ellers må man ikke sende til 255.255.255.255)
sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)

print(f"Starter UDP Broadcast på port {PORT}...")

while True:
    # 3. Generer tilfældig vægt mellem 50 og 200
    random_weight = random.randint(50, 200)
    
    # 4. Opret Chair objektet
    chair = {
        "Model": "Gamer Stol X",    
        "MaxWeight": random_weight, # Tilfældig værdi
        "HasPillow": True           
    }
    
    # 5. Konverter til JSON string
    message = json.dumps(chair)
    
    # 6. Send beskeden ud i netværket
    # .encode() for at lave teksten om til bytes
    sock.sendto(message.encode(), (BROADCAST_IP, PORT))
    
    print(f"Sendte broadcast: {message}")
    time.sleep(2)