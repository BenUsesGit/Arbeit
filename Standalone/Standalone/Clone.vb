''' <summary>
''' Cloner-Class to trully clone non-primitives that have the attribute "serializable"
''' </summary>
Public Class Clone
    Implements ICloneable
    Public Sub New()

    End Sub

    ' Gibt eine vollständige Kopie dieses Objekts zurück. Voraussetzung ist die Serialisierbarkeit aller beteiligten Objekte. 
    Public Function CloneDeep(ByVal obj As Object) As Object

        Dim Stream As New System.IO.MemoryStream(50000)
        Dim Formatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        ' Serialisierung über alle Objekte hinweg in einen Stream 
        Formatter.Serialize(Stream, obj)
        ' Zurück zum Anfang des Streams und... 
        Stream.Seek(0, System.IO.SeekOrigin.Begin)
        ' ...aus dem Stream in ein Objekt deserialisieren 
        CloneDeep = Formatter.Deserialize(Stream)
        Stream.Close()
    End Function

    Private Function ICloneable_Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class
