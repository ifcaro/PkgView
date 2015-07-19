Imports System
Imports System.Runtime.InteropServices

''' <summary>
''' Represents the thumbnail progress bar state.
''' </summary>
Public Enum ThumbnailProgressState
    ''' <summary>
    ''' No progress is displayed.
    ''' </summary>
    NoProgress = 0
    ''' <summary>
    ''' The progress is indeterminate (marquee).
    ''' </summary>
    Indeterminate = &H1
    ''' <summary>
    ''' Normal progress is displayed.
    ''' </summary>
    Normal = &H2
    ''' <summary>
    ''' An error occurred (red).
    ''' </summary>
    [Error] = &H4
    ''' <summary>
    ''' The operation is paused (yellow).
    ''' </summary>
    Paused = &H8
End Enum

'Based on Rob Jarett's wrappers for the desktop integration PDC demos.
<ComImportAttribute()> _
<GuidAttribute("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")> _
<InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
Friend Interface ITaskbarList3
    ' ITaskbarList
    <PreserveSig()> _
    Sub HrInit()
    <PreserveSig()> _
    Sub AddTab(ByVal hwnd As IntPtr)
    <PreserveSig()> _
    Sub DeleteTab(ByVal hwnd As IntPtr)
    <PreserveSig()> _
    Sub ActivateTab(ByVal hwnd As IntPtr)
    <PreserveSig()> _
    Sub SetActiveAlt(ByVal hwnd As IntPtr)

    ' ITaskbarList2
    <PreserveSig()> _
    Sub MarkFullscreenWindow(ByVal hwnd As IntPtr, <MarshalAs(UnmanagedType.Bool)> ByVal fFullscreen As Boolean)

    ' ITaskbarList3
    Sub SetProgressValue(ByVal hwnd As IntPtr, ByVal ullCompleted As UInt64, ByVal ullTotal As UInt64)
    Sub SetProgressState(ByVal hwnd As IntPtr, ByVal tbpFlags As ThumbnailProgressState)

    ' yadda, yadda - there's more to the interface, but we don't need it.
End Interface

<GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")> _
<ClassInterfaceAttribute(ClassInterfaceType.None)> _
<ComImportAttribute()> _
Friend Class CTaskbarList
End Class

''' <summary>
''' The primary coordinator of the Windows 7 taskbar-related activities.
''' </summary>
Public NotInheritable Class Windows7Taskbar
    Private Sub New()
    End Sub
    Private Shared _taskbarList As ITaskbarList3
    Friend Shared ReadOnly Property TaskbarList() As ITaskbarList3
        Get
            If _taskbarList Is Nothing Then
                SyncLock GetType(Windows7Taskbar)
                    If _taskbarList Is Nothing Then
                        _taskbarList = DirectCast(New CTaskbarList(), ITaskbarList3)
                        _taskbarList.HrInit()
                    End If
                End SyncLock
            End If
            Return _taskbarList
        End Get
    End Property

    Shared ReadOnly osInfo As OperatingSystem = Environment.OSVersion

    Friend Shared ReadOnly Property Windows7OrGreater() As Boolean
        Get
            Return (osInfo.Version.Major = 6 AndAlso osInfo.Version.Minor >= 1) OrElse (osInfo.Version.Major > 6)
        End Get
    End Property

    ''' <summary>
    ''' Sets the progress state of the specified window's
    ''' taskbar button.
    ''' </summary>
    ''' <param name="hwnd">The window handle.</param>
    ''' <param name="state">The progress state.</param>
    Public Shared Sub SetProgressState(ByVal hwnd As IntPtr, ByVal state As ThumbnailProgressState)
        If Windows7OrGreater Then
            TaskbarList.SetProgressState(hwnd, state)
        End If
    End Sub
    ''' <summary>
    ''' Sets the progress value of the specified window's
    ''' taskbar button.
    ''' </summary>
    ''' <param name="hwnd">The window handle.</param>
    ''' <param name="current">The current value.</param>
    ''' <param name="maximum">The maximum value.</param>
    Public Shared Sub SetProgressValue(ByVal hwnd As IntPtr, ByVal current As ULong, ByVal maximum As ULong)
        If Windows7OrGreater Then
            TaskbarList.SetProgressValue(hwnd, current, maximum)
        End If
    End Sub


    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Friend Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
End Class

