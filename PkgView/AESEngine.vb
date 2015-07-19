Imports System.IO
Imports System.Security.Cryptography

Public Class AESEngine
    Shared alg As Rijndael = Rijndael.Create()
    Shared crypto As ICryptoTransform = Nothing
    Shared decrypto As ICryptoTransform = Nothing
    Shared ms As MemoryStream = Nothing
    Shared cs As CryptoStream = Nothing

    Public Shared Sub free()
        If cs IsNot Nothing Then cs.Dispose()
        cs = Nothing
        If ms IsNot Nothing Then ms.Dispose()
        ms = Nothing
        If crypto IsNot Nothing Then crypto.Dispose()
        crypto = Nothing
    End Sub

    ' Encrypt a byte array into a byte array using a key and an IV 
    ' ECB does not need an IV.
    Public Shared Function Encrypt(ByVal clearData As Byte(), ByVal Key As Byte(), ByVal IV As Byte(), ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode) As Byte()
        ' Create a MemoryStream to accept the encrypted bytes 
        'Dim ms As New MemoryStream()
        If ms Is Nothing Then
            ms = New MemoryStream()
            ' Now set the key and the IV. 
            ' We need the IV (Initialization Vector) because
            ' the algorithm is operating in its default 
            ' mode called CBC (Cipher Block Chaining).
            ' The IV is XORed with the first block (8 byte) 
            ' of the data before it is encrypted, and then each
            ' encrypted block is XORed with the 
            ' following block of plaintext.
            ' This is done to make encryption more secure. 

            alg.Mode = cipherMode
            alg.Padding = paddingMode
            alg.Key = Key
            alg.IV = IV
        Else
            ms.Position = 0
        End If

        ' Create a symmetric algorithm. 
        ' We are going to use Rijndael because it is strong and
        ' available on all platforms. 
        ' You can use other algorithms, to do so substitute the
        ' next line with something like 
        '      TripleDES alg = TripleDES.Create(); 
        'Dim alg As Rijndael = Rijndael.Create()

        

        ' Create a CryptoStream through which we are going to be
        ' pumping our data. 
        ' CryptoStreamMode.Write means that we are going to be
        ' writing data to the stream and the output will be written
        ' in the MemoryStream we have provided. 

        If crypto Is Nothing Then crypto = alg.CreateEncryptor()

        If cs Is Nothing Then cs = New CryptoStream(ms, crypto, CryptoStreamMode.Write)

        ' Write the data and make it do the encryption 
        cs.Write(clearData, 0, clearData.Length)

        ' Close the crypto stream (or do FlushFinalBlock). 
        ' This will tell it that we have done our encryption and
        ' there is no more data coming in, 
        ' and it is now a good time to apply the padding and
        ' finalize the encryption process. 

        'cs.Close()

        ' Now get the encrypted data from the MemoryStream.
        ' Some people make a mistake of using GetBuffer() here,

        Dim encryptedData As Byte() = ms.ToArray()

        Return encryptedData

    End Function

    ' Encrypt a string into a string using a password
    Public Shared Function Encrypt(ByVal clearText As String, ByVal Password As String, ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode) As String
        ' First we need to turn the input string into a byte array. 
        Dim clearBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(clearText)

        ' Then, we need to turn the password into Key and IV 
        ' We are using salt to make it harder to guess our key
        ' using a dictionary attack - 
        ' trying to guess a password by enumerating all possible words. 

        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})

        ' Now get the key/IV and do the encryption using the
        ' function that accepts byte arrays. 
        ' Using PasswordDeriveBytes object we are first getting
        ' 32 bytes for the Key 
        ' (the default Rijndael key length is 256bit = 32bytes)
        ' and then 16 bytes for the IV. 
        ' IV should always be the block size, which is by default
        ' 16 bytes (128 bit) for Rijndael. 
        ' If you are using DES/TripleDES/RC2 the block size is
        ' 8 bytes and so should be the IV size. 
        ' You can also read KeySize/BlockSize properties off
        ' the algorithm to find out the sizes. 

        Dim encryptedData As Byte() = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16), cipherMode, paddingMode)

        ' Now we need to turn the resulting byte array into a string. 
        ' A common mistake would be to use an Encoding class for that.
        'It does not work because not all byte values can be
        ' represented by characters. 
        ' We are going to be using Base64 encoding that is designed
        'exactly for what we are trying to do. 

        Return Convert.ToBase64String(encryptedData)

    End Function

    ' Encrypt bytes into bytes using a password 
    Public Shared Function Encrypt(ByVal clearData As Byte(), ByVal Password As String, ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode) As Byte()
        ' We need to turn the password into Key and IV. 
        ' We are using salt to make it harder to guess our key
        ' using a dictionary attack - 
        ' trying to guess a password by enumerating all possible words. 

        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})

        ' Now get the key/IV and do the encryption using the function
        ' that accepts byte arrays. 
        ' Using PasswordDeriveBytes object we are first getting
        ' 32 bytes for the Key 
        ' (the default Rijndael key length is 256bit = 32bytes)
        ' and then 16 bytes for the IV. 
        ' IV should always be the block size, which is by default
        ' 16 bytes (128 bit) for Rijndael. 
        ' If you are using DES/TripleDES/RC2 the block size is 8
        ' bytes and so should be the IV size. 
        ' You can also read KeySize/BlockSize properties off the
        ' algorithm to find out the sizes. 

        Return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16), cipherMode, paddingMode)
    End Function

    ' Encrypt a file into another file using a password 

    Public Shared Sub Encrypt(ByVal fileIn As String, ByVal fileOut As String, ByVal Password As String, ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode)
        ' First we are going to open the file streams 
        Dim fsIn As New FileStream(fileIn, FileMode.Open, FileAccess.Read)
        Dim fsOut As New FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write)

        ' Then we are going to derive a Key and an IV from the
        ' Password and create an algorithm 

        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})

        Dim alg As Rijndael = Rijndael.Create()

        alg.Mode = cipherMode
        alg.Padding = paddingMode
        alg.Key = pdb.GetBytes(32)
        alg.IV = pdb.GetBytes(16)

        ' Now create a crypto stream through which we are going
        ' to be pumping data. 
        ' Our fileOut is going to be receiving the encrypted bytes. 

        If crypto Is Nothing Then crypto = alg.CreateEncryptor()

        Dim cs As New CryptoStream(fsOut, crypto, CryptoStreamMode.Write)

        ' Now will will initialize a buffer and will be processing
        ' the input file in chunks. 
        ' This is done to avoid reading the whole file (which can
        ' be huge) into memory. 

        Dim bufferLen As Integer = 4096
        Dim buffer As Byte() = New Byte(bufferLen - 1) {}
        Dim bytesRead As Integer

        Do
            ' read a chunk of data from the input file 
            bytesRead = fsIn.Read(buffer, 0, bufferLen)

            ' encrypt it 
            cs.Write(buffer, 0, bytesRead)
        Loop While bytesRead <> 0

        ' close everything 
        ' this will also close the unrelying fsOut stream
        cs.Close()
        fsIn.Close()
    End Sub

    ' Decrypt a byte array into a byte array using a key and an IV 
    Public Shared Function Decrypt(ByVal cipherData As Byte(), ByVal Key As Byte(), ByVal IV As Byte(), ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode) As Byte()
        ' Create a MemoryStream that is going to accept the
        ' decrypted bytes 
        Dim ms As New MemoryStream()

        ' Create a symmetric algorithm. 
        ' We are going to use Rijndael because it is strong and
        ' available on all platforms. 
        ' You can use other algorithms, to do so substitute the next
        ' line with something like 
        '     TripleDES alg = TripleDES.Create(); 

        'Dim alg As Rijndael = Rijndael.Create()

        ' Now set the key and the IV. 
        ' We need the IV (Initialization Vector) because the algorithm
        ' is operating in its default 
        ' mode called CBC (Cipher Block Chaining). The IV is XORed with
        ' the first block (8 byte) 
        ' of the data after it is decrypted, and then each decrypted
        ' block is XORed with the previous 
        ' cipher block. This is done to make encryption more secure. 
        ' There is also a mode called ECB which does not need an IV,
        ' but it is much less secure. 

        alg.Mode = cipherMode
        alg.Padding = paddingMode
        alg.Key = Key
        alg.IV = IV

        ' Create a CryptoStream through which we are going to be
        ' pumping our data. 
        ' CryptoStreamMode.Write means that we are going to be
        ' writing data to the stream 
        ' and the output will be written in the MemoryStream
        ' we have provided. 

        If decrypto Is Nothing Then crypto = alg.CreateDecryptor()

        Dim cs As New CryptoStream(ms, decrypto, CryptoStreamMode.Write)

        ' Write the data and make it do the decryption 
        cs.Write(cipherData, 0, cipherData.Length)

        ' Close the crypto stream (or do FlushFinalBlock). 
        ' This will tell it that we have done our decryption
        ' and there is no more data coming in, 
        ' and it is now a good time to remove the padding
        ' and finalize the decryption process. 

        cs.Close()

        ' Now get the decrypted data from the MemoryStream. 
        ' Some people make a mistake of using GetBuffer() here,
        ' which is not the right way. 

        Dim decryptedData As Byte() = ms.ToArray()

        Return decryptedData
    End Function

    ' Decrypt a string into a string using a password 
    Public Shared Function Decrypt(ByVal cipherText As String, ByVal Password As String, ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode) As String
        ' First we need to turn the input string into a byte array. 
        ' We presume that Base64 encoding was used 

        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)

        ' Then, we need to turn the password into Key and IV 
        ' We are using salt to make it harder to guess our key
        ' using a dictionary attack - 
        ' trying to guess a password by enumerating all possible words. 

        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})

        ' Now get the key/IV and do the decryption using
        ' the function that accepts byte arrays. 
        ' Using PasswordDeriveBytes object we are first
        ' getting 32 bytes for the Key 
        ' (the default Rijndael key length is 256bit = 32bytes)
        ' and then 16 bytes for the IV. 
        ' IV should always be the block size, which is by
        ' default 16 bytes (128 bit) for Rijndael. 
        ' If you are using DES/TripleDES/RC2 the block size is
        ' 8 bytes and so should be the IV size. 
        ' You can also read KeySize/BlockSize properties off
        ' the algorithm to find out the sizes. 

        Dim decryptedData As Byte() = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16), cipherMode, paddingMode)

        ' Now we need to turn the resulting byte array into a string. 
        ' A common mistake would be to use an Encoding class for that.
        ' It does not work 
        ' because not all byte values can be represented by characters. 
        ' We are going to be using Base64 encoding that is 
        ' designed exactly for what we are trying to do. 

        Return System.Text.Encoding.Unicode.GetString(decryptedData)
    End Function

    ' Decrypt bytes into bytes using a password 
    Public Shared Function Decrypt(ByVal cipherData As Byte(), ByVal Password As String, ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode) As Byte()
        ' We need to turn the password into Key and IV. 
        ' We are using salt to make it harder to guess our key
        ' using a dictionary attack - 
        ' trying to guess a password by enumerating all possible words. 

        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})

        ' Now get the key/IV and do the Decryption using the 
        'function that accepts byte arrays. 
        ' Using PasswordDeriveBytes object we are first getting
        ' 32 bytes for the Key 
        ' (the default Rijndael key length is 256bit = 32bytes)
        ' and then 16 bytes for the IV. 
        ' IV should always be the block size, which is by default
        ' 16 bytes (128 bit) for Rijndael. 
        ' If you are using DES/TripleDES/RC2 the block size is
        ' 8 bytes and so should be the IV size. 

        ' You can also read KeySize/BlockSize properties off the
        ' algorithm to find out the sizes. 
        Return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16), cipherMode, paddingMode)
    End Function

    ' Decrypt a file into another file using a password 
    Public Shared Sub Decrypt(ByVal fileIn As String, ByVal fileOut As String, ByVal Password As String, ByVal cipherMode As CipherMode, ByVal paddingMode As PaddingMode)

        ' First we are going to open the file streams 

        Dim fsIn As New FileStream(fileIn, FileMode.Open, FileAccess.Read)
        Dim fsOut As New FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write)

        ' Then we are going to derive a Key and an IV from
        ' the Password and create an algorithm 

        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})
        'Dim alg As Rijndael = Rijndael.Create()

        alg.Mode = cipherMode
        alg.Padding = paddingMode
        alg.Key = pdb.GetBytes(32)
        alg.IV = pdb.GetBytes(16)

        ' Now create a crypto stream through which we are going
        ' to be pumping data. 
        ' Our fileOut is going to be receiving the Decrypted bytes. 

        If decrypto Is Nothing Then crypto = alg.CreateDecryptor()

        Dim cs As New CryptoStream(fsOut, decrypto, CryptoStreamMode.Write)

        ' Now will will initialize a buffer and will be 
        ' processing the input file in chunks. 
        ' This is done to avoid reading the whole file (which can be
        ' huge) into memory. 

        Dim bufferLen As Integer = 4096
        Dim buffer As Byte() = New Byte(bufferLen - 1) {}
        Dim bytesRead As Integer

        Do
            ' read a chunk of data from the input file 
            bytesRead = fsIn.Read(buffer, 0, bufferLen)

            ' Decrypt it 

            cs.Write(buffer, 0, bytesRead)
        Loop While bytesRead <> 0

        ' close everything 
        cs.Close()
        ' this will also close the unrelying fsOut stream 
        fsIn.Close()
    End Sub
End Class