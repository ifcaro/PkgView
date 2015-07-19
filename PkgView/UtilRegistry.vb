'------------------------------------------------------------------------------
' UtilRegistry                                                      (28/Jun/06)
' Clase para manipular el registro del sistema
'
' Ejemplo para el libro Manual Imprescindible de Visual Basic 2005
'
' Adapatada para publicar en mi sitio (el Guille)                   (21/Sep/07)
'
' IMPORTANTE:
'   La manipulación del registro requiere que la aplicación se ejecute
'   con privilegios de administrador.
'   En Windows Vista habrá que ejecutar la aplicación "como administrador".
'
' MÁS IMPORTANTE:
'   La manipulación incorrecta del registro puede hacer inestable el sistema.
'   Modificar el registro para extesiones existentes puede provocar que 
'   la aplicación que estaba asociada no funcione correctamente.
'   
'   El uso de esta clase y los métodos definidos en ella
'   es bajo tu entera responsabilidad.
'
'   El autor no se hace responsable de que los cambios en el registro altere 
'   el funcionamiento del equipo.
'
'   Avisado estás... así que...
'   CUIDADO al usar esta clase y el código de ejemplo que la acompaña.
'
' ©Guillermo 'guille' Som, 2006-2007
'------------------------------------------------------------------------------
Option Strict On
Option Explicit On

Imports Microsoft.VisualBasic
Imports vb = Microsoft.VisualBasic
Imports System
Imports Microsoft.Win32

Public Delegate Sub UtilRegistryEventHandler(ByVal message As String)

