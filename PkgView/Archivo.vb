Public Class Archivo
    Public Folder As String
    Public Name As String
    Public Size As UInt64
    Public Offset As UInt64
    Public Type As UInt32
    Public Source As String

    Public Sub New( _
       ByVal m_Folder As String, _
       ByVal m_Name As String, _
       ByVal m_Size As UInt64, _
       ByVal m_Offset As UInt64, _
       ByVal m_Type As UInt32, _
       ByVal m_Source As String)
        Folder = m_Folder
        Name = m_Name
        Size = m_Size
        Offset = m_Offset
        Type = m_Type
        Source = m_Source
    End Sub

End Class