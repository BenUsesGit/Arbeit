Imports System.IO, System.Text.RegularExpressions

''' <summary>
''' Class for reading out, writing and interpret <see cref="Spline"/>s
''' </summary>
Public Class SplineHandler
    Dim SplineArr As Spline()
    Dim iPath As String
    Dim SplName As String
    Dim isThere As Boolean

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Writes Splineparameter of given spline into a file on the given path
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="path"></param>
    Public Sub WriteSpline(ByVal s As Spline, ByVal path As String)
        Dim fs As FileStream
        Dim sw As StreamWriter
        Dim sr As StreamReader

        'check wether file exists
        If File.Exists(path) Then

            fs = New FileStream(path, FileMode.Open)
            sw = New StreamWriter(fs)
            sr = New StreamReader(fs)

            Dim chain As String
            Dim rg As New Regex("ID\s" & s.gID)

            chain = sr.ReadToEnd

            'check wether the spline already exists
            If rg.IsMatch(chain) Then
                Debug.Print("Spline schon vorhanden")
            Else
                sw.Write(stitchString(s))
                sw.Write(vbCrLf)
            End If

        Else
            fs = New FileStream(path, FileMode.CreateNew)
            sw = New StreamWriter(fs)
            sr = New StreamReader(fs)
            sw.Write(stitchString(s))
            sw.Write(vbCrLf)
        End If
        sw.Close()
        sr.Close()
        fs.Dispose()
    End Sub

    ''' <overloads> Calls <see cref="WriteSpline(Spline(), String)"/> for each item in a given array of <see cref="Spline"/>s</overloads>
    Public Sub WriteSpline(ByVal arr As Spline(), ByVal path As String)
        For Each element In arr
            WriteSpline(element, path)
        Next
    End Sub

    ''' <summary>
    ''' under development
    ''' </summary>
    ''' <param name="spline"></param>
    ''' <returns></returns>
    Public Function SplineToString(ByVal spline As Spline) As String
        Dim s As String

        Return s
    End Function

    ''' <summary>
    ''' Read out a spline with given ID from given path
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="p"></param>
    ''' <returns></returns>
    Public Function ReadSpline(ByVal id As Integer, ByVal p As String) As Spline
        Dim s As New Spline
        Dim chain As String

        If File.Exists(p) Then
            Dim fs = New FileStream(p, FileMode.Open)
            Dim sr = New StreamReader(fs)
            chain = sr.ReadToEnd()
            Dim interest As Match = Regex.Match(chain, "ID" & vbTab & id & ".*")

            If interest.Success Then
                s = tearString(interest.Value)

            Else
                Debug.Print("Spline nicht vorhanden")
            End If
            sr.Close()
            fs.Dispose()
            Return s
        Else
            Debug.Print("File existiert nicht unter angegebenem Pfad")
            Return s
        End If
    End Function

    ''' <summary>
    ''' Reads out all splines out of file with given path
    ''' </summary>
    ''' <param name="p"></param>
    ''' <returns></returns>
    Public Function ReadAllSplines(ByVal p As String) As Spline()
        Dim s As New Spline
        Dim arr As Spline() = {}
        Dim chain As String

        If File.Exists(p) Then
            Dim fs As New FileStream(p, FileMode.Open)
            Dim sr As New StreamReader(fs)
            chain = sr.ReadToEnd()
            sr.Close()
            fs.Dispose()

            For Each m As Match In Regex.Matches(chain, "ID.*Ende")
                s = tearString(m.Value)
                ReDim Preserve arr(arr.Length)
                arr(arr.Length - 1) = s
            Next
        End If

        Return arr
    End Function

    ''' <summary>
    ''' Deletes the spline with given ID out of a file with given path
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="p"></param>
    Public Sub DelSpline(ByVal id As Integer, ByVal p As String)
        ' TODO at the moment the whole string is copied, certain parts are deleted and whole string is written back to file;
        'it would be better if the method would work directly in the textfile
        Dim s As New Spline
        Dim fs As FileStream
        Dim sr As StreamReader
        Dim chain As String

        If File.Exists(p) Then
            fs = New FileStream(p, FileMode.Open)
            sr = New StreamReader(fs)
            chain = sr.ReadToEnd()
            sr.Close()
            fs.Dispose()
            Dim interest As Match = Regex.Match(chain, "ID" & vbTab & id & ".*")
            If interest.Success Then
                Dim replc As String
                Dim pattern As String = "ID\s" & id & "+.*Ende" & vbCrLf
                Dim rgx As New Regex(pattern)
                replc = rgx.Replace(chain, "")
                File.WriteAllText(p, "")
                File.WriteAllText(p, replc)
            Else
                Debug.Print("Spline mit gegebener ID existiert nicht")
            End If
        Else
            Debug.Print("File existiert nicht unter angegebenem Pfad")
        End If

    End Sub

    ''' <summary>
    ''' Assembles the string to write into a textfile from given spline
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Function stitchString(ByVal s As Spline) As String
        Dim splString As String
        splString = "ID" & vbTab & s.gID & vbTab & "Order" & vbTab & s.gOrder

        Dim i As Integer
        splString = splString & vbTab & "Knots" & vbTab
        For i = 0 To s.gKnots.Length - 1
            splString = splString & s.gKnots(i) & ";"
        Next
        splString = splString & vbTab & "Points" & vbTab

        Dim j As Integer
        For j = 0 To 3

            Select Case j
                Case 0
                    splString = splString & "X" & vbTab
                Case 1
                    splString = splString & "Y" & vbTab
                Case 2
                    splString = splString & "Z" & vbTab
                Case 3
                    splString = splString & "W" & vbTab
            End Select

            For i = 0 To s.gPoints.Length - 1
                Select Case j
                    Case 0
                        splString = splString & s.gPoints(i).gX & ";"
                    Case 1
                        splString = splString & s.gPoints(i).gY & ";"
                    Case 2
                        splString = splString & s.gPoints(i).gZ & ";"
                    Case 3
                        splString = splString & s.gPoints(i).gW & ";"
                End Select
            Next
            splString = splString & vbTab
        Next
        splString = splString & "Ende"
        Return splString
    End Function

    ''' <summary>
    ''' Separates the given String into arrays and matches the values to spline parameters
    ''' </summary>
    ''' <param name="chain"></param>
    ''' <returns></returns>
    Private Function tearString(ByVal chain As String) As Spline
        ' TODO more convenient conversion of the integers out of the former extracted string; End this match of match of match ..
        Dim s As New Spline
        ' first match extracts keyword + corresponding numbers, second match extracts numbers with ; and ,
        Dim ID As Integer = Regex.Match(Regex.Match(chain, "ID\s\d*").Value, "\d+").Value
        Dim order As Integer = Regex.Match(Regex.Match(chain, "Order\s\d").Value, "\d").Value
        Dim Knots As Double() = StrToArr(Regex.Match(Regex.Match(chain, "Knots\s(?:\d*\,)?\d+.*?\t").Value, "\d+.*\;").Value)
        Dim Xs As Double() = StrToArr(Regex.Match(Regex.Match(chain, "X\s(?:\d*\,)?\d+.*?\t").Value, "\d+.*\;").Value)
        Dim Ys As Double() = StrToArr(Regex.Match(Regex.Match(chain, "Y\s(?:\d*\,)?\d+.*?\t").Value, "\d+.*\;").Value)
        Dim Zs As Double() = StrToArr(Regex.Match(Regex.Match(chain, "Z\s(?:\d*\,)?\d+.*?\t").Value, "\d+.*\;").Value)
        Dim Ws As Double() = StrToArr(Regex.Match(Regex.Match(chain, "W\s(?:\d*\,)?\d+.*?\t").Value, "\d+.*\;").Value)

        'build spline
        s.sID(ID)
        s.sOrder(order)
        s.sKnots(Knots)
        Dim p As Point() = {}
        For Each element In Xs
            ReDim Preserve p(p.Length)
            p(p.Length - 1) = New Point(element, Ys(p.Length - 1), Zs(p.Length - 1))
        Next
        s.sPoints(p)
        'default
        ' TODO build algorithm that checks whether spline should be closed or not
        s.sClose(False)
        Return s
    End Function

    ''' <summary>
    ''' Puts comma-values out of a given string into an array
    ''' </summary>
    ''' <param name="chain"></param>
    ''' <returns></returns>
    Private Function StrToArr(ByVal chain As String) As Double()
        Dim Arr As Double()
        Dim substring As String()

        substring = Regex.Split(chain, ";")
        ReDim Arr(substring.Length - 2)

        Dim i As Integer
        For i = 0 To substring.Length - 2
            Arr(i) = substring(i)
        Next

        Return Arr
    End Function

    ''' <summary>
    ''' Under development. sorts the splines in a file according their IDs
    ''' </summary>
    Private Sub SplineSort()
        ' TODO a algorithm thats sorts the splines in the given string or whatever according to their IDs
    End Sub

End Class