Public Class UtilRegistry

    Public Shared Event ErrorRegistry As UtilRegistryEventHandler


    ''' <summary>
    ''' Asocia una extensión con un ejecutable y una acción
    ''' </summary>
    ''' <param name="ext">
    ''' La extensión a asociar
    ''' </param>
    ''' <param name="exe">
    ''' El ejecutable a asociar con la extensión
    ''' </param>
    ''' <param name="exe">
    ''' El parametro
    ''' </param>
    ''' <param name="progId">
    ''' El progId a asociar con la extensión
    ''' </param>
    ''' <param name="comando">
    ''' El comando con el que se asociará, por defecto será open
    ''' </param>
    ''' <param name="descripcion">
    ''' La descripción del tipo de archivo relacionado con la extensión
    ''' </param>
    ''' <returns>
    ''' Devuelve un valor verdadero o falso 
    ''' según se haya podido registrar o no la extensión
    ''' </returns>
    ''' <remarks>
    ''' También sirve para añadir nuevos comandos a extensiones existentes
    ''' </remarks>
    Public Shared Function AsociarExtension(ByVal ext As String, _
                                            ByVal exe As String, _
                                            ByVal param As String, _
                                            ByVal progId As String, _
                                            ByVal comando As String, _
                                            ByVal descripcion As String, _
                                            ByVal icono As String) As Boolean
        ' Si no se indican valores
        ' en los tres primeros parámetros
        ' devolver False
        If String.IsNullOrEmpty(ext) _
        OrElse String.IsNullOrEmpty(exe) _
        OrElse String.IsNullOrEmpty(progId) Then
            Return False
        End If

        ' El comando predeterminado es open
        If String.IsNullOrEmpty(comando) Then
            comando = "open"
        End If

        ' Si no se especifica la descripción
        If String.IsNullOrEmpty(descripcion) Then
            descripcion = ext & " Descripción de " & progId
        End If

        ' Si no se especifica el punto
        If ext.IndexOf(".") = -1 Then
            ext = "." & ext
        End If

        Dim value As String
        Dim rk As RegistryKey = Nothing
        Dim rkShell As RegistryKey = Nothing
        Dim rkIcon As RegistryKey = Nothing

        Try
            value = GetProgId(ext)
            If String.IsNullOrEmpty(value) _
            OrElse value.Length = 0 Then
                ' Crear la clave, etc.
                rk = Registry.ClassesRoot.CreateSubKey(ext)
                rk.SetValue("", progId)

                rk = Registry.ClassesRoot.CreateSubKey(progId)
                rk.SetValue("", descripcion)

                value = "shell\" & comando & "\command"
                rkShell = rk.CreateSubKey(value)

                rk = Registry.ClassesRoot.CreateSubKey(progId & "\DefaultIcon")
                rk.SetValue("", icono)
            Else
                ' Ya existe, solo actualizar los valores
                ' pero debemos abrir la clave indicando que vamos a escribir
                ' para que nos permita crear nuevas subclaves.
                rk = Registry.ClassesRoot.OpenSubKey(progId, True)
                value = "DefaultIcon"
                rkIcon = rk.OpenSubKey(value, True)

                rk = Registry.ClassesRoot.OpenSubKey(progId, True)
                value = "shell\" & comando & "\command"
                rkShell = rk.OpenSubKey(value, True)

                ' Si es un comando que se añade, no existirá
                If rkShell Is Nothing Then
                    rkShell = rk.CreateSubKey(value)
                End If
                If rkIcon Is Nothing Then
                    rkIcon = rk.CreateSubKey(value)
                End If
            End If

            If rkShell IsNot Nothing Then
                rkShell.SetValue("", ChrW(34) & exe & ChrW(34) & " " & param & _
                                     ChrW(34) & "%1" & ChrW(34))
                rkShell.Close()
            End If

            If rkIcon IsNot Nothing Then
                rkIcon.SetValue("", icono)
                rkIcon.Close()
            End If

        Catch ex As Exception

            RaiseEvent ErrorRegistry(ex.Message)

            Return False

        Finally
            If rk IsNot Nothing Then
                rk.Close()
            End If
        End Try


        Return True
    End Function

    '------------------------------------------------------------------
    ' Sobrecargas para no usar Optional y facilitar la conversión a C#
    '------------------------------------------------------------------

    ''' <summary>
    ''' Asocia una extensión con un ejecutable y una acción
    ''' </summary>
    ''' <param name="ext">
    ''' La extensión a asociar
    ''' </param>
    ''' <param name="exe">
    ''' El ejecutable a asociar con la extensión
    ''' </param>
    ''' <param name="progId">
    ''' El progId a asociar con la extensión
    ''' </param>
    ''' <param name="comando">
    ''' El comando con el que se asociará, por defecto será open
    ''' </param>
    ''' <param name="descripcion">
    ''' La descripción del tipo de archivo relacionado con la extensión
    ''' </param>
    ''' <returns>
    ''' Devuelve un valor verdadero o falso 
    ''' según se haya podido registrar o no la extensión
    ''' </returns>
    ''' <remarks>
    ''' También sirve para añadir nuevos comandos a extensiones existentes
    ''' </remarks>
    Public Shared Function AsociarExtension(ByVal ext As String, _
                                            ByVal exe As String, _
                                            ByVal progId As String, _
                                            ByVal comando As String, _
                                            ByVal descripcion As String, _
                                            ByVal icono As String) As Boolean
        AsociarExtension(ext, exe, "", progId, comando, descripcion, icono)
    End Function

    ''' <summary>
    ''' Asocia una extensión con un ejecutable y una acción
    ''' </summary>
    ''' <param name="ext">
    ''' La extensión a asociar
    ''' </param>
    ''' <param name="exe">
    ''' El ejecutable a asociar con la extensión
    ''' </param>
    ''' <param name="progId">
    ''' El progId a asociar con la extensión
    ''' </param>
    ''' <returns>
    ''' Devuelve un valor verdadero o falso 
    ''' según se haya podido registrar o no la extensión
    ''' </returns>
    ''' <remarks>
    ''' También sirve para añadir nuevos comandos a extensiones existentes.
    ''' El comando usado es open
    ''' </remarks>
    Public Shared Function AsociarExtension(ByVal ext As String, _
                                            ByVal exe As String, _
                                            ByVal progId As String _
                                            ) As Boolean
        Return AsociarExtension(ext, exe, progId, "open", "", "")
    End Function

    ''' <summary>
    ''' Asocia una extensión con un ejecutable y una acción
    ''' </summary>
    ''' <param name="ext">
    ''' La extensión a asociar
    ''' </param>
    ''' <param name="exe">
    ''' El ejecutable a asociar con la extensión
    ''' </param>
    ''' <param name="progId">
    ''' El progId a asociar con la extensión
    ''' </param>
    ''' <param name="comando">
    ''' El comando con el que se asociará, por defecto será open
    ''' </param>
    ''' <returns>
    ''' Devuelve un valor verdadero o falso 
    ''' según se haya podido registrar o no la extensión
    ''' </returns>
    ''' <remarks>
    ''' También sirve para añadir nuevos comandos a extensiones existentes
    ''' </remarks>
    Public Shared Function AsociarExtension(ByVal ext As String, _
                                            ByVal exe As String, _
                                            ByVal progId As String, _
                                            ByVal comando As String _
                                            ) As Boolean
        Return AsociarExtension(ext, exe, progId, comando, "", "")
    End Function

    ''' <summary>
    ''' Permite quitar un comando asociado con una extensión
    ''' </summary>
    ''' <param name="ext">
    ''' La extensión de la que se quitará el comando
    ''' </param>
    ''' <param name="comando">
    ''' El comando a quitar
    ''' </param>
    ''' <returns>
    ''' Un valor verdadero o falso según todo haya ido correcto o no
    ''' </returns>
    ''' <remarks>
    ''' Este método permite quitar comandos asociados previamente, 
    ''' sin eliminar la extensión.
    ''' El comando open (o el predeterminado) 
    ''' no se puede quitar usando este método.
    ''' </remarks>
    Public Shared Function QuitarComandoExtension(ByVal ext As String, _
                                                  ByVal comando As String _
                                                  ) As Boolean
        ' No permitir cadenas vacías en el comando
        If String.IsNullOrEmpty(comando) Then
            Return False
        End If

        ' No permitir quitar el comando open
        If comando.ToLower.Contains("open") Then
            Return False
        End If

        ' Si no se especifica el punto en la extensión
        If ext.IndexOf(".") = -1 Then
            ext = "." & ext
        End If

        ' Averiguar el progId de esta extensión
        Dim progId As String = GetProgId(ext)

        Try
            If String.IsNullOrEmpty(progId) = False _
            AndAlso progId.Length > 0 Then
                Using rk As RegistryKey = Registry.ClassesRoot.OpenSubKey(progId, True)
                    If rk IsNot Nothing Then
                        Dim value As String = "shell\" & comando
                        rk.DeleteSubKeyTree(value)

                        Return True
                    End If
                End Using
            End If

        Catch ex As Exception

            RaiseEvent ErrorRegistry(ex.Message)
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Quita la extensión indicada de los tipos de archivos registrados
    ''' </summary>
    ''' <param name="ext">
    ''' La extensión a quitar del registro
    ''' </param>
    ''' <returns>
    ''' Devuelve un valor verdadero o falso según se haya quitado 
    ''' correctamente o no la extensión
    ''' </returns>
    ''' <remarks>
    ''' ATENCIÓN, este método elimina totalmente la extensión del registro
    ''' </remarks>
    Public Shared Function DesasociarExtension(ByVal ext As String) As Boolean
        ' Si no se especifica el punto
        If ext.IndexOf(".") = -1 Then
            ext = "." & ext
        End If

        Dim progId As String = GetProgId(ext)

        Try
            'If vb.Len(progId) > 0 Then
            If String.IsNullOrEmpty(progId) = False _
            AndAlso progId.Length > 0 Then

                ' Eliminar la clave
                Registry.ClassesRoot.DeleteSubKeyTree(ext)

                Registry.ClassesRoot.DeleteSubKeyTree(progId)

                Return True
            End If

        Catch ex As Exception
            RaiseEvent ErrorRegistry(ex.Message)

        End Try

        Return False
    End Function

    ''' <summary>
    ''' Comprueba si la extensión indicada está registrada.
    ''' </summary>
    ''' <param name="ext">
    ''' La extensión a comprobar
    ''' </param>
    ''' <returns>
    ''' Un valor verdadero o falso según esté registrada o no
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared Function Existe(ByVal ext As String) As Boolean
        ' Si no se especifica el punto
        If ext.IndexOf(".") = -1 Then
            ext = "." & ext
        End If

        Dim s As String = GetProgId(ext)

        If String.IsNullOrEmpty(s) Then
            Return False
        End If

        Return (s.Length > 0)
        'Return (vb.Len(getProgId(ext)) > 0)
    End Function

    ''' <summary>
    ''' Método para obtener el progId de una extensión.
    ''' </summary>
    ''' <param name="ext">
    ''' Extensión de la que se quiere obtener el progId
    ''' </param>
    ''' <returns>
    ''' Devuelve una cadena con el progId de la extensión 
    ''' o una cadena vacía si no existe
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared Function GetProgId(ByVal ext As String) As String
        Dim progId As String = ""

        Try
            Using rk As RegistryKey = Registry.ClassesRoot.OpenSubKey(ext)
                If rk IsNot Nothing Then
                    progId = rk.GetValue("").ToString
                    rk.Close()
                End If
            End Using

        Catch ex As Exception
            RaiseEvent ErrorRegistry(ex.Message)

            Return ""
        End Try

        Return progId
    End Function


End Class