Public Class Windows7ProgressBar
    Inherits ProgressBar
    Private m_showInTaskbar As Boolean
    Private m_State As ProgressBarState = ProgressBarState.Normal
    Private ownerForm As ContainerControl

    Public Sub New()
    End Sub

    Public Sub New(ByVal parentControl As ContainerControl)
        ContainerControl = parentControl
    End Sub
    Public Property ContainerControl() As ContainerControl
        Get
            Return ownerForm
        End Get
        Set(ByVal value As ContainerControl)
            ownerForm = value

            If Not ownerForm.Visible Then
                AddHandler DirectCast(ownerForm, Form).Shown, AddressOf Windows7ProgressBar_Shown
            End If
        End Set
    End Property
    'Public Overrides WriteOnly Property Site() As ISite
    '    Set(ByVal value As ISite)
    '        ' Runs at design time, ensures designer initializes ContainerControl
    '        MyBase.Site = value
    '        If value Is Nothing Then
    '            Return
    '        End If
    '        Dim service As IDesignerHost = TryCast(value.GetService(GetType(IDesignerHost)), IDesignerHost)
    '        If service Is Nothing Then
    '            Return
    '        End If
    '        Dim rootComponent As IComponent = service.RootComponent

    '        ContainerControl = TryCast(rootComponent, ContainerControl)
    '    End Set
    'End Property

    Private Sub Windows7ProgressBar_Shown(ByVal sender As Object, ByVal e As System.EventArgs)
        If ShowInTaskbar Then
            If Style <> ProgressBarStyle.Marquee Then
                SetValueInTB()
            End If

            SetStateInTB()
        End If

        RemoveHandler DirectCast(ownerForm, Form).Shown, AddressOf Windows7ProgressBar_Shown
    End Sub



    ''' <summary>
    ''' Show progress in taskbar
    ''' </summary>
    Public Property ShowInTaskbar() As Boolean
        Get
            Return m_showInTaskbar
        End Get
        Set(ByVal value As Boolean)
            If m_showInTaskbar <> value Then
                m_showInTaskbar = value

                ' send signal to the taskbar.
                If ownerForm IsNot Nothing Then
                    If Style <> ProgressBarStyle.Marquee Then
                        SetValueInTB()
                    End If

                    SetStateInTB()
                End If
            End If
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets the current position of the progress bar.
    ''' </summary>
    ''' <returns>The position within the range of the progress bar. The default is 0.</returns>
    Public Shadows Property Value() As Integer
        Get
            Return MyBase.Value
        End Get
        Set(ByVal value As Integer)
            MyBase.Value = value

            ' send signal to the taskbar.
            SetValueInTB()
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the manner in which progress should be indicated on the progress bar.
    ''' </summary>
    ''' <returns>One of the ProgressBarStyle values. The default is ProgressBarStyle.Blocks</returns>
    Public Shadows Property Style() As ProgressBarStyle
        Get
            Return MyBase.Style
        End Get
        Set(ByVal value As ProgressBarStyle)
            MyBase.Style = value

            ' set the style of the progress bar
            If m_showInTaskbar AndAlso ownerForm IsNot Nothing Then
                SetStateInTB()
            End If
        End Set
    End Property


    ''' <summary>
    ''' The progress bar state for Windows Vista & 7
    ''' </summary>
    Public Property State() As ProgressBarState
        Get
            Return m_State
        End Get
        Set(ByVal value As ProgressBarState)
            m_State = value

            Dim wasMarquee As Boolean = Style = ProgressBarStyle.Marquee

            If wasMarquee Then
                ' sets the state to normal (and implicity calls SetStateInTB() )
                Style = ProgressBarStyle.Blocks
            End If

            ' set the progress bar state (Normal, Error, Paused)
            Windows7Taskbar.SendMessage(Handle, &H410, CInt(value), 0)


            If wasMarquee Then
                ' the Taskbar PB value needs to be reset
                SetValueInTB()
            Else
                ' there wasn't a marquee, thus we need to update the taskbar
                SetStateInTB()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Advances the current position of the progress bar by the specified amount.
    ''' </summary>
    ''' <param name="value">The amount by which to increment the progress bar's current position.</param>
    Public Shadows Sub Increment(ByVal value As Integer)
        MyBase.Increment(value)

        ' send signal to the taskbar.
        SetValueInTB()
    End Sub

    ''' <summary>
    ''' Advances the current position of the progress bar by the amount of the System.Windows.Forms.ProgressBar.Step property.
    ''' </summary>
    Public Shadows Sub PerformStep()
        MyBase.PerformStep()

        ' send signal to the taskbar.
        SetValueInTB()
    End Sub

    Private Sub SetValueInTB()
        If m_showInTaskbar Then
            Dim maximum__1 As ULong = CULng(Maximum - Minimum)
            Dim progress As ULong = CULng(Value - Minimum)

            Windows7Taskbar.SetProgressValue(ownerForm.Handle, progress, maximum__1)
        End If
    End Sub

    Private Sub SetStateInTB()
        If ownerForm Is Nothing Then
            Return
        End If

        Dim thmState As ThumbnailProgressState = ThumbnailProgressState.Normal

        If Not m_showInTaskbar Then
            thmState = ThumbnailProgressState.NoProgress
        ElseIf Style = ProgressBarStyle.Marquee Then
            thmState = ThumbnailProgressState.Indeterminate
        ElseIf m_State = ProgressBarState.[Error] Then
            thmState = ThumbnailProgressState.[Error]
        ElseIf m_State = ProgressBarState.Pause Then
            thmState = ThumbnailProgressState.Paused
        End If

        Windows7Taskbar.SetProgressState(ownerForm.Handle, thmState)
    End Sub
End Class

''' <summary>
''' The progress bar state for Windows Vista & 7
''' </summary>
Public Enum ProgressBarState
    ''' <summary>
    ''' Indicates normal progress
    ''' </summary>
    Normal = 1

    ''' <summary>
    ''' Indicates an error in the progress
    ''' </summary>
    [Error] = 2

    ''' <summary>
    ''' Indicates paused progress
    ''' </summary>
    Pause = 3
End Enum
