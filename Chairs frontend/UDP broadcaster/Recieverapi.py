import socket
import json
import requests

# 1. Konfiguration
UDP_PORT = 44556 # Krav fra opgaven
# HUSK: Tjek dit portnummer i Swagger/Browser (f.eks. https://localhost:/api/chairs)
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

        # 4. Håndter JSON advarslen fra opgaven!
        # Opgaven siger: "hvis du bruger parameteren json=... så laver den det om til JSON."
        # Da 'message' ALLEREDE er en JSON-string, må vi ikke bare sende den råt ind i json=
        # Løsning: Vi laver den om til et Python-objekt (dict) først via json.loads()
        chair_object = json.loads(message)

        # 5. Send til REST API (POST)
        # verify=False er nødvendigt fordi vi bruger https på localhost (self-signed certifikat)
        response = requests.post(API_URL, json=chair_object, verify=False)

        print(f"API Svar: {response.status_code}") # 201 betyder Created (Succes)

    except Exception as e:
        print(f"Fejl: {e}")