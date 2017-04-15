# AES
# Security of Computers and Communication Networks _ Assignment2
## Encryption/Decryption interface:
 –a <AES1/AES3> : denotes the algorithm to use: "AES1" for 𝐴𝐸𝑆1 and "AES3" for 𝐴𝐸𝑆3
 –e : instruction to encrypt the input file
 –d: instruction to decrypt the input file
 –k <path>: path to the key(s) , the key should be 128 bit for 𝐴𝐸𝑆1 or 384 bit (128*3) for 𝐴𝐸𝑆3
The latter should be divided into 3 separate keys.
 –i <input file path>: a path to a file we want to encrypt/decrypt
 –o <output file path>: a path to the output file
## Usage:
 aes –a <AES1 or AES3> -e/-d –k <path-to-key-file > -i <path-to-input-file> -o <path-to-output-file>
 Example: 𝐴𝐸𝑆3
 encryption test for a Jar submission will be executed using:
 aes –a AES3 –e –k key.txt –i message.txt –o cypther.txt
 ## Hacking (breaking) interface:
 –a <AES1/AES3> : denotes the algorithm to break: "AES1" for 𝐴𝐸𝑆1 and "AES3" for 𝐴𝐸𝑆3
 –b: instruction to break the encryption algorithm
 –m <path>: denotes the path to the plain-text message
 –c <path>: denotes the path to the cipher-text message
 –o <path>: a path to the output file with the key(s) found.
## Usage:
 aes –a <AES1 or AES3> -b –m <path-to-message> –c <path-to-cipher> -o < output-path>
