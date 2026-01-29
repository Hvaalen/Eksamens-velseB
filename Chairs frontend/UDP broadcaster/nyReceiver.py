import socket

# Lyt på port 7000 på ALLE adresser
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind(('', 44556))

print("Lytter efter UDP (Tryk Ctrl+C for at stoppe)...")

while True:
    data, addr = sock.recvfrom(1024)
    print(f"Modtog: {data.decode()} fra {addr}")