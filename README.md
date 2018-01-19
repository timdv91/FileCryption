FileCryption uses the position of Bytes in the KeyFile as index for encryption.

When encrypting a random secret-file, each byte of the secret-file is searched inside
the KeyFile. The position is used as index and the index is saved to the encrypted file.

When decrypting, the secret-encrypted-file their index position Byte representative is retreaved from the
KeyFile and rewritten as the standard Byte.

