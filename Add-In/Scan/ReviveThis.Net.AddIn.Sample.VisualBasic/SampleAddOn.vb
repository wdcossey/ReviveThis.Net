'[Export](typeof (IScanModule))]
Imports System.ComponentModel.Composition
Imports ReviveThis.AddIn.Sample.VisualBasic.Classes
Imports ReviveThis.Enums
Imports System.Collections.ObjectModel
Imports ReviveThis.Interfaces
Imports System.Threading.Tasks


<Export(GetType(IDetectionAddIn))>
Public Class SampleAddOn
    'Note: You must have "<Export(GetType(IScanAddIn))>" in your Add-On, else ReviveThis.Net won't find/use it.

    Implements IDetectionAddIn

    Public ReadOnly Property Author() As String Implements IModuleInformation.Author
        Get
            Return "VB.Net Author"
        End Get
    End Property

    Public ReadOnly Property Version() As Version Implements IModuleInformation.Version
        Get
            Return New Version(0, 0, 0, 1)
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements IModuleInformation.Name
        Get
            Return "Add-On Sample (Visual Basic)"
        End Get
    End Property

    Public ReadOnly Property Description() As String() Implements IModuleInformation.Description
        Get
            Return New String() {"A Sample Add-On for ReviveThis.Net written in Visual Basic .Net", vbNullString, "Hey look, another line in the description."}
        End Get
    End Property

    Public Sub Dispose() Implements IDisposable.Dispose
        'We don't have anything to Dispose
    End Sub

    Public Async Function Scan() As Task(Of ICollection(Of IDetectionResultItem)) Implements IDetectionAddIn.Scan
        Dim result As New Collection(Of IDetectionResultItem)
        'Await Task.FromResult(0)
        result.Add(New SampleResult(ScanResultType.CustomAddIn, "This is from my Add-On Sample (Visual Basic)"))
        result.Add(New SampleResult(ScanResultType.CustomAddIn, "This appears to be another result from my Add-On Sample (Visual Basic)"))
        Return result
    End Function

    Public ReadOnly Property SortIndex() As ScanResultType Implements IDetectionAddIn.ResultType
        Get
            'The expected ScanResultType
            Return ScanResultType.CustomAddIn
        End Get
    End Property

End Class
