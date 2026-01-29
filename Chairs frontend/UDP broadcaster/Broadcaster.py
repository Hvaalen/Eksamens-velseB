import socket
import time
import json
import random

# Konfiguration
BROADCAST_IP = "255.255.255.255" # Dette er "alle adresser" (Broadcast)
PORT = 44556                      # Den port vi sender data ud på

# 1. Opret UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# 2. Tillad broadcast (vigtigt, ellers må man ikke sende til 255.255.255.255)
sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)

print(f"Starter UDP Broadcast på port {PORT}...")

while True:
    # 3. Generer tilfældig vægt mellem 50 og 200 (Krav i opgaven)
    random_weight = random.randint(50, 200)
    
    # 4. Opret Chair objektet
    chair = {
        "Model": "Gamer Stol X",    # Hardcoded (tilladt ifølge opgaven)
        "MaxWeight": random_weight, # Tilfældig værdi
        "HasPillow": True           # Hardcoded (tilladt ifølge opgaven)
    }
    
    # 5. Konverter til JSON string
    message = json.dumps(chair)
    
    # 6. Send beskeden ud i netværket
    # Vi skal huske at .encode() for at lave teksten om til bytes
    sock.sendto(message.encode(), (BROADCAST_IP, PORT))
    
    print(f"Sendte broadcast: {message}")
    
    # 7. Vent 2 sekunder før næste besked
    time.sleep(2)