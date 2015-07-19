Option Explicit On
Imports System.Threading
Imports System.IO
Imports System.Security.Cryptography

Module pkgmgr
    Public Structure PKG_header
        Public magic As Byte()
        Public type As UInt32
        Public hdr_size As UInt32
        Public unk1 As UInt32
        Public sec2_Offset As UInt32
        Public num_files As UInt32
        Public filesize As UInt64
        Public data_sec_offset As UInt64
        Public size_data As UInt64
        Public name As String
        Public qa_digest As Byte()
        Public sha1 As Byte()
        Public PKGFileKey As Byte()
        Public key As Byte()
    End Structure

    Public hdr As PKG_header
    Dim hashAlg As SHA1

    Private PS3AesKey As Byte() = New Byte(15) { _
            &H2E, &H7B, &H71, &HD7, &HC9, &HC9, &HA1, &H4E, _
            &HA3, &H22, &H1F, &H18, &H88, &H28, &HB8, &HF8}

    Private PSPAesKey As Byte() = New Byte(15) { _
            &H7, &HF2, &HC6, &H82, &H90, &HB5, &HD, &H2C, _
            &H33, &H81, &H8D, &H70, &H9B, &H60, &HE6, &H2B}

    Public Function ReadInt64(ByVal stream As FileStream) As UInt64
        Dim buf As Byte() = New Byte(7) {}

        stream.Read(buf, 0, 8)

        Array.Reverse(buf, 0, 8)

        Return BitConverter.ToUInt64(buf, 0)
    End Function

    Public Function ReadInt32(ByVal stream As FileStream) As UInt32
        Dim buf As Byte() = New Byte(3) {}

        stream.Read(buf, 0, 4)

        Array.Reverse(buf, 0, 4)

        Return BitConverter.ToUInt32(buf, 0)
    End Function

    Sub WriteInt16(ByVal stream As FileStream, ByVal num As UInt16)
        Dim buf(1) As Byte
        buf(1) = (num And &HFF)
        buf(0) = ((num And &HFF00) >> 8)
        stream.Write(buf, 0, 2)
    End Sub

    Sub WriteInt32(ByVal stream As FileStream, ByVal num As UInt32)
        Dim buf(3) As Byte
        dword_to_byte(num, buf(0), buf(1), buf(2), buf(3))
        stream.Write(buf, 0, 4)
    End Sub

    Public Function ReadString(ByVal stream As FileStream, ByVal siz As Integer) As String
        Dim str As String = ""

        Dim i As Integer = 0
        Do While (i <= siz - 1)
            str = (str & Chr(stream.ReadByte))
            i += 1
        Loop
        Return str
    End Function

    Sub WriteString(ByVal stream As FileStream, ByVal str As String, ByVal siz As Integer)
        Dim i As Integer
        Dim buf(siz) As Byte

        For i = 1 To siz
            If i < Len(str) Then
                buf(i - 1) = Asc(Mid(str, i, 1))
            Else
                buf(i - 1) = 0
            End If
        Next

        stream.Write(buf, 0, siz)
    End Sub

    Sub rotaarray(ByRef data() As Byte)
        Dim i As Integer
        Dim a, b As Byte

        For i = 0 To data.Length - 1 Step 4
            a = data(i)
            b = data(i + 1)
            data(i) = data(i + 3)
            data(i + 1) = data(i + 2)
            data(i + 2) = b
            data(i + 3) = a
        Next
    End Sub

    Function add(ByVal num1 As UInt64, ByVal num2 As UInt64) As UInt32

        Return (num1 + num2) And &HFFFFFFFF

    End Function

    Sub rol(ByRef num As UInt32, ByVal veces As Integer)
        num = (num << veces) Or (num >> (32 - veces))
    End Sub

    Function byte_to_dword(ByVal buf As Byte(), ByVal index As Integer) As UInt32
        Array.Reverse(buf, index, 4)

        Return BitConverter.ToUInt32(buf, index)
    End Function

    Function byte_to_dword_inv(ByVal buf As Byte(), ByVal index As Integer) As UInt32
        Array.Reverse(buf, index, 4)

        Return BitConverter.ToUInt32(buf, index)
    End Function

    Function byte_to_qword(buf As Byte(), index As Integer) As UInt64
        Array.Reverse(buf, index, 8)

        Return BitConverter.ToUInt64(buf, index)
    End Function

    Sub dword_to_byte(ByVal num As UInt32, ByRef a As Byte, ByRef b As Byte, ByRef c As Byte, ByRef d As Byte)
        d = (num And &HFF)
        c = ((num And &HFF00) >> 8)
        b = ((num And &HFF0000) >> 16)
        a = ((num And &HFF000000) >> 24)
    End Sub


    Sub qword_to_byte(ByVal num As UInt64, ByRef a As Byte, ByRef b As Byte, ByRef c As Byte, ByRef d As Byte, ByRef e As Byte, ByRef f As Byte, ByRef g As Byte, ByRef h As Byte)
        h = (num And &HFF)
        g = ((num And &HFF00) >> 8)
        f = ((num And &HFF0000) >> 16)
        e = ((num And &HFF000000) >> 24)
        d = ((num And &HFF00000000) >> 32)
        c = ((num And &HFF0000000000) >> 40)
        b = ((num And &HFF000000000000) >> 48)
        a = ((num And &HFF00000000000000) >> 56)
    End Sub

    Function byte_to_string(ByVal buf() As Byte, ByVal pos As Long, ByVal len As Long) As String
        Dim str As String
        Dim i As Integer

        str = ""

        For i = pos To pos + len - 1
            str = str & Chr(buf(i))
        Next

        Return str

    End Function

    Public Function RoundBytes(ByVal num As Long) As String
        If num < 1024 Then
            Return num & " bytes"
        ElseIf num < 1024 ^ 2 Then
            Return Math.Round(num / 1024, 2) & " KB"
        ElseIf num < 1024 ^ 3 Then
            Return Math.Round(num / 1024 ^ 2, 2) & " MB"
        ElseIf num < 1024 ^ 4 Then
            Return Math.Round(num / 1024 ^ 3, 2) & " GB"
        ElseIf num < 1024 ^ 5 Then
            Return Math.Round(num / 1024 ^ 4, 2) & " TB"
        End If
    End Function

    Public Function ReadPkgHeader(ByVal stream As FileStream) As PKG_header
        Dim header As New PKG_header

        header.magic = New Byte(3) {}
        header.qa_digest = New Byte(15) {}
        header.PKGFileKey = New Byte(15) {}
        header.sha1 = New Byte(19) {}

        stream.Read(header.magic, 0, 4)

        header.type = ReadInt32(stream)
        header.hdr_size = ReadInt32(stream)
        header.unk1 = ReadInt32(stream)
        header.sec2_Offset = ReadInt32(stream)
        header.num_files = ReadInt32(stream)
        header.filesize = ReadInt64(stream)
        header.data_sec_offset = ReadInt64(stream)
        header.size_data = ReadInt64(stream)
        header.name = ReadString(stream, 48)
        stream.Read(header.qa_digest, 0, 16)

        stream.Read(header.PKGFileKey, 0, 16)

        stream.Position = stream.Length - 32
        stream.Read(header.sha1, 0, 20)

        AESEngine.free()

        initkey(header)

        Return header
    End Function

    Sub initkey(ByRef header As PKG_header)

        If header.type = 1 Then
            hashAlg = SHA1.Create()

            fillkey(header)
        Else 'hdr.type = "&H80000001"
            header.key = New Byte(15) {}
            Array.Copy(header.PKGFileKey, header.key, header.PKGFileKey.Length)
        End If
    End Sub

    Sub fillkey(ByRef hdr As PKG_header)
        Dim i As Integer

        hdr.key = New Byte(63) {}

        For i = 0 To 63
            hdr.key(i) = 0
        Next

        For i = 0 To 7
            hdr.key(i) = hdr.qa_digest(i)
        Next

        For i = 0 To 7
            hdr.key(i + 8) = hdr.key(i)
        Next

        For i = 0 To 7
            hdr.key(i + 16) = hdr.qa_digest(i + 8)
        Next

        For i = 0 To 7
            hdr.key(i + 24) = hdr.key(i + 16)
        Next

    End Sub

    Function CalculeHash(ByVal type As UInt32) As Byte()
        If type = 1 Then
            Return hashAlg.ComputeHash(hdr.key)
        ElseIf type = "&H80000001" Then
            Return AESEngine.Encrypt(hdr.key, PS3AesKey, PS3AesKey, CipherMode.ECB, PaddingMode.None)
        Else
            Return AESEngine.Encrypt(hdr.key, PSPAesKey, PSPAesKey, CipherMode.ECB, PaddingMode.None)
        End If

    End Function

    Dim _posi, _posf As UInt64

    Sub _seek()
        Dim i As Long
        For i = _posi To _posf - 1 Step 16
            IncrementArray(hdr.key)
        Next

        mThreadFic.Abort()

    End Sub

    Private mThreadFic As Thread

    Sub fseek(ByVal stream As FileStream, ByVal pos As UInt64, Optional ByVal force As Boolean = False)

        If force = False And (stream.Position <= pos) And pos <> hdr.data_sec_offset Then
            _posi = stream.Position
            _posf = pos

            mThreadFic = New Thread(New ThreadStart(AddressOf _seek))
            mThreadFic.Priority = ThreadPriority.Highest
            mThreadFic.Start()

            While mThreadFic.IsAlive
                If Main.Extracting = 0 Then mThreadFic.Abort()
                Application.DoEvents()
            End While

            stream.Position = pos
        Else

            initkey(hdr)

            stream.Position = pos

            _posi = hdr.data_sec_offset
            _posf = pos

            mThreadFic = New Thread(New ThreadStart(AddressOf _seek))
            mThreadFic.Priority = ThreadPriority.Highest
            mThreadFic.Start()

            While mThreadFic.IsAlive
                If Main.Extracting = 0 Then mThreadFic.Abort()
                Application.DoEvents()
            End While
        End If
    End Sub

     Function IncrementArray(ByRef sourceArray As Byte(), Optional ByVal position As Integer = -1) As [Boolean]

        If (position < 0) Then
            If hdr.type = 1 Then
                position = 63
            Else
                position = 15
            End If
        End If

        If sourceArray(position) = &HFF Then
            If position <> 0 Then
                If IncrementArray(sourceArray, position - 1) Then
                    sourceArray(position) = &H0
                    Return True
                Else
                    Return False
                    'Maximum reached yet
                End If
            Else
                Return False
                'Maximum reached yet
            End If
        Else
            sourceArray(position) += &H1
            Return True
        End If
    End Function

    Function decrypt(ByVal stream As FileStream, ByVal lines As Integer, Optional ByVal type As UInt32 = &HFFFFFFF) As Byte()

        Dim buffer((lines * 16) - 1) As Byte
        Dim j As Integer

        Dim hashvalue(15) As Byte

        If type = "&h90000000" Then
            type = "&h80000002"
        ElseIf hdr.type <> 1 And type <> &HFFFFFFF Then
            type = "&h80000001"
        Else
            type = hdr.type
        End If

        For i = 0 To lines - 1

            hashvalue = CalculeHash(type)

            stream.Read(buffer, (16 * i), 16)

            For j = 0 To 15
                buffer((16 * i) + j) = buffer((16 * i) + j) Xor hashvalue(j)
            Next

            IncrementArray(hdr.key)

        Next

        Return buffer
    End Function

    Sub readcontent(ByVal stream As FileStream, ByVal folder As String)
        Dim buf(), buf2() As Byte
        Dim name As String
        Dim name_siz As String
        Dim size, file_offset, filetype As UInt64
        Dim letra As Char
        Dim i, j, offset As Integer

        buf = decrypt(stream, 2)

        size = byte_to_qword(buf, 8)

        fseek(stream, stream.Position - 32)

        AESEngine.free()

        buf = decrypt(stream, (size / 16))

        If hdr.type = "&h80000002" Then
            AESEngine.free()
            fseek(stream, stream.Position - size)
            buf2 = decrypt(stream, (size / 16), "&h80000001")
        Else
            Array.Resize(buf2, buf.Length)
            Array.Copy(buf, buf2, buf.Length)
        End If

        offset = (hdr.num_files * 32)

        For i = 0 To hdr.num_files - 1
            name_siz = byte_to_dword(buf, (i * 32) + 4)
            size = byte_to_qword(buf, (i * 32) + 16)
            file_offset = byte_to_qword(buf, (i * 32) + 8)
            filetype = byte_to_dword(buf, (i * 32) + 24)

            If ((filetype And "&HFF000000") = "&H90000000") Then
                name = byte_to_string(buf, offset, name_siz)
            Else
                name = byte_to_string(buf2, offset, name_siz)
            End If
            name = name.Replace("/", "\")
            j = Len(name)
            Do
                j = j - 1
                letra = Mid(name, j, 1)
            Loop Until letra = "\" Or j = 1

            If j = 1 Then
                Main.Cabinet.Add(New Archivo(folder, name, size, file_offset, filetype, ""))
            Else
                Main.Cabinet.Add(New Archivo(folder & "\" & Mid(name, 1, j - 1), Mid(name, j + 1, Len(name) - j + 1), size, file_offset, filetype, ""))
            End If

            offset = offset + name_siz

            While (offset Mod 16 <> 0)
                offset = offset + 1
            End While
        Next

    End Sub
End Module

