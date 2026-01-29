import socket

PORT = 7000
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind(('', PORT))

print("Lytter efter stole...")
while True:
    data, addr = sock.recvfrom(1024)
    print(f"Modtog fra {addr}: {data.decode()}")