Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.ObjectModel
Imports System.Runtime.Serialization
Imports ReviveThis.Entities
Imports ReviveThis.Enums
Imports ReviveThis.Interfaces
Imports ReviveThis.Structs
Imports System.Threading.Tasks

Namespace Classes

    Public Class SampleResult
        Implements IDetectionResultItem, IDetectionItemContextMenuCollection, ICustomAddInSection, IDetectionItemToolTip

        Private ReadOnly _resultType As ScanResultType
        Private ReadOnly _text As String
        Private Const SectionId As String = "ZZ"

        Public ReadOnly Property ResultType() As ScanResultType Implements IDetectionResultItem.ResultType
            Get
                Return _resultType
            End Get
        End Property

        Public ReadOnly Property LegacyString() As String Implements IDetectionResultItem.LegacyString
            Get
                Return String.Format("{0} - {1}.", SectionId, _text)
            End Get
        End Property

        Public ReadOnly Property ToolTip() As String Implements IDetectionItemToolTip.ToolTip
            Get
                Return "Add-On ToolTip Sample (Visual Basic)"
            End Get
        End Property

        Public Sub New(type As ScanResultType, text As String)
            _resultType = type
            _text = text
        End Sub

        Public ReadOnly Property MenuItems() As ICollection(Of IDetectionItemContextMenu) Implements IDetectionItemContextMenuCollection.MenuItems
            Get
                Dim items As New Collection(Of IDetectionItemContextMenu)
                items.Add(New ScanItemContextMenu("Sample context menu", AddressOf Test))
                Return items
            End Get
        End Property

        Private Shared Sub Test(sender As Object, e As EventArgs)
            MessageBox.Show("Hello from your Add-On Sample context menu.", "Add-On Sample (Visual Basic)", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        Public ReadOnly Property CustomAddInSection() As CustomAddInSection Implements ICustomAddInSection.CustomAddInSection
            Get
                Return New CustomAddInSection(SectionId, "Wow I have a custom section..?")
            End Get
        End Property

        Public ReadOnly Property CanRepair() As Boolean Implements IDetectionRepair.CanRepair
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property IsChecked() As Boolean Implements IDetectionRepair.IsChecked
            Get
                Return False
            End Get
        End Property

        Public Function Repair() As Task(Of IDetectionRepairResult) Implements IDetectionRepair.Repair
            Return Nothing
        End Function
    End Class
End Namespace